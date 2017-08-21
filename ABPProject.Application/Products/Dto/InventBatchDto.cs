using Abp.AutoMapper;
using ABPProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Products.Dto
{
    [AutoMap(typeof(InventBatch))]
    public class InventBatchDto
    {
        public int Id { get; set; }
        public string InventBatchNum { get; set; }

    }
}
