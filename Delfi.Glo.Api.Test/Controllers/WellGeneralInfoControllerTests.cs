using Delfi.Glo.Api.Controllers;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Api.Test.Controllers
{
    public class WellGeneralInfoControllerTests
    {
        private readonly ILogger<WellGeneralInfoController> _mockLogger;
        WellGeneralInfoController _controller;

        [Fact()]
        public async void GetTestEqual()
        {
            var mockService = new Mock<IGeneralInfoService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            var wellGeneralInfoDTO = new WellGeneralInfoDto()
            {
                Id = 1,
                QOil = 0,
                QLiq = 0,
                Qw = 0,
                Qg = 0,
                Wc = 0,
                GLISetPoint = 901,
                CompressorUpTime = 100,
                ProductionUpTime = 100,
                DeviceUpTime = 100,
                CurrentCycleStatus = "Rate Calculation",
                ApprovalMode = "Auto"
            };
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.Equal(result.Value, wellGeneralInfoDTO);
            
        }

        [Fact]
        public async void GetTestPositive()
        {
            var mockService = new Mock<IGeneralInfoService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            var wellGeneralInfoDTO = new WellGeneralInfoDto()
            {
                Id = 1,
                QOil = 0,
                QLiq = 0,
                Qw = 0,
                Qg = 0,
                Wc = 0,
                GLISetPoint = 901,
                CompressorUpTime = 100,
                ProductionUpTime = 100,
                DeviceUpTime = 100,
                CurrentCycleStatus = "Rate Calculation",
                ApprovalMode = "Auto"
            };
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void GetTestNegative()
        {
            var mockService = new Mock<IGeneralInfoService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.NotNull(result.Value);
        }
    }
}
