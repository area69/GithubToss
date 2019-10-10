using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_SubMajorAccountGroup
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_SubMajorAccountGroup()
        {
            getSubMajorAccountGroup = new List<SubMajorAccountGroup>();
            getSubMajorAccountGroupcolumns = new SubMajorAccountGroup();
            getSubMajorAccountGroupList = new List<SubMajorAccountGroupList>();
        }
        public List<SubMajorAccountGroupList> getSubMajorAccountGroupList { get; set; }
        public SubMajorAccountGroup getSubMajorAccountGroupcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SubMajorAccountGroupList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.SubMajorAccountGroup> getSubMajorAccountGroup { get; set; }
        public int RevisionYearDate { get; set; }
        public int RevisionYearDateTempID2 { get; set; }
        public int AllotmentClassID { get; set; }
        public int AllotmentClassIDTempID2 { get; set; }
        public int AccountGroupID { get; set; }
        public string AccountGroupCodeID { get; set; }
        public int AccountGroupIDTempID2 { get; set; }
        public string MajorAccountGroupCodeID { get; set; }
        public int MajorAccountGroupNameID { get; set; }
        public int MajorAccountGroupNameTempID2 { get; set; }
    }

    public class SubMajorAccountGroupList
    {
        public int SubMajorAccountGroupID { get; set; }
        public string RevisionYearDate { get; set; }
        public int AllotmentClass { get; set; }
        public int AccountGroupName { get; set; }
        public string MajorAccountGroupName { get; set; }
        public string SubMajorAccountGroupName { get; set; }
        public string AccountGroupCode { get; set; }
        public string MajorAccountGroupCode { get; set; }
        public string SubMajorAccountGroupCode { get; set; }
    }
}