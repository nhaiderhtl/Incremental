using System.Globalization;

namespace Incremental.Tests;

using BigNum;

public class BigNumTests
{
    public class StringToBigNumTests()
    {
        [Theory]
        [InlineData("250.75 K", 250.75, "K")]
        [InlineData("+250.75 K", 250.75, "K")]
        [InlineData("-123.45", -123.45, "")]
        [InlineData("999.99 Qd", 999.99, "Qd")]
        [InlineData("1.00 De", 1.00, "De")]
        [InlineData("123.456", 123.456, "")]
        [InlineData("0", 0, "")]
        public void ToBigNum_ValidInputs_ShouldParseCorrectly(
            string input,
            double expectedNumber,
            string expectedSuffix)
        {
            // Arrange
            var expected = new BigNum(expectedNumber, expectedSuffix);

            // Act
            var actual = new BigNum(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }

    public class BigNumToStringTests
    {
        public BigNumToStringTests()
        {
            // ensure consistent formatting regardless of machine locale
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Theory]
        // number        ↴   suffix         ↴      expected string
        [InlineData(250.75, "", "250.75")]
        [InlineData(250.75, "K", "250.75 K")]
        [InlineData(0, "", "0")]
        [InlineData(-123.45, "", "-123.45")]
        [InlineData(-123.45, "T", "-123.45 T")]
        [InlineData(1000, "M", "1000 M")]
        public void ToString_FormatsCorrectly(double number, string suffix, string expected)
        {
            // Arrange
            var bn = new BigNum(number, suffix);

            // Act
            var actual = bn.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        // round-trip: ToString → new BigNum(string) should yield the same value
        [InlineData(435.21, "")]
        [InlineData(435.21, "M")]
        [InlineData(-999.99, "Qd")]
        [InlineData(0.5, "")]
        public void ToString_RoundTrip_ShouldParseBackToSameValue(double number, string suffix)
        {
            // Arrange
            var original = new BigNum(number, suffix);

            // Act
            var str = original.ToString();
            var reparsed = new BigNum(str);

            // Assert
            Assert.Equal(original, reparsed);
        }

        [Fact]
        public void ToString_SingleDecimalZero_ShouldNotIncludeTrailingDotZero()
        {
            // e.g. 1.0 → "1"
            var bn = new BigNum(1.0, "");

            Assert.Equal("1", bn.ToString());
        }
    }

    public class BigNumOperatorTests
    {
        // + operator

        [Fact]
        public void Add_NoOverflow_ShouldKeepSuffix()
        {
            // Arrange
            var a = new BigNum(200, "");
            var b = new BigNum(300, "");

            // Act
            var result = a + b;

            // Assert
            Assert.Equal(new BigNum(500, ""), result);
        }

        [Fact]
        public void Add_SingleOverflow_ShouldIncrementSuffixOnce()
        {
            // 600 K + 500 K = 1100 → 1.1 M
            var a = new BigNum(600, "K");
            var b = new BigNum(500, "K");

            var result = a + b;

            Assert.Equal(new BigNum(1.1, "M"), result);
        }

        [Fact]
        public void Add_DoubleOverflow_ShouldIncrementSuffixTwice()
        {
            // 1_100_000 K + 0 K = 1_100_000 →
            // 1_100_000/1_000=1_100 → suffix K→M
            // 1_100/1_000  =1.1   → suffix M→B
            //TODO Fix Unit Tests wtf is that?
            var a = new BigNum(1_100_000, "K");
            var b = new BigNum(0, "K");

            var result = a + b;

            Assert.Equal(new BigNum(1.1, "B"), result);
        }


        // - operator

        [Fact]
        public void Subtract_NoUnderflow_ShouldKeepSuffix()
        {
            // 5 M - 3 M = 2 M
            var a = new BigNum(5, "M");
            var b = new BigNum(3, "M");

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(2, "M"), result);
        }

        [Fact]
        public void Subtract_SingleUnderflow_ShouldDecrementSuffixOnce()
        {
            // 0.5 M - 0.2 M = 0.3→0.3*1_000=300, suffix M→K
            var a = new BigNum(0.5, "M");
            var b = new BigNum(0.2, "M");

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(300, "K"), result);
        }

        [Fact]
        public void Subtract_EqualValues_ShouldReturnZeroWithSameSuffix()
        {
            var a = new BigNum(250, "K");
            var b = new BigNum(250, "K");

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(0, "K"), result);
        }

        [Fact]
        public void Subtract_ResultExactlyOne_ShouldKeepSuffix()
        {
            // 1.5 M - 0.5 M = 1.0 M → no underflow
            var a = new BigNum(1.5, "M");
            var b = new BigNum(0.5, "M");

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(1.0, "M"), result);
        }
    }
}