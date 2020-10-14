using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
    public class FizzBuzzTests
    {
        [Test]
        public void Test1() =>
            Assert.AreEqual("Fizz", FizzBuzz(3));

        [Test]
        public void Test2() =>
            Assert.AreEqual("Fizz", FizzBuzz(9));

        [Test]
        public void Test3() =>
            Assert.AreEqual("Buzz", FizzBuzz(20));

        [Test]
        public void Test4() =>
            Assert.AreEqual("13", FizzBuzz(13));

        [Test]
        public void Test5() =>
            Assert.AreEqual("FizzBuzz", FizzBuzz(15));
        

        public string FizzBuzz(int number)
        {
            if (number % 15 == 0)
            {
                return "FizzBuzz";
            }

            if (number % 3 == 0)
            {
                return "Fizz";
            }

            if (number % 5 == 0)
            {
                return "Buzz";
            }
            
            return number.ToString();
        }
    }
}