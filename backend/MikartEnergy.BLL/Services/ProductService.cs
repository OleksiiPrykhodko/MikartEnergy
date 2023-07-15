using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Pagination;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.Common.Enums;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace MikartEnergy.BLL.Services
{
    public class ProductService : BaseService
    {
        private readonly IEtimProductsFileReader _etimProductsFileReader;
        private List<Product> _productsList;

        public ProductService(IEtimProductsFileReader etimProductsFileReader): base()
        {
            _etimProductsFileReader = etimProductsFileReader;
            _productsList = _etimProductsFileReader.GetProducts().ToList();
        }

        public async Task<ResultModel<PaginationResponseDTO<ProductDTO>>> GetAllProductsAsync(PaginationRequestDTO request)
        {
            return await Task.Run(() =>
            new ResultModel<PaginationResponseDTO<ProductDTO>>(new PaginationResponseDTO<ProductDTO>()
            {
                Items = _productsList.Skip(base.GetSkipAmount(request)).Take(request.PageSize).Select(p => p.ToProductDTO()),
                TotalItemsNumber = _productsList.Count()
            }));
        }

        public async Task<ResultModel<ProductDTO>> GetProductByIdAsync(string id)
        {
            var idUpper = id.ToUpper();
            var product = _productsList.Find(p => p.Id == idUpper);

            if (product is not null)
            {
                return await Task.Run(() => new ResultModel<ProductDTO>(product.ToProductDTO()));
            }

            return await Task.Run(() =>
            {
                var productDTO = new ProductDTO() { Id = idUpper };
                var result = new ResultModel<ProductDTO>(productDTO);
                result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Product was not found by ID.");
                return result;
            });
        }

        public async Task<ResultModel<PaginationResponseDTO<ProductMinimalDTO>>> GetAllProductsMinamalAsync(PaginationRequestDTO request)
        {
            return await Task.Run(() => 
            new ResultModel<PaginationResponseDTO<ProductMinimalDTO>>(new PaginationResponseDTO<ProductMinimalDTO>()
            {
                Items = _productsList.Skip(base.GetSkipAmount(request)).Take(request.PageSize).Select(p => p.ToProductMinimalDTO()),
                TotalItemsNumber = _productsList.Count()
            }));
        }

        public async Task<ResultModel<ProductMinimalDTO>> GetProductMinamalByIdAsync(string id)
        {
            var idUpper = id.ToUpper();
            var product = _productsList.Find(p => p.Id == idUpper);

            if (product is not null)
            {
                return await Task.Run(() => new ResultModel<ProductMinimalDTO>(product.ToProductMinimalDTO()));
            }

            return await Task.Run(() =>
            {
                var productDTO = new ProductMinimalDTO() { Id = idUpper };
                var result = new ResultModel<ProductMinimalDTO>(productDTO);
                result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Product was not found by ID.");
                return result;
            });
        }



    }
}
