using Abp.AutoMapper;
using System;

namespace ABPProject.ClientPayments.Dto
{
    [AutoMap(typeof(ClientPayment))]
    public class ClientPaymentDto
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public string SalesOrderNum { get; set; }
        public string ClientName { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>
        public string PaymentBank { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string BankTransactionNum { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// 收款银行
        /// </summary>
        public string ReceiptBank { get; set; }
        /// <summary>
        /// 收款账号
        /// </summary>
        public string ReceiptAccountId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserName { get; set; }
    }
}
