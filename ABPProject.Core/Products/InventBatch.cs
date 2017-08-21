using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Products
{
    [Table("InventBatch")]
    public class InventBatch: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }

        public virtual int ProductId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string InventBatchNum { get; set; }
    }
}
