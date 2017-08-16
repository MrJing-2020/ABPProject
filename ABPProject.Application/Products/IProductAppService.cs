using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Products
{
    public interface IProductAppService: IApplicationService
    {
        PagedResultDto<ProductListDto> GetPagedProduct(PageParams pageArg);
        Task<EditProductInput> GetProductById(OneParam param);
        Task EditProduct(EditProductInput input);
        Task DeleteProduct(ArrayParams param);
        Task<string[]> GetUnitList();
        Task StopProduct(OneParam param);
    }
}
