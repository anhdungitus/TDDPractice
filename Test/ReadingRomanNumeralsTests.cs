using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Test
{
    [TestFixture]
    public class ReadingRomanNumeralsTests
    {
        [Test]
        public void Read_And_Return_Right_Single()
        {
            var reader = new ReadingRomanNumerals();
            reader.Read("I").Should().Be(1);
            reader.Read("V").Should().Be(5);
            reader.Read("X").Should().Be(10);
            reader.Read("L").Should().Be(50);
            reader.Read("C").Should().Be(100);
            reader.Read("D").Should().Be(500);
            reader.Read("M").Should().Be(1000);
        }
    }

    public class ReadingRomanNumerals
    {
        public int Read(string romanNumber)
        {
            if (romanNumber == "I") return 1;
            if (romanNumber == "V") return 5;
            if (romanNumber == "X") return 10;
            if (romanNumber == "L") return 50;
            if (romanNumber == "C") return 100;
            if (romanNumber == "D") return 500;
            if (romanNumber == "M") return 1000;
            return 0;
        }
    }
}