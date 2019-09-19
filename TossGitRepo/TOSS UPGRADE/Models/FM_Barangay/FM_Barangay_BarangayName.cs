using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Barangay
{
    public class FM_Barangay_BarangayName
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Barangay_BarangayName()
        {
            getBarangayName = new List<Barangay_BarangayName>();
            getBarangayNamecolumns = new Barangay_BarangayName();
            getBarangayNameList = new List<BarangayNameList>();

            getBarangayCollector = new List<Barangay_CollectorName>();
            getBarangayCollectorcolumns = new Barangay_CollectorName();
            getBarangayCollectorList = new List<BarangayCollectorList>();
        }
        public List<BarangayNameList> getBarangayNameList { get; set; }
        public Barangay_BarangayName getBarangayNamecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BarangayNameList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Barangay_BarangayName> getBarangayName { get; set; }


        public List<BarangayCollectorList> getBarangayCollectorList { get; set; }
        public Barangay_CollectorName getBarangayCollectorcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BarangayCollectorList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Barangay_CollectorName> getBarangayCollector { get; set; }


    }
    public class BarangayNameList
    {
        public int BarangayID { get; set; }
        public string BarangayName { get; set; }
    }

    public class BarangayCollectorList
    {
        public int BarangayCollectorID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}