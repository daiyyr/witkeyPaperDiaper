using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace widkeyPaperDiaper
{
    public class Appointment
    {
        private string CardNo { get; set; }
        private string CardPassword { get; set; }
        private string ChineseName { get; set; }
        private string JapaneseName { get; set; }
        private string Phone { get; set; }

        public Appointment(string cardNo, string cardPassword, string chineseName, string japaneseName, string phone)
        {
            CardNo = cardNo;
            CardPassword = cardPassword;
            ChineseName = chineseName;
            JapaneseName = japaneseName;
            Phone = phone;
        }
    }
}
