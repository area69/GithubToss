using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_GeneralAccount
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_GeneralAccount()
        {
            getGeneralAccount = new List<GeneralAccount>();
            getGeneralAccountcolumns = new GeneralAccount();
            getGeneralAccountList = new List<GeneralAccountList>();
        }
        public List<GeneralAccountList> getGeneralAccountList { get; set; }
        public GeneralAccount getGeneralAccountcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> GeneralAccountList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.GeneralAccount> getGeneralAccount { get; set; }
        public int RevisionYearDate { get; set; }
        public int RevisionYearDateTempID2 { get; set; }
        public int AllotmentClassID { get; set; }
        public int AllotmentClassIDTempID2 { get; set; }
        public int AccountGroupID { get; set; }
        public int AccountGroupIDTempID2 { get; set; }
        public string AccountGroupCodeID { get; set; }
        public string MajorAccountGroupCodeID { get; set; }
        public string SubMajorAccountGroupCodeID { get; set; }
        public int MajorAccountGroupNameID { get; set; }
        public int MajorAccountGroupNameTempID2 { get; set; }
        public int SubMajorAccountGroupNameID { get; set; }
        public int SubMajorAccountGroupNameTempID2 { get; set; }
        public int GeneralAccountGroupNameID { get; set; }
        public int GeneralAccountGroupNameTempID { get; set; }
        public bool isReserve { get; set; }
        public bool isRelease { get; set; }
        public bool isContinuing { get; set; }
        public bool isCashAdvance { get; set; }
        public bool isNormalBalance { get; set; }
    }
    public class GeneralAccountList
    {
        public int GeneralAccountID { get; set; }
        public string RevisionYearDate { get; set; }
        public int AllotmentClass { get; set; }
        public int AccountGroupName { get; set; }
        public int MajorAccountGroupName { get; set; }
        public string SubMajorAccountGroupName { get; set; }
        public string AccountGroupCode { get; set; }
        public string MajorAccountGroupCode { get; set; }
        public string SubMajorAccountGroupCode { get; set; }
        public string GeneralAccountName { get; set; }
        public string GeneralAccountCode { get; set; }

    }
}