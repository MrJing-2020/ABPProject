using Abp.AutoMapper;

namespace ABPProject.Clients.Dto
{
    [AutoMap(typeof(Client))]
    public class ClientListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string NameAlias { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyNature { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public decimal RegisteredCapital { get; set; }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string AccountBank { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 资产总额
        /// </summary>
        public decimal TotalAssets { get; set; }
        /// <summary>
        /// 净资产
        /// </summary>
        public decimal NetAsset { get; set; }
        /// <summary>
        /// 公司年收入
        /// </summary>
        public decimal AnnualIncome { get; set; }
        /// <summary>
        /// 净利润
        /// </summary>
        public decimal NetProfit { get; set; }
    }
}
