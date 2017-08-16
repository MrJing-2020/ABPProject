using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.InventSites
{
    [Table("InventLocation")]
    public class InventLocation: Entity, ISoftDelete
    {
        public virtual int InventSiteId { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
