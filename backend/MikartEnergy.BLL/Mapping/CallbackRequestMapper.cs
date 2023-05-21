using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Mapping
{
    public static class CallbackRequestMapper
    {
        public static CallbackRequest ToCallbackRequest(this NewCallbackRequestDTO dto)
        {
            return new CallbackRequest{
                AuthorFirstName = dto.AuthorFirstName,
                AuthorLastName = dto.AuthorLastName,
                AuthorEmail = dto.AuthorEmail,
                AuthorPhone = dto.AuthorPhone,
                IntrerestedIn = dto.IntrerestedIn,
                Message = dto.Message,
                Budget = dto.Budget,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static CallbackRequestDTO ToCallbackRequestDTO(this CallbackRequest entity)
        {
            return new CallbackRequestDTO
            {
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                InWork = entity.InWork,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                AuthorFirstName = entity.AuthorFirstName,
                AuthorLastName = entity.AuthorLastName,
                AuthorEmail = entity.AuthorEmail,
                AuthorPhone = entity.AuthorPhone,
                IntrerestedIn = entity.IntrerestedIn,
                Message = entity.Message,
                Budget = entity.Budget
            };
        }

        public static CallbackRequest ToCallbackRequest(this CallbackRequestDTO dto)
        {
            return new CallbackRequest
            {
                Id = dto.Id,
                IsDeleted = dto.IsDeleted,
                InWork = dto.InWork,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorFirstName = dto.AuthorFirstName,
                AuthorLastName = dto.AuthorLastName,
                AuthorEmail = dto.AuthorEmail,
                AuthorPhone = dto.AuthorPhone,
                IntrerestedIn = dto.IntrerestedIn,
                Message = dto.Message,
                Budget = dto.Budget
            };
        }

    }
}
