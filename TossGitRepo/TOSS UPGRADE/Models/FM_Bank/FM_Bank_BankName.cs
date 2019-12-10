using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TOSS_UPGRADE.Models.GlobalClass;

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


            getAccountType = new List<BankAccount_AccountType>();
            getAccountTypeColumns = new BankAccount_AccountType();
            getAccountTypeList = new List<AccountTypeList>();

            
            getBankAccount = new List<BankAccountTable>();
            getBankAccountColumns = new BankAccountTable();
            getBankAccountList = new List<BankAccountList>();

            globalClasses = new GlobalClasses();
        }
        public List<BankList> getBankList { get; set; }
        public BankTable getBankColumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BankList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankTable> getBank { get; set; }


        public GlobalClasses globalClasses { get; set; }
        public List<AccountTypeList> getAccountTypeList { get; set; }
        public BankAccount_AccountType getAccountTypeColumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountTypeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankAccount_AccountType> getAccountType { get; set; }


        public BankAccountTable getBankAccountColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankAccountTable> getBankAccount { get; set; }
        public List<BankAccountList> getBankAccountList { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BankAccountBankList { get; set; }
        public int BankAccountBankID { get; set; }
        public int BankAccountBankTempID { get; set; }
        public int BankAccountAccountTypeID { get; set; }
        public int BankAccountAccountTypeTempID { get; set; }
        public int BankAccountFundTypeID { get; set; }
        public int BankAccountFundTypeTempID { get; set; }
        public int BankAccountGeneralAccountNameID { get; set; }
        public int BankAccountGeneralAccountNameTempID { get; set; }
        public string BankAccountGeneralAccountCodeID { get; set; }
    }
    public class BankList
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankAddress { get; set; }
    }
    public class AccountTypeList
    {
        public int AccountTypeID { get; set; }
        public string AccountType { get; set; }
    }
    public class BankAccountList
    {
        public int BankAccountID { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public int SubMajorAccountName { get; set; }
        public string GeneralAccountName { get; set; }
        public string GeneralAccountCode { get; set; }
        public int FundName { get; set; }
        public string AccountType { get; set; }

    }
}