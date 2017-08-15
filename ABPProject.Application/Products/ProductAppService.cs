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
using Abp.UI;
using ABPProject.Projects;

namespace ABPProject.Products
{
    [AbpAuthorize(PermissionNames.Product)]
    public class ProductAppService: ABPProjectAppServiceBase, IProductAppService
    {
        private readonly ProductManager _productManager;
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<UnitOfMeasureTranslation, int> _unitRepository;
        public ProductAppService(ProductManager productManager, IRepository<Product, int> productRepository, IRepository<UnitOfMeasureTranslation, int> unitRepository)
        {
            _productManager = productManager;
            _productRepository = productRepository;
            _unitRepository = unitRepository;
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
            var unitList = await _unitRepository.GetAllListAsync();
            return product.MapTo<EditProductInput>();
        }

        public async Task<string[]> GetUnitList()
        {
            var unitList = await _unitRepository.GetAllListAsync();
            return unitList.Select(m => m.Description).ToArray();
        }

        /// <summary>
        /// 判断产品名是否重复
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool NameIsExist(string name)
        {
            bool isExist = false;
            var count = _productRepository.GetAllList(m => m.Name == name).Count();
            if (count > 0)
            {
                isExist = true;
            }
            return isExist;
        }

        public async Task EditProduct(EditProductInput input)
        {
            var id = input.Id;
            if (id == null)
            {
                var count = _productRepository.GetAllList(m => m.Name == input.Name).Count();
                if (count > 0)
                {
                    throw new UserFriendlyException(string.Format("产品名 {0} 已经存在", input.Name));
                }
                var product = input.MapTo<Product>();
                await _productRepository.InsertAsync(product);
            }
            else
            {
                var count = _productRepository.GetAllList(m => m.Name == input.Name && m.Id != id).Count();
                if (count > 0)
                {
                    throw new UserFriendlyException(string.Format("产品名 {0} 已经存在", input.Name));
                }
                var product = await _productRepository.GetAsync((int)id);
                //oldProduct.Name = input.Name;
                //oldProduct.Description = input.Description;
                product = input.MapTo<EditProductInput, Product>(product);
                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task CreateProduct(EditProductInput input)
        {
            var count = _productRepository.GetAllList(m => m.Name == input.Name).Count();
            if (count > 0)
            {
                throw new UserFriendlyException(string.Format("产品名 {0} 已经存在", input.Name));
            }
            var product = input.MapTo<Product>();
            await _productRepository.InsertAsync(product);
        }

        public async Task DeleteProduct(ArrayParams param)
        {
            await _productRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
