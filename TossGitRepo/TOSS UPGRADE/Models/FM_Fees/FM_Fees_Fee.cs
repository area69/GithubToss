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


            getFeeCategory = new List<FeeCategory>();
            getFeeCategorycolumns = new FeeCategory();
            getFeeCategoryList = new List<FeeCategoryList>();

            fieldFeeDDs = new List<FieldFeeDD>();
        }
        public List<FieldFeeList> getFieldFeeList { get; set; }
        public FieldFee getFieldFeecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FieldFeeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.FieldFee> getFieldFee { get; set; }

        public List<FeeCategoryList> getFeeCategoryList { get; set; }
        public FeeCategory getFeeCategorycolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FeeCategoryList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.FeeCategory> getFeeCategory { get; set; }
        public int RevisionYearID { get; set; }
        public int RevisionYearTempID { get; set; }
        public int FundTypeID { get; set; }
        public int FundTypeTempID { get; set; }

        public int AFDescriptionID { get; set; }
        public int AFDescriptionTempID { get; set; }
        public int FeeCategoryID { get; set; }
        public int FeeCategoryTempID { get; set; }
        public int AccountCodeID { get; set; }
        public int AccountCodeTempID { get; set; }
        public List<FieldFeeDD> fieldFeeDDs { get; set; }
        public bool isRequired { get; set; }
        public int AccountFormTempID { get; set; }
    }

    public class FieldFeeList
    {
        public int FieldFeeID { get; set; }
        public string FieldFeeDescription { get; set; }
        public string Rate { get; set; }
        public int AccountCode { get; set; }
        public int FundType { get; set; }
        public int FeeCategory { get; set; }
    }
    public class FeeCategoryList
    {
        public int FeeCategoryID { get; set; }
        public string FeeCategoryName { get; set; }
        public string AFDescription { get; set; }
    }
    public class FieldFeeDD
    {
        public int SubFundID { get; set; }
        public string FundName { get; set; }

    }
}