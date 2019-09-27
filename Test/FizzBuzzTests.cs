using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Test
{
    // number % 3 => return Fizz
    // number % 5 => return Buzz
    // number % 3 && number % 5 => return FizzBuzz
    // other => return ""

    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void Modulo3_Return_Fizz()
        {
            FizzBuzz(3).Should().Be("Fizz");
            FizzBuzz(6).Should().Be("Fizz");
        }

        [Test]
        public void Modulo5_Return_Buzz()
        {
            FizzBuzz(5).Should().Be("Buzz");
            FizzBuzz(10).Should().Be("Buzz");
        }

        [Test]
        public void Modulo5_And_3_Return_Fizz_Buzz()
        {
            FizzBuzz(15).Should().Be("FizzBuzz");
            FizzBuzz(0).Should().Be("FizzBuzz");
        }

        [Test]
        public void No_Modulo5_And_3_Return_Empty()
        {
            FizzBuzz(4).Should().Be("");
        }

        private string FizzBuzz(int number)
        {
            if (number % 15 == 0)
                return "FizzBuzz";

            if (number % 3 == 0)
                return "Fizz";

            if (number % 5 == 0)
                return "Buzz";
            return "";
        }
    }
}