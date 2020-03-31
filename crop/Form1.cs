using Emgu.CV;
using Emgu.CV.Structure;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace crop
{
    public partial class Form1 : Form
    {
        private string basePath;
        private string savePath;
        private string json;
        private int shift;
        private int subImageNum;
        private string jsonVersion;

        class Fuck
        {
            public RTree.Rectangle rectangle;
            public Point point;
        }
        List<Fuck> cropRRectangles = new List<Fuck>();
        public Form1()
        {
            InitializeComponent();
            //string aa =Path.GetFileNameWithoutExtension("file:${path}4.jpg");
            //aa = "";
        }

        /// <summary>
        /// 图片等分函数
        /// </summary>
        /// <param name="imgPath">图片路径</param>
        /// <param name="subWidth">等分的宽</param>
        /// <param name="subHeight">等分的高</param>
        /// <param name="subImageNum">宽高几等分</param>
        public bool crop(string imgPath, int subWidth, int subHeight, int subImageNum)
        {

            int num = 1;
            try
            {
                byte[] photoBytes = File.ReadAllBytes(imgPath);
                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        Point startPoint = new Point(0, 0);
                        Size partSize = new Size(subWidth, subHeight);

                        for (int i = 0; i < subImageNum; i++) // 我是横行
                        {
                            for (int j = 0; j < subImageNum; j++) //我是纵向
                            {
                                using (MemoryStream outStream = new MemoryStream())
                                {
                                    if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
                                    if (!cbCrop.Checked)
                                    {
                                        imageFactory.Load(inStream)
                                                .Crop(new Rectangle(startPoint, partSize))
                                                .Save(outStream);
                                        Image.FromStream(outStream).Save(Path.Combine(savePath, Path.GetFileNameWithoutExtension(imgPath) + "-" + num + Path.GetExtension(imgPath)));
                                    }
                                    Fuck ff = new Fuck();
                                    ff.rectangle = new RTree.Rectangle(startPoint.X, startPoint.Y, startPoint.X + partSize.Width, startPoint.Y + partSize.Height, 0, 0);
                                    ff.point = new Point(startPoint.X, startPoint.Y);
                                    cropRRectangles.Add(ff);
                                }
                                num++;
                                startPoint = new Point(startPoint.X + subWidth, startPoint.Y);
                            }
                            startPoint = new Point(0, startPoint.Y + subHeight);
                        }
                    }
                    photoBytes = null;
                    // Do something with the stream.
                }
            }
            catch (Exception er)
            {

            }

            if (num >= subImageNum * subImageNum) return true;
            return false;
        }
        /// <summary>
        /// 解决删除目录提示：System.IO.IOException: 目录不是空的。
        /// 删除一个目录，先遍历删除其下所有文件和目录（递归）
        /// </summary>
        /// <param name="strPath">绝对路径</param>
        /// <returns>是否已经删除</returns>
        public bool DeleteADirectory(string strPath)
        {
            string[] strTemp;
            try
            {
                //先删除该目录下的文件
                strTemp = System.IO.Directory.GetFiles(strPath);
                foreach (string str in strTemp)
                {
                    System.IO.File.Delete(str);
                }
                //删除子目录，递归
                strTemp = System.IO.Directory.GetDirectories(strPath);
                foreach (string str in strTemp)
                {
                    DeleteADirectory(str);
                }
                //删除该目录
                System.IO.Directory.Delete(strPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            savePath = tbSavePath.Text.Trim();
            jsonVersion = tbJsonVersion.Text.Trim();
            shift = Int32.Parse(tbShift.Text.Trim());
            subImageNum = Int32.Parse(this.tbSubImageNum.Text.Trim());
            basePath = System.IO.Path.GetDirectoryName(tbPath.Text);
            if (Directory.Exists(savePath)) DeleteADirectory(savePath);
            System.IO.Directory.CreateDirectory(savePath);

            //读取文件内容
            StreamReader sr = new StreamReader(tbPath.Text, System.Text.Encoding.Default);
            json = sr.ReadToEnd().TrimStart();
            sr.Close();

            JObject finaljObject = new JObject();
            JObject onlyAssets = new JObject();
            //string json = "{\"orderId\":\"000001\",\"goodsId[0]\":\"001\",\"goodsId[3]\":\"003\",\"goodsId[10]\":\"new data\"}";
            JObject jsonObj = (JsonConvert.DeserializeObject(json) as JObject)["assets"] as JObject;
            finaljObject.Add(new JProperty("tags", (JsonConvert.DeserializeObject(json) as JObject)["tags"]));
            foreach (var p in jsonObj.Properties().ToArray())
            {
                string id = p.Name;
                OneAsset one = JsonConvert.DeserializeObject<OneAsset>(p.Value.ToString());
                if (one.state == 0)
                {
                    LogHelper.WriteLog(one.name + " 未查看,直接跳过截图");
                    continue;
                }

                #region 这里读取图片进行等分
                //等分数量
                //int subImageNum = Int32.Parse(this.tbSubImageNum.Text.Trim());
                //得到原图的宽高
                int subWidth, subHeight;
                //srcHeight = 2056;    ////获取原图宽、高  
                //srcWidth = 2456;
                subWidth = one.size.width / subImageNum + shift;
                subHeight = one.size.height / subImageNum + shift;
                if (!crop(Path.Combine(basePath, HttpUtility.UrlDecode(one.name)), subWidth, subHeight, subImageNum)) continue;
                #endregion

                #region 
                //所有缺陷框
                RTree.RTree<Region> tree = new RTree.RTree<Region>();
                if (one.state == 2)//这里需要操作素材打的框
                {
                    #region 这里写单个的区块的素材操作了
                    string oneJsonFile = Path.Combine(basePath, id + "-asset.json");
                    using (StreamReader srTemp = new StreamReader(oneJsonFile, System.Text.Encoding.Default))
                    {
                        OneAssetWithRegions oneAR = JsonConvert.DeserializeObject<OneAssetWithRegions>(srTemp.ReadToEnd().TrimStart());

                        #region 重头戏来了，这里处理完救赎
                        foreach (var fuck in oneAR.regions)
                        {
                            tree.Add(new RTree.Rectangle(fuck.boundingBox.left,
                                fuck.boundingBox.top,
                                fuck.boundingBox.left,
                                fuck.boundingBox.top,
                                0, 0), fuck);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
                string oldName = one.name;
                #region 这里做一些逻辑判断
                for (int n = 1; n <= subImageNum * subImageNum; n++)
                {
                    one.id = id + n;
                    one.size = new AssetsSize() { width = subWidth, height = subHeight }; // 这里其实写不写无所谓,但追求完美还是写!!!!注意如果下次还要等分，这里还是写完整
                    one.name = Path.GetFileNameWithoutExtension(oldName) + "-" + n + Path.GetExtension(oldName);
                    one.path = "file:${path}" + one.name;

                    if (tree.Count > 0)
                    {
                        //判断所有的边框是否存在在裁剪后的边框内
                        Fuck fuck = cropRRectangles[n - 1];
                        var objects = tree.Contains(fuck.rectangle);
                        if (objects.Count > 0)//有边框那么，改状态，还要生成本地json文件
                        {
                            one.state = 2;
                            OneAssetWithRegions fuckOne = new OneAssetWithRegions();
                            fuckOne.asset = one;
                            fuckOne.version = jsonVersion;

                            foreach (var re in objects)
                            {
                                re.boundingBox = new BoundingBox()
                                {
                                    left = re.boundingBox.left - fuck.point.X,
                                    top = re.boundingBox.top - fuck.point.Y,
                                    width = re.boundingBox.width,
                                    height = re.boundingBox.height
                                };
                                re.points.Clear();
                                re.points.Add(new AssetPoint() { x = re.boundingBox.left, y = re.boundingBox.top });
                                re.points.Add(new AssetPoint() { x = re.boundingBox.left + re.boundingBox.width, y = re.boundingBox.top });
                                re.points.Add(new AssetPoint() { x = re.boundingBox.left + re.boundingBox.width, y = re.boundingBox.top + re.boundingBox.height });
                                re.points.Add(new AssetPoint() { x = re.boundingBox.left, y = re.boundingBox.top + re.boundingBox.height });

                            }

                            fuckOne.regions = objects;
                            File.WriteAllText(Path.Combine(savePath, id + n + "-asset.json"), JsonConvert.SerializeObject(fuckOne));
                            onlyAssets.Add(new JProperty(one.id, JObject.Parse(JsonConvert.SerializeObject(one))));
                            LogHelper.WriteLog("ng " + HttpUtility.UrlDecode(one.name));
                        }
                        else//没有那么
                        {
                            one.state = 1;
                            if (cbSaveOk.Checked)
                            {
                                onlyAssets.Add(new JProperty(one.id, JObject.Parse(JsonConvert.SerializeObject(one))));
                            }
                            LogHelper.WriteLog("ok " + HttpUtility.UrlDecode(one.name));
                        }
                    }
                    //if (one.state == 1)//这里不用判断素材框， 直接草他个等分数的平方
                    //{

                    //}
                    //else


                    //#region 这里处理完了就可以知道 图片等分出来的区块的state，然后新建个-asset.json，然后再吧assets新增
                    ////one.state = 2;

                    //#endregion
                }
                #endregion
                //写入一个字符串
                int o = 0;
            }
            finaljObject.Add(new JProperty("assets", onlyAssets));
            string bb = finaljObject.ToString();
            File.WriteAllText(Path.Combine(savePath, "import.power-ai"), bb);

            MessageBox.Show("处理完成");


            //            ////////给原图分块
            //            for (int j = 0; j < subImageNum; j++)

            //            {
            //                for (int i = 0; i < subImageNum; i++)
            //                {
            //                    if (j < subImageNum - 1 && i < subImageNum - 1)
            //                    {
            //                        cv::Mat temImage(subHeight, subWidth, CV_8UC3, cv::Scalar(0,0,0));
            //            cv::Mat imageROI = src(cv::Rect(i * subWidth, j * subHeight, temImage.cols, temImage.rows));
            //            cv::addWeighted(temImage, 1.0, imageROI, 1.0, 0., temImage);
            //            subImages.push_back(temImage);
            //        }else{                
            //                cv::Mat temImage(srcHeight - (subImageNum - 1) * subHeight, srcWidth - (subImageNum - 1) * subWidth, CV_8UC3, cv::Scalar(0,0,0));
            //                cv::Mat imageROI = src(cv::Rect(i * subWidth, j * subHeight, temImage.cols, temImage.rows));
            //        cv::addWeighted(temImage, 1.0, imageROI, 1.0, 0., temImage);
            //                subImages.push_back(temImage);                
            //            }
            //}
            //std::cout<<subImages[0]<<std::endl;   //测试语句，打印出子图矩阵
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //新建一个文件对话框
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();

            //设置对话框标题
            pOpenFileDialog.Title = "打开power-ai文件";

            //设置打开文件类型
            pOpenFileDialog.Filter = "power-ai文件（*.power-ai）|*.power-ai";

            //监测文件是否存在
            pOpenFileDialog.CheckFileExists = true;

            //文件打开后执行以下程序
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = System.IO.Path.GetFullPath(pOpenFileDialog.FileName);                             //绝对路径
                tbPath.Text = fileName;
                savePath = tbSavePath.Text = Path.Combine(System.IO.Path.GetDirectoryName(fileName), "new-power-ai");

                //    System.IO.Path.GetExtension(openFileDialog1.FileName);                           //文件扩展名
                //    System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);//文件名没有扩展
                //System.IO.Path.GetFileName(openFileDialog1.FileName);                          //得到文件
                //    System.IO.Path.GetDirectoryName(openFileDialog1.FileName);                  //得到路径
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //新建一个文件对话框
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();

            //设置对话框标题
            pOpenFileDialog.Title = "打开power-ai文件";

            //设置打开文件类型
            pOpenFileDialog.Filter = "power-ai文件（*.power-ai）|*.power-ai";

            //监测文件是否存在
            pOpenFileDialog.CheckFileExists = true;

            //文件打开后执行以下程序
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = System.IO.Path.GetFullPath(pOpenFileDialog.FileName);                             //绝对路径
                string fuckBasePath = System.IO.Path.GetDirectoryName(fileName);


                //basePath = System.IO.Path.GetDirectoryName(tbPath.Text);
                //读取文件内容
                StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);
                json = sr.ReadToEnd().TrimStart();
                sr.Close();
                //string json = "{\"orderId\":\"000001\",\"goodsId[0]\":\"001\",\"goodsId[3]\":\"003\",\"goodsId[10]\":\"new data\"}";
                JObject finaljObject = new JObject();
                JObject onlyAssets = new JObject();
                finaljObject.Add(new JProperty("tags", (JsonConvert.DeserializeObject(json) as JObject)["tags"]));
                JObject jsonObj = (JsonConvert.DeserializeObject(json) as JObject)["assets"] as JObject;
                foreach (var p in jsonObj.Properties().ToArray())
                {
                    OneAsset one = JsonConvert.DeserializeObject<OneAsset>(p.Value.ToString());
                    if (one.state == 1) continue;
                    string id = p.Name;
                    #region 这里写单个的区块的素材操作了
                    string oneJsonFile = Path.Combine(fuckBasePath, id + "-asset.json");
                    OneAssetWithRegions oneAR;
                    using (StreamReader srTemp = new StreamReader(oneJsonFile, System.Text.Encoding.Default))
                    {
                        oneAR = JsonConvert.DeserializeObject<OneAssetWithRegions>(srTemp.ReadToEnd().TrimStart());
                        List<Region> regions = new List<Region>();
                        int edge = 5;
                        foreach (var re in oneAR.regions)
                        {
                            if (re.boundingBox.left > oneAR.asset.size.width)
                            {
                                Console.WriteLine("width remove " + oneAR.asset.id);
                                continue;
                            }
                            if (re.boundingBox.top > oneAR.asset.size.height)
                            {
                                Console.WriteLine("height remove " + oneAR.asset.id);
                                continue;
                            }

                            if (re.boundingBox.left < edge)
                            {
                                re.boundingBox.left = edge;
                            }

                            if(re.boundingBox.top < edge)
                            {
                                re.boundingBox.top = edge;
                            }

                            if (re.boundingBox.left + re.boundingBox.width > oneAR.asset.size.width - edge)
                            {
                                int width = (int)(oneAR.asset.size.width - re.boundingBox.left - edge);
                                if (width <= 0) continue;
                                Console.WriteLine("1 " + oneAR.asset.id);
                                Console.WriteLine(width);
                                re.boundingBox.width = width;
                            }
                            if (re.boundingBox.top + re.boundingBox.height > oneAR.asset.size.height - edge)
                            {
                                int height = (int)(oneAR.asset.size.height - re.boundingBox.top - edge);
                                if (height <= 0) continue;
                                Console.WriteLine("2 " + oneAR.asset.id);
                                Console.WriteLine(height);
                                re.boundingBox.height = height;
                            }
                            re.points.Clear();
                            re.points.Add(new AssetPoint() { x = re.boundingBox.left, y = re.boundingBox.top });
                            re.points.Add(new AssetPoint() { x = re.boundingBox.left + re.boundingBox.width, y = re.boundingBox.top });
                            re.points.Add(new AssetPoint() { x = re.boundingBox.left + re.boundingBox.width, y = re.boundingBox.top + re.boundingBox.height });
                            re.points.Add(new AssetPoint() { x = re.boundingBox.left, y = re.boundingBox.top + re.boundingBox.height });
                            regions.Add(re);
                        }
                        oneAR.regions = regions;
                    }
                    if (oneAR.regions.Count>0)
                    {
                        onlyAssets.Add(new JProperty(one.id, JObject.Parse(JsonConvert.SerializeObject(one))));
                        File.WriteAllText(Path.Combine(fuckBasePath, id + "-asset.json"), JsonConvert.SerializeObject(oneAR));
                    }
                    #endregion
                }
                finaljObject.Add(new JProperty("assets", onlyAssets));
                string bb = finaljObject.ToString();
                File.WriteAllText(Path.Combine(fuckBasePath, "import.power-ai"), bb);
                MessageBox.Show("处理完成");


            }


           
        }
    }
}
