using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;

namespace ABPProject.ClientPayments
{
    public class ClientPayment: Entity, IMayHaveTenant, IMayHaveOrganizationUnit, IHasCreationTime, ICreationAudited, ISoftDelete
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual int SalesOrderId { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public virtual string PaymentBank { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public virtual string BankTransactionNum { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public virtual decimal PaymentSum { get; set; }
        /// <summary>
        /// 收款银行
        /// </summary>
        public virtual string ReceiptBank { get; set; }
        /// <summary>
        /// 收款账号
        /// </summary>
        public virtual string ReceiptAccountId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int State { get; set; }
    }
}
