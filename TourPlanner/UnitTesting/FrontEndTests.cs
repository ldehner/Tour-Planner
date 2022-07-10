

namespace UnitTesting
{
    public class FrontEndTests
    {

        private Tourlist _tourList;

        public FrontEndTests()
        {
            _tourList = new Tourlist();
            Tour t = new Tour();
        } 

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}