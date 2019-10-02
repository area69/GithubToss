using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_RevisionYear
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_RevisionYear()
        {
            getRevisionYear = new List<RevisionYear>();
            getRevisionYearcolumns = new RevisionYear();
            getRevisionYearList = new List<RevisionYearList>();
        }
        public List<RevisionYearList> getRevisionYearList { get; set; }
        public RevisionYear getRevisionYearcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> RevisionYearList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.RevisionYear> getRevisionYear { get; set; }
        public bool IsUsed { get; set; }
    }
    public class RevisionYearList
    {
        public string RevisionYearID { get; set; }
        public string RevisionYear { get; set; }
        public bool IsUsed { get; set; }
        public string Remarks { get; set; }
    }
}