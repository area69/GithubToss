using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_AllotmentClass
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_AllotmentClass()
        {
            getAllotmentClass = new List<AllotmentClass>();
            getAllotmentClasscolumns = new AllotmentClass();
            getAllotmentClassList = new List<AllotmentClassList>();
        }
        public List<AllotmentClassList> getAllotmentClassList { get; set; }
        public AllotmentClass getAllotmentClasscolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AllotmentClassList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AllotmentClass> getAllotmentClass { get; set; }
        public int RevisionYearDate { get; set; }
        public int RevisionYearDateTempID { get; set; }
    }
    public class AllotmentClassList
    {
        public int AllotmentClassID { get; set; }
        public string RevisionYearDate { get; set; }
        public string AllotmentClass { get; set; }
    }
}