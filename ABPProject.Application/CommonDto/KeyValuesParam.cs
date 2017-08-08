using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.CommonDto
{
    public class KeyValuesParam<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue[] Values { get; set; }

    }
}
