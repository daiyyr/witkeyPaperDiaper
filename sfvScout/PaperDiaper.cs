using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net.Cache;
using System.Text.RegularExpressions;

namespace widkeyPaperDiaper
{
    public class PaperDiaper
    {
        static string rgx;
        static Match myMatch;

        int threadNo = 0;
        string verificationCode;
        CookieCollection cookieContainer = null;

        Mail163<PaperDiaper> mail;
        Appointment appointment;



        public List<string> gFriends = new List<string>();

        string gFileName = null;

        string eventId = "";
        Form1 form1;
        string keyURL;
        bool requireVeriCode = false;

        public PaperDiaper(Form1 f, Appointment app, Mail163<PaperDiaper> em)
        {
            form1 = f;
            appointment = app;
            mail = em;
        }

        public int writeResult(string content)
        {
            if (gFriends.Count > 0)
            {
                if (gFileName == null)
                {
                    gFileName = "save_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo) + ".txt";
                    form1.setLogT("Create file: " + System.Environment.CurrentDirectory + "\\" + gFileName);
                }
                Form1.writeFile(System.Environment.CurrentDirectory + "\\" + gFileName, content);

            }
            return 1;
        }



        public int probe(string prefecture, string county)
        {
            form1.setLogT("probe " + county + "..");

            string respHtml = Form1.weLoveYue(
                form1,
                "http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=" + prefecture + "&event_type=6&sid=" + county + "&kmws=",

                "GET", "", false, "", ref  cookieContainer,
                false
                );



            if (respHtml.Equals("Found"))
            {
                form1.setLogT("first GET eccur an error!");
                return -1;
            }

            //       <input type="submit" name="sbmt" value="予約" >
            rgx = @"<input type=""submit"" name=""sbmt"" value=""";
            myMatch = (new Regex(rgx)).Match(respHtml);
            if (!myMatch.Success)
            {
                form1.setLogT("no available appointment.");
                return -5;// no available appointment
            }

            if (respHtml.Contains("eventId"))
            {
                rgx = @"(?<=\[\]\,{""eventId"":)\d+?(?=\,)";  //there will be no veri code
                requireVeriCode = false;
            }
            else
            {
                rgx = @"(?<=event_id"" value="")\d+?(?="")";  //there will be a veri code, and we can cheeck up with captcha
                //name="event_id" value="2543729870"
                requireVeriCode = true;
            }
            myMatch = (new Regex(rgx)).Match(respHtml);
            if (myMatch.Success)
            {
                eventId = myMatch.Groups[0].Value;
            }

            if (1 == 1)//     !respHtml.Contains("captcha")   daiyyr
            {
                // jump to verification
            }
            else
            {

                //post eventId to get the verification code page
                respHtml = Form1.weLoveYue(
                form1,
                "http://aksale.advs.jp/cp/akachan_sale_pc/captcha.cgi",
                "POST", 
                "http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=" + county + "&event_type=6&sid=37194&kmws=", 
                false,
                "sbmt=%97%5C%96%F1&event_id=2543729870&event_type=6",
               ref  cookieContainer,
                false
                );


                //show verification code

                //<img src="./captcha/144445570520561.jpeg" alt="画像認証" /><br />
                //http://aksale.advs.jp/cp/akachan_sale_pc/captcha/144445570520561.jpeg

                string cCodeGuid = "";
                rgx = @"(?<=img src=""\./captcha/)\d+?(?=\.jpeg)";
                myMatch = (new Regex(rgx)).Match(respHtml);
                if (myMatch.Success)
                {
                    cCodeGuid = myMatch.Groups[0].Value;
                }
                lock (form1.pictureBox1)
                {
                    
                    if (form1.textBox2.InvokeRequired)
                    {
                        delegate2 sl = new delegate2(delegate()
                        {
                            form1.pictureBox1.ImageLocation = @"http://aksale.advs.jp/cp/akachan_sale_pc/captcha/" + cCodeGuid + ".jpeg";
                            form1.textBox2.Text = "";
                            form1.textBox2.ReadOnly = false;
                            form1.textBox2.Focus();
                            form1.label9.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                            form1.label9.Visible = true;
                        });
                        form1.textBox2.Invoke(sl);
                    }
                    else
                    {
                        form1.pictureBox1.ImageLocation = @"http://aksale.advs.jp/cp/akachan_sale_pc/captcha/" + cCodeGuid + ".jpeg";
                        form1.textBox2.Text = "";
                        form1.textBox2.ReadOnly = false;
                        form1.textBox2.Focus();
                        form1.label9.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                        form1.label9.Visible = true;
                    }

                    while (form1.textBox2.Text.Length < 5)
                    {
                        Thread.Sleep(30);
                    }

                    verificationCode = form1.textBox2.Text.Substring(0, 5);
                    if (form1.textBox2.InvokeRequired)
                    {
                        delegate2 sl = new delegate2(delegate()
                        {
                            form1.textBox2.ReadOnly = true;
                            form1.label9.Visible = false;
                        });
                        form1.textBox2.Invoke(sl);
                    }
                    else
                    {
                        form1.textBox2.ReadOnly = true;
                        form1.label9.Visible = false;
                    }
                }// end of lock picturebox1

                
                //submit the veri code
                respHtml = Form1.weLoveYue(
                form1,
                "http://aksale.advs.jp/cp/akachan_sale_pc/_mail.cgi",
                "POST",
                "http://aksale.advs.jp/cp/akachan_sale_pc/captcha.cgi",
                false,
                "input_captcha=" + verificationCode + "&sbmt=%8E%9F%82%D6&event_id="+eventId+"&event_type=6",
               ref  cookieContainer,
                false
                );
            



                while (respHtml.Contains("captcha"))
                {
                    form1.setLogT("验证码错误！请重新输入");
                    rgx = @"(?<=img src=""\./captcha/)\d+?(?=\.jpeg)";
                    myMatch = (new Regex(rgx)).Match(respHtml);
                    if (myMatch.Success)
                    {
                        cCodeGuid = myMatch.Groups[0].Value;
                    }
                    lock (form1.pictureBox1)
                    {

                        if (form1.textBox2.InvokeRequired)
                        {
                            delegate2 sl = new delegate2(delegate()
                            {
                                form1.pictureBox1.ImageLocation = @"http://aksale.advs.jp/cp/akachan_sale_pc/captcha/" + cCodeGuid + ".jpeg";
                                form1.textBox2.Text = "";
                                form1.textBox2.ReadOnly = false;
                                form1.textBox2.Focus();
                                form1.label9.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                                form1.label9.Visible = true;
                            });
                            form1.textBox2.Invoke(sl);
                        }
                        else
                        {
                            form1.pictureBox1.ImageLocation = @"http://aksale.advs.jp/cp/akachan_sale_pc/captcha/" + cCodeGuid + ".jpeg";
                            form1.textBox2.Text = "";
                            form1.textBox2.ReadOnly = false;
                            form1.textBox2.Focus();
                            form1.label9.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                            form1.label9.Visible = true;
                        }

                        while (form1.textBox2.Text.Length < 5)
                        {
                            Thread.Sleep(30);
                        }

                        verificationCode = form1.textBox2.Text.Substring(0, 5);
                        if (form1.textBox2.InvokeRequired)
                        {
                            delegate2 sl = new delegate2(delegate()
                            {
                                form1.textBox2.ReadOnly = true;
                                form1.label9.Visible = false;
                            });
                            form1.textBox2.Invoke(sl);
                        }
                        else
                        {
                            form1.textBox2.ReadOnly = true;
                            form1.label9.Visible = false;
                        }
                    }// end of lock picturebox1

                    //submit the veri code
                    respHtml = Form1.weLoveYue(
                    form1,
                    "http://aksale.advs.jp/cp/akachan_sale_pc/_mail.cgi",
                    "POST",
                    "http://aksale.advs.jp/cp/akachan_sale_pc/captcha.cgi",
                    false,
                    "input_captcha=" + verificationCode + "&sbmt=%8E%9F%82%D6&event_id=" + eventId + "&event_type=6",
                    ref cookieContainer,
                    false
                );
                
                }//end of while wrong code 
            }//end of if need vervification code


            //post email
            Form1.weLoveMuYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi"
                ,
                "POST",
                requireVeriCode ? "http://aksale.advs.jp/cp/akachan_sale_pc/_mail.cgi" :
                ("http://aksale.advs.jp/cp/akachan_sale_pc/_mail.cgi?sbmt=%97%5C%96%F1&event_id="+eventId+"&event_type=6")
                ,
                false, 
                "mail1="+mail.address.Replace("@","%40")+"&mail2="+mail.address.Replace("@","%40")+"&sbmt=%8E%9F%82%D6&event_id="+eventId+"&event_type=6",
          //    "mail1=15985830370%40163.com&mail2=15985830370%40163.com&sbmt=%8E%9F%82%D6&event_id=5393381489&event_type=6"
                ref cookieContainer
                );

            respHtml = Form1.weLoveYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_confirm.cgi"
                ,
                "POST", 
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi",
                false,
                "sbmt=%91%97%90M&mail1=" + mail.address.Replace("@", "%2540").Replace(".", "%252e") + "&mail2=" + mail.address.Replace("@", "%2540").Replace(".", "%252e") + "&event_id=" + eventId + "&event_type=6",
          //    sbmt=%91%97%90M&mail1=15985830370%2540163%252ecom&mail2=15985830370%2540163%252ecom&event_id=7938283049&event_type=6
                ref cookieContainer,
                false
          );

            
            if (respHtml.Contains("下記メールアドレスにメールを送信しました"))
            {               
                form1.setLogtRed("step1 succeed, checking email: " + mail.address);
            }
            else
            {
                form1.setLogtRed("email submitting failed: " + mail.address);
                return -1;
            }


            keyURL = mail.queery("ご注文予約案内", @"https://aksale(\s|\S)+?(?=\r)");

            setAppointment(mail.address, keyURL);

            return 1;
        }




        public int setAppointment(string email, string url){

            form1.setLogT("start setting appointment from " + email );

            string html = Form1.weLoveYue(
                form1,
                url
                ,
                "GET",
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi",
                false,
                "",
                ref cookieContainer,
                false
                );


            if (!html.Equals("Found"))
            {
                form1.setLogT("error code from page whose url("+ url +") getting in" + email);
                return -1;
            }

            html =  Form1.weLoveYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/form_card_no.cgi"
                ,
                "POST", 
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi",
                false,
                "card_no=" + appointment.CardNo + "&sbmt=%8E%9F%82%D6",
                ref cookieContainer,
                false
                );
                               
            if (html.Contains("恐れ入りますが、もう一度最初から操作してください"))
            {
                form1.setLogT("no available quota, url from: " + email);
                return -2;
            }

            html = Form1.weLoveYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/reg_form_event_1.cgi"
                ,
                "POST",
                "https://aksale.advs.jp/cp/akachan_sale_pc/form_card_no.cgi",
                false,
                "password="+appointment.CardPassword
                +"&sei=%9B%C1&mei=%94%F2%94%F2&sei_kana=%83T&mei_kana=%83C%83q%83q&tel1=090&tel2=8619&tel3=3569"//daiyyr
                //chinese charocter's length is also 1
                +"&sbmt=%8E%9F%82%D6",
                ref cookieContainer,
                false
                );

            html = Form1.weLoveYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/reg_confirm_event.cgi"
                ,
                "POST",
                "https://aksale.advs.jp/cp/akachan_sale_pc/reg_form_event_1.cgi",
                false,
                "sbmt=%91%97%90M",
                ref cookieContainer,
                false
                );
            
            if(html.Contains("ご予約ありがとうございます")){
                form1.setLogT("Setting appointment succeed!");
            }

            return 1;
        }


        public delegate void delegate2();


        public void startProbe()
        {
            if (form1.mailGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    form1.deleteMail.Enabled = false;
                    form1.deleteApp.Enabled = false;
                });
                form1.mailGrid.Invoke(sl);
            }
            else
            {
                form1.deleteMail.Enabled = false;
                form1.deleteApp.Enabled = false;
            }
            
            form1.setLogT("开始扫描..");
            while (true)
            {
                if (Form1.gForceToStop)
                {
                    break;
                }

                int r1 = 0;

            //新潟亀田ｱﾋﾟﾀ店  M
            //   http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%90V%8a%83%8c%a7&event_type=6&sid=37140&kmws=

                //ｱﾘｵ上田店 M
            //   http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%92%b7%96%ec%8c%a7&event_type=6&sid=37194&kmws=
            //songben M
            //   http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%92%b7%96%ec%8c%a7&event_type=6&sid=37196&kmws=

                //ららぽｰと横浜店
           // http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%90_%93%de%90%ec%8c%a7&event_type=6&sid=37139&kmws=

                while ((r1 = this.probe("%90_%93%de%90%ec%8c%a7", "37139")) == -1)   //daiyyr
                {
  
                }
                if (r1 == -5) //no available appointment
                {
                    goto delay;
                }
                    

        delay://不写在while的开头，避免第一次就延时
                if (form1.rate.Text.Equals(""))
                {
                    Thread.Sleep(100);
                }
                else 
                {
                    try{
                        if (Convert.ToInt32(form1.rate.Text) > 0){
                            Thread.Sleep(Convert.ToInt32(form1.rate.Text));
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                    }
                }
            }//end of while


            if (form1.mailGrid.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    form1.deleteMail.Enabled = true;
                });
                form1.mailGrid.Invoke(sl);
            }
            else
            {
                form1.deleteMail.Enabled = true;
            }

            Form1.gForceToStop = false;
            return;
        }


    }
}





//detail use string2url ok?  use string2url with encode

//import data;   slip phone

//static method in form1 ---->  MushroomUtil  --  in environment (using namespace.MushroomUtil)

//delete grib

//adress choosen commbobox