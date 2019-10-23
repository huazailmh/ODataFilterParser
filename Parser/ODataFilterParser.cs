using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODataFilterParser
{
    public static class ODataFilterParser
    {

        public static string Parser(string filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            if (String.IsNullOrEmpty(filter))
                return filter;

            var stream = new AntlrInputStream(filter);
            var lexer = new ODataLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new ODataParser(tokens);
            var tree = parser.program();

            var visitor = new ODataFilterVisitor();
            var result = visitor.Visit(tree);

            return result?.ToString();
        }
    }

}
