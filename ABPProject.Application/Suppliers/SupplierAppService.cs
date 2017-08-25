using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ABPProject.CommonDto;
using ABPProject.Suppliers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace ABPProject.Suppliers
{
    public class SupplierAppService: ABPProjectAppServiceBase, ISupplierAppService
    {
        private readonly IRepository<Supplier, int> _supplierRepository;

        public SupplierAppService(
            IRepository<Supplier, int> supplierRepository
            )
        {
            _supplierRepository = supplierRepository;
        }

        public PagedResultDto<SupplierListDto> GetPagedSupplier(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Supplier> supplier = _supplierRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from supplierItem in supplier
                        where (isSearch ? supplierItem.Name.Contains(input.SearchText)|| supplierItem.NameAlias.Contains(input.SearchText) : true)
                        select supplierItem).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList().MapTo<List<SupplierListDto>>();
            return new PagedResultDto<SupplierListDto>(count, resultList);
        }


        public async Task<EditSupplierInput> GetSupplierById(OneParam param)
        {
            var supplier = await _supplierRepository.GetAsync(param.Id);
            var supplierDto = supplier.MapTo<EditSupplierInput>();
            return supplierDto;
        }

        public async Task EditSupplier(EditSupplierInput input)
        {
            var supplier = input.MapTo<Supplier>();
            var supplierUpdate = await _supplierRepository.InsertOrUpdateAsync(supplier);
        }

        public async Task DeleteSupplier(ArrayParams param)
        {
            await _supplierRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
