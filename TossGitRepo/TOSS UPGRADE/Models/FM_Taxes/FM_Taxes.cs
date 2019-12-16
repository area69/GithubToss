using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Taxes
{
    public class FM_Taxes
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Taxes()
        {
            getTax = new List<Tax>();
            getTaxcolumns = new Tax();
            getTaxList = new List<TaxList>();
        }
        public List<TaxList> getTaxList { get; set; }
        public Tax getTaxcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> TaxList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Tax> getTax { get; set; }
        public bool isUsed { get; set; }
        public int GeneralAccountID { get; set; }
        public int GeneralAccountTempID { get; set; }
    }
    public class TaxList
    {
        public int TaxID { get; set; }
        public string TaxDescription { get; set; }
        public string ShortDescription { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxBase { get; set; }
        public string GeneralAccount { get; set; }
        public bool isUsed { get; set; }
    }
}