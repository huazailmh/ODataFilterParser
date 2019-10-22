using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace ODataFilterParser
{
    public class ODataVisitor : ODataBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitParenthesis(ODataParser.ParenthesisContext context)
        {
            object obj = "(" + Visit(context.expression()).ToString() + ")";
            return obj;
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
            var left = Visit(context.expression(0)).ToString();
            var right = Visit(context.expression(1)).ToString();

            if (context.keyword.Type == ODataParser.K_AND)
            {
                return $"{left} AND {right}";
            }
            else if (context.keyword.Type == ODataParser.K_OR)
            {
                return $"{left} OR {right}";
            }

            return string.Empty;
        }

        public override object VisitEquals(ODataParser.EqualsContext context)
        {
            var left = Visit(context.expression(0)).ToString();
            var right = Visit(context.expression(1)).ToString();

            switch (context.operate.Type)
            {
                case ODataParser.Equal:
                    return $"{left} = {right}";

                case ODataParser.NotEqual:
                    return $"{left} != {right}";

                case ODataParser.GreaterThan:
                    return $"{left} > {right}";

                case ODataParser.GreaterThanOrEqual:
                    return $"{left} >= {right}";

                case ODataParser.LessThan:
                    return $"{left} < {right}";

                case ODataParser.LessThanOrEqual:
                    return $"{left} <= {right}";

                default:
                    return string.Empty;
            }

            return string.Empty;
        }

        public override object VisitText([NotNull] ODataParser.TextContext context)
        {
            object obj = context.GetText();
            return obj;
        }
    }
}
