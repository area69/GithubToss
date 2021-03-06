﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ChartOfAccounts
{
    public class FM_ChartOfAccounts_MajorAccountGroup
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ChartOfAccounts_MajorAccountGroup()
        {
            getMajorAccountGroup = new List<MajorAccountGroup>();
            getMajorAccountGroupcolumns = new MajorAccountGroup();
            getMajorAccountGroupList = new List<MajorAccountGroupList>();

        }
        public List<MajorAccountGroupList> getMajorAccountGroupList { get; set; }
        public MajorAccountGroup getMajorAccountGroupcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> MajorAccountGroupList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.MajorAccountGroup> getMajorAccountGroup { get; set; }
        public int RevisionYearDate { get; set; }
        public int RevisionYearDateTempID { get; set; }
        public int AllotmentClassID { get; set; }
        public int AllotmentClassIDTempID { get; set; }
        public int AccountGroupID { get; set; }
        public string AccountGroupCodeID { get; set; }
        public int AccountGroupIDTempID { get; set; }
    }
    public class MajorAccountGroupList
    {
        public int MajorAccountGroupID { get; set; }
        public string RevisionYearDate { get; set; }
        public int AllotmentClass { get; set; }
        public string AccountGroupCode { get; set; }
        public string AccountGroupName { get; set; }
        public string MajorAccountGroupName { get; set; }
        public string MajorAccountGroupCode { get; set; }
    }
}