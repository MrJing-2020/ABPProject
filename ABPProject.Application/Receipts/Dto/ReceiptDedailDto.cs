using System;

namespace ABPProject.Receipts.Dto
{
    public class ReceiptDedailDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string SalesOrderNum { get; set; }
        public string ReceiptWay { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string JournalName { get; set; }
        public string PostingProfile { get; set; }
        public string BankName { get; set; }
        public string JournalBalanceNum { get; set; }
        public decimal JournalBalance { get; set; }
        public string LineAmountCaps { get; set; }
        public string BankTransactionNum { get; set; }
        public string SupplyOfGoods { get; set; }
        public string ContractNum { get; set; }
        public string Remark { get; set; }
        public string Currency { get; set; }
        public string ReceiptAbstract { get; set; }
        public string CreatorUserName { get; set; }
        public int State { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
