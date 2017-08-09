﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders.Dto
{
    [AutoMapFrom(typeof(SalesOrder))]
    public class SalesOrderListDto: EntityDto<int>
    {
    }
}
