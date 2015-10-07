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

        string email = "15985830370@163.com";
        string password = "dyyr7921129";
        string cardNo = "0123456789123";

        public List<string> gFriends = new List<string>();

        string gFileName = null;
        string collection_token = "";
        string cursor = "";
        string profile_id = "";
        string user_id = "";
        string eventId = "";
        int succeed = 0;
        int failed = 0;
        int successInOneProbe = 0;
        int SUMsuccessInOneProbe;
        Form1 form1;

        public PaperDiaper(Form1 f)
        {
            form1 = f;
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



        public int probe(string county)
        {
            form1.setLogT("probe " + county + "..");

            string respHtml = Form1.weLoveYue(
                form1,
                "http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%92%b7%96%ec%8c%a7&event_type=6&sid=37194&kmws=",
                //上田县 M
            //   http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%92%b7%96%ec%8c%a7&event_type=6&sid=37196&kmws=
            //songben M
                //   http://aksale.advs.jp/cp/akachan_sale_pc/search_event_list.cgi?area2=%92%b7%96%ec%8c%a7&event_type=6&sid="+ county +"&kmws=

                "GET", "", false, "");



            if (respHtml.Equals("Found"))
            {
                form1.setLogT("first GET eccur an error!");
                return -1;
            }

            rgx = @"<input type=""submit"" name=""sbmt"" value=""予約"" >";
            myMatch = (new Regex(rgx)).Match(respHtml);
            if (!myMatch.Success)
            {
                form1.setLogT("no available appointment.");
                return -5;// no available appointment
            }

            rgx = @"(?<=\[\]\,{""eventId"":)\d+?(?=\,)";
            myMatch = (new Regex(rgx)).Match(respHtml);
            if (myMatch.Success)
            {
                eventId = myMatch.Groups[0].Value;
            }

            /*
            rgx = @"(?<=name=""event_id"" value="")\d+?(?="")";
            myMatch = (new Regex(rgx)).Match(respHtml);
            if (myMatch.Success)
            {
                collection_token = myMatch.Groups[0].Value;
            }
            collection_token = Form1.ToUrlEncode(collection_token);

            rgx = @"^\d+(?=\%)";
            myMatch = (new Regex(rgx)).Match(collection_token);
            if (myMatch.Success)
            {
                profile_id = myMatch.Groups[0].Value;
            }
            */

            Form1.weLoveMuYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi"
                ,
                "POST", 
                "http://aksale.advs.jp/cp/akachan_sale_pc/_mail.cgi?sbmt=%97%5C%96%F1&event_id=7938283049&event_type=6",
                false, 
                "mail1="+email.Replace("@","%40")+"&mail2="+email.Replace("@","%40")+"&sbmt=%8E%9F%82%D6&event_id="+eventId+"&event_type=6"
          //    "mail1=15985830370%40163.com&mail2=15985830370%40163.com&sbmt=%8E%9F%82%D6&event_id=5393381489&event_type=6"
                );

          string html =  Form1.weLoveYue(
                form1,
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_confirm.cgi"
                ,
                "POST", 
                "https://aksale.advs.jp/cp/akachan_sale_pc/mail_form.cgi",
                false, 
                "sbmt=%91%97%90M&mail1="+email.Replace("@","%2540").Replace(".","%252e")+"&mail2="+email.Replace("@","%2540").Replace(".","%252e")+"&event_id="+eventId+"&event_type=6"
          //    sbmt=%91%97%90M&mail1=15985830370%2540163%252ecom&mail2=15985830370%2540163%252ecom&event_id=7938283049&event_type=6
                );
            
            if(html.Contains("下記メールアドレスにメールを送信しました")){
                form1.setLogtRed("step1 succeed, checking email: " + email);
            }
            else
            {
                form1.setLogtRed("email submitting failed: " + email);
            }


            Mail163<PaperDiaper> paper = new Mail163<PaperDiaper>(email, password, form1, this, "ご注文予約案内","???");
            Thread t = new Thread(paper.queery);
            t.Start();



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
                ""
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
                "card_no="+cardNo+"&sbmt=%8E%9F%82%D6"
                );




            return 1;
        }


        public delegate void delegate2();


        public void startProbe()
        {
            if (form1.urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    form1.deleteB.Enabled = false;
                });
                form1.urlList.Invoke(sl);
            }
            else
            {
                form1.deleteB.Enabled = false;
            }
            /*
            if (form1.urlList.Items.Count == 0)
            {
                form1.setLogT("empty user list! please import a userID file");
                if (form1.urlList.InvokeRequired)
                {
                    delegate2 sl = new delegate2(delegate()
                    {
                        form1.deleteB.Enabled = true;
                    });
                    form1.urlList.Invoke(sl);
                }
                else
                {
                    form1.deleteB.Enabled = true;
                }
                Form1.gForceToStop = false;
                return;
            }
             
            Login login = new Login(form1) { };

            if (!Form1.gLoginOkFlag)
            {
                login.loginT();
                if (!Form1.gLoginOkFlag)
                {
                    if (form1.urlList.InvokeRequired)
                    {
                        delegate2 sl = new delegate2(delegate()
                        {
                            form1.deleteB.Enabled = true;
                        });
                        form1.urlList.Invoke(sl);
                    }
                    else
                    {
                        form1.deleteB.Enabled = true;
                    }
                    Form1.gForceToStop = false;
                    return;
                }
            }
            * */
            form1.setLogT("开始扫描..");
            while (true)
            {
                if (Form1.gForceToStop)
                {
                    break;
                }

    //            for (int i = 0; i < form1.urlList.Items.Count; i++)
    //            {
                    int r1 = 0;
    //                while ((r1 = this.probe(form1.urlList.GetItemText(form1.urlList.Items[i]))) == -1)
                    while ((r1 = this.probe("") )== -1)
                    {
            /*          Form1.gLoginOkFlag = false;
                        login.loginT();
                        if (!Form1.gLoginOkFlag)
                        {
                            if (form1.urlList.InvokeRequired)
                            {
                                delegate2 sl = new delegate2(delegate()
                                {
                                    form1.deleteB.Enabled = true;
                                });
                                form1.urlList.Invoke(sl);
                            }
                            else
                            {
                                form1.deleteB.Enabled = true;
                            }
                            Form1.gForceToStop = false;
                            return;
                        }
             */ 
                    }
                    if (r1 == -5)
                    {
                        goto delay;
                    }
                    if (form1.urlList.InvokeRequired)
                    {
                        delegate2 sl = new delegate2(delegate()
                        {
                            if (r1 == -2)
                            {
                                //red daiyyr
                                failed++;
                            }
                            else
                            {
            //                    form1.urlList.SetItemChecked(i, true);
                                succeed++;
            //                    form1.setLogT(" got from " + form1.urlList.GetItemText(form1.urlList.Items[i]) + ": " + successInOneProbe);
                                SUMsuccessInOneProbe += successInOneProbe;
                                successInOneProbe = 0;
                            }
                        });
                        form1.urlList.Invoke(sl);
                    }
                    else
                    {
                        if (r1 == -2)
                        {
                            //red
                            failed++;
                        }
                        else
                        {
             //               form1.urlList.SetItemChecked(i, true);
                            succeed++;
                        }
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
                        catch(Exception e){
                            Thread.Sleep(100);
                        }
                    }
                    
                    
   //             }//end of 'for' for checklistbox
            }
            form1.setLogT( "列表扫描结束，成功列表项: " + succeed + ", 失败列表项: " + failed + ", 共收集好友: " + SUMsuccessInOneProbe);
            succeed = 0;
            failed = 0;
            SUMsuccessInOneProbe = 0;
            if (gFileName != null)
            {
                form1.setLogT("Result in " + System.Environment.CurrentDirectory + "\\" + gFileName);

            }
            gFriends.Clear();
            if (form1.urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    form1.deleteB.Enabled = true;
                });
                form1.urlList.Invoke(sl);
            }
            else
            {
                form1.deleteB.Enabled = true;
            }
            Form1.gForceToStop = false;
            return;
        }


    }
}
