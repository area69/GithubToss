using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TOSS_UPGRADE.Models.FM_Fees
{
    public class FM_Fees_Fee
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Fees_Fee()
        {
            getFieldFee = new List<FieldFee>();
            getFieldFeecolumns = new FieldFee();
            getFieldFeeList = new List<FieldFeeList>();
        }
        public List<FieldFeeList> getFieldFeeList { get; set; }
        public FieldFee getFieldFeecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FieldFeeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.FieldFee> getFieldFee { get; set; }
    }
    public class FieldFeeList
    {
        public int FieldFeeID { get; set; }
        public string FieldFeeDescription { get; set; }
        public string Rate { get; set; }
        public string AccountCode { get; set; }
        public string FundType { get; set; }
        public string FeeCategory { get; set; }
    }
}