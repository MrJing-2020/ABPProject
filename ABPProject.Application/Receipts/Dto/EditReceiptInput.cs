using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Receipts.Dto
{
    [AutoMap(typeof(Receipt))]
    public class EditReceiptInput
    {
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }

        public int? Id { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        [Required]
        public int ClientId { get; set; }
        /// <summary>
        /// 销售订单Id
        /// </summary>
        public int SalesOrderId { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        [Required]
        public string ReceiptWay { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [Required]
        public string PaymentMethod { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// 日记账名称
        /// </summary>
        [Required]
        public string JournalName { get; set; }
        /// <summary>
        /// 过账模板
        /// </summary>
        [Required]
        public string PostingProfile { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        [Required]
        public string BankName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        [Required]
        public string JournalBalanceNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Required]
        public decimal JournalBalance { get; set; }
        /// <summary>
        /// 金额大写
        /// </summary>
        [Required]
        public string LineAmountCaps { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string BankTransactionNum { get; set; }
        /// <summary>
        /// 货源
        /// </summary>
        public string SupplyOfGoods { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        [Required]
        public string ContractNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 货币
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 收款单摘要
        /// </summary>
        public string ReceiptAbstract { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
