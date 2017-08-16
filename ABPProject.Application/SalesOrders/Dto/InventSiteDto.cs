using Abp.AutoMapper;
using ABPProject.InventSites;
using System.Collections.Generic;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(InventSite))]
    public class InventSiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InventLocationDto> InventLocations { get; set; }
    }
}
