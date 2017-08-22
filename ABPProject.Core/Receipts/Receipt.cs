using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABPProject.Receipts
{
    /// <summary>
    /// 收款单
    /// </summary>
    [Table("Receipt")]
    public class Receipt: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete, IModificationAudited
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual int ClientId { get; set; }
        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual int SalesOrderId { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        public virtual string ReceiptWay { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual string PaymentMethod { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public virtual DateTime ReceiptDate { get; set; }
        /// <summary>
        /// 日记账名称
        /// </summary>
        public virtual string JournalName{ get; set; }
        /// <summary>
        /// 过账模板
        /// </summary>
        public virtual string PostingProfile { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public virtual string BankName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public virtual string JournalBalanceNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal JournalBalance{ get; set; }
        /// <summary>
        /// 金额大写
        /// </summary>
        public virtual string LineAmountCaps { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public virtual string BankTransactionNum { get; set; }
        /// <summary>
        /// 货源
        /// </summary>
        public virtual string SupplyOfGoods { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string ContractNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 货币
        /// </summary>
        public virtual string Currency { get; set; }
        /// <summary>
        /// 收款单摘要
        /// </summary>
        public virtual string ReceiptAbstract { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int State { get; set; }
    }
}
