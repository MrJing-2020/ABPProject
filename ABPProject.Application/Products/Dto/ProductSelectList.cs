using Abp.AutoMapper;
using System.Collections.Generic;

namespace ABPProject.Products.Dto
{
    public class ProductSelectList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InventBatchDto> InventBatchs{get;set;}
    }
}
