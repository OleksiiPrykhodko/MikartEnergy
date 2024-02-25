using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.UnitTests.Fixtures
{
    public class CallbackRequestsFixtures
    {
        public static CallbackRequest GetCallbackRequest(
            bool isDeleted = false,
            string authorEmail = "test@email.com",
            string authorFirstName = "Test",
            string authorLastName = "Test",
            string authorPhone = "+380123456789",
            int budget = 10000,
            string message = "It is test callback request.",
            string intrerestedIn = "Big project.",
            bool inWork = true)
        {
            return new CallbackRequest
            {
                Id = Guid.NewGuid(),
                IsDeleted = isDeleted,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AuthorEmail = authorEmail,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName,
                AuthorPhone = authorPhone,
                Budget = budget,
                Message = message,
                IntrerestedIn = intrerestedIn,
                InWork = inWork,
            };
        }

        public static List<CallbackRequest> GetCallbackRequests()
        {
            var callbackRequests = new List<CallbackRequest>()
            {
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AuthorEmail = "DeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 10000,
                    Message = "I like tests!",
                    IntrerestedIn = "Work",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AuthorEmail = "DeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 20000,
                    Message = "I like tests!",
                    IntrerestedIn = "Work",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 5000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AuthorEmail = "NotDeletedEmail@email.com",
                    AuthorFirstName = "Test",
                    AuthorLastName = "Test",
                    AuthorPhone = "Test",
                    Budget = 20000,
                    Message = "I like tests!",
                    IntrerestedIn = "Big project",
                    InWork = true,
                }
            };

            return callbackRequests;
        }

        public static NewCallbackRequestDTO GetNewCallbackRequestDTO(
            string authorEmail = "AuthorEmail@email.com",
            string authorFirstName = "Test",
            string authorLastName = "Test",
            string authorPhone = "+380123456789",
            int budget = 5000,
            string intrerestedIn = "Test",
            string message = "Test")
        {
            return new NewCallbackRequestDTO()
            {
                AuthorEmail = authorEmail,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName,
                AuthorPhone = authorPhone,
                Budget = budget,
                IntrerestedIn = intrerestedIn,
                Message = message
            };
        }

    }
}
