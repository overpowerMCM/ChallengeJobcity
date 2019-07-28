using NUnit.Framework;
using StockBot.Sources;
using System.Collections.Generic;

namespace StockBot.Test.Sources
{
    [TestFixture]
    class BotProcessHelperTest
    {

        BotProcessHelper helper;

        [SetUp]
        public void BeginTest()
        {
            helper = new BotProcessHelper();
        }

        [Test]
        public void ParseNullData_ShouldResturn_NullList()
        {

            //Action
            List<List<string>> list = helper.ParseCSV(null);

            //Assert
            Assert.IsNull(list);
        }

        [Test]
        public void ParseCVS_CSVByteArrayData_ShouldReturnAList()
        {
            //Action
            List<List<string>> list = helper.ParseCSV( System.Text.Encoding.UTF8.GetBytes( "1,2,3,4,5,6" ) );

            //Assert
            Assert.IsNotNull(list);
        }

        [TestCase("1,2,3,4", 4)]
        [TestCase("1", 1)]
        public void ParseCVS_SingleLine_ElementCount( string csv, int count )
        {
            List<List<string>> list = helper.ParseCSV(System.Text.Encoding.UTF8.GetBytes(csv));

            Assert.AreEqual( list[0].Count, count  );
        }

        [TestCase("1,2\n2,3\n3,4", 3)]
        [TestCase("1,1,1,1", 1)]
        public void ParseCSV_MultipleLines_Count(string csv, int count)
        {
            List<List<string>> list = helper.ParseCSV(System.Text.Encoding.UTF8.GetBytes(csv));

            Assert.AreEqual( list.Count, count );
        }
    }
}
