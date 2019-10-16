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
using TOSS_UPGRADE.Models.FM_Barangay;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceBarangayController : Controller
    {
        // GET: FileMaintenanceBarangay
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BarangayNameTab()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayName/BarangayNameIndex", model);
        }
        public ActionResult BarangayCollectorTab()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayCollector/BarangayCollectorIndex", model);
        }
        public ActionResult BarangayBankAccountTab()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayBankAccount/BarangayBankAccountIndex", model);
        }

        #region Barangay
        //Barangay

        //Table Barangay Name
        public ActionResult Get_BarangayNameTable()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            List<BarangayNameList> tbl_AccountableForm = new List<BarangayNameList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Barangay_BarangayName";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayNameList()
                        {
                            BarangayID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BarangayName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayNameList = tbl_AccountableForm.ToList();
            return PartialView("BarangayName/_BarangayNameTable", model.getBarangayNameList);
        }
        //Get Add Barangay Name Partial View
        public ActionResult Get_AddBarangayName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayName/_AddBarangayName", model);
        }
        //Add Barangay Name
        public JsonResult AddBarangayName(FM_Barangay_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = new Barangay_BarangayName();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Barangay_BarangayName.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }
        //Get Barangay Name
        public ActionResult Get_UpdateBarangayName(FM_Barangay_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            model.getBarangayNamecolumns.BarangayID = tblBarangayName.BarangayID;
            model.getBarangayNamecolumns.BarangayName = tblBarangayName.BarangayName;
            return PartialView("BarangayName/_UpdateBarangayName", model);
        }
        public ActionResult UpdateBarangayName(FM_Barangay_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == model.getBarangayNamecolumns.BarangayID select e).FirstOrDefault();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("BarangayName/_UpdateBarangayName", model);
        }
        //Delete Barangay Name
        public ActionResult DeleteBarangayName(FM_Barangay_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            TOSSDB.Barangay_BarangayName.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion
        #region Barangay Collector Name

        //Table Barangay Collector Name
        public ActionResult Get_BarangayCollectorTable()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            List<BarangayCollectorList> tbl_AccountableForm = new List<BarangayCollectorList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Barangay_CollectorName";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayCollectorNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayCollectorList()
                        {
                            BarangayCollectorID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FirstName = GlobalFunction.ReturnEmptyString(dr[1]),
                            MiddleName = GlobalFunction.ReturnEmptyString(dr[2]),
                            LastName = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayCollectorList = tbl_AccountableForm.ToList();
            return PartialView("BarangayCollector/_BarangayCollectorTable", model.getBarangayCollectorList);
        }
        
        //Get Add Barangay Collector Partial View
        public ActionResult Get_AddCollectorName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayCollector/_AddBarangayCollector", model);
        }

        //Add Collector Name
        public JsonResult AddBarangayCollectorName(FM_Barangay_BarangayName model)
        {
            Barangay_CollectorName tblBarangayName = new Barangay_CollectorName();
            tblBarangayName.FirstName = model.getBarangayCollectorcolumns.FirstName;
            tblBarangayName.MiddleName = model.getBarangayCollectorcolumns.MiddleName;
            tblBarangayName.LastName = model.getBarangayCollectorcolumns.LastName;
            TOSSDB.Barangay_CollectorName.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }

        //Get Collector Name
        public ActionResult Get_UpdateCollectorName(FM_Barangay_BarangayName model, int BarangayCollectorID)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == BarangayCollectorID select e).FirstOrDefault();
            model.getBarangayCollectorcolumns.BarangayCollectorID = tblBarangayName.BarangayCollectorID;
            model.getBarangayCollectorcolumns.FirstName = tblBarangayName.FirstName;
            model.getBarangayCollectorcolumns.MiddleName = tblBarangayName.MiddleName;
            model.getBarangayCollectorcolumns.LastName = tblBarangayName.LastName;
            return PartialView("BarangayCollector/_UpdateBarangayCollector", model);
        }
        public ActionResult UpdateCollectorName(FM_Barangay_BarangayName model)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == model.getBarangayCollectorcolumns.BarangayCollectorID select e).FirstOrDefault();
            tblBarangayName.FirstName = model.getBarangayCollectorcolumns.FirstName;
            tblBarangayName.MiddleName = model.getBarangayCollectorcolumns.MiddleName;
            tblBarangayName.LastName = model.getBarangayCollectorcolumns.LastName;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("BarangayCollector/_UpdateBarangayCollector", model);
        }
        //Delete Collector Name
        public ActionResult DeleteCollectorName(FM_Barangay_BarangayName model, int BarangayCollectorID)
        {
            Barangay_CollectorName tblBarangayName = (from e in TOSSDB.Barangay_CollectorName where e.BarangayCollectorID == BarangayCollectorID select e).FirstOrDefault();
            TOSSDB.Barangay_CollectorName.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region BankBankAccount
        //Table Barangay Bank Account
        public ActionResult Get_BarangayBankAccountTable()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            List<BarangayBankAccountList> tbl_AccountableForm = new List<BarangayBankAccountList>();

            var SQLQuery = "SELECT BarangayBankAccountID,BarangayName,BankAccountTable.BankAccountID FROM DB_TOSS.dbo.Barangay_BarangayBankAccount,Barangay_BarangayName,BankAccountTable where dbo.Barangay_BarangayName.BarangayID = dbo.Barangay_BarangayBankAccount.BarangayID AND dbo.BankAccountTable.BankAccountID = dbo.Barangay_BarangayBankAccount.BankAccountID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayCollectorNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayBankAccountList()
                        {
                            BarangayBankAccountID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BarangayName = GlobalFunction.ReturnEmptyString(dr[1]),
                            BankAccountID = GlobalFunction.ReturnEmptyInt(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayBankAccountList = tbl_AccountableForm.ToList();
            return PartialView("BarangayBankAccount/_BarangayBankAccountTable", model.getBarangayBankAccountList);
        }
        //Get Add Barangay Collector Partial View
        public ActionResult Get_AddBarangayBankAccount()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            return PartialView("BarangayBankAccount/_AddBarangayBankAccount", model);
        }
        //Dropdown BarangayName
        public ActionResult GetDynamicBarangayName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.Barangay_BarangayName.ToList() select new { BarangayID = s.BarangayID, BarangayName = s.BarangayName }), "BarangayID", "BarangayName");
            return PartialView("BarangayBankAccount/_DynamicDDBarangayName", model);
        }

        //Dropdown BarangayName
        public ActionResult GetSelectedDynamicBarangayName(int BarangayTempID)
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.Barangay_BarangayName.ToList() select new { BarangayID = s.BarangayID, BarangayName = s.BarangayName }), "BarangayID", "BarangayName");
            model.BarangayID = BarangayTempID;
            return PartialView("BarangayBankAccount/_DynamicDDBarangayName", model);
        }
        //Dropdown Bank Account
        public ActionResult GetDynamicBarangayBankName()
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.BankTables.ToList() select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            return PartialView("BarangayBankAccount/_DynamicDDBarangayBankName", model);
        }
        public ActionResult GetSelectedDynamicBarangayBankName(int BankTempID)
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.BankTables.ToList() select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            model.BankID = BankTempID;
            return PartialView("BarangayBankAccount/_DynamicDDBarangayBankName", model);
        }
        //Dropdown Bank Account Number
        public ActionResult GetDynamicBarangayBankAccountNumber(int BankID)
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.BankAccountTables.ToList() where s.BankID == BankID select new { BankAccountID = s.BankAccountID, AccountNo = s.AccountNo }), "BankAccountID", "AccountNo");
            return PartialView("BarangayBankAccount/_DynamicDDBankAccountNumber", model);
        }
        public ActionResult GetSelectedDynamicBarangayBankAccountNumber(int BankID, int AccountNumberTempID)
        {
            FM_Barangay_BarangayName model = new FM_Barangay_BarangayName();
            model.BarangayBankAccountList = new SelectList((from s in TOSSDB.BankAccountTables.ToList() where s.BankID == BankID select new { BankAccountID = s.BankAccountID, AccountNo = s.AccountNo }), "BankAccountID", "AccountNo");
            model.AccountNumberID = AccountNumberTempID;
            return PartialView("BarangayBankAccount/_DynamicDDBankAccountNumber", model);
        }
        //Add Bank Account
        public JsonResult AddBarangayBankAccount(FM_Barangay_BarangayName model)
        {
            Barangay_BarangayBankAccount tblBarangayAccount = new Barangay_BarangayBankAccount();
            tblBarangayAccount.BarangayID = model.BarangayID;
            tblBarangayAccount.BankAccountID = model.AccountNumberID;
            TOSSDB.Barangay_BarangayBankAccount.Add(tblBarangayAccount);
            TOSSDB.SaveChanges();
            return Json(tblBarangayAccount);
        }

        //Get Collector Name
        public ActionResult Get_UpdateBarangayBankAccount(FM_Barangay_BarangayName model, int BarangayBankAccountID)
        {
            Barangay_BarangayBankAccount tblBarangayName = (from e in TOSSDB.Barangay_BarangayBankAccount where e.BarangayBankAccountID == BarangayBankAccountID select e).FirstOrDefault();
            model.getBarangayBankAccountcolumns.BarangayBankAccountID = tblBarangayName.BarangayBankAccountID;
            model.BarangayTempID = tblBarangayName.BarangayID;
            model.BankTempID = tblBarangayName.BankAccountTable.BankTable.BankID;
            model.AccountNumberTempID = tblBarangayName.BankAccountID;
            return PartialView("BarangayBankAccount/_UpdateBarangayBankAccount", model);
        }
        #endregion
    }
}