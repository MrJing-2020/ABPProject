using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ABPProject.Clients.Dto;
using ABPProject.CommonDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace ABPProject.Clients
{
    public class ClientAppService: ABPProjectAppServiceBase, IClientAppService
    {
        private readonly IRepository<Client, int> _clientRepository;

        public ClientAppService(
            IRepository<Client, int> clientRepository
            )
        {
            _clientRepository = clientRepository;
        }

        public PagedResultDto<ClientListDto> GetPagedClient(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Client> client = _clientRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from clientItem in client
                        where (isSearch ? clientItem.Name.Contains(input.SearchText)|| clientItem.NameAlias.Contains(input.SearchText) : true)
                        select clientItem).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList().MapTo<List<ClientListDto>>();
            return new PagedResultDto<ClientListDto>(count, resultList);
        }


        public async Task<EditClientInput> GetClientById(OneParam param)
        {
            var client = await _clientRepository.GetAsync(param.Id);
            var clientDto = client.MapTo<EditClientInput>();
            return clientDto;
        }

        public async Task EditClient(EditClientInput input)
        {
            var client = input.MapTo<Client>();
            var clientUpdate = await _clientRepository.InsertOrUpdateAsync(client);
        }

        public async Task DeleteClient(ArrayParams param)
        {
            await _clientRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
