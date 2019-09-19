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
    }
}