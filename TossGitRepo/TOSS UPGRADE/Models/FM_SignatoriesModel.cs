using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models
{
    public class FM_SignatoriesModel
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_SignatoriesModel()
        {
            getSignatoriesTable = new List<SignatoriesTable>();
            getSignatoriesColumns = new SignatoriesTable();
            getSignatoriesList = new List<SignatoriesList>();
        }
        public SignatoriesTable getSignatoriesColumns { get; set; }
         public List<SignatoriesList> getSignatoriesList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.SignatoriesTable> getSignatoriesTable { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> DepartmentList { get; set; }
        public int PositionID { get; set; }
        public int PositionTempID { get; set; }
        public int DepartmentID { get; set; }
        public int DepartmentTempID { get; set; }
        public int FunctionID { get; set; }
        public int FunctionTempID { get; set; }
        public bool isDeptHeads { get; set; }
        public bool isDeptHeadsTempID { get; set; }
        public bool isActive { get; set; }
    }


    public class SignatoriesList
    {
        public int SignatoriesID { get; set; }
        public string SignatoriesName { get; set; }
        public string PreferredName { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public string FunctionName { get; set; }
        public string DivisionName  { get; set; } 
        public bool isDeptHead { get; set; }
        public bool isActive { get; set; }
    }
}