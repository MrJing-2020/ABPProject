using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace ABPProject.Products.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductListDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
