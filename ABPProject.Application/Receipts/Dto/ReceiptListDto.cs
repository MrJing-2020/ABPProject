using Abp.AutoMapper;
using System;

namespace ABPProject.Receipts.Dto
{
    [AutoMapFrom(typeof(Receipt))]
    public class ReceiptListDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string JournalName { get; set; }
        public string BankName { get; set; }
        public decimal JournalBalance { get; set; }
        public int State { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
