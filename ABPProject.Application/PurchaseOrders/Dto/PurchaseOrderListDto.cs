﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders.Dto
{
    [AutoMapFrom(typeof(PurchaseOrder))]
    public class PurchaseOrderListDto: EntityDto<int>
    {
        public virtual string PurchId { get; set; }
        public virtual string PurchName { get; set; }
        public virtual string InventSite { get; set; }
        public virtual string InventLocation { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual string PurchSecontractNum { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public List<PurchaseOrderItemListDto> PurchaseOrderItems { get; set; }
    }
}