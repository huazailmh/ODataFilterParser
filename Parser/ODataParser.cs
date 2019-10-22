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
            if (String.IsNullOrWhiteSpace(filter))
                return filter;

            var stream = new AntlrInputStream(filter);
            var lexer = new ODataLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new ODataParser(tokens);
            var tree = parser.program();

            var visitor = new ODataVisitor();
            var result = visitor.Visit(tree);

            return result.ToString();
        }
    }

}
