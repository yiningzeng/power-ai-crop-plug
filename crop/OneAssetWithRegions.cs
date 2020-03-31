using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crop
{
    /// <summary>
    /// 为什么这个类不合并到OneAsset里面去，因为合并进去会造成项目文件最后导出一致残留空的json字段
    /// </summary>
    class OneAssetWithRegions
    {
        public OneAsset asset { get; set; }
        public List<Region> regions { get; set; }

        public string version { get; set; }
    }

    class Region
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<string> tags { get; set; }
        public BoundingBox boundingBox { get; set; }
        public List<AssetPoint> points { get; set; }
    }
    class BoundingBox
    {
        public float left { get; set; }
        public float top { get; set; }
        public float height { get; set; }
        public float width { get; set; }
    }
    class AssetPoint
    {
        public float x { get; set; }
        public float y { get; set; }
    }

}
