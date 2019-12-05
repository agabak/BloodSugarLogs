using BloodSugarLog.DL;
using BloodSugarLog.Entities;
using BloodSugarLog.Models;
using BloodSugarLog.Services;
using BloodSugarLog.Test.Mock.Class;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BloodSugarLog.Test.Services
{
    public class BloodSugarLogService_Test
    {
        private Mock<FakeUserManager> _userManagerMock;
        private Mock<FakeSignInManager> _signInManagerMock;

        public BloodSugarLogService_Test()
        {
             _userManagerMock = new Mock<FakeUserManager>();
             _signInManagerMock = new Mock<FakeSignInManager>();
        }

         [Fact]
        public async Task BloodSugarLogService_Return_BloodLogsForPassedInUser()
        {
            var options = FakeDataDBContext();

            using(var context = new BloodSugarDbContext(options))
            {
                var service = new BloodSugarLogService(_userManagerMock.Object, _signInManagerMock.Object,  context);
                 _userManagerMock.Setup(x => x.FindByEmailAsync("agaba@test.com"))
                                 .ReturnsAsync(new ApplicationUser()
                                    {
                                        UserName = "agaba@test.com",
                                        Id = "1234tyy"
                                    });
                var results = await service.GetBloodLogs("agaba@test.com");
                var count = results.Count;
                Assert.Equal(2,count);
            }
        }

         [Fact]
        public async Task BloodSugarLogService_Return_PassUserWithNoResultReturnZero()
        {
            var options = FakeDataDBContext();

            using(var context = new BloodSugarDbContext(options))
            {
                var service = new BloodSugarLogService(_userManagerMock.Object, _signInManagerMock.Object,  context);
                 _userManagerMock.Setup(x => x.FindByEmailAsync("agaba@test.com"))
                                 .ReturnsAsync(new ApplicationUser()
                                    {
                                        UserName = "agaba@test.com",
                                        Id = "1234"
                                    });
                var results = await service.GetBloodLogs("agaba@test.com");
                var count = results.Count;
                Assert.Equal(0,count);
            }
        }

        [Fact]
        public async Task BloodSugarLogService_Create_Happ_Path()
        {
              var options = FakeDataDBContext();
            using(var context = new BloodSugarDbContext(options))
            {
                var service = new BloodSugarLogService(_userManagerMock.Object, _signInManagerMock.Object,  context);
                 _userManagerMock.Setup(x => x.FindByEmailAsync("agaba@test.com"))
                                 .ReturnsAsync(new ApplicationUser()
                                    {
                                        UserName = "agaba@test.com",
                                        Id = "1234yy"
                                    });
                var result = await service.Create(new CreateCommandModel {FoodName = "Pilau", BloodValue = 123, Name ="agaba@test.com" });
               
                Assert.True(result);
            }

        }

        [Fact]
        public async Task BloodSugarLogService_Create_Fail_Path()
        {
              var options = FakeDataDBContext();
            using(var context = new BloodSugarDbContext(options))
            {
                var service = new BloodSugarLogService(_userManagerMock.Object, _signInManagerMock.Object,  context);
                // No user in the database
                var result = await service.Create(new CreateCommandModel {BloodValue = 123 });
               
                Assert.False(result);
            }
        }



        private DbContextOptions<BloodSugarDbContext> FakeDataDBContext()
        {

            var options = new DbContextOptionsBuilder<BloodSugarDbContext>()
                              .UseInMemoryDatabase(databaseName: "bloodSugarlogdb")
                              .Options;

            using (var context = new BloodSugarDbContext(options))
            {
                context.FoodInTakes.Add(new FoodInTake
                {
                     ApplicationUserId = "1234tyy",
                     BloodValue = 100,
                     Name = "Pizza",
                    TakeTime  = new DateTime(2019,12,05)
                });

                context.FoodInTakes.Add(new FoodInTake
                {
                     ApplicationUserId = "1234tyy",
                     BloodValue = 99,
                     Name = "Pancake",
                    TakeTime  = new DateTime(2019,12,05)
                });

                context.FoodInTakes.Add(new FoodInTake
                {
                     ApplicationUserId = "1234tyy6",
                     BloodValue = 110,
                     Name = "Cheesecake",
                    TakeTime  = new DateTime(2019,12,05)
                });

                context.FoodInTakes.Add(new FoodInTake
                {
                     ApplicationUserId = "1234tyy4",
                     BloodValue = 100,
                     Name = "Pizza, Milk, butter milk",
                    TakeTime  = new DateTime(2019,12,05)
                });

                context.SaveChanges();
            }

            return options;
        }
    }
}
