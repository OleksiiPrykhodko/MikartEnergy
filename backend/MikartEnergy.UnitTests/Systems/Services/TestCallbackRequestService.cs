using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.UnitTests.Systems.Services
{
    public class TestCallbackRequestService
    {
        [Fact]
        public async void CreateCallbackRequestAsync_PostNewCallbackRequest_ReturnResultModelWithDtoTypeCallbackRequestDTO()
        {
            //Arrange
            var newCallbackRequestDTO = new NewCallbackRequestDTO()
            {
                AuthorEmail = "AuthorEmail@mail.com",
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                AuthorPhone = "88005553535",
                Budget = 3000,
                IntrerestedIn = "Test",
                Message = "Test"
            };

            var dataBaseContext = CreateDbContext();
            var callbackRequestService = new CallbackRequestService(dataBaseContext);

            //Act
            var result = await callbackRequestService.CreateCallbackRequestAsync(newCallbackRequestDTO);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ResultModel<CallbackRequestDTO>>();
        }

        [Fact]
        public async void CreateCallbackRequestAsync_AfterPostNewCallbackRequest_DataBaseContainesNewCallbackRequest()
        {
            //Arrange
            var newCallbackRequestDTO = new NewCallbackRequestDTO()
            {
                AuthorEmail = "AuthorEmail@mail.com",
                AuthorFirstName = "Test",
                AuthorLastName = "Test",
                AuthorPhone = "88005553535",
                Budget = 3000,
                IntrerestedIn = "Test",
                Message = "Test"
            };

            var dataBaseContext = CreateDbContext();
            var callbackRequestService = new CallbackRequestService(dataBaseContext);

            //Act
            await callbackRequestService.CreateCallbackRequestAsync(newCallbackRequestDTO);

            //Assert
            dataBaseContext.CallbackRequests.ToArray().Should().NotBeEmpty();
        }

        [Fact]
        public async void GetAllCallbackRequestsAsync_GetCallbackRequests_ReturnNotNullAndRightType()
        {
            //Arrange
            var databaseContext = CreateDbContext();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var result = await callbackRequestService.GetAllCallbackRequestsAsync(true);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ResultModel<IEnumerable<CallbackRequestDTO>>>();
        }


        [Fact]
        public async void GetAllCallbackRequestsAsync_GetNotDeletedCallbackRequests_ReturnOnlyNotDeleted()
        {
            //Arrange
            var databaseContext = CreateDbContext();

            databaseContext.CallbackRequests.AddRange(
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = true,

                    AuthorEmail = "DeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 100,
                    Message = "I like tests!",
                    IntrerestedIn = "Work",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,

                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 10000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,

                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 10000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                });

            databaseContext.SaveChanges();

            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var onlyNotDeleted = await callbackRequestService.GetAllCallbackRequestsAsync(false);

            //Assert
            onlyNotDeleted.DTO.Should().OnlyContain(c => !c.IsDeleted);
        }

        [Fact]
        public async void GetAllCallbackRequestsAsync_GetAllCallbackRequests_ReturnAllWithDeleted()
        {
            //Arrange
            var testCallbackRequests = new CallbackRequest[]
            {
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = true,

                    AuthorEmail = "DeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 100,
                    Message = "I like tests!",
                    IntrerestedIn = "Work",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,

                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 10000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false,

                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 10000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                }
            };

            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.AddRange(testCallbackRequests);
            databaseContext.SaveChanges();

            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var allCallbackRequests = await callbackRequestService.GetAllCallbackRequestsAsync(true);

            //Assert
            allCallbackRequests.DTO.Should().HaveSameCount(testCallbackRequests);
        }

        private MikartContext CreateDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MikartContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MikartContext(contextOptions);
            return context;
        }
    }
}
