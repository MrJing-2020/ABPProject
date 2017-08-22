using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Products
{
    [Table("UnitOfMeasureTranslation")]
    public class UnitOfMeasureTranslation: Entity
    {
        public virtual string Description { get; set; }
    }
}
