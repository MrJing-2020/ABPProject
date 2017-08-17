using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.PurchaseOrders
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete, IModificationAudited
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 采购订单编号
        /// </summary>
        public virtual string PurchNum { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public virtual int SupplierId { get; set; }
        //public virtual string PurchName { get; set; }
        public virtual int InventSiteId { get; set; }
        public virtual int InventLocationId { get; set; }
        public virtual DateTime DeliveryDate { get; set; }

        /// <summary>
        /// 采购合同编号
        /// </summary>
        public virtual int ContractId { get; set; }
        public virtual int State { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItem { get; set; }
    }
}
