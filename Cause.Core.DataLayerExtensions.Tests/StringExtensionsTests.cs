using NUnit.Framework;

namespace Cause.Core.DataLayerExtensions.Tests
{
    [TestFixture]
    public class Tests
    {
        [TestCase]
        public void EmptyStringAreReturnedWithoutTransformation()
        {
            var value = string.Empty;
            Assert.AreEqual(value, value.ToSnakeCase());
        }

        [TestCase]
        public void NullStringAreReturnedWithoutTransformation()
        {
            string value = null;
            Assert.AreEqual(value, value.ToSnakeCase());
        }

        [TestCase("MyTable", "my_table")]
        [TestCase("MyTable33", "my_table33")]
        [TestCase("MyTableHasLongName", "my_table_has_long_name")]
        public void StringIsCorrectlyTransformedToSnakeCase(string name, string result)
        {
            Assert.AreEqual(result, name.ToSnakeCase());
        }
    }
}