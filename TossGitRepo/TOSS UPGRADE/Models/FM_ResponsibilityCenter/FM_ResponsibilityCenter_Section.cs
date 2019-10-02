using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ResponsibilityCenter
{
    public class FM_ResponsibilityCenter_Section
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ResponsibilityCenter_Section()
        {

            getSection = new List<Section>();
            getSectioncolumns = new Section();
            getSectionList = new List<SectionList>();
        }
        public List<SectionList> getSectionList { get; set; }
        public Section getSectioncolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SectionList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Section> getSection { get; set; }
        public int DepartmentID { get; set; }
        public int DepartmentTempID { get; set; }
        public int FunctionID { get; set; }
        public int FunctionTempID { get; set; }
    }

    public class SectionList
    {
        public int SectionID { get; set; }

        public string SectionName { get; set; }

        public string DepartmentName { get; set; }
        public string FunctionName { get; set; }
    }
}