using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SDK.yop.client;
using SDK.yop.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Controllers.extends;
using WebApplication1.Dal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() : base()
        {
            //Util util = new Util();
            //util.ScheduleStart();
            WebUnity = new UnitOfWork();
        }

        private UnitOfWork WebUnity;

        public new void Dispose()
        {
            base.Dispose();
            WebUnity.Dispose();
        }

        [ActionName("index")]
        [WebApplication1Adaptive]
        public ActionResult Index()
        {
            //需要配置正确web.config中的connectionStrings mysql连接，否则会报错，报错可注释，只保留return View();
            List<r_user> r_userlist = WebUnity.UserRepository.Get().ToList<r_user>();
            WebUnity.Dispose();
            ViewData["list"] = r_userlist;

            return View();
        }

        [ActionName("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewData["Message"] = "message";

            return View();
        }

        [ActionName("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ActionName("scheduleAndFileSystem")]
        public ActionResult ScheduleAndFileSystem()
        {
            Util util = new Util();
            util.ScheduleStart();

            return View();
        }

        [ActionName("uploadPPT")]
        public ActionResult UploadPPT()
        {
            return View();
        }

        [ActionName("aes")]
        public ActionResult AES()
        {
            string key = "1bec493fda794aef4f1556321fbb2257";
            string content = "123456";
            Util util = new Util();
            string encrypt = Util.AesEncrypt(key, content, null);
            string dencrypt = Util.AesDencrypt(key, encrypt, null);
            ViewData["key"] = key;
            ViewData["content"] = content;
            ViewData["encrypt"] = encrypt;
            ViewData["dencrypt"] = dencrypt;
            return View();
        }

        [ActionName("ip")]
        public ActionResult IP()
        {
            string ip = staticUtil.GetRealIp();
            ViewData["ip"] = ip;
            return View();
        }

        [ActionName("deepclone")]
        public ActionResult DeepClone()
        {
            IList<string> list = new List<string>();
            list.Add("a");
            list.Add("b");
            list.Add("c");
            IList<string> list2 = staticUtil.DeepClone<string>(list);
            ViewData["list"] = list;
            ViewData["list2"] = list2;
            return View();
        }

        [ActionName("openerAndFileReader")]
        public ActionResult OpenerAndFileReader()
        {
            return View();
        }

        [ActionName("smtp")]
        public ActionResult SMTP()
        {
            string username = "用户名";
            string password = "密码";
            string domain = "服务器域名或ip";
            string url = "服务器域名或ip";
            string fromAddress = "发送地址";
            string fromName = "发件人";
            string subject = "标题";
            string body = "内容";
            string recipient = "收件人";
            Util util = new Util();
            //util.exchangeSendEmail(username, password, domain, url, subject, body, recipient);
            util.smtpSendEmail(username, password, url, fromAddress, fromName, subject, body, recipient);

            return Content("ok");
        }

        [ActionName("exchange")]
        public ActionResult Exchange()
        {
            string username = "用户名";
            string password = "密码";
            string domain = "服务器域名或ip";
            string url = "服务器域名或ip";
            string fromAddress = "发送地址";
            string fromName = "发件人";
            string subject = "标题";
            string body = "内容";
            string recipient = "收件人";
            Util util = new Util();
            util.exchangeSendEmail(username, password, domain, url, subject, body, recipient);
            //util.smtpSendEmail(username, password, url, fromAddress, fromName, subject, body, recipient);

            return Content("ok");
        }

        [ActionName("deserialize")]
        public ActionResult Deserialize()
        {
            string str = "{\"userName\":\"sysadmin\",\"email\":\"xxx@xxx.com\",\"other\":\"remenber to use EncodeURIComponent()\",\"timespan\":\"" + DateTime.Now.ToString() + "\"}";
            Dictionary<string, string> dic = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(str);
            ViewData["dic"] = dic;
            return View();
        }

        [ActionName("formExample")]
        public ActionResult FormExample()
        {
            return View();
        }

        [ActionName("dictionarySort")]
        public ActionResult DictionarySort()
        {
            //C# 可通过linq的OrderBy，OrderByDescending，ThenByDescending进行排序
            return View();
        }

        [ActionName("formSubmit")]
        public ActionResult FormSubmit()
        {
            string formMsg = string.Empty;
            using (Stream s = Request.InputStream)
            {
                StreamReader reader = new StreamReader(s);
                formMsg = reader.ReadToEnd();
            }
            string actionMsg = formMsg.Substring(formMsg.IndexOf('{'), formMsg.LastIndexOf('}') - formMsg.IndexOf('{') + 1);

            //获取配置文件的办法
            //webConfig:configuration标签下
            //<appSettings>
            //    <add key="SolutionUrl" value="http://..." />
            //</appSettings>
            //controller:
            //string solutionUrl = WebConfigurationManager.AppSettings["SolutionUrl"];
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(solutionUrl);
            string url = "https://www.baidu.com/";//填入请求的url,url那边需要写response，此处为了方便，手动设置response的内容为ok
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Method = "post";
            request.Timeout = 10000;
            Stream responseStream = null;
            StreamReader sReader = null;
            String value = null;
            request.UserAgent = Request.UserAgent;
            //如果需要POST数据

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(actionMsg);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);


            WebResponse res = request.GetResponse();//由于url没response，此处会报错

            try
            {
                // 获取响应流
                responseStream = res.GetResponseStream();
                // 对接响应流(以"utf-8"字符集)
                sReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                // 开始读取数据
                value = sReader.ReadToEnd();
            }
            catch (Exception)
            {
                //日志异常
            }
            finally
            {
                //强制关闭
                writer.Close();//关闭请求流
                if (sReader != null)
                {
                    sReader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (res != null)
                {
                    res.Close();
                }
            }

            return Content("ok");
        }

        #region 图形验证码
        [ActionName("code")]
        public ActionResult Code()
        {
            ValidateCode vCode = new ValidateCode();
            byte[] bytes = vCode.CreateValidateGraphic();
            return File(bytes, @"image/jpeg");
        }

        [ActionName("myCode")]
        public ActionResult MyCode()
        {
            MyValidateCode vCode = new MyValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        [ActionName("imgCode")]
        public ActionResult ImgCode()
        {
            return View();
        }

        [HttpPost]
        [ActionName("imgCode")]
        public ActionResult ImgCode(FormCollection form)
        {
            string checkcode = form["checkcode"].ToString();
            string ValidateCode = form["ValidateCode"].ToString();
            string msg = "";
            if (Session["checkcode"].ToString().Equals(checkcode))
            {
                msg += "第一行验证码正确,";
            }
            else
            {
                msg += "第一行验证码不正确,";
            }
            if (Session["ValidateCode"].ToString().Equals(ValidateCode))
            {
                msg += "第二行验证码正确";
            }
            else
            {
                msg += "第二行验证码不正确";
            }

            ViewData["msg"] = msg;

            return View();
        }
        #endregion

        #region 上传附件ppt
        [ActionName("uploadPPT2")]
        [HttpPost]
        public ActionResult UploadPPT2()
        {
            //错误列表
            List<string> uploadErrors = new List<string>();

            // 1、文件名（包含除去ppt的路径）；2、文件大小；3、文件显示名
            List<string> fileInfo = new List<string>();

            string result = "success";
            try
            {
                // 上传到根目录下的~/content/ppt     * 按月份新建文件夹       * 命名按照原来的文件名  
                HttpPostedFileBase file = Request.Files["file1"];

                //判断文件是否合法
                if (isAllowPPT(uploadErrors, file))
                {
                    //文件合法，开始上传
                    string month = DateTime.Now.Month.ToString();
                    month = Convert.ToInt32(month) < 10 ? "0" + month : month;
                    string path = Server.MapPath("/Content/ppt/" + DateTime.Now.Year.ToString() + month);
                    string fName;
                    if (file.FileName.IndexOf('\\') > -1)
                    {
                        fName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                        fName = file.FileName;
                    }
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + "/" + fName);
                    fileInfo.Add(("/content/ppt/" + DateTime.Now.Year.ToString() + month + "/" + fName));
                    fileInfo.Add((((float)(file.ContentLength * 1.00 / (1024 * 1024))).ToString("f2") + "MB"));
                    fileInfo.Add(fName);
                }
                else
                {
                    result = "fail";
                }
            }
            catch
            {
                uploadErrors.Add("上传失败");
                result = "fail";
            }
            ViewData["uploadErrors"] = uploadErrors;
            ViewData["fileInfo"] = fileInfo;
            ViewData["state"] = result;
            return View("uploadPPT2");
        }

        // 判断文件大小，不超过30MB  判断文件后缀，只允许ppt,pptx
        private bool isAllowPPT(List<string> uploadErrors, HttpPostedFileBase file)
        {
            if (file.ContentLength == 0)
            {
                uploadErrors.Add("未找到文件");
                return false;
            }
            if ((file.ContentLength / (1024 * 1024)) > 30)
            {
                uploadErrors.Add("文件大小超过30MB");
                return false;
            }
            if (getFilePostfix(file.FileName).ToLower() != "ppt" &&
                getFilePostfix(file.FileName).ToLower() != "pptx")
            {
                uploadErrors.Add("文件类型只能为“ppt或pptx”");
                return false;
            }
            return true;
        }

        //获取文件后缀名
        private string getFilePostfix(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.') + 1);
        }
        #endregion

        #region 银行卡支付
        [ActionName("yopPay")]
        public ActionResult YOPPay()
        {
            return View();
        }

        [ActionName("yopRequest")]
        public ActionResult YOPRequest()
        {
            YOPModel yoppay = new YOPModel
            {
                requestNo = Request.Form["requestNo"].ToString(),
                orderAmount = Request.Form["orderAmount"].ToString(),
                fundAmount = Request.Form["fundAmount"].ToString(),
                merchantOrderDate = Request.Form["merchantOrderDate"].ToString(),
                bankCode = Request.Form["bankCode"].ToString(),
                productName = Request.Form["productName"].ToString(),
                productDesc = Request.Form["productDesc"].ToString(),
                ip = staticUtil.GetRealIp()
            };
            YopRequest request = new YopRequest(yoppay.merchantNo, yoppay.privatekey, "https://open.yeepay.com/yop-center", yoppay.yopPublicKey);

            request.addParam("requestNo", yoppay.requestNo);
            request.addParam("orderAmount", yoppay.orderAmount);
            request.addParam("fundAmount", yoppay.fundAmount);
            request.addParam("payTool", yoppay.payTool);
            request.addParam("merchantOrderDate", yoppay.merchantOrderDate);
            request.addParam("bankCode", yoppay.bankCode);
            request.addParam("serverCallbackUrl", yoppay.serverCallbackUrl);
            request.addParam("webCallbackUrl", yoppay.webCallbackUrl + "?soid=" + yoppay.requestNo);
            request.addParam("mcc", yoppay.mcc);
            request.addParam("productCatalog", yoppay.productCatalog);
            request.addParam("productName", yoppay.productName);
            request.addParam("productDesc", yoppay.productDesc);
            request.addParam("ip", yoppay.ip);

            System.Diagnostics.Debug.WriteLine(request.toQueryString());
            YopResponse response = YopClient3.postRsa("/rest/v1.0/payplus/order/consume", request);

            if (response.isSuccess() && response.validSign)
            {
                //Response.Write("返回结果签名验证成功!" + "<br>");
                //Response.Write("response.resul:" + response.result + "<br>");
                JObject obj = (JObject)JsonConvert.DeserializeObject(response.result.ToString());
                string requestNo = Convert.ToString(obj["requestNo"]);//订单号（唯一请求号）
                string redirectUrl = Convert.ToString(obj["redirectUrl"]);//请求成功后返回的支付链接地址，自适应浏览器，商户处理跳转以完成支付。
                string fundAmount = Convert.ToString(obj["fundAmount"]);//请求成功后返回，商户订单总金额
                string status = Convert.ToString(obj["status"]);//状态 UNPAY-未支付 SUCCESS-支付成功
                string code = Convert.ToString(obj["code"]);// 返回码
                string message = Convert.ToString(obj["message"]);//返回码的详细说明
                string divideCheck = Convert.ToString(obj["divideCheck"]);//实时分账状态 成功为 SUCCESS
                string divideErrorMassage = Convert.ToString(obj["divideErrorMassage"]);//实时分账信息 成功为 分账校验通过
                if (code.Equals("1"))
                {
                    return Redirect(redirectUrl);
                }
                else
                {
                    return Content(code + "  " + message);
                }
            }
            else
            {
                string errorText = JsonConvert.SerializeObject(response.error);
                return Content("请求失败，返回结果:" + errorText + "<br>");
                //Response.Write("请求失败，返回结果:" + errorText + "<br>");
            }
        }

        [ActionName("yopNotifyReturn")]
        public ActionResult YOPNotifyReturn()
        {
            string response = Request.Params["response"];
            YOPModel yoppay = new YOPModel();
            string sourceData = SHA1withRSA.NoticeDecrypt(response, yoppay.privatekey, yoppay.yopPublicKey);
            JObject obj = (JObject)JsonConvert.DeserializeObject(sourceData);//序列化

            string requestNo = Convert.ToString(obj["requestNo"]);
            string merchantUserId = Convert.ToString(obj["merchantUserId"]);
            string orderAmount = Convert.ToString(obj["orderAmount"]);
            string fundAmount = Convert.ToString(obj["fundAmount"]);
            string paidAmount = Convert.ToString(obj["paidAmount"]);
            string status = Convert.ToString(obj["status"]);
            string payTool = Convert.ToString(obj["payTool"]);
            string cardLast = Convert.ToString(obj["cardLast"]);
            string cardType = Convert.ToString(obj["cardType"]);
            string bankCode = Convert.ToString(obj["bankCode"]);
            string couponInfo = Convert.ToString(obj["couponInfo"]);
            string code = Convert.ToString(obj["code"]);
            string message = Convert.ToString(obj["message"]);

            string str = "";
            if (status.Equals("SUCCESS") && code.Equals("1"))
            {
                //支付成功，写入数据库
                str = "支付成功，写入数据库";
            }
            else
            {
                //支付失败
                str = "支付失败";
            }
            //返回支付结果页
            return Content(str);
        }

        [ActionName("yopReturn")]
        public ActionResult YOPReturn()
        {
            string view = "";
            string soid = Request.QueryString["soid"].ToString();
            if (Request.UserAgent.ToLower().IndexOf("mobile") == -1)
            {
                //返回PC成功页
                view = "PC";
            }
            else
            {
                //返回Mobile成功页
                view = "Mobile";
            }
            view += ":支付成功,订单号为" + soid;
            return Content(view);
        }
        #endregion

        [ActionName("jsonTable")]
        public ActionResult JsonTable()
        {
            return View();
        }

        [ActionName("uEditor")]
        public ActionResult UEditor()
        {
            return View();
        }

        [HttpPost]
        [ActionName("uEditor")]
        public ActionResult UEditor(FormCollection form)
        {
            ueditor ueditor = new ueditor()
            {
                content = form["content"].ToString()
            };
            WebUnity.UEditorRepository.Insert(ueditor);
            WebUnity.Save();
            WebUnity.Dispose();

            return View();
        }

        [ActionName("uEditorShow")]
        public ActionResult UEditorShow()
        {
            List<ueditor> ueditor = WebUnity.UEditorRepository.Get().ToList<ueditor>();
            WebUnity.Dispose();
            ViewData["ueditor"] = ueditor;

            return View();
        }

        [ActionName("randomNumber")]
        public ActionResult RandomNumber()
        {
            int[] arr = Util.GetRandomArray(10, 1, 10);
            ViewData["arr"] = arr;

            return View();
        }

        [ActionName("showImage")]
        public ActionResult ShowImage()
        {
            return View();
        }

        [ActionName("qrCode")]
        public ActionResult QRCode()
        {
            byte[] img = Util.GetQRCodeByLink("www.baidu.com");

            return File(img, "image/jpeg");
        }

        [HttpGet]
        [ActionName("webApiFile")]
        public HttpResponseMessage WebApiFile()
        {
            byte[] img = Util.GetQRCodeByLink("www.baidu.com");
            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(img)
                //或者
                //Content = new StreamContent(stream)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return resp;
        }

        [ActionName("createImage")]
        public ActionResult CreateImage()
        {
            int randomNumber = Util.GetRandomArray(1, 1, 5)[0];
            Bitmap mImg;
            if(randomNumber == 1)
            {
                mImg = new Bitmap(Server.MapPath("/Content/images/CreateImage/orange.jpg"));
            }
            else if(randomNumber == 2)
            {
                mImg = new Bitmap(Server.MapPath("/Content/images/CreateImage/pink.jpg"));
            }
            else if (randomNumber == 3)
            {
                mImg = new Bitmap(Server.MapPath("/Content/images/CreateImage/purple.jpg"));
            }
            else if (randomNumber == 4)
            {
                mImg = new Bitmap(Server.MapPath("/Content/images/CreateImage/yellow.jpg"));
            }
            else
            {
                mImg = new Bitmap(Server.MapPath("/Content/images/CreateImage/young.jpg"));
            }
            Graphics g = Graphics.FromImage(mImg);
            Font font = new Font("Arial", 34, FontStyle.Regular, GraphicsUnit.Pixel);
            //SolidBrush bg = new SolidBrush(Color.FromArgb(100, Color.Black));
            g.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, new SolidBrush(Color.Black), 100, 100);
            MemoryStream ms = new MemoryStream();
            mImg.Save(ms, ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/jpeg");
        }

        [ActionName("parseObjectArray")]
        public ActionResult ParseObjectArray()
        {
            int[] randomNumber = Util.GetRandomArray(2, 1, 10);
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            for(int i = 0;i < randomNumber[0]; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                for (int j = 0; j < randomNumber[1]; j++)
                {
                    dic["time" + j.ToString()] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                list.Add(dic);
            }
            string json = new JavaScriptSerializer().Serialize(list);
            List<Dictionary<string, string>> data = Util.ParseObjectArray(json);
            ViewData["number1"] = randomNumber[0];
            ViewData["number2"] = randomNumber[1];
            ViewData["data"] = data;

            return View();
        }
    }
}