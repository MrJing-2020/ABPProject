using Abp.AutoMapper;
using System;

namespace ABPProject.Clients.Dto
{
    [AutoMap(typeof(Client))]
    public class EditClientInput
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }


        public int? Id { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public virtual string NameAlias { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public virtual string CompanyNature { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Linkman { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public virtual decimal RegisteredCapital { get; set; }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public virtual string Introduce { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public virtual string AccountBank { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public virtual string AccountId { get; set; }
        /// <summary>
        /// 资产总额
        /// </summary>
        public virtual decimal TotalAssets { get; set; }
        /// <summary>
        /// 净资产
        /// </summary>
        public virtual decimal NetAsset { get; set; }
        /// <summary>
        /// 公司年收入
        /// </summary>
        public virtual decimal AnnualIncome { get; set; }
        /// <summary>
        /// 净利润
        /// </summary>
        public virtual decimal NetProfit { get; set; }
    }
}
