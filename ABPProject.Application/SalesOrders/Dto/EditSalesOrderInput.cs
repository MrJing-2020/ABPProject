using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMap(typeof(SalesOrder))]
    public class EditSalesOrderInput
    {
    }
}
