﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Bank
{
    public class FM_Bank
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Bank()
        {
            getBank = new List<BankTable>();
            getBankColumns = new BankTable();
            getBankList = new List<BankList>();
        }
        public List<BankList> getBankList { get; set; }
        public BankTable getBankColumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BankList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankTable> getBank { get; set; }
        public int BankID { get; set; }
    }
    public class BankList
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankAddress { get; set; }
    }
}