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
        private List<Product> _productsList;
        private readonly MikartContext _context;

        public ProductService(IEtimProductsFileReader etimProductsFileReader, MikartContext context): base()
        {
            _productsList = etimProductsFileReader.GetProducts().ToList();

            _context = context;
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
            var product = _productsList.Find(p => p.SupplierPID == idUpper);

            if (product is not null)
            {
                var dto = product.ToProductDTO();
                dto.RelatedProducts = _productsList
                    .IntersectBy(product.RelatedProductIDs, p => p.SupplierPID)
                    .Select(p => p.ToProductMinimalDTO());
                return await Task.Run(() => new ResultModel<ProductDTO>(dto));
            }

            return await Task.Run(() =>
            {
                var productDTO = new ProductDTO() { SupplierPID = idUpper };
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
            var product = _productsList.Find(p => p.SupplierPID == idUpper);

            if (product is not null)
            {
                return await Task.Run(() => new ResultModel<ProductMinimalDTO>(product.ToProductMinimalDTO()));
            }

            return await Task.Run(() =>
            {
                var productDTO = new ProductMinimalDTO() { SupplierPID = idUpper };
                var result = new ResultModel<ProductMinimalDTO>(productDTO);
                result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Product was not found by ID.");
                return result;
            });
        }



    }
}
