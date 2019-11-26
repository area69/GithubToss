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
        }
        public List<FieldFeeDD> fieldFeeDDs { get; set; }

    }
    public class FieldFeeDD
    {
        public int SubFundID { get; set; }
        public string FundName { get; set; }

    }
}