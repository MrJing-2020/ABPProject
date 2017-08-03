using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.CommonDto
{
    /// <summary>
    /// 参数为Id（应对post接口无法接受 非对象 单参数的问题）
    /// </summary>
    public class OneParam
    {
        public int Id { get; set; }
    }
}
