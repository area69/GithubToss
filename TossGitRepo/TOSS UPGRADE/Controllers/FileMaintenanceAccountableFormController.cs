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
using TOSS_UPGRADE.Models.FM_AccountableForm;
namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceAccountableFormController : Controller
    {
        // GET: FileMaintenanceAccountableForm
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountableFormTab()
        {
            FM_AccountableForm model = new FM_AccountableForm();
            return PartialView("AccountableForm/AccountableFormIndex", model);
        }
        public ActionResult InventoryofAccountableFormTab()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/InventoryofAFIndex", model);
        }
        public ActionResult AssignmentofAccountableFormTab()
        {
            FM_AccountableForm model = new FM_AccountableForm();
            return PartialView("AssignmentofAccountableForm/AssignmentofAFIndex", model);
        }

        #region Accountable Form Details
            //Table Accountable Form
        public ActionResult Get_AccountableFormTable()
        {
            FM_AccountableForm model = new FM_AccountableForm();
            List<AccountableFormList> tbl_AccountableForm = new List<AccountableFormList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableFormTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new AccountableFormList()
                        {
                            AccountFormID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountFormName = GlobalFunction.ReturnEmptyString(dr[1]),
                            isCTC = GlobalFunction.ReturnEmptyBool(dr[3]),
                            Description = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormList = tbl_AccountableForm.ToList();
            return PartialView("AccountableForm/_AccountableFormTable", model.getAccountableFormList);
        }
        //Get Add Accountable Form Partial View
        public ActionResult Get_AddAccountableForm()
        {
            FM_AccountableForm model = new FM_AccountableForm();
            return PartialView("AccountableForm/_AddAccountableForm", model);
        }

        //Add Accountable Form
        public JsonResult AddAccountableForm(FM_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = new AccountableFormTable();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.AccountableFormTables.Add(tblAccountableForm);
            TOSSDB.SaveChanges();
            return Json(tblAccountableForm);
        }

        //Get Update Accountable Form
        public ActionResult Get_UpdateAccountableForm(FM_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            model.getAccountableFormcolumns.AccountFormID = tblAccountableForm.AccountFormID;
            model.getAccountableFormcolumns.AccountFormName = tblAccountableForm.AccountFormName;
            model.getAccountableFormcolumns.isCTC = tblAccountableForm.isCTC;
            model.getAccountableFormcolumns.Description = tblAccountableForm.Description;
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Update Accountable Form
        public ActionResult UpdateAccountableForm(FM_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == model.getAccountableFormcolumns.AccountFormID select e).FirstOrDefault();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.Entry(tblAccountableForm);
            TOSSDB.SaveChanges();
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableForm(FM_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            TOSSDB.AccountableFormTables.Remove(tblAccountableForm);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Inventory of Accountable Form
        #region OR
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTable()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 1 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormInvtList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormInvt.Add(new AccountableFormInvtList()
                        {
                            AFORID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyInt(dr[1]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StartingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyDateTime(dr[6]),
                            isIssued = GlobalFunction.ReturnEmptyBool(dr[7])
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormInvtList = tbl_AccountableFormInvt.ToList();
            return PartialView("InventoryofAccountableForm/OR/_ORDetailsTable", model.getAccountableFormInvtList);
        }
        //Get Add Accountable Form Inventory Partial View
        public ActionResult Get_AddInvntAccountableForm()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/OR/_AddORDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableform()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Cash Ticket" && s.Description != "Parking Ticket" orderby s.AccountFormName ascending select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult GetSelectedDynamicAccountableform(int AFIDTempID)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Cash Ticket" && s.Description != "Parking Ticket" orderby s.AccountFormName ascending select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempID;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        //Add Accountable Form Inventory
        public ActionResult AddAccountableFormInventory(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            foreach(var item in model.Accountable)
            {
                tblAccountableFormInventory.AccountFormID = item.AF;
                tblAccountableFormInventory.StubNo = item.StubNo;
                tblAccountableFormInventory.Quantity = item.Quantity;
                tblAccountableFormInventory.StartingOR = item.StartingOR;
                tblAccountableFormInventory.EndingOR = item.EndingOR;
                tblAccountableFormInventory.isIssued = item.isIssued;
                tblAccountableFormInventory.Date = item.Date;
                tblAccountableFormInventory.AFTableID = 1;
                TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
                TOSSDB.SaveChanges();
            }
            return Json(tblAccountableFormInventory, JsonRequestBehavior.AllowGet);
        }
        //Get Position Update
        public ActionResult Get_UpdateAccountableFormInventory(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StubNo = tblAccountableFormInventory.StubNo;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/OR/_UpdateORDetails", model);
        }
        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventory(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.getAccountableFormInvtcolumns.AFORID select e).FirstOrDefault();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return PartialView("InventoryofAccountableForm/OR/_UpdateORDetails", model);
        }
        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventory(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region CT
        //CT
        public ActionResult Get_AddInvntAccountableFormCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/CT/_AddCTDetails", model);
        }

        public ActionResult GetDynamicAccountableformCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description == "Cash Ticket" || s.Description == "Parking Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/CT/_DynamicDDAFCT", model);
        }
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTableCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 2 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormInvtList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormInvt.Add(new AccountableFormInvtList()
                        {
                            AFORID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyInt(dr[1]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StartingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyDateTime(dr[6]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormInvtList = tbl_AccountableFormInvt.ToList();
            return PartialView("InventoryofAccountableForm/CT/_CTDetailsTable", model.getAccountableFormInvtList);
        }
         
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventoryCT(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            foreach (var item in model.Accountable)
            {
                tblAccountableFormInventory.AccountFormID = item.AF;
                tblAccountableFormInventory.StubNo = item.StubNo;
                tblAccountableFormInventory.Quantity = item.Quantity;
                tblAccountableFormInventory.StartingOR = item.StartingOR;
                tblAccountableFormInventory.EndingOR = item.EndingOR;
                tblAccountableFormInventory.isIssued = item.isIssued;
                tblAccountableFormInventory.Date = item.Date;
                tblAccountableFormInventory.AFTableID = 2;
                TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
                TOSSDB.SaveChanges();
            }
            return Json(tblAccountableFormInventory, JsonRequestBehavior.AllowGet);
        }
        //Get Position Update
        public ActionResult GetSelectedDynamicAccountableformCT(int AFIDTempIDCT)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description == "Cash Ticket" || s.Description == "Parking Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCT;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult Get_UpdateAccountableFormInventoryCT(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.StubNo = tblAccountableFormInventory.StubNo;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/CT/_UpdateCTDetails", model);
        }
        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventoryCT(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.getAccountableFormInvtcolumns.AFORID select e).FirstOrDefault();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return PartialView("InventoryofAccountableForm/CT/_UpdateCTDetails", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventoryCT(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region CTC
        //CTC
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTableCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 3 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormInvtList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormInvt.Add(new AccountableFormInvtList()
                        {
                            AFORID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyInt(dr[1]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StartingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyDateTime(dr[6]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormInvtList = tbl_AccountableFormInvt.ToList();
            return PartialView("InventoryofAccountableForm/CTC/_CTCDetailsTable", model.getAccountableFormInvtList);
        }
        //Get Add Accountable Form Inventory Partial View
        public ActionResult Get_AddInvntAccountableFormCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/CTC/_AddCTCDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableformCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.Description != "Cash Ticket" && s.Description != "Parking Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/CTC/_DynamicDDAccountableFormCTC", model);
        }
        public ActionResult GetSelectedDynamicAccountableformCTC(int AFIDTempIDCTC)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCTC;
            return PartialView("InventoryofAccountableForm/CTC/_DynamicDDAccountableFormCTC", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventoryCTC(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            foreach (var item in model.Accountable)
            {
                tblAccountableFormInventory.AccountFormID = item.AF;
                tblAccountableFormInventory.StubNo = item.StubNo;
                tblAccountableFormInventory.Quantity = item.Quantity;
                tblAccountableFormInventory.StartingOR = item.StartingOR;
                tblAccountableFormInventory.EndingOR = item.EndingOR;
                tblAccountableFormInventory.isIssued = item.isIssued;
                tblAccountableFormInventory.Date = item.Date;
                tblAccountableFormInventory.AFTableID = 3;
                TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
                TOSSDB.SaveChanges();
            }
            return Json(tblAccountableFormInventory, JsonRequestBehavior.AllowGet);
        }
        //Get Position Update
        public ActionResult Get_UpdateAccountableFormInventoryCTC(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StubNo = tblAccountableFormInventory.StubNo;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/CTC/_UpdateCTCDetails", model);
        }

        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventoryCTC(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.getAccountableFormInvtcolumns.AFORID select e).FirstOrDefault();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return PartialView("InventoryofAccountableForm/CTC/_UpdateCTCDetails", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventoryCTC(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        //Assignment of Accountable Form
        #region Treasurer Collector
        public ActionResult Get_AddAssignTreasurerCollector()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_AddTreasurerCollector", model);
        }
        public ActionResult GetDynamicASFFundType()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundTitle = s.FundName }), "FundID", "FundTitle");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCFundType", model);
        }
        //public ActionResult GetDynamicAFASF()
        //{
        //    FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
        //    model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Inventory.ToList() where s.AccountableFormTable.isCTC == false && s.AccountableFormTable.AccountableForm_Description.Description != "Cash Ticket" select new { AFORID = s.AFORID, AccountFormName = s.AccountableFormTable.AccountFormName }), "AFORID", "AccountFormName");
        //    return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCAF", model);
        //}
        public ActionResult GetDynamicAFASF()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Parking Ticket" && s.Description != "Cash Ticket" orderby s.AccountFormName ascending select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCAF", model);
        }
        public ActionResult GetDynamicAFCollector()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.CollectorTables.ToList() select new { CollectorID = s.CollectorID, CollectorName = s.CollectorName }), "CollectorID", "CollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDCollectorOfficer", model);
        }
        public ActionResult GetDynamicAFStartOR(int AccountFormID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Inventory.ToList() where s.AccountFormID == AccountFormID && s.isIssued == false select new { AFORID = s.AFORID, StartingOR = s.StartingOR }), "AFORID", "StartingOR");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDStartOR", model);
        }
        public ActionResult Get_AddAssignTC(int AFORID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            AccountableForm_Inventory tblAFIventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            if (tblAFIventory != null)
            {
                model.AccountableFormAssignmentEndingORID = tblAFIventory.EndingOR;
                model.AccountableFormAssignmentQuantityID = tblAFIventory.Quantity;
                if (tblAFIventory.StubNo != 0)
                {
                    model.AccountableFormAssignmentStubNoID = Convert.ToInt32(tblAFIventory.StubNo);
                }
                else
                {
                    model.AccountableFormAssignmentStubNoID = 0;
                }

            }

            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_AddTreasurerCollectorInvt", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormAssign(FM_AccountableFormAssignment model)
        {
            AccountableForm_Assignment tblAccountableFormAssignment = new AccountableForm_Assignment();
            tblAccountableFormAssignment.FundID = model.AccountableFormAssignmentFundID;
            tblAccountableFormAssignment.AFORID = model.AccountableFormAssignmentID;
            tblAccountableFormAssignment.CollectorID = model.AccountableFACollectorID;
            tblAccountableFormAssignment.Date = model.getAccountableFormAssigncolumns.Date;
            TOSSDB.AccountableForm_Assignment.Add(tblAccountableFormAssignment);
            TOSSDB.SaveChanges();

            AccountableForm_Inventory tblAccountableFormAssignmentInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.AccountableFormAssignmentID select e).FirstOrDefault();
            if (tblAccountableFormAssignmentInventory.isIssued == false)
            {
                tblAccountableFormAssignmentInventory.isIssued = true;
            }
            else
            {
                tblAccountableFormAssignmentInventory.isIssued = false;
            }
            TOSSDB.Entry(tblAccountableFormAssignmentInventory);
            TOSSDB.SaveChanges();

            return Json("");
        }
        //public ActionResult GetAccountFormID(int AFORID)
        //{
        //    var AFI = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
        //    var result = new { AccountFormID = AFI.AccountFormID };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Get_AccountableFormAssignmentTable()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            List<AccountableFormAssignmentList> tbl_AccountableFormAss = new List<AccountableFormAssignmentList>();

            var SQLQuery = "SELECT AccountableForm_Assignment.AssignAFID,CollectorTable.CollectorName,dbo.AccountableForm_Assignment.Date,AccountableForm_Inventory.StubNo,AccountableForm_Inventory.StartingOR,AccountableForm_Inventory.EndingOR,AccountableForm_Inventory.Quantity, AccountableFormTable.AccountFormName,Fund.FundName, dbo.AccountableForm_Assignment.IsTransferred FROM DB_TOSS.dbo.AccountableForm_Assignment,dbo.AccountableForm_Inventory,dbo.CollectorTable,dbo.Fund,AccountableFormTable where AccountableForm_Inventory.AFORID = AccountableForm_Assignment.AFORID AND dbo.CollectorTable.CollectorID = AccountableForm_Assignment.CollectorID AND dbo.Fund.FundID = dbo.AccountableForm_Assignment.FundID AND dbo.AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID and AccountableForm_Assignment.IsTransferred IS NULL and AccountableForm_Assignment.IsConsumed IS NULL and AccountableForm_Assignment.IsDefault IS NULL";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormAssList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormAss.Add(new AccountableFormAssignmentList()
                        {
                            CollectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            AssignAFID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyString(dr[7]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[8]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormAssList = tbl_AccountableFormAss.ToList();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_TreasurerCollectorTable", model.getAccountableFormAssList);
        }
        public ActionResult Get_AddTransferReturnOR()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_AddTransferReturnOR", model);
        }
        public ActionResult Get_TransferReturnORTable()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            List<AFTransferReturnORList> tbl_AFTransferReturnOR = new List<AFTransferReturnORList>();

            var SQLQuery = "SELECT  AccountableForm_Assignment.AssignAFID,CollectorTable.CollectorName,dbo.AccountableForm_Assignment.Date,AccountableForm_Inventory.StubNo,AccountableForm_Inventory.StartingOR,AccountableForm_Inventory.EndingOR,AccountableForm_Inventory.Quantity, AccountableFormTable.AccountFormName,Fund.FundName, SubCollectorTable.SubCollectorName, AccountableForm_Assignment.IsTransferred FROM DB_TOSS.dbo.AccountableForm_Assignment,dbo.AccountableForm_Inventory,dbo.CollectorTable,dbo.Fund,AccountableFormTable ,dbo.SubCollectorTable where AccountableForm_Assignment.IsTransferred = 1 And AccountableForm_Inventory.AFORID = AccountableForm_Assignment.AFORID AND dbo.CollectorTable.CollectorID = AccountableForm_Assignment.CollectorID AND dbo.Fund.FundID = dbo.AccountableForm_Assignment.FundID AND dbo.AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID AND dbo.AccountableForm_Assignment.SubCollectorID = SubCollectorTable.SubCollectorID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AFTransferReturnORList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AFTransferReturnOR.Add(new AFTransferReturnORList()
                        {
                            AssignAFID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            CollectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            Date = GlobalFunction.ReturnEmptyString(dr[2]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            AF = GlobalFunction.ReturnEmptyString(dr[7]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[8]),
                            SubCollector = GlobalFunction.ReturnEmptyString(dr[9]),
                            IsTransferred = GlobalFunction.ReturnEmptyBool(dr[10]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAFTransferReturnORList = tbl_AFTransferReturnOR.ToList();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_TransferReturnORTable", model.getAFTransferReturnORList);
        }

        public ActionResult GetDynamicMainCollector()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.CollectorTables.ToList() select new { CollectorID = s.CollectorID, CollectorName = s.CollectorName }), "CollectorID", "CollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDMainCollector", model);
        }
        public ActionResult GetDynamicSubCollector(int CollectorID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.SubCollectorTables.ToList() where s.SubCollectorID != CollectorID select new { SubCollectorID = s.SubCollectorID, SubCollectorName = s.SubCollectorName }), "SubCollectorID", "SubCollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDSubCollector", model);
        }
        public ActionResult GetDynamicTCTRORStubNo(int CollectorID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Assignment.ToList() where s.CollectorID == CollectorID && s.IsTransferred == null select new { AssignAFID = s.AssignAFID, StubNo = s.AccountableForm_Inventory.StubNo }), "AssignAFID", "StubNo");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDTransferReturnORStubNo", model);
        }
        public JsonResult AddTransferReturnOR(FM_AccountableFormAssignment model)
        {
            AccountableForm_Assignment tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Assignment where e.AssignAFID == model.AccountableTCTRORStubNoID select e).FirstOrDefault();
            tblAccountableFormInventory.SubCollectorID = model.AccountableTCTRORSubCID;
            tblAccountableFormInventory.IsTransferred = true;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json("");
        }

        public ActionResult GetDynamicTCTRORPV(int StubNo)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            AccountableForm_Inventory tblAFIventory = (from e in TOSSDB.AccountableForm_Inventory where e.StubNo == StubNo select e).FirstOrDefault();
            if (tblAFIventory != null)
            {
                model.AccountableTCTRORStartingORID = tblAFIventory.StartingOR;
                model.AccountableTCTROREndingORID = tblAFIventory.EndingOR;
                model.AccountableTCTRORQuantityID = tblAFIventory.Quantity;
            }

            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_AddTransferReturnPVOR", model);
        }

        //Get Update AF Transfer / Return OR
        public ActionResult Get_UpdateAFTransferReturnOR(FM_AccountableFormAssignment model, int AssignAFID)
        {
            AccountableForm_Assignment tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Assignment where e.AssignAFID == AssignAFID select e).FirstOrDefault();
            tblAccountableFormInventory.SubCollectorID = null;
            tblAccountableFormInventory.IsTransferred = null;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Field Collector
        public ActionResult Get_AddAssignFieldCollector()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            return PartialView("AssignmentofAccountableForm/FieldCollector/_AddFieldCollector", model);
        }
        public ActionResult GetDynamicASFFundTypeFC()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundTitle = s.FundName }), "FundID", "FundName");
            return PartialView("AssignmentofAccountableForm/FieldCollector/_DynamicDDFCFundType", model);
        }
        public ActionResult GetDynamicAFASFFC()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("AssignmentofAccountableForm/FieldCollector/_DynamicDDFCAF", model);
        }
        public ActionResult GetDynamicAFCollectorFC()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.CollectorTables.ToList() select new { CollectorID = s.CollectorID, CollectorName = s.CollectorName }), "CollectorID", "CollectorName");
            return PartialView("AssignmentofAccountableForm/FieldCollector/_DynamicDDCollectorOfficerFC", model);
        }
        public ActionResult GetDynamicAFStartORFC(int AccountFormID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Inventory.ToList() where s.AccountFormID == AccountFormID && s.isIssued == false select new { AFORID = s.AFORID, StartingOR = s.StartingOR }), "AFORID", "StartingOR");
            return PartialView("AssignmentofAccountableForm/FieldCollector/_DynamicDDStartORFC", model);
        }
        public ActionResult Get_AddAssignAFFC(int AFORID)
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            AccountableForm_Inventory tblAFIventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            if (tblAFIventory != null)
            {
                model.AccountableFormAssignmentEndingORID = tblAFIventory.EndingOR;
                model.AccountableFormAssignmentQuantityID = tblAFIventory.Quantity;
                if (tblAFIventory.StubNo != 0)
                {
                    model.AccountableFormAssignmentStubNoID = Convert.ToInt32(tblAFIventory.StubNo);
                }
                else
                {
                    model.AccountableFormAssignmentStubNoID = 0;
                }

            }

            return PartialView("AssignmentofAccountableForm/FieldCollector/_AddFieldCollectorIvnt", model);
        }
        public ActionResult Get_AFAssignmentFieldCollectorTable()
        {
            FM_AccountableFormAssignment model = new FM_AccountableFormAssignment();
            List<AccountableFormAssignmentList> tbl_AccountableFormAss = new List<AccountableFormAssignmentList>();

            var SQLQuery = "SELECT AccountableForm_Assignment.AssignAFID,CollectorTable.CollectorName,dbo.AccountableForm_Assignment.Date,AccountableForm_Inventory.StubNo,AccountableForm_Inventory.StartingOR,AccountableForm_Inventory.EndingOR,AccountableForm_Inventory.Quantity, AccountableFormTable.AccountFormName,FundType_FundName.FundTitle, dbo.AccountableForm_Assignment.IsConsumed, dbo.AccountableForm_Assignment.IsDefault FROM DB_TOSS.dbo.AccountableForm_Assignment,dbo.AccountableForm_Inventory,dbo.CollectorTable,dbo.FundType_FundName,AccountableFormTable where AccountableForm_Inventory.AFORID = AccountableForm_Assignment.AFORID AND dbo.CollectorTable.CollectorID = AccountableForm_Assignment.CollectorID AND dbo.FundType_FundName.FundID = dbo.AccountableForm_Assignment.FundID AND dbo.AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID and AccountableForm_Assignment.IsTransferred IS NULL and AccountableForm_Assignment.IsConsumed IS NOT NULL and AccountableForm_Assignment.IsDefault IS NOT NULL";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormAssList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormAss.Add(new AccountableFormAssignmentList()
                        {
                            AssignAFID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyString(dr[7]),
                            CollectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[8]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyString(dr[2]),
                            isConsumed = GlobalFunction.ReturnEmptyBool(dr[9]),
                            isDefault = GlobalFunction.ReturnEmptyBool(dr[10]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormAssList = tbl_AccountableFormAss.ToList();
            return PartialView("AssignmentofAccountableForm/FieldCollector/_FieldCollectorTable", model.getAccountableFormAssList);
        }
        public JsonResult AddAccountableFormAssignFieldCollector(FM_AccountableFormAssignment model)
        {
            AccountableForm_Assignment tblAccountableFormAssignment = new AccountableForm_Assignment();
            tblAccountableFormAssignment.FundID = model.AccountableFormAssignmentFundID;
            tblAccountableFormAssignment.AFORID = model.AccountableFormAssignmentID;
            tblAccountableFormAssignment.CollectorID = model.AccountableFACollectorID;
            tblAccountableFormAssignment.Date = model.getAccountableFormAssigncolumns.Date;
            tblAccountableFormAssignment.IsConsumed = false;
            tblAccountableFormAssignment.IsDefault = false;
            TOSSDB.AccountableForm_Assignment.Add(tblAccountableFormAssignment);
            TOSSDB.SaveChanges();


            AccountableForm_Inventory tblAccountableFormAssignmentInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.AccountableFormAssignmentID select e).FirstOrDefault();
            if (tblAccountableFormAssignmentInventory.isIssued == false)
            {
                tblAccountableFormAssignmentInventory.isIssued = true;
            }
            else
            {
                tblAccountableFormAssignmentInventory.isIssued = false;
            }
            TOSSDB.Entry(tblAccountableFormAssignmentInventory);
            TOSSDB.SaveChanges();

            return Json("");
        }
        public ActionResult Get_UpdateAFAssignFieldCollector(FM_AccountableFormAssignment model, int AssignAFID)
        {
            AccountableForm_Assignment tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Assignment where e.AssignAFID == AssignAFID select e).FirstOrDefault();
            tblAccountableFormInventory.IsDefault = true;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }



        #endregion
    }
}