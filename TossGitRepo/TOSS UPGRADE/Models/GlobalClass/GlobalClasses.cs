using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.GlobalClass
{
    public class GlobalClasses
    {
        public GlobalClasses()
        {
            fieldFeeDDs = new List<FieldFeeDD>();
            BankAccDDs = new List<BankAccDD>();
        }
        public List<FieldFeeDD> fieldFeeDDs { get; set; }
        public List<BankAccDD> BankAccDDs { get; set; }
    }
    public class FieldFeeDD
    {
        public int SubFundID { get; set; }
        public string FundName { get; set; }

    }
    public class BankAccDD
    {
        public int BankAccountID { get; set; }
        public string AccountNo { get; set; }

    }
}