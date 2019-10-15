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
using TOSS_UPGRADE.Models.FM_ChartOfAccounts;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceChartofAccountController : Controller
    {
        // GET: FileMaintenanceChartofAccount   
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RevisionYearTab()
        {
            FM_ChartOfAccounts_RevisionYear model = new FM_ChartOfAccounts_RevisionYear();
            return PartialView("RevisionYear/RevisionIndex", model);
        }
        public ActionResult AllotmentClassTab()
        {
            FM_ChartOfAccounts_AllotmentClass model = new FM_ChartOfAccounts_AllotmentClass();
            return PartialView("AllotmentClass/AllotmentClassIndex", model);
        }
        public ActionResult AccountGroupTab()
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            return PartialView("AccountGroup/AccountGroupIndex", model);
        }
        public ActionResult MajorAccountGroupTab()
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            return PartialView("MajorAccountGroup/MajorAccountGroupIndex", model);
        }
        public ActionResult SubMajorAccountGroupTab()
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            return PartialView("SubMajorAccountGroup/SubMajorAccountGroupIndex", model);
        }
        public ActionResult GeneralAccountGroupTab()
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            return PartialView("GeneralAccount/GeneralAccountIndex", model);
        }
        public ActionResult SubsidiaryLedgerTab()
        {
            FM_ChartOfAccounts_SubsidiaryLedger model = new FM_ChartOfAccounts_SubsidiaryLedger();
            return PartialView("SubsidiaryLedger/SubsidiaryLedgerIndex", model);
        }
        #region RevisionYear
        //Table Revision Year
        public ActionResult Get_RevisionYearTable()
        {
            FM_ChartOfAccounts_RevisionYear model = new FM_ChartOfAccounts_RevisionYear();
            List<RevisionYearList> tbl_RevisionYear = new List<RevisionYearList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.RevisionYear";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_RevisionYearList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_RevisionYear.Add(new RevisionYearList()
                        {
                            RevisionYearID = GlobalFunction.ReturnEmptyString(dr[0]),
                            RevisionYear = GlobalFunction.ReturnEmptyString(dr[1]),
                            IsUsed = GlobalFunction.ReturnEmptyBool(dr[2]),
                            Remarks = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getRevisionYearList = tbl_RevisionYear.ToList();
            return PartialView("RevisionYear/_RevisionYearTable", model.getRevisionYearList);
        }
        //Get Add Revision Year Partial View
        public ActionResult Get_AddRevisionYear()
        {
            FM_ChartOfAccounts_RevisionYear model = new FM_ChartOfAccounts_RevisionYear();
            return PartialView("RevisionYear/_AddRevisionYear", model);
        }
        //Add Revision Year
        public JsonResult AddRevisionYear(FM_ChartOfAccounts_RevisionYear model)
        {
            RevisionYear tblBarangayName = new RevisionYear();
            tblBarangayName.RevisionYearDate = model.getRevisionYearcolumns.RevisionYearDate;
            tblBarangayName.IsUsed = model.IsUsed;
            tblBarangayName.Remarks = model.getRevisionYearcolumns.Remarks;
            TOSSDB.RevisionYears.Add(tblBarangayName);

            AllotmentClass tblAllotment = new AllotmentClass();
            tblAllotment.RevisionYearID = model.getRevisionYearcolumns.RevisionYearID;
            tblAllotment.AllotmentClassName = "N/A";
            TOSSDB.AllotmentClasses.Add(tblAllotment);
            TOSSDB.SaveChanges();
            return Json("");
        }
        //Get Revision Year
        public ActionResult Get_UpdateRevisionYear(FM_ChartOfAccounts_RevisionYear model, int RevisionYearID)
        {
            RevisionYear tblBarangayName = (from e in TOSSDB.RevisionYears where e.RevisionYearID == RevisionYearID select e).FirstOrDefault();
            model.getRevisionYearcolumns.RevisionYearID = tblBarangayName.RevisionYearID;
            model.getRevisionYearcolumns.RevisionYearDate = tblBarangayName.RevisionYearDate;
            model.IsUsed = tblBarangayName.IsUsed;
            model.getRevisionYearcolumns.Remarks = tblBarangayName.Remarks;
            return PartialView("RevisionYear/_UpdateRevisionYear", model);
        }
        public ActionResult UpdateRevisionYear(FM_ChartOfAccounts_RevisionYear model)
        {
            RevisionYear tblBarangayName = (from e in TOSSDB.RevisionYears where e.RevisionYearID == model.getRevisionYearcolumns.RevisionYearID select e).FirstOrDefault();
            tblBarangayName.RevisionYearDate = model.getRevisionYearcolumns.RevisionYearDate;
            tblBarangayName.IsUsed = model.IsUsed;
            tblBarangayName.Remarks = model.getRevisionYearcolumns.Remarks;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("RevisionYear/_UpdateRevisionYear", model);
        }
        //Delete Revision Year
        public ActionResult DeleteRevisionYear(FM_ChartOfAccounts_RevisionYear model, int RevisionYearID)
        {
            RevisionYear tblRevisionYear = (from e in TOSSDB.RevisionYears where e.RevisionYearID == RevisionYearID select e).FirstOrDefault();
            TOSSDB.RevisionYears.Remove(tblRevisionYear);

            AllotmentClass tblAllotment = (from e in TOSSDB.AllotmentClasses where e.RevisionYearID == RevisionYearID select e).FirstOrDefault();
            TOSSDB.AllotmentClasses.Remove(tblAllotment);
            TOSSDB.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
        #region Allotment Class
        //Table Allotment Class
        public ActionResult Get_AllotmentClassTable()
        {
            FM_ChartOfAccounts_AllotmentClass model = new FM_ChartOfAccounts_AllotmentClass();
            List<AllotmentClassList> tbl_AllotmentClass = new List<AllotmentClassList>();

            var SQLQuery = "SELECT AllotmentClassID,RevisionYearDate,AllotmentClassName,IsUsed FROM DB_TOSS.dbo.AllotmentClass,dbo.RevisionYear where RevisionYear.RevisionYearID = AllotmentClass.RevisionYearID AND IsUsed = 1 AND AllotmentClassName != 'N/A'  order by AllotmentClassName asc;";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AllotmentClassList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AllotmentClass.Add(new AllotmentClassList()
                        {
                            AllotmentClassID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            RevisionYearDate = GlobalFunction.ReturnEmptyString(dr[1]),
                            AllotmentClass = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAllotmentClassList = tbl_AllotmentClass.ToList();
            return PartialView("AllotmentClass/_AllotmentClassTable", model.getAllotmentClassList);
        }
        //Get Add Allotment Class Partial View
        public ActionResult Get_AddAllotmentClass()
        {
            FM_ChartOfAccounts_AllotmentClass model = new FM_ChartOfAccounts_AllotmentClass();
            return PartialView("AllotmentClass/_AddAllotmentClass", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicRevisionYear()
        {
            FM_ChartOfAccounts_AllotmentClass model = new FM_ChartOfAccounts_AllotmentClass();
            model.AllotmentClassList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("AllotmentClass/_DynamicDDRevisionYearDate", model);
        }
        //Dropdown Revision Year
        public ActionResult GetSelectedDynamicRevisionYear(int RevisionYearDateTempID)
        {
            FM_ChartOfAccounts_AllotmentClass model = new FM_ChartOfAccounts_AllotmentClass();
            model.AllotmentClassList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID;
            return PartialView("AllotmentClass/_DynamicDDRevisionYearDate", model);
        }
        //Add Allotment Class
        public JsonResult AddAllotmentClass(FM_ChartOfAccounts_AllotmentClass model)
        {
            AllotmentClass tblAllotmentClass = new AllotmentClass();
            tblAllotmentClass.RevisionYearID = model.RevisionYearDate;
            tblAllotmentClass.AllotmentClassName = model.getAllotmentClasscolumns.AllotmentClassName;
            TOSSDB.AllotmentClasses.Add(tblAllotmentClass);
            TOSSDB.SaveChanges();
            return Json(tblAllotmentClass);
        }
        //Get Allotment Class
        public ActionResult Get_UpdateAllotmentClass(FM_ChartOfAccounts_AllotmentClass model, int AllotmentClassID)
        {
            AllotmentClass tblAllotmentClass = (from e in TOSSDB.AllotmentClasses where e.AllotmentClassID == AllotmentClassID select e).FirstOrDefault();
            model.getAllotmentClasscolumns.AllotmentClassID = tblAllotmentClass.AllotmentClassID;
            model.RevisionYearDateTempID = tblAllotmentClass.RevisionYearID;
            model.getAllotmentClasscolumns.AllotmentClassName = tblAllotmentClass.AllotmentClassName;
            return PartialView("AllotmentClass/_UpdateAllotmentClass", model);
        }
        public ActionResult UpdateAllotmentClass(FM_ChartOfAccounts_AllotmentClass model)
        {
            AllotmentClass tblAllotmentClass = (from e in TOSSDB.AllotmentClasses where e.AllotmentClassID == model.getAllotmentClasscolumns.AllotmentClassID select e).FirstOrDefault();
            tblAllotmentClass.RevisionYearID = model.RevisionYearDate;
            tblAllotmentClass.AllotmentClassName = model.getAllotmentClasscolumns.AllotmentClassName;
            TOSSDB.Entry(tblAllotmentClass);
            TOSSDB.SaveChanges();
            return PartialView("AllotmentClass/_UpdateAllotmentClass", model);
        }
        //Delete Allotment Class
        public ActionResult DeleteAllotmentClass(FM_ChartOfAccounts_AllotmentClass model, int AllotmentClassID)
        {
            AllotmentClass tblAllotment = (from e in TOSSDB.AllotmentClasses where e.AllotmentClassID == AllotmentClassID select e).FirstOrDefault();
            TOSSDB.AllotmentClasses.Remove(tblAllotment);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Account Group
        //Table Account Group
        public ActionResult Get_AccountGroupTable()
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            List<AccountGroupList> tbl_AllotmentClass = new List<AccountGroupList>();

            var SQLQuery = "SELECT AccountGroupID,AccountGroupName,AccountGroupCode,AllotmentClass.RevisionYearID,AllotmentClassName FROM DB_TOSS.dbo.AccountGroup,AllotmentClass where AllotmentClass.AllotmentClassID = AccountGroup.AllotmentClassID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountGroupList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AllotmentClass.Add(new AccountGroupList()
                        {
                            AccountGroupID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountGroupName = GlobalFunction.ReturnEmptyString(dr[1]),
                            AccountGroupCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            RevisionYearDate = GlobalFunction.ReturnEmptyInt(dr[3]),
                            AllotmentClass = GlobalFunction.ReturnEmptyString(dr[4]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountGroupList = tbl_AllotmentClass.ToList();
            return PartialView("AccountGroup/_AccountGroupTable", model.getAccountGroupList);
        }
        //Get Add Allotment Class Partial View
        public ActionResult Get_AddAccountGroup()
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            return PartialView("AccountGroup/_AddAccountGroup", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicAccountGroupRevisionYear()
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            model.AccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("AccountGroup/_DynamicDDRevisionYearDate", model);
        }
        public ActionResult GetSelectedDynamicAccountGroupRevisionYear(int RevisionYearDateTempID)
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            model.AccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true || s.RevisionYearID == RevisionYearDateTempID select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID;
            return PartialView("AccountGroup/_DynamicDDRevisionYearDate", model);
        }
        //Dropdown Allotment Class
        public ActionResult GetDynamicAccountGroupAllotmentClass(int RevisionYearID)
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            model.AccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            return PartialView("AccountGroup/_DynamicDDAllotmentClassName", model);
        }
        public ActionResult GetSelectedDynamicAccountGroupAllotmentClass(int RevisionYearID,int AllotmentClassIDTempID)
        {
            FM_ChartOfAccounts_AccountGroup model = new FM_ChartOfAccounts_AccountGroup();
            model.AccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.Where(a => a.RevisionYearID == RevisionYearID).ToList() select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            model.AllotmentClassID = AllotmentClassIDTempID;
            return PartialView("AccountGroup/_DynamicDDAllotmentClassName", model);
        }
        //Add Allotment Class
        public JsonResult AddAccountGroup(FM_ChartOfAccounts_AccountGroup model)
        {
            AccountGroup tblAccountGroup = new AccountGroup();
            tblAccountGroup.AllotmentClassID = model.AllotmentClassID;
            tblAccountGroup.AccountGroupName = model.getAccountGroupcolumns.AccountGroupName;
            tblAccountGroup.AccountGroupCode = model.getAccountGroupcolumns.AccountGroupCode;
            TOSSDB.AccountGroups.Add(tblAccountGroup);
            TOSSDB.SaveChanges();
            return Json(tblAccountGroup);
        }
        //Get Allotment Class
        public ActionResult Get_UpdateAccountGroup(FM_ChartOfAccounts_AccountGroup model, int AccountGroupID)
        {
            AccountGroup tblAllotmentClass = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            model.getAccountGroupcolumns.AccountGroupID = tblAllotmentClass.AccountGroupID;
            model.AllotmentClassIDTempID = tblAllotmentClass.AllotmentClassID;
            model.RevisionYearDateTempID = tblAllotmentClass.AllotmentClass.RevisionYear.RevisionYearID;
            model.getAccountGroupcolumns.AccountGroupName = tblAllotmentClass.AccountGroupName;
            model.getAccountGroupcolumns.AccountGroupCode = tblAllotmentClass.AccountGroupCode;
            return PartialView("AccountGroup/_UpdateAccountGroup", model);
        }
        public ActionResult UpdateAccountGroup(FM_ChartOfAccounts_AccountGroup model)
        {
            AccountGroup tblAccountGroup = (from e in TOSSDB.AccountGroups where e.AccountGroupID == model.getAccountGroupcolumns.AccountGroupID select e).FirstOrDefault();
            tblAccountGroup.AllotmentClassID = model.AllotmentClassID;
            tblAccountGroup.AccountGroupName = model.getAccountGroupcolumns.AccountGroupName;
            tblAccountGroup.AccountGroupCode = model.getAccountGroupcolumns.AccountGroupCode;
            TOSSDB.Entry(tblAccountGroup);
            TOSSDB.SaveChanges();
            return PartialView("AccountGroup/_UpdateAccountGroup", model);
        }
        //Delete Allotment Class
        public ActionResult DeleteAccountGroup(FM_ChartOfAccounts_AccountGroup model, int AccountGroupID)
        {
            AccountGroup tblBarangayName = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            TOSSDB.AccountGroups.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Major Account Group
        //Table Major Account Group
        public ActionResult Get_MajorAccountGroupTable()
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            List<MajorAccountGroupList> tbl_AllotmentClass = new List<MajorAccountGroupList>();

            var SQLQuery = "SELECT MajorAccountGroupID,MajorAccountGroupName,MajorAccountGroupCode,AccountGroup.AllotmentClassID,AccountGroupCode,AccountGroupName FROM DB_TOSS.dbo.MajorAccountGroup,AccountGroup where AccountGroup.AccountGroupID = MajorAccountGroup.AccountGroupID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountGroupList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AllotmentClass.Add(new MajorAccountGroupList()
                        {
                            MajorAccountGroupID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            MajorAccountGroupName = GlobalFunction.ReturnEmptyString(dr[1]),
                            MajorAccountGroupCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            //RevisionYearDate = GlobalFunction.ReturnEmptyString(dr[3]),
                            AccountGroupCode = GlobalFunction.ReturnEmptyString(dr[4]),
                            AllotmentClass = GlobalFunction.ReturnEmptyInt(dr[3]),
                            AccountGroupName = GlobalFunction.ReturnEmptyString(dr[5])
                        });
                    }
                }
                Connection.Close();
            }
            model.getMajorAccountGroupList = tbl_AllotmentClass.ToList();
            return PartialView("MajorAccountGroup/_MajorAccountGroupTable", model.getMajorAccountGroupList);
        }
        //Get Add Major Account Group Partial View
        public ActionResult Get_MajorAddAccountGroup()
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            return PartialView("MajorAccountGroup/_AddMajorAccountGroup", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicMajorAccountGroupRevisionYear()
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("MajorAccountGroup/_DynamicDDRevisionYearDate", model);
        }

        public ActionResult GetSelectedDynamicMajorAccountGroupRevisionYear(int RevisionYearDateTempID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true || s.RevisionYearID == RevisionYearDateTempID select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID;
            return PartialView("MajorAccountGroup/_DynamicDDRevisionYearDate", model);
        }
        //Dropdown Allotment Class
        public ActionResult GetDynamicMajorAccountGroupAllotmentClass(int RevisionYearID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            return PartialView("MajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        public ActionResult GetSelectedDynamicMajorAccountGroupAllotmentClass(int RevisionYearID, int AllotmentClassIDTempID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID || s.AllotmentClassID == AllotmentClassIDTempID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            model.AllotmentClassID = AllotmentClassIDTempID;
            return PartialView("MajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        //Dropdown Account Group
        public ActionResult GetDynamicMajorAccountGroupAccountGroupName(int AllotmentClassID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            return PartialView("MajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        public ActionResult GetSelectedDynamicMajorAccountGroupAccountGroupName(int AllotmentClassID,int AccountGroupIDTempID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            model.AccountGroupID = AccountGroupIDTempID;
            return PartialView("MajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        //Dropdown Account Group Code
        public ActionResult GetMajorAccountCodeField(int AccountGroupID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            AccountGroup tblSector = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            model.AccountGroupCodeID = tblSector.AccountGroupCode + " -";
            return PartialView("MajorAccountGroup/_DynamicDDAccountGroupCode", model);
        }
        //Add Major Account Group Year
        public JsonResult AddMajorAccountGroup(FM_ChartOfAccounts_MajorAccountGroup model)
        {
            MajorAccountGroup tblMajorAccountGroup = new MajorAccountGroup();
            tblMajorAccountGroup.AccountGroupID = model.AccountGroupID;
            //tblMajorAccountGroup.RevisionYearID = model.RevisionYearDate;
            //tblMajorAccountGroup.AllotmentClassID = model.AllotmentClassID;
            tblMajorAccountGroup.MajorAccountGroupName = model.getMajorAccountGroupcolumns.MajorAccountGroupName;
            tblMajorAccountGroup.MajorAccountGroupCode = model.getMajorAccountGroupcolumns.MajorAccountGroupCode;
            TOSSDB.MajorAccountGroups.Add(tblMajorAccountGroup);
            TOSSDB.SaveChanges();
            return Json("");
        }
        //Get Major Account Group
        public ActionResult Get_UpdateMajorAccountGroup(FM_ChartOfAccounts_MajorAccountGroup model, int MajorAccountGroupID)
        {
            MajorAccountGroup tblMajorAccountGroup = (from e in TOSSDB.MajorAccountGroups where e.MajorAccountGroupID == MajorAccountGroupID select e).FirstOrDefault();
            model.getMajorAccountGroupcolumns.MajorAccountGroupID = tblMajorAccountGroup.MajorAccountGroupID;
            model.AccountGroupIDTempID = tblMajorAccountGroup.AccountGroupID;
            model.AllotmentClassIDTempID = tblMajorAccountGroup.AccountGroup.AllotmentClass.AllotmentClassID;
            model.RevisionYearDateTempID = tblMajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID;
            model.getMajorAccountGroupcolumns.MajorAccountGroupName = tblMajorAccountGroup.MajorAccountGroupName;
            model.getMajorAccountGroupcolumns.MajorAccountGroupCode = tblMajorAccountGroup.MajorAccountGroupCode;
            return PartialView("MajorAccountGroup/_UpdateMajorAccountGroup", model);
        }
        public ActionResult UpdateMajorAccountGroup(FM_ChartOfAccounts_MajorAccountGroup model)
        {
            MajorAccountGroup tblMajorAccountGroup = (from e in TOSSDB.MajorAccountGroups where e.MajorAccountGroupID == model.getMajorAccountGroupcolumns.MajorAccountGroupID select e).FirstOrDefault();
            tblMajorAccountGroup.AccountGroupID = model.AccountGroupID;
            tblMajorAccountGroup.MajorAccountGroupName = model.getMajorAccountGroupcolumns.MajorAccountGroupName;
            tblMajorAccountGroup.MajorAccountGroupCode = model.getMajorAccountGroupcolumns.MajorAccountGroupCode;
            TOSSDB.Entry(tblMajorAccountGroup);
            TOSSDB.SaveChanges();
            return PartialView("MajorAccountGroup/_UpdateMajorAccountGroup", model);
        }
        //Delete Major Account Group
        public ActionResult DeleteMajorAccountGroup(FM_ChartOfAccounts_MajorAccountGroup model, int MajorAccountGroupID)
        {
            MajorAccountGroup tblMajorAccountGroup = (from e in TOSSDB.MajorAccountGroups where e.MajorAccountGroupID == MajorAccountGroupID select e).FirstOrDefault();
            TOSSDB.MajorAccountGroups.Remove(tblMajorAccountGroup);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region SubMajor Account Group
        //Table Major Account Group
        public ActionResult Get_SubMajorAccountGroupTable()
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            List<SubMajorAccountGroupList> tbl_SubMajorAccount = new List<SubMajorAccountGroupList>();

            var SQLQuery = "SELECT SubMajorAccountGroupID,SubMajorAccountGroupName,SubMajorAccountGroupCode,MajorAccountGroupName,MajorAccountGroupCode,MajorAccountGroup.AccountGroupID FROM DB_TOSS.dbo.SubMajorAccountGroup,MajorAccountGroup where MajorAccountGroup.MajorAccountGroupID = SubMajorAccountGroup.MajorAccountGroupID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountGroupList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_SubMajorAccount.Add(new SubMajorAccountGroupList()
                        {
                            SubMajorAccountGroupID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SubMajorAccountGroupName = GlobalFunction.ReturnEmptyString(dr[1]),
                            MajorAccountGroupCode = GlobalFunction.ReturnEmptyString(dr[4]),
                            MajorAccountGroupName = GlobalFunction.ReturnEmptyString(dr[3]),
                            SubMajorAccountGroupCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            AccountGroupName = GlobalFunction.ReturnEmptyInt(dr[5])
                        });
                    }
                }
                Connection.Close();
            }
            model.getSubMajorAccountGroupList = tbl_SubMajorAccount.ToList();
            return PartialView("SubMajorAccountGroup/_SubMajorAccountGroupTable", model.getSubMajorAccountGroupList);
        }
        //Get Add Sub Major Account Group Partial View
        public ActionResult Get_SubMajorAddAccountGroup()
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            return PartialView("SubMajorAccountGroup/_AddSubMajorAccountGroup", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicSubMajorAccountGroupRevisionYear()
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("SubMajorAccountGroup/_DynamicDDRevisionYearDate", model);
        }
        public ActionResult GetSelectedDynamicSubMajorAccountGroupRevisionYear(int RevisionYearDateTempID2)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true || s.RevisionYearID == RevisionYearDateTempID2 select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID2;
            return PartialView("SubMajorAccountGroup/_DynamicDDRevisionYearDate", model);
        }
        //Dropdown Allotment Class
        public ActionResult GetDynamicSubMajorAccountGroupAllotmentClass(int RevisionYearID)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            return PartialView("SubMajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        public ActionResult GetSelectedDynamicSubMajorAccountGroupAllotmentClass(int RevisionYearID, int AllotmentClassIDTempID2)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID || s.AllotmentClassID == AllotmentClassIDTempID2 orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            model.AllotmentClassID = AllotmentClassIDTempID2;
            return PartialView("SubMajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        //Dropdown Account Group
        public ActionResult GetDynamicSubMajorAccountGroupAccountGroupName(int AllotmentClassID)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            return PartialView("SubMajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        public ActionResult GetSelectedDynamicSubMajorAccountGroupAccountGroupName(int AllotmentClassID, int AccountGroupIDTempID2)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID || a.AccountGroupID == AccountGroupIDTempID2).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            model.AccountGroupID = AccountGroupIDTempID2;
            return PartialView("SubMajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        public ActionResult GetDynamicSubMajorAccountGroupMajorAccountGroupName(int AccountGroupID)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.MajorAccountGroups.Where(a => a.AccountGroupID == AccountGroupID).ToList() select new { MajorAccountGroupID = s.MajorAccountGroupID, MajorAccountGroupName = s.MajorAccountGroupName }), "MajorAccountGroupID", "MajorAccountGroupName");
            return PartialView("SubMajorAccountGroup/_DynamicDDMajorAccountName", model);
        }
        public ActionResult GetSelectedDynamicSubMajorAccountGroupMajorAccountGroupName(int AccountGroupID, int MajorAccountGroupNameTempID2)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            model.SubMajorAccountGroupList = new SelectList((from s in TOSSDB.MajorAccountGroups.Where(a => a.AccountGroupID == AccountGroupID).ToList() select new { MajorAccountGroupID = s.MajorAccountGroupID, MajorAccountGroupName = s.MajorAccountGroupName }), "MajorAccountGroupID", "MajorAccountGroupName");
            model.MajorAccountGroupNameID = MajorAccountGroupNameTempID2;
            return PartialView("SubMajorAccountGroup/_DynamicDDMajorAccountName", model);
        }
        //Dropdown Account Group Code
        public ActionResult GetSubMajorAccountCodeField(int AccountGroupID)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            AccountGroup tblSector = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            model.AccountGroupCodeID = tblSector.AccountGroupCode + " - ";
            return PartialView("SubMajorAccountGroup/_DynamicDDAccountGroupCode", model);
        }
        public ActionResult GetSubMajorAccountCodeField2(int MajorAccountGroupID)
        {
            FM_ChartOfAccounts_SubMajorAccountGroup model = new FM_ChartOfAccounts_SubMajorAccountGroup();
            MajorAccountGroup tblSector = (from e in TOSSDB.MajorAccountGroups where e.MajorAccountGroupID == MajorAccountGroupID select e).FirstOrDefault();
            model.MajorAccountGroupCodeID = tblSector.MajorAccountGroupCode + " - ";
            return PartialView("SubMajorAccountGroup/_DynamicDDMajorAccountGroupCode", model);
        }
        public JsonResult AddSubMajorAccountGroup(FM_ChartOfAccounts_SubMajorAccountGroup model)
        {
            SubMajorAccountGroup tblSubMajorAccountGroup = new SubMajorAccountGroup();
            tblSubMajorAccountGroup.SubMajorAccountGroupID = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupID;
            tblSubMajorAccountGroup.SubMajorAccountGroupName = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupName;
            tblSubMajorAccountGroup.SubMajorAccountGroupCode = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupCode;
            tblSubMajorAccountGroup.MajorAccountGroupID = Convert.ToInt32(model.MajorAccountGroupNameID);
            TOSSDB.SubMajorAccountGroups.Add(tblSubMajorAccountGroup);
            TOSSDB.SaveChanges();
            return Json("");
        }
        public ActionResult Get_UpdateSubMajorAccountGroup(FM_ChartOfAccounts_SubMajorAccountGroup model, int SubMajorAccountGroupID)
        {
            SubMajorAccountGroup tblSubMajorAccountGroup = (from e in TOSSDB.SubMajorAccountGroups where e.SubMajorAccountGroupID == SubMajorAccountGroupID select e).FirstOrDefault();
            model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupID = tblSubMajorAccountGroup.SubMajorAccountGroupID;
            model.AccountGroupIDTempID2 = tblSubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupID;
            model.AllotmentClassIDTempID2 = tblSubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.AllotmentClassID;
            model.RevisionYearDateTempID2 = tblSubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID;
            model.MajorAccountGroupNameTempID2 = tblSubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupID;
            model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupName = tblSubMajorAccountGroup.SubMajorAccountGroupName;
            model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupCode = tblSubMajorAccountGroup.SubMajorAccountGroupCode;
            return PartialView("SubMajorAccountGroup/_UpdateSubMajorAccountGroup", model);
        }
        public ActionResult UpdateSubMajorAccountGroup(FM_ChartOfAccounts_SubMajorAccountGroup model)
        {
            SubMajorAccountGroup tblSubMajorAccountGroup = (from e in TOSSDB.SubMajorAccountGroups where e.SubMajorAccountGroupID == model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupID select e).FirstOrDefault();
            tblSubMajorAccountGroup.SubMajorAccountGroupID = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupID;
            tblSubMajorAccountGroup.SubMajorAccountGroupName = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupName;
            tblSubMajorAccountGroup.SubMajorAccountGroupCode = model.getSubMajorAccountGroupcolumns.SubMajorAccountGroupCode;
            tblSubMajorAccountGroup.MajorAccountGroupID = Convert.ToInt32(model.MajorAccountGroupNameID);
            TOSSDB.Entry(tblSubMajorAccountGroup);
            TOSSDB.SaveChanges();
            return PartialView("SubMajorAccountGroup/_UpdateSubMajorAccountGroup", model);
        }
        //Delete Major Account Group
        public ActionResult DeleteSubMajorAccountGroup(FM_ChartOfAccounts_SubMajorAccountGroup model, int SubMajorAccountGroupID)
        {
            SubMajorAccountGroup tblMajorAccountGroup = (from e in TOSSDB.SubMajorAccountGroups where e.SubMajorAccountGroupID == SubMajorAccountGroupID select e).FirstOrDefault();
            TOSSDB.SubMajorAccountGroups.Remove(tblMajorAccountGroup);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region General Account

        //Table General Account Group
        public ActionResult Get_GeneralAccountTable()
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            List<GeneralAccountList> tbl_GeneralAccount = new List<GeneralAccountList>();

            var SQLQuery = "SELECT GeneralAccountID,GeneralAccountName,GeneralAccountCode,SubMajorAccountGroup.MajorAccountGroupID,SubMajorAccountGroupName,SubMajorAccountGroupCode,isReserve,ReservePercent,isFullReserve,isContinuing,isOBRCashAdvance,isNormalBalance,isStatus FROM DB_TOSS.dbo.GeneralAccount,SubMajorAccountGroup WHERE dbo.SubMajorAccountGroup.SubMajorAccountGroupID = dbo.GeneralAccount.SubMajorAccountGroupID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountGroupList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_GeneralAccount.Add(new GeneralAccountList()
                        {
                            GeneralAccountID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            GeneralAccountName = GlobalFunction.ReturnEmptyString(dr[1]),
                            GeneralAccountCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            MajorAccountGroupName = GlobalFunction.ReturnEmptyInt(dr[3]),
                            SubMajorAccountGroupName = GlobalFunction.ReturnEmptyString(dr[4]),
                            SubMajorAccountGroupCode = GlobalFunction.ReturnEmptyString(dr[5])
                        });
                    }
                }
                Connection.Close();
            }
            model.getGeneralAccountList = tbl_GeneralAccount.ToList();
            return PartialView("GeneralAccount/_GeneralAccountTable", model.getGeneralAccountList);
        }
        //Get Add General Account Partial View
        public ActionResult Get_AddGeneralAccount()
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            return PartialView("GeneralAccount/_AddGeneralAccount", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicGeneralAccountRevisionYear()
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            return PartialView("GeneralAccount/_DynamicDDRevisionYearDate", model);
        }
        public ActionResult GetSelectedDynamicGeneralAccountRevisionYear(int RevisionYearDateTempID2)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true || s.RevisionYearID == RevisionYearDateTempID2 select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID2;
            return PartialView("GeneralAccount/_DynamicDDRevisionYearDate", model);
        }

        //Dropdown Allotment Class
        public ActionResult GetDynamicGeneralAccountAllotmentClass(int RevisionYearID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            return PartialView("GeneralAccount/_DynamicDDAllotmentClassName", model);
        }

        public ActionResult GetSelectedDynamicGeneralAccountAllotmentClass(int RevisionYearID, int AllotmentClassIDTempID2)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID || s.AllotmentClassID == AllotmentClassIDTempID2 orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            model.AllotmentClassID = AllotmentClassIDTempID2;
            return PartialView("GeneralAccount/_DynamicDDAllotmentClassName", model);
        }
        //Dropdown Account Group
        public ActionResult GetDynamicGeneralAccountGroupName(int AllotmentClassID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            return PartialView("GeneralAccount/_DynamicDDAccountGroupName", model);
        }

        public ActionResult GetSelectedDynamicGeneralAccountGroupName(int AllotmentClassID, int AccountGroupIDTempID2)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID || a.AccountGroupID == AccountGroupIDTempID2).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            model.AccountGroupID = AccountGroupIDTempID2;
            return PartialView("GeneralAccount/_DynamicDDAccountGroupName", model);
        }
        public ActionResult GetDynamicGeneralAccountMajorGroupName(int AccountGroupID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.MajorAccountGroups.Where(a => a.AccountGroupID == AccountGroupID).ToList() select new { MajorAccountGroupID = s.MajorAccountGroupID, MajorAccountGroupName = s.MajorAccountGroupName }), "MajorAccountGroupID", "MajorAccountGroupName");
            return PartialView("GeneralAccount/_DynamicDDMajorAccountName", model);
        }
        public ActionResult GetSelectedDynamicGeneralAccountMajorGroupName(int AccountGroupID, int MajorAccountGroupNameTempID2)
        {
            FM_ChartOfAccounts_GeneralAccount  model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.MajorAccountGroups.Where(a => a.AccountGroupID == AccountGroupID || a.MajorAccountGroupID == MajorAccountGroupNameTempID2).ToList() select new { MajorAccountGroupID = s.MajorAccountGroupID, MajorAccountGroupName = s.MajorAccountGroupName }), "MajorAccountGroupID", "MajorAccountGroupName");
            model.MajorAccountGroupNameID = MajorAccountGroupNameTempID2;
            return PartialView("GeneralAccount/_DynamicDDMajorAccountName", model);
        }

        //Dropdown Account Group Code
        public ActionResult GetGeneralAccountCodeField(int AccountGroupID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            AccountGroup tblSector = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            model.AccountGroupCodeID = tblSector.AccountGroupCode + " - ";
            return PartialView("GeneralAccount/_DynamicDDAccountGroupCode", model);
        }
        public ActionResult GetGeneralAccountCodeField2(int MajorAccountGroupID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            MajorAccountGroup tblSector = (from e in TOSSDB.MajorAccountGroups where e.MajorAccountGroupID == MajorAccountGroupID select e).FirstOrDefault();
            model.MajorAccountGroupCodeID = tblSector.MajorAccountGroupCode + " - ";
            return PartialView("GeneralAccount/_DynamicDDMajorAccountGroupCode", model);
        }
        public ActionResult GetGeneralAccountCodeField3(int SubMajorAccountGroupID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            SubMajorAccountGroup tblSector = (from e in TOSSDB.SubMajorAccountGroups where e.SubMajorAccountGroupID == SubMajorAccountGroupID select e).FirstOrDefault();
            model.SubMajorAccountGroupCodeID = tblSector.SubMajorAccountGroupCode + "-";
            return PartialView("GeneralAccount/_DynamicDDSubMajorAccountGroupCode", model);
        }
        public ActionResult GetDynamicGeneralAccountSubMajorGroupName(int MajorAccountGroupID)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.SubMajorAccountGroups.Where(a => a.MajorAccountGroupID == MajorAccountGroupID).ToList() select new { SubMajorAccountGroupID = s.SubMajorAccountGroupID, SubMajorAccountGroupName = s.SubMajorAccountGroupName }), "SubMajorAccountGroupID", "SubMajorAccountGroupName");
            return PartialView("GeneralAccount/_DynamicDDSubMajorAccountGroupName", model);
        }

        public ActionResult GetSelectedDynamicGeneralAccountSubMajorGroupName(int MajorAccountGroupID, int SubMajorAccountGroupNameTempID2)
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.SubMajorAccountGroups.Where(a => a.MajorAccountGroupID == MajorAccountGroupID || a.SubMajorAccountGroupID == SubMajorAccountGroupNameTempID2).ToList() select new { SubMajorAccountGroupID = s.SubMajorAccountGroupID, SubMajorAccountGroupName = s.SubMajorAccountGroupName }), "SubMajorAccountGroupID", "SubMajorAccountGroupName");
            model.SubMajorAccountGroupNameID = SubMajorAccountGroupNameTempID2;
            return PartialView("GeneralAccount/_DynamicDDSubMajorAccountGroupName", model);
        }
        //Dropdown Revision Year
        public ActionResult GetDynamicGeneralAccountName()
        {
            FM_ChartOfAccounts_GeneralAccount model = new FM_ChartOfAccounts_GeneralAccount();
            model.GeneralAccountList = new SelectList((from s in TOSSDB.GeneralAccounts.ToList()  select new { GeneralAccountID = s.GeneralAccountID, GeneralAccountName = s.GeneralAccountName }), "GeneralAccountID", "GeneralAccountName");
            return PartialView("GeneralAccount/_DynamicDDGeneralAccountName", model);
        }
        public JsonResult AddGeneralAccountGroup(FM_ChartOfAccounts_GeneralAccount model)
        {
            GeneralAccount tblSubMajorAccountGroup = new GeneralAccount();
            tblSubMajorAccountGroup.GeneralAccountID = model.getGeneralAccountcolumns.GeneralAccountID;
            tblSubMajorAccountGroup.GeneralAccountName = model.getGeneralAccountcolumns.GeneralAccountName;
            tblSubMajorAccountGroup.GeneralAccountCode = model.getGeneralAccountcolumns.GeneralAccountCode;
            tblSubMajorAccountGroup.SubMajorAccountGroupID = Convert.ToInt32(model.SubMajorAccountGroupNameID);
            var StatusTemp1 = model.MiscellaneousAccount;
            var StatusTemp2 = model.ContraAccount;
            var StatusTemp3 = model.SubAccount;

            tblSubMajorAccountGroup.isReserve = model.isReserve;
            tblSubMajorAccountGroup.ReservePercent = model.getGeneralAccountcolumns.ReservePercent;
            tblSubMajorAccountGroup.isFullReserve = model.isRelease;
            tblSubMajorAccountGroup.isContinuing = model.isContinuing;
            tblSubMajorAccountGroup.isOBRCashAdvance = model.isCashAdvance;
            tblSubMajorAccountGroup.isNormalBalance = model.isNormalBalance;
            TOSSDB.GeneralAccounts.Add(tblSubMajorAccountGroup);
            TOSSDB.SaveChanges();
            return Json("");
        }
        public ActionResult Get_UpdateGeneralAccountGroup(FM_ChartOfAccounts_GeneralAccount model, int GeneralAccountID)
        {
            GeneralAccount tblGeneralAccount = (from e in TOSSDB.GeneralAccounts where e.GeneralAccountID == GeneralAccountID select e).FirstOrDefault();
            model.getGeneralAccountcolumns.GeneralAccountID = tblGeneralAccount.GeneralAccountID;
            model.AccountGroupIDTempID2 = tblGeneralAccount.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AccountGroupID;
            model.AllotmentClassIDTempID2 = tblGeneralAccount.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.AllotmentClassID;
            model.RevisionYearDateTempID2 = tblGeneralAccount.SubMajorAccountGroup.MajorAccountGroup.AccountGroup.AllotmentClass.RevisionYear.RevisionYearID;
            model.MajorAccountGroupNameTempID2 = tblGeneralAccount.SubMajorAccountGroup.MajorAccountGroup.MajorAccountGroupID;
            model.SubMajorAccountGroupNameTempID2 = tblGeneralAccount.SubMajorAccountGroup.SubMajorAccountGroupID;
            model.getGeneralAccountcolumns.GeneralAccountName = tblGeneralAccount.GeneralAccountName;
            model.getGeneralAccountcolumns.GeneralAccountCode = tblGeneralAccount.GeneralAccountCode;
            model.isReserve = tblGeneralAccount.isReserve;
            model.isRelease = tblGeneralAccount.isFullReserve;
            model.isContinuing = tblGeneralAccount.isContinuing;
            model.isCashAdvance = tblGeneralAccount.isOBRCashAdvance;
            model.isNormalBalance = tblGeneralAccount.isNormalBalance;
            model.getGeneralAccountcolumns.ReservePercent = tblGeneralAccount.ReservePercent;
            return PartialView("GeneralAccount/_UpdateGeneralAccount", model);
        }

        #endregion
    }
}