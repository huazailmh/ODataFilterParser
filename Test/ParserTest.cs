using Antlr4.Runtime;
using System;
using Xunit;

namespace ODataFilterParser.Test
{
    public class ParserTest
    {
        [Fact]
        public void TestStartsWith()
        {
            var sql = ODataFilterParser.Parser("startswith(username, 'xie')");
            Assert.Equal("username like 'xie'%", sql);
        }

        [Fact]
        public void TestEndsWith()
        {
            var sql = ODataFilterParser.Parser("endswith(username, 'xie')");
            Assert.Equal("username like %'xie'", sql);
        }
        [Fact]
        public void TestContains()
        {
            var sql = ODataFilterParser.Parser("contains(username, 'xie')");
            Assert.Equal("username like %'xie'%", sql);
        }

        [Theory]
        [InlineData("username EQ 'xie jian'", "username = 'xie jian'")]
        [InlineData("[table] EQ 'xie jian'", "[table] = 'xie jian'")]
        [InlineData("username NE 'xie jian'", "username != 'xie jian'")]
        [InlineData("username GT 'xie jian'", "username > 'xie jian'")]
        [InlineData("username GE 'xie jian'", "username >= 'xie jian'")]
        [InlineData("username LT 'xie jian'", "username < 'xie jian'")]
        [InlineData("username LE 'xie jian'", "username <= 'xie jian'")]

        public void TestStringCompare(string input, string result)
        {
            var sql = ODataFilterParser.Parser(input);
            Assert.Equal(result, sql);
        }

        [Theory]
        [InlineData("a EQ '1' AND a EQ '2'", "a = '1' AND a = '2'")]
        [InlineData("a EQ '1' and a EQ '2'", "a = '1' AND a = '2'")]
        [InlineData("a EQ '1' OR a EQ '2'", "a = '1' OR a = '2'")]
        [InlineData("a EQ '1' or a EQ '2'", "a = '1' OR a = '2'")]
        public void TestLogic(string input, string result)
        {
            var sql = ODataFilterParser.Parser(input);
            Assert.Equal(result, sql);
        }

        [Theory]
        [InlineData("(a EQ 'a')", "(a = 'a')")]
        [InlineData("((a EQ 'a'))", "((a = 'a'))")]
        [InlineData("a EQ 'a' and (age GT 10 OR age LT 100)", "a = 'a' AND (age > 10 OR age < 100)")]
        public void TestParenthesis(string input, string result)
        {
            var sql = ODataFilterParser.Parser(input);
            Assert.Equal(result, sql);

        }

        [Theory]
        [InlineData("age EQ 100", "age = 100")]
        [InlineData("age EQ -100", "age = -100")]
        [InlineData("age GT 1.1", "age > 1.1")]
        [InlineData("age GT -1.1", "age > -1.1")]
        [InlineData("age EQ 0.10", "age = 0.1")]
        public void TestDecimalCompare(string input, string result)
        {
            var sql = ODataFilterParser.Parser(input);
            Assert.Equal(result, sql);
        }

        [Theory]
        [InlineData("age in (1,2,3)", "age in [1,2,3]")]
        [InlineData("age in (1,2,-3)", "age in [1,2,-3]")]
        [InlineData("name in ('a','b','ccc')", "name in ['a','b','ccc']")]
        public void TestIn(string input, string result)
        {
            var sql = ODataFilterParser.Parser(input);
            Assert.Equal(result, sql);
        }


        [Fact]
        public void TestEmpyt()
        {
            Assert.Equal("", ODataFilterParser.Parser(""));
        }

        [Fact]
        public void TestNull()
        {
            Assert.Throws<ArgumentNullException>(() => ODataFilterParser.Parser(null));
        }


        [Theory]
        [InlineData("notvalue")]
        [InlineData("username = ")]
        [InlineData("12username = ")]
        [InlineData("user*name = ")]
        [InlineData("username = '123'")]
        [InlineData("username EQ abc")]
        [InlineData("username EQ 'abc' and")]
        [InlineData("username  'abc' and age EQ '12'")]
        [InlineData("startswith(,'abc')")]
        [InlineData("startswith(username,)")]
        [InlineData("startswith(,123)")]
        [InlineData("startswith(,)")]
        [InlineData("endswith(username,)")]
        [InlineData("contains(username,)")]
        [InlineData("name in")]
        [InlineData("in ('a','b')")]
        [InlineData("name ('a','b')")]
        [InlineData("name in 'a','b')")]
        [InlineData("name in ('a','b'")]
        [InlineData("name in ('a','b)")]
        [InlineData("name in (username)")]
        public void TestException(string invalidValue)
        {
            var result = ODataFilterParser.Parser(invalidValue);
            Assert.True(result == null);
        }


    }
}
