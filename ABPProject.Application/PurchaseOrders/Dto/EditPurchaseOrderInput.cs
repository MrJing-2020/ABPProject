using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders.Dto
{
    [AutoMap(typeof(PurchaseOrder))]
    public class EditPurchaseOrderInput
    {
        public int? Id { get; set; }
        public string PurchNum { get; set; }
        //public virtual string PurchName { get; set; }
        public int SupplierId { get; set; }
        public string InventSiteId { get; set; }
        public string InventLocationId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ContractId { get; set; }
        public List<EditPurchaseOrderItemDto> PurchaseOrderItems { get; set; }

        #region 方便修改操作提交数据(不用显示出来)
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; } 
        #endregion
    }
}
