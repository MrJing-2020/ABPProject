using Abp.Domain.Repositories;
using ABPProject.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using ABPProject.Extend;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABPProject.Products.Dto;
using Abp.Authorization;
using ABPProject.Authorization;

namespace ABPProject.Products
{
    [AbpAuthorize(PermissionNames.Product)]
    public class ProductAppService: ABPProjectAppServiceBase, IProductAppService
    {
        private readonly ProductManager _productManager;
        private readonly IRepository<Product, int> _productRepository;
        public ProductAppService(ProductManager productManager, IRepository<Product, int> productRepository)
        {
            _productManager = productManager;
            _productRepository = productRepository;
        }

        public PagedResultDto<ProductListDto> GetPagedProduct(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<Product> product = null;
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                product = _productRepository.GetAll().Where(m => m.Name.Contains(input.SearchText) || m.Description.Contains(input.SearchText));
                count = product.Count();
            }
            else
            {
                product = _productRepository.GetAll();
                count = _productRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                product = input.SortOrder == "desc" ? product.OrderBy(input.SortName, true).PageBy(input.PageInput) :
                product.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                product = product.OrderByDescending(m => m.CreationTime).PageBy(input.PageInput);
            }
            return new PagedResultDto<ProductListDto>(count, product.MapTo<List<ProductListDto>>());
        }

        public async Task<EditProductInput> GetProductById(OneParam param)
        {
            var product = await _productRepository.GetAsync(param.Id);
            return product.MapTo<EditProductInput>();
        }

        public async Task EditProduct(EditProductInput input)
        {
            var id = input.Id;
            if (id == null)
            {
                var product = input.MapTo<Product>();
                await _productRepository.InsertAsync(product);
            }
            else
            {
                var oldProduct = await _productRepository.GetAsync((int)id);
                oldProduct.Name = input.Name;
                oldProduct.Description = input.Description;
                await _productRepository.UpdateAsync(oldProduct);
            }
        }

        public async Task DeleteProduct(ArrayParams param)
        {
            await _productRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
