using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
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
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
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

            return callbackRequests;
        }
    }
}
