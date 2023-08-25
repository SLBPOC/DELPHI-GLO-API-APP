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
            var mockService = new Mock<ICrudService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            var wellGeneralInfoDTO = new WellGeneralInfoDto()
            {
                Id = 1,
                Qo = 0,
                Ql = 0,
                Qw = 0,
                Qg = 0,
                Wc = 0,
                GlInjectionSetPoint = 901,
                CompressorUpTime = 100,
                DeviceUpTime = 100,
                ProcessorState = "Rate Calculation",
                ApprovalMode = "Auto",
                WellViewComment1 = "test",
                WellViewComment2 = "test",
                WellViewComment3 = "test",
                WellViewComment4 = "test"
            };
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.Equal(result.Value, wellGeneralInfoDTO);
            
        }

        [Fact]
        public async void GetTestPositive()
        {
            var mockService = new Mock<ICrudService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            var wellGeneralInfoDTO = new WellGeneralInfoDto()
            {
                Id = 1,
                Qo = 0,
                Ql = 0,
                Qw = 0,
                Qg = 0,
                Wc = 0,
                GlInjectionSetPoint = 901,
                CompressorUpTime = 100,
                DeviceUpTime = 100,
                ProcessorState = "Rate Calculation",
                ApprovalMode = "Auto",
                WellViewComment1 = "test",
                WellViewComment2 = "test",
                WellViewComment3 = "test",
                WellViewComment4 = "test"
            };
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void GetTestNegative()
        {
            var mockService = new Mock<ICrudService<WellGeneralInfoDto>>();
            var wellGeneralInfoDTO1 = new WellGeneralInfoDto();
            wellGeneralInfoDTO1 = null;
            
            mockService.Setup(p => p.GetAsync(1)).ReturnsAsync(wellGeneralInfoDTO1);
            _controller = new WellGeneralInfoController(_mockLogger, mockService.Object);
            var result = await _controller.Get(1);
            Assert.NotNull(result.Value);
        }
    }
}
