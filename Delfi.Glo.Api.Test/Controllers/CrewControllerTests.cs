using Microsoft.Extensions.Logging;
using Moq;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;

namespace Delfi.Glo.Api.Controllers.Tests
{
    public class CrewControllerTests
    {
        private readonly ILogger<CrewController> _mockLogger;

        [Fact()]
        public void GetTest()
        {
            var mockService = new Mock<ICrudService<CrewDto>>();

            var controller = new CrewController(_mockLogger, mockService.Object);
            var actionResult = controller.Get();

            Assert.NotNull(actionResult);
        }

        [Fact()]
        public void CreateTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }
    }
}