using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        private readonly MikartContext _context;

        public ProductService(MikartContext context) : base()
        {
            _context = context;
        }

        public async Task<ResultModel<PaginationResponseDTO<ProductDTO>>> GetAllProductsAsync(PaginationRequestDTO request)
        {
            var products = _context.Products
                .Include(p => p.Keywords)
                .Include(p => p.RelatedProducts)
                .Include(p => p.TechnicalData)
                .ThenInclude(td => td.TechnicalFeature)
                .Include(p => p.TechnicalData)
                .ThenInclude(t => t.TechnicalValues);

            var paginationResponseDTO = new PaginationResponseDTO<ProductDTO>()
            {
                Items = products.Skip(base.GetSkipAmount(request)).Take(request.PageSize).Select(p => p.ToProductDTO()),
                TotalItemsNumber = products.Count()
            };

            var resultModel = new ResultModel<PaginationResponseDTO<ProductDTO>>(paginationResponseDTO);
            return resultModel;
        }

        public async Task<ResultModel<ProductDTO>> GetProductBySupplierPidAsync(string supplierPID)
        {
            var supplierPIDinUppercase = supplierPID.ToUpper();
            var product = await _context.Products
                .Include(p => p.Keywords)
                .Include(p => p.RelatedProducts)
                .Include(p => p.TechnicalData)
                .ThenInclude(td => td.TechnicalFeature)
                .Include(p => p.TechnicalData)
                .ThenInclude(td => td.TechnicalValues)
                .FirstOrDefaultAsync(p => p.SupplierPID == supplierPIDinUppercase);

            if (product is not null)
            {
                var productDTO = product.ToProductDTO();
                var resultModel = new ResultModel<ProductDTO>(productDTO);
                return resultModel;
            }

            var productErrorDTO = new ProductDTO() { SupplierPID = supplierPIDinUppercase };
            var result = new ResultModel<ProductDTO>(productErrorDTO);
            result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Product was not found by ID.");
            return result;
        }

        public async Task<ResultModel<PaginationResponseDTO<ProductMinimalDTO>>> GetAllProductsMinamalAsync(PaginationRequestDTO request)
        {
            var products = await _context.Products.ToListAsync();
            var paginationResponseDTO = new PaginationResponseDTO<ProductMinimalDTO>()
            {
                Items = products.Skip(base.GetSkipAmount(request)).Take(request.PageSize).Select(p => p.ToProductMinimalDTO()),
                TotalItemsNumber = products.Count()
            };
            var resultModel = new ResultModel<PaginationResponseDTO<ProductMinimalDTO>>(paginationResponseDTO);
            return resultModel;
        }

        public async Task<ResultModel<ProductMinimalDTO>> GetProductMinamalBySupplierPidAsync(string supplierPID)
        {
            var supplierPIDinUppercase = supplierPID.ToUpper();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.SupplierPID == supplierPIDinUppercase);

            if (product is not null)
            {
                var productDTO = product.ToProductMinimalDTO();
                var resultModel = new ResultModel<ProductMinimalDTO>(productDTO);
                return resultModel;
            }

            var productErrorDTO = new ProductMinimalDTO() { SupplierPID = supplierPIDinUppercase };
            var result = new ResultModel<ProductMinimalDTO>(productErrorDTO);
            result.AddErrorToDTO(ResponseError.NotFound.ToString(), "Product was not found by ID.");
            return result;
        }

        public async Task<ResultModel<string[]>> GetOrderNumbersByFirstCharsAsync(string startOfOrderNumber)
        {
            if (string.IsNullOrWhiteSpace(startOfOrderNumber))
            {
                var badResult = new ResultModel<string[]>(new string[0]);
                badResult.AddErrorToDTO(ResponseError.StringIsNullOrEmptyOrWhiteSpace.ToString(), 
                    "Start chars of order number can't be null, empty or white space.");
                return badResult;
            }

            var startOfOrderNumberInUpperCose = startOfOrderNumber.ToUpper();
            var matchedOrderNumbers = await _context.Products
                .Where(product => product.SupplierPID.StartsWith(startOfOrderNumberInUpperCose))
                .Select(product => product.SupplierPID)
                .ToArrayAsync();

            var result = new ResultModel<string[]>(matchedOrderNumbers);
            return result;
        }


    }
}
