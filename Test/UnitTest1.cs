using System;
using Xunit;

namespace ODataFilterParser.Test
{
    public class UnitTest1
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

        [Fact]
        public void TestEmpyt()
        {
            var sql = ODataFilterParser.Parser("");
            Assert.Equal("", sql);
        }


        [Fact]
        public void TestException()
        {
            var sql = ODataFilterParser.Parser("not value");
            Assert.Equal("", sql);
        }


    }
}
