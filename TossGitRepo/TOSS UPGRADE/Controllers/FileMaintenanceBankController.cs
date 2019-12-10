using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOSS_UPGRADE.GlobalFunctions;
using TOSS_UPGRADE.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TOSS_UPGRADE.Models.FM_Bank;
using TOSS_UPGRADE.Models.GlobalClass;


namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceBankController : Controller
    {
        // GET: FileMaintenanceBank    
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BankTab()
        {
            FileMaintenanceBankController model = new FileMaintenanceBankController();
            return PartialView("Bank/BankIndex", model);
        }
        public ActionResult AccountTypeTab()
        {
            FileMaintenanceBankController model = new FileMaintenanceBankController();
            return PartialView("AccountType/AccountTypeIndex", model);
        }
        public ActionResult BankAccountTab()
        {
            FileMaintenanceBankController model = new FileMaintenanceBankController();
            return PartialView("BankAccount/BankAccountIndex", model);
        }
        #region Bank
        //Table Bank Form
        public ActionResult Get_BankTable()
        {
            FM_Bank model = new FM_Bank();
            List<BankList> tbl_Bank = new List<BankList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BankTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Bank.Add(new BankList()
                        {
                            BankID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BankName = GlobalFunction.ReturnEmptyString(dr[1]),
                           BankCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            BankAddress = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBankList = tbl_Bank.ToList();
            return PartialView("Bank/_BankTable", model.getBankList);
        }

        //Get Add Bank Name Partial View
        public ActionResult Get_AddBankName()
        {
            FM_Bank model = new FM_Bank();
            return PartialView("Bank/_AddBank", model);
        }

        public JsonResult AddBankName(FM_Bank model)
        {
            BankTable tblBarangayName = new BankTable();
            tblBarangayName.BankName = model.getBankColumns.BankName;
            tblBarangayName.BankCode = model.getBankColumns.BankCode;
            tblBarangayName.BankAddress = model.getBankColumns.BankAddress;
            TOSSDB.BankTables.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }
        //Get Barangay Name
        public ActionResult Get_UpdateBankName(FM_Bank model, int BankID)
        {
            BankTable tblBarangayName = (from e in TOSSDB.BankTables where e.BankID == BankID select e).FirstOrDefault();
            model.getBankColumns.BankID = tblBarangayName.BankID;
            model.getBankColumns.BankName = tblBarangayName.BankName;
            model.getBankColumns.BankCode = tblBarangayName.BankCode;
            model.getBankColumns.BankAddress = tblBarangayName.BankAddress;
            return PartialView("Bank/_UpdateBank", model);
        }
        public ActionResult UpdateBankName(FM_Bank model)
        {
            BankTable tblBarangayName = (from e in TOSSDB.BankTables where e.BankID == model.getBankColumns.BankID select e).FirstOrDefault();
            tblBarangayName.BankName = model.getBankColumns.BankName;
            tblBarangayName.BankCode = model.getBankColumns.BankCode;
            tblBarangayName.BankAddress = model.getBankColumns.BankAddress;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("Bank/_UpdateBank", model);
        }
        //Delete Barangay Name
        public ActionResult DeleteBankName(FM_Bank model, int BankID)
        {
            BankTable tblBarangayName = (from e in TOSSDB.BankTables where e.BankID == BankID select e).FirstOrDefault();
            TOSSDB.BankTables.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region Account Type
        public ActionResult Get_AccountTypeTable()
        {
            FM_Bank model = new FM_Bank();
            List<AccountTypeList> tbl_AccountType = new List<AccountTypeList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankAccount_AccountType order by AccountType asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountTypeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountType.Add(new AccountTypeList()
                        {
                            AccountTypeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountType = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }

            model.getAccountTypeList = tbl_AccountType.ToList();


            return PartialView("AccountType/_AccountTypeTable", model.getAccountTypeList);

        }

        //Get Add Account Type Partial View
        public ActionResult Get_AddAccountType()
        {
            FM_Bank model = new FM_Bank();
            return PartialView("AccountType/_AddAccountType", model);
        }

        //Add Account Type
        public JsonResult AddAccountType(FM_Bank model)
        {
            BankAccount_AccountType tblAccountType = new BankAccount_AccountType();
            tblAccountType.AccountType = GlobalFunction.ReturnEmptyString(model.getAccountTypeColumns.AccountType);
            TOSSDB.BankAccount_AccountType.Add(tblAccountType);
            TOSSDB.SaveChanges();
            return Json(tblAccountType);
        }

        //Get Update Account Type
        public ActionResult Get_UpdateAccountType(FM_Bank model, int AccountTypeID)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == AccountTypeID select e).FirstOrDefault();
            model.getAccountTypeColumns.AccountTypeID = tblAccountType.AccountTypeID;
            model.getAccountTypeColumns.AccountType = tblAccountType.AccountType;
            return PartialView("AccountType/_UpdateAccountType", model);
        }

        //Update Account Type
        public ActionResult UpdateAccountType(FM_Bank model)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == model.getAccountTypeColumns.AccountTypeID select e).FirstOrDefault();
            tblAccountType.AccountType = model.getAccountTypeColumns.AccountType;
            TOSSDB.Entry(tblAccountType);
            TOSSDB.SaveChanges();
            return PartialView("AccountType/_UpdateAccountType", model);
        }

        //Delete Account Type
        public ActionResult DeleteAccountType(FM_Bank model, int AccountTypeID)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == AccountTypeID select e).FirstOrDefault();
            TOSSDB.BankAccount_AccountType.Remove(tblAccountType);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion
        #region Bank Account
        //Table Bank Account Form
        public ActionResult Get_BankAccountTable()
        {
            FM_Bank model = new FM_Bank();
            List<BankAccountList> tbl_Bank = new List<BankAccountList>();

            var SQLQuery = "SELECT BankAccountID,BankName,AccountNo,AccountName,dbo.GeneralAccount.SubMajorAccountGroupID,GeneralAccountName,GeneralAccountCode,AccountType,SubFund.SubFundID FROM DB_TOSS.dbo.BankAccountTable,SubFund,BankTable,BankAccount_AccountType,GeneralAccount WHERE SubFund.SubFundID = BankAccountTable.SubFundID AND BankTable.BankID = BankAccountTable.BankID AND BankAccount_AccountType.AccountTypeID = BankAccountTable.AccountTypeID AND GeneralAccount.GeneralAccountID = BankAccountTable.GeneralAccountID;";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BankAccountTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Bank.Add(new BankAccountList()
                        {
                            BankAccountID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BankName = GlobalFunction.ReturnEmptyString(dr[1]),
                            AccountNo = GlobalFunction.ReturnEmptyString(dr[2]),
                            AccountName = GlobalFunction.ReturnEmptyString(dr[3]),
                            SubMajorAccountName = GlobalFunction.ReturnEmptyInt(dr[4]),
                            GeneralAccountName = GlobalFunction.ReturnEmptyString(dr[5]),
                            GeneralAccountCode = GlobalFunction.ReturnEmptyString(dr[6]),
                            AccountType = GlobalFunction.ReturnEmptyString(dr[7]),
                            FundName = GlobalFunction.ReturnEmptyInt(dr[8]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBankAccountList = tbl_Bank.ToList();
            return PartialView("BankAccount/_BankAccountTable", model.getBankAccountList);
        }
        //Get Add Bank Acccount Partial View
        public ActionResult Get_AddBankAccount()
        {
            FM_Bank model = new FM_Bank();
            return PartialView("BankAccount/_AddBankAccount", model);
        }
        //Dropdown Bank
        public ActionResult GetDynamicBank()
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankTables.ToList() select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            return PartialView("BankAccount/_DynamicDDBankName", model);
        }
        public ActionResult GetSelectedDynamicBank(int BankAccountBankTempID)
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankTables.ToList() select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            model.BankAccountBankID = BankAccountBankTempID;
            return PartialView("BankAccount/_DynamicDDBankName", model);
        }
        //Dropdown Account Type
        public ActionResult GetDynamicAccountType()
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankAccount_AccountType.ToList() select new { AccountTypeID = s.AccountTypeID, AccountType = s.AccountType }), "AccountTypeID", "AccountType");
            return PartialView("BankAccount/_DynamicDDAccountType", model);
        }
        public ActionResult GetSelectedDynamicAccountType(int BankAccountAccountTypeTempID)
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankAccount_AccountType.ToList() select new { AccountTypeID = s.AccountTypeID, AccountType = s.AccountType }), "AccountTypeID", "AccountType");
            model.BankAccountAccountTypeID = BankAccountAccountTypeTempID;
            return PartialView("BankAccount/_DynamicDDAccountType", model);
        }
        //Dropdown Fund Type
        public ActionResult GetDynamicFundType()
        {
            FM_Bank model = new FM_Bank();
            var Acronym = "";
            foreach (var item in TOSSDB.SubFunds.ToList())
            {
                for (var i = 0; i < item.Fund.FundName.Length;)
                {
                    if (i == 0)
                    {
                        Acronym += item.Fund.FundName[i].ToString();
                    }
                    if (item.Fund.FundName[i] == ' ')
                    {
                        Acronym += item.Fund.FundName[i + 1].ToString();
                    }
                    i++;
                }
                model.globalClasses.fieldFeeDDs.Add(new FieldFeeDD
                {
                    SubFundID = item.SubFundID,
                    FundName = Acronym + " - " + item.SubFundName,
                });
                Acronym = "";

            }
            model.BankAccountBankList = new SelectList(model.globalClasses.fieldFeeDDs.ToList(), "SubFundID", "FundName");
            return PartialView("BankAccount/_DynamicDDFundName", model);
        }
        public ActionResult GetSelectedDynamicFundType(int BankAccountFundTypeTempID)
        {
            FM_Bank model = new FM_Bank();
            var Acronym = "";
            foreach (var item in TOSSDB.SubFunds.ToList())
            {
                for (var i = 0; i < item.Fund.FundName.Length;)
                {
                    if (i == 0)
                    {
                        Acronym += item.Fund.FundName[i].ToString();
                    }
                    if (item.Fund.FundName[i] == ' ')
                    {
                        Acronym += item.Fund.FundName[i + 1].ToString();
                    }
                    i++;
                }
                model.globalClasses.fieldFeeDDs.Add(new FieldFeeDD
                {
                    SubFundID = item.SubFundID,
                    FundName = Acronym + " - " + item.SubFundName,
                });
                Acronym = "";

            }
            model.BankAccountBankList = new SelectList(model.globalClasses.fieldFeeDDs.ToList(), "SubFundID", "FundName");
            model.BankAccountFundTypeID = BankAccountFundTypeTempID;
            return PartialView("BankAccount/_DynamicDDFundName", model);
        }

        //Dropdown General Account
        public ActionResult GetDynamicGeneralAccount()
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountName = s.SubMajorAccountGroup.SubMajorAccountGroupName + ", " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountName");
            return PartialView("BankAccount/_DynamicDDGeneralAccountName", model);
        }
        public ActionResult GetSelectedDynamicGeneralAccount(int BankAccountGeneralAccountNameTempID)
        {
            FM_Bank model = new FM_Bank();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList() select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountName = s.SubMajorAccountGroup.SubMajorAccountGroupName + ", " + s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountName");
            model.BankAccountGeneralAccountNameID = BankAccountGeneralAccountNameTempID;
            return PartialView("BankAccount/_DynamicDDGeneralAccountName", model);
        }
        public ActionResult GetDynamicGeneralAccountCode(int GeneralAccountID)
        {
            FM_Bank model = new FM_Bank();
            GeneralAccount tblSector = (from e in TOSSDB.GeneralAccounts where e.GeneralAccountID == GeneralAccountID select e).FirstOrDefault();
            model.BankAccountGeneralAccountCodeID = tblSector.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupCode + "- " + tblSector.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupCode + "- " + tblSector.SubMajorAccountGroup.SubMajorAccountGroupCode + "- " + tblSector.GeneralAccountCode;
            return PartialView("BankAccount/_DynamicDDGeneralCode", model);
        }
        public JsonResult AddBankAccount(FM_Bank model)
        {
            BankAccountTable tblBankAccount = new BankAccountTable();
            tblBankAccount.BankID = model.BankAccountBankID;
            tblBankAccount.SubFundID = model.BankAccountFundTypeID;
            tblBankAccount.GeneralAccountID = model.BankAccountGeneralAccountNameID;
            tblBankAccount.AccountTypeID = model.BankAccountAccountTypeID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
            TOSSDB.BankAccountTables.Add(tblBankAccount);
            TOSSDB.SaveChanges();
            return Json("");
        }
        //Get Bank Account Name
        public ActionResult Get_UpdateBankAccount(FM_Bank model, int BankAccountID)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == BankAccountID select e).FirstOrDefault();
            model.getBankAccountColumns.BankAccountID = tblBankAccount.BankAccountID;
            model.getBankAccountColumns.AccountName = tblBankAccount.AccountName;
            model.getBankAccountColumns.AccountNo = tblBankAccount.AccountNo;
            model.BankAccountBankTempID = tblBankAccount.BankID;
            model.BankAccountAccountTypeTempID = tblBankAccount.AccountTypeID;
            model.BankAccountGeneralAccountNameTempID = tblBankAccount.GeneralAccountID;
            model.BankAccountFundTypeTempID = tblBankAccount.SubFundID;
            return PartialView("BankAccount/_UpdateBankAccount", model);
        }
        public ActionResult UpdateBankAccount(FM_Bank model)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == model.getBankAccountColumns.BankAccountID select e).FirstOrDefault();
            tblBankAccount.BankID = model.BankAccountBankID;
            tblBankAccount.SubFundID = model.BankAccountFundTypeID;
            tblBankAccount.GeneralAccountID = model.BankAccountGeneralAccountNameID;
            tblBankAccount.AccountTypeID = model.BankAccountAccountTypeID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
            TOSSDB.Entry(tblBankAccount);
            TOSSDB.SaveChanges();
            return PartialView("BankAccount/_UpdateBankAccount", model);
        }
        //Delete Bank Account Name
        public ActionResult DeleteBankAccount(FM_Bank model, int BankAccountID)
        {
            BankAccountTable tblBarangayName = (from e in TOSSDB.BankAccountTables where e.BankAccountID == BankAccountID select e).FirstOrDefault();
            TOSSDB.BankAccountTables.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}