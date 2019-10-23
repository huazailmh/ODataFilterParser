using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace ODataFilterParser
{
    internal class ODataFilterVisitor : ODataBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitParenthesis(ODataParser.ParenthesisContext context)
        {
            object result = Visit(context.expression());
            if(result!=null)
                return "(" + result.ToString() + ")";

            return null;
        }

        //public override object VisitMultiplyDivide(ODataParser.MultiplyDivideContext context)
        //{
        //    double left = Convert.ToDouble(Visit(context.expression(0)));
        //    double right = Convert.ToDouble(Visit(context.expression(1)));

        //    object obj = new object(); 
        //    if (context.operate.Type == ODataParser.MUL) {
        //        obj = left * right;
        //    } else if (context.operate.Type == ODataParser.DIV) {
        //        if (right == 0) {
        //            throw new Exception("Cannot divide by zero.");
        //        }
        //        obj = left / right;
        //    }

        //    return obj;
        //}

        protected override object AggregateResult(object aggregate, object nextResult)
        {
            if (aggregate == null && nextResult != null)
                return nextResult;
            else if (aggregate != null && nextResult == null)
                return aggregate;
            else if (aggregate != null && nextResult != null)
                return aggregate + " " + nextResult;

            return base.AggregateResult(aggregate, nextResult);
        }

        public override object VisitLogic([NotNull] ODataParser.LogicContext context)
        {
            if (context.exception != null)
                throw context.exception;

            var left = Visit(context.expression(0)).ToString();
            var right = Visit(context.expression(1))?.ToString();

            //表达式不合规时,右侧值为null
            if (right == null)
                return null;

            if (context.logic.Type == ODataParser.K_AND)
            {
                return $"{left} AND {right}";
            }
            else if (context.logic.Type == ODataParser.K_OR)
            {
                return $"{left} OR {right}";
            }

            return string.Empty;
        }


        public override object VisitCompareText(ODataParser.CompareTextContext context)
        {
            if (context.exception != null)
                throw context.exception;

            var column = Visit(context.column).ToString();

            var right = context.value.Text;

            var compare = context.compare.Type;

            switch (compare)
            {
                case ODataParser.Equal:
                    return $"{column} = {right}";

                case ODataParser.NotEqual:
                    return $"{column} != {right}";

                case ODataParser.GreaterThan:
                    return $"{column} > {right}";

                case ODataParser.GreaterThanOrEqual:
                    return $"{column} >= {right}";

                case ODataParser.LessThan:
                    return $"{column} < {right}";

                case ODataParser.LessThanOrEqual:
                    return $"{column} <= {right}";

                default:
                    return string.Empty;
            }
        }


        public override object VisitCompareDecimal(ODataParser.CompareDecimalContext context)
        {
            if (context.exception != null)
                throw context.exception;

            var column = Visit(context.column).ToString();
            var right = (double)Visit(context.value);
            //var right = context.value.GetText();
            var compare = context.compare.Type;

            switch (compare)
            {
                case ODataParser.Equal:
                    return $"{column} = {right}";

                case ODataParser.NotEqual:
                    return $"{column} != {right}";

                case ODataParser.GreaterThan:
                    return $"{column} > {right}";

                case ODataParser.GreaterThanOrEqual:
                    return $"{column} >= {right}";

                case ODataParser.LessThan:
                    return $"{column} < {right}";

                case ODataParser.LessThanOrEqual:
                    return $"{column} <= {right}";

                default:
                    return string.Empty;
            }
        }

        public override object VisitStartsWith([NotNull] ODataParser.StartsWithContext context)
        {
            if (context.exception != null)
                return null;

            //表达式不合规时,名称或者值的StartIndex就是 -1
            if (context.column.children == null || context.value.StartIndex == -1)
                return null;

            var column = Visit(context.column).ToString();
            var val = context.value.Text;


            return $"{column} like {val}%";
        }

        public override object VisitEndsWith([NotNull] ODataParser.EndsWithContext context)
        {
            if (context.exception != null)
                return null;

            //表达式不合规时,名称或者值的StartIndex就是 -1
            if (context.column.children == null || context.value.StartIndex == -1)
                return null;

            var column = Visit(context.column).ToString();
            var val = context.value.Text;

            return $"{column} like %{val}";

        }

        public override object VisitContains([NotNull] ODataParser.ContainsContext context)
        {
            if (context.exception != null)
                return null;

            //表达式不合规时,名称或者值的StartIndex就是 -1
            if (context.column.children == null || context.value.StartIndex == -1)
                return null;

            var column = Visit(context.column).ToString();
            var val = context.value.Text;

            return $"{column} like %{val}%";
        }

        public override object VisitDecimal([NotNull] ODataParser.DecimalContext context)
        {
            return double.Parse(context.GetText());
        }


        public override object VisitInDecimal([NotNull] ODataParser.InDecimalContext context)
        {
            if (context.exception != null)
                return null;
            //缺少括号的情况
            if (((IToken)context.LP().Payload).StartIndex == -1 || ((IToken)context.RP().Payload).StartIndex == -1)
                return null;

            var column = Visit(context.column).ToString();
            var value = (List<double>)Visit(context.value);
            var result = string.Join(",", value);
            return $"{column} in [{result}]";
        }

        public override object VisitInText([NotNull] ODataParser.InTextContext context)
        {
            if (context.exception != null)
                return null;

            //缺少括号的情况
            if (((IToken)context.LP().Payload).StartIndex == -1 || ((IToken)context.RP().Payload).StartIndex == -1)
                return null;

            if (context.column.children == null || context.value.children == null)
                return null;

            var column = Visit(context.column).ToString();
            var value = (List<string>)Visit(context.value);
            var result = string.Join(",", value);
            return $"{column} in [{result}]";
        }

        public override object VisitColumn_name([NotNull] ODataParser.Column_nameContext context)
        {
            return context.GetText();
        }

        public override object VisitDecimal_array([NotNull] ODataParser.Decimal_arrayContext context)
        {
            var list = new List<double>();
            foreach (var item in context.NUMBER())
            {
                list.Add(double.Parse(item.GetText()));
            }
            return list;
        }

        public override object VisitString_array([NotNull] ODataParser.String_arrayContext context)
        {
            var list = new List<string>();
            foreach (var item in context.TEXT())
            {
                list.Add(item.GetText());
            }
            return list;
        }

        //public override object VisitText([NotNull] ODataParser.TextContext context)
        //{
        //    object obj = context.GetText();
        //    return obj;
        //}
    }
}
