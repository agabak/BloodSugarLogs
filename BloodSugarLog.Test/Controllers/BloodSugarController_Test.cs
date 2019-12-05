using BloodSugarLog.Controllers;
using BloodSugarLog.Models;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public BloodSugarController_Test()
        {
            _servicMock = new Mock<IBloodSugarLogService>();
            _controller = new BloodSugarController(_servicMock.Object);
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
    }
}
