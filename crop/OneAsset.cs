using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crop
{
    class OneAsset
    {
        public string format { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public AssetsSize size { get; set; }
        public int state { get; set; } // 0: 未看过；1：查看过未标记就是好图；2：标记
        public int type { get; set; }
    }
    class AssetsSize
    {
        public int width { get; set; }
        public int height { get; set; }
    }

}
