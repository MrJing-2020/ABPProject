using Abp.Authorization;
using Abp.Domain.Repositories;
using ABPProject.Authorization;
using ABPProject.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using ABPProject.Users;
using ABPProject.ClientPayments.Dto;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Clients;
using Abp.AutoMapper;
using System.Globalization;

namespace ABPProject.ClientPayments
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class ClientPaymentAppService: ABPProjectAppServiceBase, IClientPaymentAppService
    {
        private readonly IRepository<ClientPayment, int> _clientPaymentRepository;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Client, int> _clientRepository;

        public ClientPaymentAppService(
            IRepository<ClientPayment, int> clientPaymentRepository,
            IRepository<SalesOrder, int> salesOrderRepository,
            IRepository<User, long> userRepository,
            IRepository<Client, int> clientRepository
            )
        {
            _clientPaymentRepository = clientPaymentRepository;
            _salesOrderRepository = salesOrderRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public PagedResultDto<ClientPaymentDto> GetPagedClientPayment(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<ClientPayment> clientPayment = _clientPaymentRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from clientPaymentItem in clientPayment
                        join salesOrder in _salesOrderRepository.GetAll() on clientPaymentItem.SalesOrderId equals salesOrder.Id
                        join client in _clientRepository.GetAll() on salesOrder.ClientId equals client.Id
                        join userInfo in _userRepository.GetAll() on clientPaymentItem.CreatorUserId equals userInfo.Id
                        where (isSearch ? salesOrder.SalesNum.Contains(input.SearchText) || client.Name.Contains(input.SearchText) : true)
                        select new ClientPaymentDto
                        {
                            Id = clientPaymentItem.Id,
                            SalesOrderId = clientPaymentItem.SalesOrderId,
                            SalesOrderNum = salesOrder.SalesNum,
                            ClientName= client.Name,
                            PaymentBank = clientPaymentItem.PaymentBank,
                            BankTransactionNum = clientPaymentItem.BankTransactionNum,
                            PaymentSum = clientPaymentItem.PaymentSum,
                            ReceiptBank = clientPaymentItem.ReceiptBank,
                            ReceiptAccountId = clientPaymentItem.ReceiptAccountId,
                            State = clientPaymentItem.State,
                            CreatorUserName = userInfo.UserName,
                            CreationTime = clientPaymentItem.CreationTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            return new PagedResultDto<ClientPaymentDto>(count, resultList);
        }

        public ClientPaymentDto ClientPaymentDetail(OneParam param)
        {
            var clientPaymentDetail = (from clientPaymentItem in _clientPaymentRepository.GetAll()
                        join salesOrder in _salesOrderRepository.GetAll() on clientPaymentItem.SalesOrderId equals salesOrder.Id
                        join client in _clientRepository.GetAll() on salesOrder.ClientId equals client.Id
                        join userInfo in _userRepository.GetAll() on clientPaymentItem.CreatorUserId equals userInfo.Id
                        where clientPaymentItem.Id==param.Id
                        select new ClientPaymentDto
                        {
                            Id = clientPaymentItem.Id,
                            SalesOrderId = clientPaymentItem.SalesOrderId,
                            SalesOrderNum = salesOrder.SalesNum,
                            ClientName = client.Name,
                            PaymentBank = clientPaymentItem.PaymentBank,
                            BankTransactionNum = clientPaymentItem.BankTransactionNum,
                            PaymentSum = clientPaymentItem.PaymentSum,
                            ReceiptBank = clientPaymentItem.ReceiptBank,
                            ReceiptAccountId = clientPaymentItem.ReceiptAccountId,
                            State = clientPaymentItem.State,
                            CreatorUserName = userInfo.UserName,
                            CreationTime = clientPaymentItem.CreationTime
                        }).FirstOrDefault();
            return clientPaymentDetail;
        }
    }
}
