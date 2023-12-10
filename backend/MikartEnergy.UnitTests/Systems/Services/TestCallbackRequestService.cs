using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Services;
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
        public async void GetAllCallbackRequestsAsync_GetNotDeletedCallbackRequests_ReturnOnlyNotDeleted()
        {
            //Arrange
            var databaseContext = CreateContext();

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

            var databaseContext = CreateContext();
            databaseContext.CallbackRequests.AddRange(testCallbackRequests);
            databaseContext.SaveChanges();

            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var allCallbackRequests = await callbackRequestService.GetAllCallbackRequestsAsync(true);

            //Assert
            allCallbackRequests.DTO.Should().HaveSameCount(testCallbackRequests);
        }

        private MikartContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<MikartContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MikartContext(contextOptions);
            return context;
        }
    }
}
