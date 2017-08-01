using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABPProject.CommonDto
{
    public class PageParams
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchText { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
    }
}