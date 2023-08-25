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
    public class AlertsControllerTests
    {
        private readonly ILogger<AlertsController> _mockLogger;
        [Fact()]
        public void GetTest()
        {
            var mockService = new Mock<ICrudService<AlertsDto>>();

            var controller = new AlertsController(_mockLogger, mockService.Object);
            var actionResult = controller.Get();

            Assert.NotNull(actionResult);
        }

        [Fact()]
        public void GetTestById()
        {
            var alertDto = new AlertsDto()
            {
                WellName = "Test",
                AlertType = "Low",
                AlertStatus = "Snooze",
                AlertLevel = "High",
                AlertDescription = "Test",
                TimeandDate=null,
                UserId="test123"
             
            };

        var mockService = new Mock<ICrudService<AlertsDto>>();

            mockService.Setup(expression: p => p.GetAsync(1)).ReturnsAsync(alertDto);

            var controller = new AlertsController(_mockLogger, mockService.Object);
            var actionResult = controller.Get(1);
            Assert.Equal(alertDto, actionResult.Result.Value);
        }
        [Fact()]
        public void GetAlertTestPositive()
        {
            var alertDto = new AlertsDto()
            {
                WellName = "Test",
                AlertType = "Low",
                AlertStatus = "Snooze",
                AlertLevel = "High",
                AlertDescription = "Test",
                TimeandDate = null,
                UserId = "test123"

            };

            var mockService = new Mock<ICrudService<AlertsDto>>();

            mockService.Setup(expression: p => p.GetAsync(1)).ReturnsAsync(alertDto);

            var controller = new AlertsController(_mockLogger, mockService.Object);
            var actionResult = controller.Get(1);
            Assert.NotNull(actionResult.Result.Value);
        }
    }
}
