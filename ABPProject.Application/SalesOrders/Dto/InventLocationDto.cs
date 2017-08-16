using Abp.AutoMapper;
using ABPProject.InventSites;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(InventLocation))]
    public class InventLocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
