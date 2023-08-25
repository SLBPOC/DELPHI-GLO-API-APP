using Delfi.Glo.Api.Controllers;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Api.Test.Controllers
{
    public class EventControllerTests
    {
        private readonly ILogger<EventController> _mockLogger;

        [Fact()]
        public void GetTest()
        {
            var mockService = new Mock<ICrudService<EventDto>>();

            var controller = new EventController(_mockLogger, mockService.Object);
            var actionResult = controller.Get();

            Assert.NotNull(actionResult);
        }

        [Fact()]
        public void GetEventTestEqual()
        {
            var eventDto = new EventDto()
            {
                WellName = "Test",
                EventType = "Over Pumping",
                EventStatus = "High",
                EventDescription = "Test",
                Priority = "LOw",
                CreationDateTime =null

            };

            var mockService = new Mock<ICrudService<EventDto>>();

            mockService.Setup(expression: p => p.GetAsync(1)).ReturnsAsync(eventDto);

            var controller = new EventController(_mockLogger, mockService.Object);
            var actionResult = controller.Get(1);
            Assert.Equal( eventDto,actionResult.Result.Value);
        }
    }
}
