using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_AccountableForm
{
    public class FM_AccountableFormInventory
    {
        public FM_AccountableFormInventory()
        {
            getAccountableFormInvt = new List<AccountableForm_Inventory>();
            getAccountableFormInvtcolumns = new AccountableForm_Inventory();
            getAccountableFormInvtList = new List<AccountableFormInvtList>();
            Accountable = new List<AccountableFormInvtList>();
        }
        public List<AccountableFormInvtList> getAccountableFormInvtList { get; set; }
        public AccountableForm_Inventory getAccountableFormInvtcolumns { get; set; }

        //public List<AccountableForm_Inventory> getAccountableFormInvtcolumnsList { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormInvtList { get; set; }

        public int AFTempID { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Inventory> getAccountableFormInvt { get; set; }
        public int AccountableFormInvtID { get; set; }
        public int hidORInventoryQuantity { get; set; }
        public List<AccountableFormInvtList> Accountable { get; set; }
    }
    public class AccountableFormInvtList
    {
        public int AFORID { get; set; }
        public int AF { get; set; }
        public int StubNo { get; set; }
        public int Quantity { get; set; }
        public int StartingOR { get; set; }
        public int EndingOR { get; set; }
        public string DateIssued { get; set; }
        public bool isIssued { get; set; }
    }

}