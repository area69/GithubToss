using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_AccountGroup
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_AccountGroup()
        {
            getAccountGroup = new List<AccountGroup>();
            getAccountGroupcolumns = new AccountGroup();
            getAccountGroupList = new List<AccountGroupList>();
        }
        public List<AccountGroupList> getAccountGroupList { get; set; }
        public AccountGroup getAccountGroupcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountGroupList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountGroup> getAccountGroup { get; set; }
        public int RevisionYearDate { get; set; }
        public int RevisionYearDateTempID { get; set; }
        public int AllotmentClassID { get; set; }
        public int AllotmentClassIDTempID { get; set; }
    }
    public class AccountGroupList
    {
        public int AccountGroupID { get; set; }
        public string RevisionYearDate { get; set; }
        public string AllotmentClass { get; set; }
        public string AccountGroupName { get; set; }
        public string AccountGroupCode { get; set; }
    }
}