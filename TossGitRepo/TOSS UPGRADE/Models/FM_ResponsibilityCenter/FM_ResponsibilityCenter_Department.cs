using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ResponsibilityCenter
{
    public class FM_ResponsibilityCenter_Department
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ResponsibilityCenter_Department()
        {
            getDepartment = new List<Signatory_DepartmentTable>();
            getDepartmentcolumns = new Signatory_DepartmentTable();
            getDepartmentList = new List<DepartmentList>();
        }
        public int FundNameID { get; set; }
        public int FundNameTempID { get; set; }
        public List<DepartmentList> getDepartmentList { get; set; }
        public Signatory_DepartmentTable getDepartmentcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> DepartmentList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Signatory_DepartmentTable> getDepartment { get; set; }
        public int SectorNameID { get; set; }
        public int SectorNameTempID { get; set; }
        public int SubSectorNameID { get; set; }

        public int SubSectorNameTempID { get; set; }
        public int OfficeTypeNameID { get; set; }

        public int OfficeTypeNameTempID { get; set; }
    }
    public class DepartmentList
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentAbbreviation { get; set; }
        public string ResponsibilityCode { get; set; }
        public string Sector { get; set; }
        public int SubSector { get; set; }
        public string OfficeType { get; set; }
        public string FundName { get; set; }

    }
}