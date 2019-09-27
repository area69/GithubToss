using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Payee
{
    public class FM_Payee_Payee
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Payee_Payee()
        {

            getPayee = new List<Payee>();
            getPayeecolumns = new Payee();
            getPayeeList = new List<PayeeList>();
        }
        public List<PayeeList> getPayeeList { get; set; }
        public Payee getPayeecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> PayeeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Payee> getPayee { get; set; }
        public int DepartmentID { get; set; }
        public int DepartmentIDTempID { get; set; }
    }
    public class PayeeList
    {
        public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAddress { get; set; }
        public string DepartmentName { get; set; }
    }
}