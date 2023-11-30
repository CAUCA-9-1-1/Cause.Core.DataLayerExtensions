using FluentAssertions;
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
            value.Should().Be(value.ToSnakeCase());
        }

        [TestCase]
        public void NullStringAreReturnedWithoutTransformation()
        {
            ((string)null).ToSnakeCase().Should().BeNull();
        }

        [TestCase("MyTable", "my_table")]
        [TestCase("MyTable33", "my_table33")]
        [TestCase("MyTableHasLongName", "my_table_has_long_name")]
        public void StringIsCorrectlyTransformedToSnakeCase(string name, string result)
        {
            name.ToSnakeCase().Should().Be(result);
        }
    }
}