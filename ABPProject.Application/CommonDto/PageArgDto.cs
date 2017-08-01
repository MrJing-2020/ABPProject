using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.CommonDto
{
    /// <summary>
    /// 分页参数实体
    /// </summary>
    public class PageArgDto
    {
        public PagedInputDto PageInput { get; set; }
        public string SearchText { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }

        public PageArgDto(PageParams param)
        {
            PagedInputDto tempData = new PagedInputDto();
            tempData.MaxResultCount = param.PageSize;
            tempData.SkipCount = param.PageNumber > 0 ? (param.PageNumber - 1) * param.PageSize : 0;
            PageInput = tempData;
            SearchText = param.SearchText;
            SortName = param.SortName;
            SortOrder = param.SortOrder;
        }
    }
}
