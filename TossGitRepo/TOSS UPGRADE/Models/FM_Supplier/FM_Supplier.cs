using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Supplier
{
    public class FM_Supplier
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Supplier()
        {
            getSupplier = new List<Supplier>();
            getSuppliercolumns = new Supplier();
            getSupplierList = new List<SupplierList>();
        }
        public List<SupplierList> getSupplierList { get; set; }
        public Supplier getSuppliercolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SupplierList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Supplier> getSupplier { get; set; }
        public string TaxType { get; set; }
    }
    public class SupplierList
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string ProductServices { get; set; }
        public string Address { get; set; }
        public string TaxType { get; set; }
        public string DTIRegNo { get; set; }
        public string CDARegistry { get; set; }
        public string FaxNo { get; set; }
        public string TelNo { get; set; }
        public string TIN { get; set; }
        public string MFName { get; set; }
        public string MFAddress { get; set; }
        public string MFContactNo { get; set; }
        public string AccreNumber { get; set; }
        public string AccreDate { get; set; }
        public string AccreValidity { get; set; }
        public string AccreApprovedBy { get; set; }
        public string AccreMOA { get; set; }
    }
}