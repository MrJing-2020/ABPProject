using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Products.Dto
{
    [AutoMap(typeof(Product))]
    public class EditProductInput
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Required]
        public string InventoryUnit { get; set; }
        [Required]
        public string PurchaseUnit { get; set; }
        [Required]
        public string SalesUnit { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [Required]
        public string NameAlias { get; set; }
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
