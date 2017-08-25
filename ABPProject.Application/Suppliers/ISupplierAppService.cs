using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Suppliers.Dto;
using System.Threading.Tasks;

namespace ABPProject.Suppliers
{
    public interface ISupplierAppService : IApplicationService
    {
        PagedResultDto<SupplierListDto> GetPagedSupplier(PageParams pageArg);
        Task<EditSupplierInput> GetSupplierById(OneParam param);
        Task EditSupplier(EditSupplierInput input);
        Task DeleteSupplier(ArrayParams param);
    }
}
