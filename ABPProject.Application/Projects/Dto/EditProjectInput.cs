using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Projects.Dto
{
    [AutoMap(typeof(Project))]
    public class EditProjectInput
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public int AXProjectId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 风险
        /// </summary>
        [Required]
        public string Risk { get; set; }
        /// <summary>
        /// 买入方式
        /// </summary>
        [Required]
        public string BuyMethod { get; set; }
        /// <summary>
        /// 赎回方式
        /// </summary>
        [Required]
        public string RedeemMethod { get; set; }
        /// <summary>
        /// 收益率
        /// </summary>
        [Required]
        [Range(typeof(decimal), "0.00", "99999999.99", ErrorMessage = "收益格式不正确")]
        public decimal Yield { get; set; }


        #region 更新数据的时候用
        public int? TenantId { get; set; }
        public long? OrganizationUnitId { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        #endregion
    }
}
