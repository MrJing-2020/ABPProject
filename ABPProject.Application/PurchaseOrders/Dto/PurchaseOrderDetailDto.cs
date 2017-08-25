using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders.Dto
{
    public class PurchaseOrderDetailDto
    {
        public int Id { get; set;}
        public string PurchNum { get; set; }
        public string ContractNum { get; set; }
        public string SupplierName { get; set; }
        public string InventSiteName { get; set; }
        public string InventLocationName { get; set; }
        public Decimal TotalPrices { get; set; }
        public int State { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
        public List<PurchaseOrderItemDetailDto> PurchaseOrderItems { get; set; }
    }
}
