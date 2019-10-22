using Antlr4.Runtime;
using System;

namespace ODataFilterParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = @"aaa LE xde";
            //string input = @"aaa = xde AND bbb = ff or ccc = gg AND (ddd = b or eee = abc)";
            //string input = @"aaa EQ de AND bbb EQ ff or ccc EQ gg AND (ddd EQ b or eee EQ abc)";

            //string input = @"123 = 312 AND 3 = 4 or 5 EQ 66 AND (77 EQ 88 or 99 LE 1010)";
            string input = @"aaa EQ de123 AND bbb NE ff or ccc EQ gg AND (ddd EQ b or eee LE abc)";
            //string input = @"aaa EQ 'de' AND 'bbb' eQ 'ff' or ccc EQ 'gg' AND (ddd EQ 'b' or eee EQ 'a123')";
            //string input = "[aaa] EQ \"de\" AND bbb eQ \"ff\" or ccc EQ \"gg\" AND (ddd EQ \"b\" or eee EQ \"a123\")";

            var stream = new AntlrInputStream(input);
            var lexer = new ODataLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new ODataParser(tokens);
            var tree = parser.program();

            var visitor = new ODataVisitor();
            var result = visitor.Visit(tree);

            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(" in:" + input);
            Console.WriteLine("out:" + result);
            Console.ReadKey();
        }
    }
}
