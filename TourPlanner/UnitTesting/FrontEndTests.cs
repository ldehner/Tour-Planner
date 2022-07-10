using Tour_planner.Business;
using TourPlanner.Business;

namespace UnitTesting
{
    public class FrontEndTests
    {

        private Tourlist _tourList;

        public FrontEndTests()
        {
            _tourList = new Tourlist();
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