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
        private readonly IRepository<Project, int> _projectRepository;
        public ProductAppService(ProductManager productManager, 
            IRepository<Product, int> productRepository, 
            IRepository<UnitOfMeasureTranslation, int> unitRepository,
            IRepository<Project, int> projectRepository)
        {
            _productManager = productManager;
            _productRepository = productRepository;
            _unitRepository = unitRepository;
            _projectRepository = projectRepository;
        }

        public PagedResultDto<ProductListDto> GetPagedProduct(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Product> product = _productRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from productItem in product
                        join project in _projectRepository.GetAll() on productItem.ProjectId equals project.Id
                        where (isSearch ? productItem.Name.Contains(input.SearchText) || project.Name.Contains(input.SearchText) : true)
                        select new ProductListDto
                        {
                            Id = productItem.Id,
                            Name = productItem.Name,
                            ProjectName = project.Name,
                            NameAlias = productItem.NameAlias,
                            Category = productItem.Category,
                            Description = productItem.Description,
                            Stopped = productItem.Stopped,
                            SalesUnit = productItem.SalesUnit,
                            CreationTime = productItem.CreationTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            return new PagedResultDto<ProductListDto>(count, resultList);
        }

        public async Task<EditProductInput> GetProductById(OneParam param)
        {
            var product = await _productRepository.GetAsync(param.Id);
            var unitList = await _unitRepository.GetAllListAsync();
            return product.MapTo<EditProductInput>();
        }

        public async Task<object> GetSelectList()
        {
            var unitList = await _unitRepository.GetAllListAsync();
            var projectList = await _projectRepository.GetAllListAsync();
            return new { unitList = unitList.Select(m => m.Description).ToArray(), projectList = projectList.Select(m => new { Id = m.Id, Name = m.Name, AXProjectId = m.AXProjectId }) };
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

        public async Task DeleteProduct(ArrayParams param)
        {
            await _productRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }

        public async Task StopProduct(OneParam param)
        {
            var product = await _productRepository.GetAsync(param.Id);
            if (product.Stopped)
            {
                product.Stopped = false;
            }
            else
            {
                product.Stopped = true;
            }
            await _productRepository.UpdateAsync(product);
        }
    }
}
