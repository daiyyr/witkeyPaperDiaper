using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using System.Net.Cache;

namespace widkeyPaperDiaper
{
    


    public partial class Form1 : Form
    {
        
        public static bool debug = false;


        public static bool gForceToStop = false;
        public static bool gLoginOkFlag = false;
        static DateTime expireDate = new DateTime(2015, 10, 16);
        static string rgx;
        static Match myMatch;
        static string gHost = "aksale.advs.jp";
        public County selecteCounty = null;
        public int selectedShop = -1;
        public string selectedType = null;

        CookieCollection cookieContainerForTest;

        List<Appointment> Applist;
        List<Mail163<PaperDiaper>> Maillist;
        List<County> Countylist = new List<County>();
        
        
        public class County
        {
            public string Name;
            public List<string> Shops { get; set; }
            public List<string> Sids { get; set; }
            public County(string name, List<string> shops, List<string> sids)
            {
                Name = name;
                Shops = shops;
                Sids = sids;
            }
        }

        public Form1()
        {
            InitializeComponent();
            Countylist.Add(new County(
                "北海道",
                new List<string> { "旭川店", "屯田ｲﾄｰﾖｰｶﾄﾞｰ店", "ｱﾘｵ札幌店", "新さっぽろ店" },
                new List<string> { "37116", "37305", "37185", "37187" })
                );
            Countylist.Add(new County(
                "青森県",
                new List<string> { "青森ｻﾝﾛｰﾄﾞ店" },
                new List<string> { "37106" })
                );
            Countylist.Add(new County(
                "宮城県",
                new List<string> { "仙台泉店", "ﾗﾗｶﾞｰﾃﾞﾝ長町店", "ｱﾘｵ仙台泉店" },
                new List<string> { "37091", "37175", "37208" })
                );


            label6.Text = "expire date: " + expireDate.ToString("yyyy-MM-dd");
            if (debug)
            {
                button1.Visible = true;
                testLog.Visible = true;
                this.ClientSize = new System.Drawing.Size(1150, 960);
            }
            else
            {
                DateTime t = GetNistTime(this);
                if (t == DateTime.MinValue)
                {
                    setLogT("请连接互联网后重新启动程序");
                    autoB.Visible = false;
                }
                else
                {
                    if ((t - expireDate).Days > 0)
                    {
                        setLogT("程序已过期，请联系作者");
                        autoB.Visible = false;
                    }
                }
            }
        }

        

        public delegate void setLog(string str1);
        public void setLogT(string s)
        {
            if (logT.InvokeRequired)
            {
                // 实例一个委托，匿名方法，
                setLog sl = new setLog(delegate(string text)
                {
                    logT.AppendText(DateTime.Now.ToString() + " " + text + Environment.NewLine);
                });
                // 把调用权交给创建控件的线程，带上参数
                logT.Invoke(sl, s);
            }
            else
            {
                logT.AppendText(DateTime.Now.ToString() + " " + s + Environment.NewLine);
            }
        }

        public void setLogtRed(string s)//something wrong, if it's first line, no red
        {
            if (logT.InvokeRequired)
            {
                setLog sl = new setLog(delegate(string text)
                {
                    logT.AppendText(DateTime.Now.ToString() + " " + text + Environment.NewLine);
                    int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                    if (i > 1)
                    {
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionColor = Color.Red;
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionFont = new Font(logT.Font, FontStyle.Bold);
                    }
                });
                logT.Invoke(sl, s);
            }
            else
            {
                logT.AppendText(DateTime.Now.ToString() + " " + s + Environment.NewLine);
                int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                if (i > 1)
                {
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionColor = Color.Red;
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionFont = new Font(logT.Font, FontStyle.Bold);
                }
            }
        }

        public delegate void DSetTestLog(HttpWebRequest req, string respHtml);
        public void setTestLog(HttpWebRequest req, string respHtml)
        {
            if (testLog.InvokeRequired)
            {
                DSetTestLog sl = new DSetTestLog(delegate(HttpWebRequest req1, string text)
                {
                    testLog.Text = Environment.NewLine + "返回的HTML源码：";
                    testLog.Text += Environment.NewLine + text;
                });
                testLog.Invoke(sl, req, respHtml);
            }
            else
            {
                testLog.Text = Environment.NewLine + "返回的HTML源码：";
                testLog.Text += Environment.NewLine + respHtml;
            }
        }

        /*
        public void alarm()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(WHA_avac.Properties.Resources.mtl);
            player.Load();
            player.PlayLooping();
        }
        */

        public static DateTime GetNistTime(Form1 form1)
        {
            DateTime dateTime = DateTime.MinValue;

//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                form1.setLogT("WebException: " + webEx.Status.ToString());
                return dateTime;
            }
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
//                string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
//                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
//                double milliseconds = Convert.ToInt64(time) / 1000.0;
//                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();

                string html = stream.ReadToEnd();//0=1443934730460
                string time = Regex.Match(html, @"(?<=0\=)\d{10}").Value;
                dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(time + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                dateTime = dateTime.Add(toNow);
                
                //   dateTime = DateTime.ParseExact(time, "MM月dd日", System.Globalization.CultureInfo.InvariantCulture);
            }
            return dateTime;
        }

        public static string ToUrlEncode(string strCode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)  
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换    
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return (sb.ToString());
        }

        public static string ToUrlEncode(string strCode, System.Text.Encoding encode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = encode.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)  
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换    
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return (sb.ToString());
        }

        public static void writeFile(string file, string content)
        {
            FileStream aFile;
            StreamWriter sw;
            aFile = new FileStream(file, FileMode.Append);
            sw = new StreamWriter(aFile);
            sw.Write(content);
            sw.Close();
        }




        public static void setRequest(HttpWebRequest req, CookieCollection cookies)
        {
            //req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //req.Accept = "*/*";
            //req.Connection = "keep-alive";
            //req.KeepAlive = true;
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E";
            //req.Headers["Accept-Encoding"] = "gzip, deflate";
            //req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            req.Host = gHost;

            req.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.10; rv:40.0) Gecko/20100101 Firefox/40.0";
            req.AllowAutoRedirect = false;
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.PerDomainCapacity = 40;
            if (cookies != null)
            {
                req.CookieContainer.Add(cookies);
            }
            req.ContentType = "application/x-www-form-urlencoded";
        }

        public static int writePostData(Form1 form1, HttpWebRequest req, string Encode)
        {
            byte[] postBytes = Encoding.UTF8.GetBytes(Encode);
            //req.ContentLength = postBytes.Length;  // cause InvalidOperationException: 写入开始后不能设置此属性。
            Stream postDataStream = null;
            try
            {
                postDataStream = req.GetRequestStream();
                postDataStream.Write(postBytes, 0, postBytes.Length);
            }
            catch (WebException webEx)
            {
                form1.setLogT("While writing post data," + webEx.Status.ToString());
                return -1;
            }
            
            postDataStream.Close();
            return 1;
        }

        public static string resp2html(HttpWebResponse resp )
        {
            
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(resp.GetResponseStream());
                return stream.ReadToEnd();
                //Shift_JIS
            }
            else
            {
                return resp.StatusDescription;
            }

        }

        public static string resp2html(HttpWebResponse resp, string charSet, Form1 form1)
        {
            var buffer = GetBytes(resp); 
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                if (String.IsNullOrEmpty(charSet) || string.Compare(charSet, "ISO-8859-1") == 0)
                {
                    charSet = GetEncodingFromBody(buffer);
                }

                try
                {
                    var encoding = Encoding.GetEncoding(charSet);  //Shift_JIS
                    var str = encoding.GetString(buffer);                

                    return str;
                }
                catch (Exception ex)
                {
                    form1.setLogT("resp2html, " + ex.ToString());
                    return string.Empty;
                }


                /*
                string respHtml = "";
                char[] cbuffer = new char[256];
                Stream respStream = resp.GetResponseStream();
                StreamReader respStreamReader = new StreamReader(respStream, encoding);//respStream,Encoding.UTF8
                int byteRead = 0;
                try
                {
                    byteRead = respStreamReader.Read(cbuffer, 0, 256);

                }
                catch (WebException webEx)
                {
                    setLogT("respStreamReader, " + webEx.Status.ToString());
                    return "";
                }
                while (byteRead != 0)
                {
                    string strResp = new string(cbuffer, 0, byteRead);
                    respHtml = respHtml + strResp;
                    try
                    {
                        byteRead = respStreamReader.Read(cbuffer, 0, 256);
                    }
                    catch (WebException webEx)
                    {
                        setLogT("respStreamReader, " + webEx.Status.ToString());
                        return "";
                    }

                }
                respStreamReader.Close();
                respStream.Close();
                return respHtml;

                */

            }
            else
            {
                return resp.StatusDescription;
            }

        }

        private static byte[] GetBytes(WebResponse response)
        {
            var length = (int)response.ContentLength;
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[0x100];

                using (var rs = response.GetResponseStream())
                {
                    for (var i = rs.Read(buffer, 0, buffer.Length); i > 0; i = rs.Read(buffer, 0, buffer.Length))
                    {
                        memoryStream.Write(buffer, 0, i);
                    }
                }

                data = memoryStream.ToArray();
            }

            return data;
        }

        private static string GetEncodingFromBody(byte[] buffer)
        {
            var regex = new Regex(@"<meta(\s+)http-equiv(\s*)=(\s*""?\s*)content-type(\s*""?\s+)content(\s*)=(\s*)""text/html;(\s+)charset(\s*)=(\s*)(?<charset>[a-zA-Z0-9-]+?)""(\s*)(/?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var str = Encoding.ASCII.GetString(buffer);
            var regMatch = regex.Match(str);
            if (regMatch.Success)
            {
                var charSet = regMatch.Groups["charset"].Value;
                return charSet;
            }

            return Encoding.ASCII.BodyName;
        }

        /* 
         * return response status
         * especially, if found, return"found: http......"
         */
        public static string weLoveMuYue(Form1 form1, string url, string method, string referer, bool allowAutoRedirect, string postData, ref CookieCollection cookies)
        {
            string result;
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, cookies);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                if (method.Equals("POST"))
                {
					if (writePostData (form1, req, postData) < 0) {
						continue;
					}
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT("GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        form1.setLogT( "wrong address"); //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT("本次请求被服务器拒绝，可尝试调高间隔时间"); //500
                    }
                    continue;
                }
                if (resp != null)
                {
                    result = resp.StatusDescription;
                    if (result == "Found")
                    {
                        result += ":"+ resp.Headers["location"];
                    }
                }
                else
                {
                    continue;
                }
                if (debug)
                {
                    respHtml = resp2html(resp);
                    form1.setTestLog(req, respHtml);
                }
                cookies = req.CookieContainer.GetCookies(req.RequestUri);
                resp.Close();
                break;
            }
            return string.Empty;
        }

        /* unregular host
         * return response status
         * especially, if found, return"found: http......"
         * 
         */
        public static string weLoveMuYue(Form1 form1, string url, string method, string referer, bool allowAutoRedirect, string postData, ref CookieCollection cookies, string host)
        {
            string result;
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, cookies);
                req.Host = host;
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                if (method.Equals("POST"))
                {
                    if (writePostData(form1, req, postData) < 0)
                    {
                        continue;
                    }
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT("GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        form1.setLogT("wrong address"); //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT("本次请求被服务器拒绝，可尝试调高间隔时间"); //500
                    }
                    continue;
                }
                if (resp != null)
                {
                    result = resp.StatusDescription;
                    if (result == "Found")
                    {
                        result += ":"+ resp.Headers["location"];
                    }
                }
                else
                {
                    continue;
                }
                if (debug)
                {
                    respHtml = resp2html(resp);
                    form1.setTestLog(req, respHtml);
                }
                cookies = req.CookieContainer.GetCookies(req.RequestUri);
                resp.Close();
                break;
            }
            return string.Empty;
        }


        /* 
         * return response HTML
         */
        public static string weLoveYue(Form1 form1, string url, string method, string referer, bool allowAutoRedirect, string postData, ref CookieCollection cookies, bool responseInUTF8)
        {
            if (form1 == null)
            {
                return string.Empty;
            }
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, cookies);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                if (method.Equals("POST"))
                {
					if (writePostData (form1, req, postData) < 0) {
						continue;
					}
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT("GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        return "wrong address"; //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT( "本次请求被服务器拒绝，可尝试调高间隔时间" ); //500
                    }
                    continue;
                }
                if (resp != null)
                {   
                    if(responseInUTF8)
                    {
                        respHtml = resp2html(resp);
                    }else
                    {
                        respHtml = resp2html(resp, resp.CharacterSet, form1); // like  Shift_JIS
                    }

                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                    cookies = req.CookieContainer.GetCookies(req.RequestUri);
                    if (debug)
                    {
                        form1.setTestLog(req, respHtml);
                    }
                    resp.Close();
                    return respHtml;
                }
                else
                {
                    continue;
                }
            }
        }

        /* 
         * return responsive HTML
         * unregular host
         */
        public static string weLoveYue(Form1 form1, string url, string method, string referer, bool allowAutoRedirect, string postData, ref CookieCollection cookies, string host, bool responseInUTF8)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, cookies);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                req.Host = host;
                if (method.Equals("POST"))
                {
                    if (writePostData(form1, req, postData) < 0)
                    {
                        continue;
                    }
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT("GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        return "wrong address"; //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT("本次请求被服务器拒绝，可尝试调高间隔时间"); //500
                    }
                    continue;
                }
                if (resp != null)
                {
                    if(responseInUTF8)
                    {
                        respHtml = resp2html(resp);
                    }else
                    {
                        respHtml = resp2html(resp, resp.CharacterSet, form1); // like  Shift_JIS
                    }
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                    cookies = req.CookieContainer.GetCookies(req.RequestUri);
                    if (debug)
                    {
                        form1.setTestLog(req, respHtml);
                    }
                    resp.Close();
                    return respHtml;
                }
                else
                {
                    continue;
                }
            }
        }

        /*
         * do not handle the response
         */
        public static HttpWebResponse weLoveYueer(Form1 form1, HttpWebResponse resp, string url, string method, string referer, bool allowAutoRedirect, string postData, ref CookieCollection cookies)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                setRequest(req, cookies);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                if (method.Equals("POST"))
                {
                    if (writePostData(form1, req, postData) < 0)
                    {
						continue;
					}
                }
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT("GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        form1.setLogT( "wrong address"); //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT("本次请求被服务器拒绝，可尝试调高间隔时间"); //500
                    }
                    continue;
                }
                if (resp != null)
                {
                    cookies = req.CookieContainer.GetCookies(req.RequestUri);
                    return resp;
                }
                else
                {
                    continue;
                }
            }
        }




        private void loginB_Click(object sender, EventArgs e)
        {
            /*
            Login login = new Login(this);
            Thread t = new Thread(login.loginT);
            t.Start();
            */
        }

        private void autoB_Click(object sender, EventArgs e)
        {
            setLogtRed("user operation: start probing");
            if (debug)
            {
                PaperDiaper paper = new PaperDiaper(
                    this,
                    new Appointment ("2800048300159", "abc123456", "崔飛飛", "サイヒヒ", "090-8619-3569"),
                    new Mail163<PaperDiaper>("15985830370@163.com","dyyr7921129",this));
                Thread t = new Thread(paper.startProbe);
                t.Start();
            }
            else
            {
                if (selecteCounty == null || selectedShop == -1 || selectedType == null)
                {
                    this.setLogT("please choose type, county and shop");
                    return;
                }
                if (selectedShop >= selecteCounty.Shops.Count)
                {
                    this.setLogT("invalid selected shop");
                    return;
                }

                if (Applist == null || Applist.Count < 1)
                {
                    this.setLogT("please import valid appointment details!");
                    return;
                }
                if (Maillist == null || Maillist.Count < 1)
                {
                    this.setLogT("please import valid email details!");
                    return;
                }
                for (int i = 0; i < Applist.Count && i < Maillist.Count; i++)
                {
                    PaperDiaper paper = new PaperDiaper(this, Applist[i], Maillist[i]);
                    Thread t = new Thread(paper.startProbe);
                    t.Start();
                }
            }
            
        }


        private void logT_TextChanged(object sender, EventArgs e)
        {
            logT.SelectionStart = logT.Text.Length;
            logT.ScrollToCaret();
        }

        public class EmailForshow{
            public string Email { get; set; }
            public string Password { get; set; }

            public EmailForshow(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
            

        public delegate void delegate2();

        public void addEmails()
        {
            Maillist = new List<Mail163<PaperDiaper>>();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.txt)|*.txt|(*.html)|*.html";

            List<EmailForshow> mailForshow = new List<EmailForshow>();

            if (mailGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    //打开对话框, 判断用户是否正确的选择了文件
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //获取用户选择的文件，并判断文件大小不能超过20K，fileInfo.Length是以字节为单位的
                        FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                        if (fileInfo.Length > 504800)
                        {
                            MessageBox.Show("上传的文件不能大于500K");
                        }
                        else
                        {
                            //在这里就可以写获取到正确文件后的代码了
                            string[] lines = File.ReadAllLines(fileDialog.FileName, System.Text.Encoding.GetEncoding("GB18030"));
                            foreach (string line in lines)
                            {
                                if (line.Length == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    Regex regex = new Regex(@"( ){2,}");
                                    string[] s = regex.Replace(line.Trim(), " ").Split(' ');
                                    if (s.Length != 2)
                                    {
                                        setLogT("ignore invalid line: " + line);
                                    }
                                    else
                                    {
                                        Maillist.Add(new Mail163<PaperDiaper>(s[0], s[1], this));
                                        mailForshow.Add(new EmailForshow(s[0], s[1]));
                                    }
                                }
                            }
                            if (Maillist.Count > 0)
                            {
                                var source = new BindingSource();
                                source.DataSource = mailForshow;
                                mailGrid.DataSource = source;
                            }
                        }
                    }
                });
                mailGrid.Invoke(sl);
            }
            else //do not use delegate
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    if (fileInfo.Length > 204800)
                    {
                        MessageBox.Show("上传的文件不能大于200K");
                    }
                    else
                    {
                        string[] lines = File.ReadAllLines(fileDialog.SafeFileName);
                        foreach (string line in lines)
                        {
                            if (line.Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                Regex regex = new Regex(@"( ){2,}");
                                string[] s = regex.Replace(line.Trim(), " ").Split(' ');
                                if (s.Length != 2)
                                {
                                    setLogT("ignore invalid line: " + line);
                                }
                                else
                                {
                                    Maillist.Add(new Mail163<PaperDiaper>(s[0], s[1], this));
                                }
                            }
                        }
                        if (Maillist.Count > 0)
                        {
                            var source = new BindingSource();
                            source.DataSource = Maillist;
                            DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
                            txtCol.DataPropertyName = txtCol.Name = txtCol.HeaderText = "email";
                            //daiyyr
                            mailGrid.Columns.Add(txtCol);
                            txtCol.DataPropertyName = txtCol.Name = txtCol.HeaderText = "password";
                            mailGrid.Columns.Add(txtCol);
                            mailGrid.DataSource = source;
                        }
                    }
                }
            }
        }

        public void deleteEmails()
        {

            /*daiyyr
            if (mailGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    for (int i = mailGrid.CheckedItems.Count - 1; i >= 0; i--)
                    {
                        mailGrid.Items.Remove(mailGrid.CheckedItems[i]);
                    }
                });
                mailGrid.Invoke(sl);
            }
            else
            {
                for (int i = mailGrid.CheckedItems.Count - 1; i >= 0; i--)
                {
                    mailGrid.Items.Remove(mailGrid.CheckedItems[i]);
                }
            }
            */


            /*
             * put the delete result to the file
            string strCollected = string.Empty;
            for (int i = 0; i < urlList.Items.Count; i++)
            {
                if (strCollected == string.Empty)
                {
                    strCollected = urlList.GetItemText(urlList.Items[i]);
                }
                else
                {
                    strCollected += "\n" + urlList.GetItemText(urlList.Items[i]);
                }
            }
            writeFile(System.Environment.CurrentDirectory + "\\" + "urlList", strCollected);
             * */
        }

        private void addB_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(addEmails);
            t.Start();
        }

        private void deleteB_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(deleteEmails);
            t.Start();
        }

        public void addDetails()
        {
            Applist = new List<Appointment>();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.txt)|*.txt|(*.html)|*.html";

            if (appointmentGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    //打开对话框, 判断用户是否正确的选择了文件
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //获取用户选择的文件，并判断文件大小不能超过20K，fileInfo.Length是以字节为单位的
                        FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                        if (fileInfo.Length > 504800)
                        {
                            MessageBox.Show("上传的文件不能大于500K");
                        }
                        else
                        {
                            //在这里就可以写获取到正确文件后的代码了
                            string[] lines = File.ReadAllLines(fileDialog.FileName, System.Text.Encoding.GetEncoding("GB18030"));
                            foreach (string line in lines)
                            {
                                if (line.Length == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    Regex regex = new Regex(@"( ){2,}");
                                    string[] s = regex.Replace(line.Trim(), " ").Split(' ');
                                    if (s.Length != 5)
                                    {
                                        setLogT("ignore invalid line: " + line); //500
                                    }
                                    else
                                    {
                                        Applist.Add(new Appointment(s[0], s[1], s[2], s[3], s[4]));
                                    }
                                }
                            }
                            if (Applist.Count > 0)
                            {
                                var source = new BindingSource();
                                source.DataSource = Applist;
                                appointmentGrid.DataSource = source;
                            }
                        }
                    }
                });
                appointmentGrid.Invoke(sl);
            }
            else //do not use delegate
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    if (fileInfo.Length > 204800)
                    {
                        MessageBox.Show("上传的文件不能大于200K");
                    }
                    else
                    {
                        string[] lines = File.ReadAllLines(fileDialog.SafeFileName);
                        foreach (string line in lines)
                        {
                            if (line.Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                Regex regex = new Regex(@"( ){2,}");
                                string[] s = regex.Replace(line.Trim(), " ").Split(' ');
                                if (s.Length != 5)
                                {
                                    setLogT("ignore invalid line: " + line); //500
                                }
                                else
                                {
                                    Applist.Add(new Appointment(s[0], s[1], s[2], s[3], s[5]));
                                }
                            }
                        }
                        if (Applist.Count > 0)
                        {
                            var source = new BindingSource();
                            source.DataSource = Applist;
                            appointmentGrid.DataSource = source;
                //            dataGridView1.
                        }
                    }
                }
            }

        }

        public void deleteDetails()
        {
            /*
            if (appointmentGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    for (int i = appointmentGrid.CheckedItems.Count - 1; i >= 0; i--)
                    {
                        appointmentGrid.Items.Remove(appointmentGrid.CheckedItems[i]);
                    }
                });
                appointmentGrid.Invoke(sl);
            }
            else
            {
                for (int i = appointmentGrid.CheckedItems.Count - 1; i >= 0; i--)
                {
                    appointmentGrid.Items.Remove(appointmentGrid.CheckedItems[i]);
                }
            }
             */ 
        }

        private void addDetails_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(addDetails);
            t.Start();
        }

        private void deleteDetails_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(deleteDetails);
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setLogT(Form1.ToUrlEncode("北海道", System.Text.Encoding.GetEncoding("shift-jis")));

            /*
            PaperDiaper paper = new PaperDiaper(
                    this,
                    new Appointment("2800048300159", "abc123456", "崔飛飛", "サイヒヒ", "090-8619-3569"),
                    new Mail163<PaperDiaper>("15985830370@163.com", "dyyr7921129", this));
            paper.searchMailDirectely();
            */
            //     %9b%c1     %94%f2%94%f2          %83t          %83C%83q%83q      090      8619      3569
           //"&sei=%9B%C1&mei=%94%F2%94%F2&sei_kana=%83T&mei_kana=%83C%83q%83q&tel1=090&tel2=8619&tel3=3569"

            string x1 = Form1.ToUrlEncode("崔飛飛".Substring(0, 1), System.Text.Encoding.GetEncoding("shift-jis")),
                   x2 = Form1.ToUrlEncode("崔飛飛".Substring(1, "崔飛飛".Length - 1), System.Text.Encoding.GetEncoding("shift-jis")),
                   y1 = Form1.ToUrlEncode("サイヒヒ".Substring(0, 1), System.Text.Encoding.GetEncoding("shift-jis")),
                   y2 = Form1.ToUrlEncode("サイヒヒ".Substring(1, "サイヒヒ".Length - 1), System.Text.Encoding.GetEncoding("shift-jis")),
                   z1 = Regex.Match("090-8619-3569", @"\d+(?=\-)").Value,
                   z2 = Regex.Match("090-8619-3569", @"(?<=\d+\-)\d+(?=-)").Value,
                   z3 = Regex.Match("090-8619-3569", @"(?<=\d+\-\d+\-)\d+").Value;
            setLogT(x1+" "+x2+" "+y1+" "+y2+" "+z1+" "+z2+" "+z3);

             


            string pattern = @"^";
            string replacement = "1-1-";
            string result = Regex.Replace("12345", pattern, replacement);
            setLogT(result);

            rgx = @"(?<=aa).*?(?=aa)";
            myMatch = (new Regex(rgx)).Match("qqqqqaaqwdsfaafferaafe222aa2222444aa444444222faaloveaa");
            while (myMatch.Success)
            {
                setLogT(myMatch.Groups[0].Value);
                myMatch = myMatch.NextMatch();
            }

            string message = "4344.34334.23.24.";
            Regex rex = new Regex(@"^(\.|\d)+$");
            if (rex.IsMatch(message))
            {
                //float result2 = float.Parse(message);
                setLogT("match");
            }
            else
                setLogT("not match");

            int aa;
            if ((aa = 4) == 4)
            {
                setLogT(aa.ToString());
            }

            Regex regex = new Regex(@"( ){2,}");
            setLogT(regex.Replace("22      22", " "));
            string[] s = regex.Replace(" abc def kkk   333 ppp ".Trim(), " ").Split(' ');
            setLogT(s.Length.ToString());

            string test2 = "abc";
     //       testCall(test2);
            setLogT(test2);

            Appointment test3 = new Appointment("159","","","","");
            testCall(ref test3);
            setLogT(test3.CardNo);

            setLogT("崔飛飛 " + Form1.ToUrlEncode("崔飛飛", System.Text.Encoding.GetEncoding("shift-jis")));//Shift_JIS     ??
            setLogT("サイヒヒ "+Form1.ToUrlEncode("サイヒヒ"));
            setLogT("090-8619-3569 "+Form1.ToUrlEncode("090-8619-3569"));

            /*
            string respHtml = Form1.weLoveYue(
                this,
                "https://aksale.advs.jp/cp/akachan_sale_pc/form_card_no.cgi"
                ,
                "POST",
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi",
                false,
                "card_no=" + "1234567890123" + "&sbmt=%8E%9F%82%D6",
                ref cookieContainerForTest,
                false
                );
             */ 
            setLogT("崔飛飛 ".Length.ToString());

            
        }
        void testCall(ref Appointment t)
        {
            t = new Appointment("152", "", "", "", "");
        }

        private void textBox1_keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                autoB.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gForceToStop = true;
            setLogtRed("user operation: stop probing");
        }

        private void rate_Validating(object sender, CancelEventArgs e)
        {
            string str = rate.Text;
            if (str.Length > 0)
            {
                if (!Regex.Match(str, @"^\d+$").Success)
                {
                    e.Cancel = true;
                    MessageBox.Show("频率只能填写数字");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < Countylist.Count)
            {
                selecteCounty = Countylist[comboBox1.SelectedIndex];
                comboBox2.Items.Clear();
                for (int i = 0; i < selecteCounty.Shops.Count; i++)
                {
                    comboBox2.Items.Add(selecteCounty.Shops[i]);
                }
            }
            else
            {
                selecteCounty = null;
            }
            
                
            //comboBox1.SelectedItem.ToString()
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedShop = comboBox2.SelectedIndex;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedType = comboBox2.SelectedIndex == 0 ? "6" : "7";//only made 6 / 7 for temp
        }



        /*
         // 只能控制写入尾部的字符 
        private void rate_TextChanged(object sender, EventArgs e)
        {
            string str = rate.Text;
            int len = str.Length;
            if (len > 0)
            {
                if (!Regex.Match(str.Substring(len - 1, 1), @"\d").Success)
                {
                    rate.Text = str.Substring(0, len - 1);
                }
            }
        }

        
         // 将导致输入内容无法删除
        private void rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
             
        }
        */
    }
}
