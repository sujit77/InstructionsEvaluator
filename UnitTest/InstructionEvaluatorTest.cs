using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace UnitTest
{
   
    [TestFixture]
    public class InstructionEvaluatorTest
    {
       
        private string InputFileForTestCase1;
        private string InputFileForTestCase2;
        private string InputFileForTestCase3;       

        [SetUp]
        public void SetUp()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            InputFileForTestCase1  = directory + "\\TestData\\input_01.txt";
            InputFileForTestCase2 = directory + "\\TestData\\input_02.txt";
            InputFileForTestCase3 = directory + "\\TestData\\inputMaster.txt";

        }
        [Test]
        public void TestCase1()
        {
           
            int expectedResult = 4;
            var actualResult = InstructionsEvaluator.InstructionEvaluator.Evaluate(InputFileForTestCase1);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void TestCase2()
        {
            
            int expectedResult = 0;
            var actualResult = InstructionsEvaluator.InstructionEvaluator.Evaluate(InputFileForTestCase2);
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        [Ignore("Ignore this test as it takes long time")]
        public void TestCase3()
        {
           
            int expectedResult = 348086909;
            var actualResult = InstructionsEvaluator.InstructionEvaluator.Evaluate(InputFileForTestCase3);
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
