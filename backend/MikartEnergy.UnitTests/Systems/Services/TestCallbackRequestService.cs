using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.Enums;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;
using MikartEnergy.UnitTests.Fixtures;
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
            var newCallbackRequestDTO = CallbackRequestsFixtures.GetNewCallbackRequestDTO();
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
            var newCallbackRequestDTO = CallbackRequestsFixtures.GetNewCallbackRequestDTO();
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
            databaseContext.CallbackRequests.AddRange(CallbackRequestsFixtures.GetCallbackRequests());
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
            var callbackRequests = CallbackRequestsFixtures.GetCallbackRequests();
            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.AddRange(callbackRequests);
            databaseContext.SaveChanges();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var allCallbackRequests = await callbackRequestService.GetAllCallbackRequestsAsync(true);

            //Assert
            allCallbackRequests.DTO.Should().HaveSameCount(callbackRequests);
        }

        [Fact]
        public async void DeleteCallbackRequestAsync_CallWithIdContanedInDataBase_ReturnTrue()
        {
            //Arrange
            var callbackRequest = CallbackRequestsFixtures.GetCallbackRequest();
            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.Add(callbackRequest);
            databaseContext.SaveChanges();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var result = await callbackRequestService.DeleteCallbackRequestAsync(callbackRequest.Id);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DeleteCallbackRequestAsync_CallWithUnknownId_ReturnFalse()
        {
            //Arrange
            var databaseContext = CreateDbContext();
            var callbackRequestService = new CallbackRequestService(databaseContext);
            var fakeGuid = Guid.NewGuid();

            //Act
            var result = await callbackRequestService.DeleteCallbackRequestAsync(fakeGuid);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void DeleteCallbackRequestAsync_CallWithIdOfDeletedCallbackRequest_ReturnFalse()
        {
            //Arrange
            var callbackRequest = CallbackRequestsFixtures.GetCallbackRequest(isDeleted: true);
            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.Add(callbackRequest);
            databaseContext.SaveChanges();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            //Act
            var result = await callbackRequestService.DeleteCallbackRequestAsync(callbackRequest.Id);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void UpdateCallbackRequestAsync_CallWithCorrectData_ReturnUpdatedCallbackRequestWithSuccessFlagAndWithoutErrors()
        {
            //Arrange
            var callbackRequest = CallbackRequestsFixtures.GetCallbackRequest();
            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.Add(callbackRequest);
            databaseContext.SaveChanges();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            var updated = callbackRequest.ToCallbackRequestDTO();
            updated.AuthorEmail = "SomeNew@mail.com";
            databaseContext.Entry(callbackRequest).State = EntityState.Detached;

            //Act
            var result = await callbackRequestService.UpdateCallbackRequestAsync(updated);

            //Assert
            result.DTO.AuthorEmail.Should().BeEquivalentTo(updated.AuthorEmail);
            result.Successful.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void UpdateCallbackRequestAsync_CallWithUnknownID_ReturnResultModelWithSameDtoAndUnsuccessWithErrorMessages()
        {
            //Arrange
            var callbackRequest = CallbackRequestsFixtures.GetCallbackRequest();
            var databaseContext = CreateDbContext();
            databaseContext.CallbackRequests.Add(callbackRequest);
            databaseContext.SaveChanges();
            var callbackRequestService = new CallbackRequestService(databaseContext);

            var updated = callbackRequest.ToCallbackRequestDTO();
            updated.Id = Guid.NewGuid();
            updated.AuthorEmail = "SomeNew@updated.mail";

            //Act
            var result = await callbackRequestService.UpdateCallbackRequestAsync(updated);

            //Assert
            result.DTO.Should().BeEquivalentTo(updated);
            result.Successful.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().ContainKey(ResponseError.NotFound.ToString());
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
