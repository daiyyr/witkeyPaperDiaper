using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace widkeyPaperDiaper
{
    class Mail163 <T>
    {
        Form1 form1;
        string address;
        string password;
        T source;
        string queeryTitle;
        string contentRegex;
        string sid;

        bool haveLogin = false;

        public Mail163(string add, string pw, Form1 f, T source, string queeryTitle, string contentRegex)
        {
            this.address = add;
            this.password = pw;
            this.form1 = f;
            this.source = source;
            this.queeryTitle = queeryTitle;
            this.contentRegex = contentRegex;
        }

        public int login()
        {
            string html = Form1.weLoveYue(
                form1,
                "https://mail.163.com/entry/cgi/ntesdoor?df=mail163_letter&from=web&funcid=loginone&iframe=1&language=-1&passtype=1&product=mail163&net=n&style=-1&race=378_264_390_gz&uid="+ address,
        //       https://mail.163.com/entry/cgi/ntesdoor?df=mail163_letter&from=web&funcid=loginone&iframe=1&language=-1&passtype=1&product=mail163&net=n&style=-1&race=378_264_390_gz&uid=15985830370@163.com
                "POST",
                "http://mail.163.com/",
                false,
                "savelogin=0&url2=http%3A%2F%2Fmail.163.com%2Ferrorpage%2Ferror163.htm&username="+address+"&password="+password,
               //savelogin=0&url2=http%3A%2F%2Fmail.163.com%2Ferrorpage%2Ferror163.htm&username=15985830370&password=dyyr0125
                "mail.163.com"
                );

            if( Regex.Match(html, @"<html><head><script type=""text/javascript"">window.location.href = ""http://mail.163.com/errorpage/error163").Success){
                //            <html><head><script type="text/javascript">window.location.href = "http://mail.163.com/errorpage/error163
                form1.setLogT("email " + address + "password err!");
                return -2;
            }

            sid = Regex.Match(html, @"(?<=\.jsp\?sid=)\w+?(?=&df=)").Value;
        // .jsp?sid=JAmlshvoaqUOwBHuygoonwWhpMKSFGmL&df=mail163_letter"



            //to get sid cookie
            html = Form1.weLoveYue(
                form1,
                "http://hwwebmail.mail.163.com/js6/main.jsp?sid="+ sid +"&df=mail163_letter",
//               http://hwwebmail.mail.163.com/js6/main.jsp?sid=JAmlshvoaqUOwBHuygoonwWhpMKSFGmL&df=mail163_letter
                "GET",
                "http://mail.163.com/",
                false,
                "",
                //savelogin=0&url2=http%3A%2F%2Fmail.163.com%2Ferrorpage%2Ferror163.htm&username=15985830370&password=dyyr0125
                "mail.163.com"
                );

            if (html.Contains("网易邮箱6.0版"))
            {
                form1.setLogT("login " + address + " succeed.");
                haveLogin = true;
                return 1;
            }
            else
            {
                form1.setLogT("login " + address + " failed.");
                return -1;
            }
            
        }

        public void queery()
        {
            if (!haveLogin)
            {
                login();
            }

            while (true)
            {
                // get unread mails
                string html = Form1.weLoveYue(
                    form1,
                    "http://hwwebmail.mail.163.com/js6/s?sid=" + sid + "&func=mbox:listMessages&FrameMasterMailPopupClose=1",
                    //       http://hwwebmail.mail.163.com/js6/s?sid=JAmlshvoaqUOwBHuygoonwWhpMKSFGmL&func=mbox:listMessages&FrameMasterMailPopupClose=1
                    "POST",
                    "http://mail.163.com/",
                    false,
                    "var=%3C%3Fxml%20version%3D%221.0%22%3F%3E%3Cobject%3E%3Cobject%20name%3D%22filter%22%3E%3Cobject%20name"
                    + "%3D%22flags%22%3E%3Cboolean%20name%3D%22read%22%3Efalse%3C%2Fboolean%3E%3C%2Fobject%3E%3C%2Fobject%3E"
                    + "%3Cstring%20name%3D%22order%22%3Edate%3C%2Fstring%3E%3Cboolean%20name%3D%22desc%22%3Etrue%3C%2Fboolean"
                    + "%3E%3Carray%20name%3D%22fids%22%3E%3Cint%3E1%3C%2Fint%3E%3Cint%3E3%3C%2Fint%3E%3Cint%3E18%3C%2Fint%3E"
                    + "%3C%2Farray%3E%3Cint%20name%3D%22limit%22%3E20%3C%2Fint%3E%3Cint%20name%3D%22start%22%3E0%3C%2Fint%3E"
                    + "%3Cboolean%20name%3D%22skipLockedFolders%22%3Etrue%3C%2Fboolean%3E%3Cboolean%20name%3D%22returnTag%22"
                    + "%3Etrue%3C%2Fboolean%3E%3Cboolean%20name%3D%22returnTotal%22%3Etrue%3C%2Fboolean%3E%3C%2Fobject%3E",

                    "mail.163.com"
                    );

                if (html.Contains("<string name=\"subject\">"+queeryTitle+"</string>"))
                //<string name="subject">ご注文予約案内</string>
                {
                    // find the url


                    break;
                }
                else
                {
                    //do not find the notification mail
                }
            }

        }



    }
}
