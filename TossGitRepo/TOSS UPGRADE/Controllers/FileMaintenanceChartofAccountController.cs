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
        public ActionResult GetSelectedDynamicMajorAccountGroupRevisionYear(int RevisionYearDateTempID1)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.RevisionYears.ToList() where s.IsUsed == true select new { RevisionYearID = s.RevisionYearID, RevisionYearDate = s.RevisionYearDate }), "RevisionYearID", "RevisionYearDate");
            model.RevisionYearDate = RevisionYearDateTempID1;
            return PartialView("MajorAccountGroup/_DynamicDDRevisionYearDate", model);
        }
        //Dropdown Allotment Class
        public ActionResult GetDynamicMajorAccountGroupAllotmentClass(int RevisionYearID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            return PartialView("MajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        public ActionResult GetSelectedDynamicMajorAccountGroupAllotmentClass(int RevisionYearID, int AllotmentClassIDTempID1)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AllotmentClasses.ToList() where s.RevisionYearID == RevisionYearID orderby s.AllotmentClassName ascending select new { AllotmentClassID = s.AllotmentClassID, AllotmentClassName = s.AllotmentClassName }), "AllotmentClassID", "AllotmentClassName");
            model.AllotmentClassID = AllotmentClassIDTempID1;
            return PartialView("MajorAccountGroup/_DynamicDDAllotmentClassName", model);
        }
        //Dropdown Account Group
        public ActionResult GetDynamicMajorAccountGroupAccountGroupName(int AllotmentClassID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            return PartialView("MajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        public ActionResult GetSelectedDynamicMajorAccountGroupAccountGroupName(int AllotmentClassID,int AccountGroupIDTempID1)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            model.MajorAccountGroupList = new SelectList((from s in TOSSDB.AccountGroups.Where(a => a.AllotmentClassID == AllotmentClassID).ToList() select new { AccountGroupID = s.AccountGroupID, AccountGroupName = s.AccountGroupName }), "AccountGroupID", "AccountGroupName");
            model.AccountGroupID = AccountGroupIDTempID1;
            return PartialView("MajorAccountGroup/_DynamicDDAccountGroupName", model);
        }
        //Dropdown Account Group Code
        public ActionResult GetMajorAccountCodeField(int AccountGroupID)
        {
            FM_ChartOfAccounts_MajorAccountGroup model = new FM_ChartOfAccounts_MajorAccountGroup();
            AccountGroup tblSector = (from e in TOSSDB.AccountGroups where e.AccountGroupID == AccountGroupID select e).FirstOrDefault();
            model.AccountGroupCodeID = tblSector.AccountGroupCode + " - ";
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
            //model.RevisionYearDateTempID1 = tblMajorAccountGroup.RevisionYearID;
            //model.AllotmentClassIDTempID1 = tblMajorAccountGroup.AllotmentClassID;
            model.AccountGroupIDTempID1 = tblMajorAccountGroup.AccountGroupID;
            model.getMajorAccountGroupcolumns.MajorAccountGroupName = tblMajorAccountGroup.MajorAccountGroupName;
            model.getMajorAccountGroupcolumns.MajorAccountGroupCode = tblMajorAccountGroup.MajorAccountGroupCode;
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

        #endregion
    }
}