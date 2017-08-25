using Abp.AutoMapper;

namespace ABPProject.Suppliers.Dto
{
    [AutoMap(typeof(Supplier))]
    public class SupplierListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 供应商名
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
    }
}
