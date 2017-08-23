using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ABPProject.Authorization;
using ABPProject.Clients;
using ABPProject.CommonDto;
using ABPProject.Contracts;
using ABPProject.Receipts.Dto;
using ABPProject.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using ABPProject.SimEntitys;

namespace ABPProject.Receipts
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class ReceiptAppService: ABPProjectAppServiceBase, IReceiptAppService
    {
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        private readonly IRepository<Contract, int> _contractRepository;
        private readonly IRepository<Client, int> _clientRepository;
        private readonly IRepository<Receipt, int> _receiptRepository;
        private readonly IRepository<BankAccout, int> _bankAccoutRepository;




        public ReceiptAppService(
            IRepository<SalesOrder, int> salesOrderRepository,
            IRepository<Contract, int> contractRepository,
            IRepository<Client, int> clientRepository,
            IRepository<Receipt, int> receiptRepository,
            IRepository<BankAccout, int> bankAccoutRepository
            )
        {
            _salesOrderRepository = salesOrderRepository;
            _contractRepository = contractRepository;
            _clientRepository = clientRepository;
            _receiptRepository = receiptRepository;
            _bankAccoutRepository = bankAccoutRepository;
        }

        public PagedResultDto<ReceiptListDto> GetPagedReceipt(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Receipt> receipt = _receiptRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from receiptItem in receipt
                        join client in _clientRepository.GetAll() on receiptItem.ClientId equals client.Id
                        where (isSearch ? client.Name.Contains(input.SearchText) : true)
                        select new ReceiptListDto
                        {
                            Id = receiptItem.Id,
                            ClientName = client.Name,
                            JournalBalance = receiptItem.JournalBalance,
                            JournalName = receiptItem.JournalName,
                            State = receiptItem.State,
                            ReceiptDate = receiptItem.ReceiptDate,
                            CreationTime = receiptItem.CreationTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            return new PagedResultDto<ReceiptListDto>(count, resultList);
        }

        /// <summary>
        /// 客户，合同，销售订单，银行名和收款账号
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetSelectList()
        {
            var client = (await _clientRepository.GetAllListAsync()).Select(m => new { Id = m.Id, Name = m.Name });
            var contract = (await _contractRepository.GetAllListAsync()).Select(m => new { Id = m.Id, Name = m.Name });
            var salesOrder = (await _salesOrderRepository.GetAllListAsync()).Select(m => new { Id = m.Id, SalesNum = m.SalesNum });
            var bank = (await _bankAccoutRepository.GetAllListAsync()).Select(m => new { BankName = m.BankName, AccountId = m.AccountId });
            return new { client = client, contract = contract, salesOrder = salesOrder, bank = bank };
        }

        public async Task<EditReceiptInput> GetReceiptById(OneParam param)
        {
            var receipt = await _receiptRepository.GetAsync(param.Id);
            var receiptDto = receipt.MapTo<EditReceiptInput>();
            return receiptDto;
        }

        public async Task EditReceipt(EditReceiptInput input)
        {
            var receipt = input.MapTo<Receipt>();
            receipt.ReceiptDate = receipt.ReceiptDate.AddHours(8);
            var receiptUpdate = await _receiptRepository.InsertOrUpdateAsync(receipt);
        }

        public async Task DeleteReceipt(ArrayParams param)
        {
            await _receiptRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
