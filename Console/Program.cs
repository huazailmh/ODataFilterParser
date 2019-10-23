using System;

namespace ODataFilterParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string input = @"startswith(aaa, 'xd AND e') and endswith(bbb, ""555"") or contains(ccc, '666')";
            string input = @"aaa LE 1";
            //string input = @"aaa LE xde";
            //string input = "aaa LE \"xd e\"";
            //string input = @"aaa = xde AND bbb = ff or ccc = gg AND (ddd = b or eee = abc)";
            //string input = @"aaa EQ de AND bbb EQ ff or ccc EQ gg AND (ddd EQ b or eee EQ abc)";

            //string input = @"123 = 312 AND 3 = 4 or 5 EQ 66 AND (77 EQ 88 or 99 LE 1010)";
            //string input = @"aaa EQ de123 AND [bbb] NE ff or ccc EQ gg AND (ddd EQ b or eee LE abc)";
            //string input = @"aaa EQ 'de' AND 'bbb' eQ 'ff' or ccc EQ 'gg' AND (ddd EQ 'b' or eee EQ 'a123')";
            //string input = "[aaa] EQ \"de\" AND bbb eQ \"ff\" or ccc EQ 'gg' AND (ddd EQ \"b\" or eee LE \"a123\")";
            var sql = ODataFilterParser.Parser(input);
            Console.WriteLine(" in:" + input);
            Console.WriteLine("out:" + sql);
            //Console.ReadKey();
        }
    }
}
