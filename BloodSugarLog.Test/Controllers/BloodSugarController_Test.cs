using BloodSugarLog.Controllers;
using BloodSugarLog.Models;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BloodSugarLog.Test.Controllers
{
    public class BloodSugarController_Test
    {
         private readonly Mock<IBloodSugarLogService> _servicMock;
         private readonly  BloodSugarController _controller;
         private ClaimsPrincipal fakeUser;
         private readonly Mock<ILogger<BloodSugarController>> _mockLogger;

        public BloodSugarController_Test()
        {
            _servicMock = new Mock<IBloodSugarLogService>();
            _mockLogger = new Mock<ILogger<BloodSugarController>>();
            _controller = new BloodSugarController(_servicMock.Object, _mockLogger.Object);
             var context = new ControllerContext
                        {
                            HttpContext = new DefaultHttpContext
                            {
                                User = fakeUser
                            }
                        };

            _controller.ControllerContext = context;
             context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "123"),
                    new Claim(ClaimTypes.Name, "testing@test.com"),
                    new Claim(ClaimTypes.Email, "test@example.com")
                }));
        }

        
        [Fact]
        public void List_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ModelStateValide_CreateCallOnce()
        {
          
             var createFood = new CreateCommandModel{ FoodName = "Pizza", BloodValue = 100};

            _servicMock.Setup(c => c.Create(createFood)).ReturnsAsync(true);

             var result = await _controller.Create(createFood);
            _servicMock.Verify(x => x.Create(It.IsAny<CreateCommandModel>()), Times.Once);

        }

        [Fact]
        public async Task Create_ModelStateValide_CreateRedirectToIndexActionResult()
        {
          
             var createFood = new CreateCommandModel{ FoodName = "Pizza", BloodValue = 100 };

                 _servicMock.Setup(c => c.Create(createFood)).ReturnsAsync(true);

             var result = await _controller.Create(createFood);
             var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

       [Fact]
        public async Task Create_InvalidModelState_CreatePersonAccomplishmentNeverExecutes()
        {
            _controller.ModelState.AddModelError("FoodName", "Name is required");

            var personAccomplishment = new CreateCommandModel { BloodValue = 123 };
            await _controller.Create(personAccomplishment);
            _servicMock.Verify(x => x.Create(It.IsAny<CreateCommandModel>()), Times.Never);
        }

    }
}
