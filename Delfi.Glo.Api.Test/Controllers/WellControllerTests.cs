using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Delfi.Glo.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Delfi.Glo.Entities.Db;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;
using Delfi.Glo.Common.Helpers;

namespace Delfi.Glo.Api.Test.Controllers
{
    public class WellControllerTests
    {
        private readonly ILogger<WellController> _mockLogger;

        [Fact()]
        public void GetTestById()
        {
              var wellDto = new WellDto()
            {
                WellName="Test",          
                WellPriority="High",  
                Wc=1,
                GLISetPoint = 1,
             //   CompressorUpTime=1,
                DeviceUpTime=1,
                TimeStamp= Convert.ToDateTime( "2023-08-04 00:00:00+05:30"),
                QLiq=1,
                QOil=1,
                LastCycleStatus="Good",
                CurrentGLISetpoint=1,
                CurrentCycleStatus = "Good",
                ApprovalStatus = "Approved"
            };
            var mockFilterService = new Mock<IFilterService<WellDto>>();
            var mockWellService = new Mock<IWellService<WellInfoByRangeDto>>();
            var mockService = new Mock<ICrudService<WellDto>>();
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellDto);
            var controller = new WellController(_mockLogger, mockService.Object, mockFilterService.Object, (IWellService<WellDetailsDto>)mockWellService.Object);

            var actionResult = controller.Get(1);

            Assert.True(wellDto.Equals(actionResult));
        }


        

        [Fact()]
        public void GetTest()
        {
            var mockService = new Mock<ICrudService<WellDto>>();
            var mockFilterService = new Mock<IFilterService<WellDto>>();
            var mockWellService = new Mock<IWellService<WellInfoByRangeDto>>();
            var controller = new WellController(_mockLogger, mockService.Object, mockFilterService.Object,(IWellService<WellDetailsDto>)mockWellService.Object);
            var actionResult = controller.Get();

            Assert.NotNull(actionResult);
        }

    }
}
