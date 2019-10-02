using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_ResponsibilityCenter
{
    public class FM_ResponsibilityCenter_Function
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_ResponsibilityCenter_Function()
        {
            getFunction = new List<Function>();
            getFunctioncolumns = new Function();
            getFunctionList = new List<FunctionList>();
        }
        public List<FunctionList> getFunctionList { get; set; }
        public Function getFunctioncolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FunctionList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Function> getFunction { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentCode { get; set; }
        public string FundName { get; set; }
        public int FundTempID { get; set; }
        public int DepartmentTempID { get; set; }
        public int SectorNameID { get; set; }
        public int SectorNameTempID { get; set; }
        public int SubSectorNameID { get; set; }
        public int SubSectorNameTempID { get; set; }
        public int OfficeTypeNameID { get; set; }
        public int OfficeTypeNameTempID { get; set; }
    }
    public class FunctionList
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionAbbreviation { get; set; }
        public string DepartmentName { get; set; }
        public string Sector { get; set; }
        public int SubSector { get; set; }
        public string OfficeType { get; set; }
        public int FundID { get; set; }
    }
   
}