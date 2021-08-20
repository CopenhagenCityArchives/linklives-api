using linklives_api.Controllers;
using linklives_api_dal.Repositories;
using Moq;
using NUnit.Framework;

namespace linklives_api_Test
{
    [TestFixture]
    public class LifecourseTests
    {
        private ILifeCourseRepository lc_repo;
        private IPersonAppearanceRepository pa_repo;
        private ISourceRepository source_repo;
        private LifeCourseController controller;

        [SetUp]
        public void Setup()
        {
            var lc_repo = new Mock<ILifeCourseRepository>();
            var pa_repo = new Mock<IPersonAppearanceRepository>();
            var source_repo = new Mock<ISourceRepository>();
            var controller = new LifeCourseController(lc_repo.Object, pa_repo.Object, source_repo.Object);
        }

        [Test]
        public void Test1()
        {
            
        }
    }
}