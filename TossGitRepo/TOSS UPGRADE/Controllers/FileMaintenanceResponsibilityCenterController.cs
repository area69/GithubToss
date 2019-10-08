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
using TOSS_UPGRADE.Models.FM_ResponsibilityCenter;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceResponsibilityCenterController : Controller
    {
        // GET: FileMaintenanceResponsibilityCenter   
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DepartmentTab()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            return PartialView("Department/DepartmentIndex", model);
        }
        public ActionResult FunctionTab()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            return PartialView("Function/FunctionIndex", model);
        }
        public ActionResult SectionTab()
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            return PartialView("Section/SectionIndex", model);
        }
        #region Department

        //Get Add Position Partial View
        public ActionResult Get_AddDepartment()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            return PartialView("Department/_AddDepartment", model);
        }

        //Position Table Partial View
        public ActionResult Get_DepartmentTable()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            List<DepartmentList> tbl_Position = new List<DepartmentList>();

            var SQLQuery = "SELECT DepartmentID,DepartmentName,DepartmentAbbreviation,ResponsibilityCode,Fund.FundName,Sector.SectorName,SubSectorID,OfficeTypeName,DepartmentCode FROM DB_TOSS.dbo.Signatory_DepartmentTable,OfficeType,Sector,Fund where dbo.Fund.FundID = Signatory_DepartmentTable.FundID AND OfficeType.OfficeTypeID = Signatory_DepartmentTable.OfficeTypeID AND  Sector.SectorID = Signatory_DepartmentTable.SectorID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SignatoryDepartmentList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Position.Add(new DepartmentList()
                        {
                            DepartmentID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            DepartmentName = GlobalFunction.ReturnEmptyString(dr[1]),
                            DepartmentAbbreviation = GlobalFunction.ReturnEmptyString(dr[2]),
                            ResponsibilityCode = GlobalFunction.ReturnEmptyString(dr[3]),
                            FundName = GlobalFunction.ReturnEmptyString(dr[4]),
                            Sector = GlobalFunction.ReturnEmptyString(dr[5]),
                            SubSector = GlobalFunction.ReturnEmptyInt(dr[6]),
                            OfficeType = GlobalFunction.ReturnEmptyString(dr[7]),
                            DepartmentCode = GlobalFunction.ReturnEmptyString(dr[8]),

                        });
                    }
                }
                Connection.Close();
            }
            model.getDepartmentList = tbl_Position.ToList();
            return PartialView("Department/_DepartmentTable", model.getDepartmentList);

        }

        //Dropdown Fund
        public ActionResult GetDynamicFund()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundName = s.FundName }), "FundID", "FundName");
            return PartialView("Department/_DynamicDDFundName", model);
        }

        public ActionResult GetSelectedDynamicFund(int FundNameTempID)
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundName = s.FundName }), "FundID", "FundName");
            model.FundNameID = FundNameTempID;
            return PartialView("Department/_DynamicDDFundName", model);
        }

        ////Dropdown SubSector
        public ActionResult GetDynamicSector()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorName = s.SectorName }), "SectorID", "SectorName");
            return PartialView("Department/_DynamicDDSectorName", model);
        }

        public ActionResult GetSelectedDynamicSector(int SectorNameTempID)
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorName = s.SectorName }), "SectorID", "SectorName");
            model.SectorNameID = SectorNameTempID;
            return PartialView("Department/_DynamicDDSectorName", model);
        }

        //Get Dynamic SubSector For Update Partial View
        public ActionResult GetDynamicSubSector(int SectorID)
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.SubSectors.Where(a => a.SectorID == SectorID).ToList() select new { SubSectorID = s.SubSectorID, SubSectorName = s.SubSectorName }), "SubSectorID", "SubSectorName");
            return PartialView("Department/_DynamicDDSubSector", model);
        }

        public ActionResult GetSelectedDynamicSubSector(int SectorID, int SubSectorNameIDTempID)
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.SubSectors.Where(a => a.SectorID == SectorID).ToList() select new { SubSectorID = s.SubSectorID, SubSectorName = s.SubSectorName }), "SubSectorID", "SubSectorName");
            model.SubSectorNameID = SubSectorNameIDTempID;
            return PartialView("Department/_DynamicDDSubSector", model);
        }

        //Dropdown Office Type
        public ActionResult GetDynamicOfficeType()
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.OfficeTypes.ToList() select new { OfficeTypeID = s.OfficeTypeID, OfficeTypeName = s.OfficeTypeName }), "OfficeTypeID", "OfficeTypeName");
            return PartialView("Department/_DynamicDDOfficeType", model);
        }
        public ActionResult GetSelectedDynamicOfficeType(int OfficeTypeNameTempID)
        {
            FM_ResponsibilityCenter_Department model = new FM_ResponsibilityCenter_Department();
            model.DepartmentList = new SelectList((from s in TOSSDB.OfficeTypes.ToList() select new { OfficeTypeID = s.OfficeTypeID, OfficeTypeName = s.OfficeTypeName }), "OfficeTypeID", "OfficeTypeName");
            model.OfficeTypeNameID = OfficeTypeNameTempID;
            return PartialView("Department/_DynamicDDOfficeType", model);
        }

        //Add Department
        public JsonResult AddDepartment(FM_ResponsibilityCenter_Department model)
        {
            Signatory_DepartmentTable tblDepartment = new Signatory_DepartmentTable();
            tblDepartment.DepartmentName = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentName);
            tblDepartment.DepartmentCode = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentCode);
            tblDepartment.DepartmentAbbreviation = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentAbbreviation);
            tblDepartment.ResponsibilityCode = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.ResponsibilityCode);
            tblDepartment.SectorID = GlobalFunction.ReturnEmptyInt(model.SectorNameID);
            var SubSectorTemp = GlobalFunction.ReturnEmptyInt(model.SubSectorNameID);

            if (SubSectorTemp == 0)
            {
                tblDepartment.SubSectorID = null;
            }
            else
            {
                tblDepartment.SubSectorID = model.SubSectorNameID;
            }
            tblDepartment.OfficeTypeID = GlobalFunction.ReturnEmptyInt(model.OfficeTypeNameID);
            tblDepartment.FundID = GlobalFunction.ReturnEmptyInt(model.FundNameID);
            TOSSDB.Signatory_DepartmentTable.Add(tblDepartment);
            TOSSDB.SaveChanges();
            return Json(tblDepartment);
        }

        //Get Update SubSector
        public ActionResult Get_UpdateDepartment(FM_ResponsibilityCenter_Department model, int DepartmentID)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            model.getDepartmentcolumns.DepartmentID = tblDepartment.DepartmentID;
            model.getDepartmentcolumns.DepartmentName = tblDepartment.DepartmentName;
            model.getDepartmentcolumns.DepartmentAbbreviation = tblDepartment.DepartmentAbbreviation;
            model.getDepartmentcolumns.ResponsibilityCode = tblDepartment.ResponsibilityCode;
            model.FundNameTempID = Convert.ToInt32(tblDepartment.FundID);
            model.SectorNameTempID = Convert.ToInt32(tblDepartment.SectorID);
            model.SubSectorNameTempID = Convert.ToInt32(tblDepartment.SubSectorID);
            model.OfficeTypeNameTempID = Convert.ToInt32(tblDepartment.OfficeTypeID);
            model.getDepartmentcolumns.DepartmentCode = tblDepartment.DepartmentCode;
            return PartialView("Department/_UpdateDepartment", model);
        }

        //Update SubSector
        public ActionResult UpdateDepartment(FM_ResponsibilityCenter_Department model)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == model.getDepartmentcolumns.DepartmentID select e).FirstOrDefault();
            tblDepartment.DepartmentName = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentName);
            tblDepartment.DepartmentCode = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentCode);
            tblDepartment.DepartmentAbbreviation = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.DepartmentAbbreviation);
            tblDepartment.ResponsibilityCode = GlobalFunction.ReturnEmptyString(model.getDepartmentcolumns.ResponsibilityCode);
            tblDepartment.SectorID = GlobalFunction.ReturnEmptyInt(model.SectorNameID);
            var SubSectorTemp = GlobalFunction.ReturnEmptyInt(model.SubSectorNameID);

            if (SubSectorTemp == 0)
            {
                tblDepartment.SubSectorID = null;
            }
            else
            {
                tblDepartment.SubSectorID = model.SubSectorNameID;
            }
            tblDepartment.OfficeTypeID = GlobalFunction.ReturnEmptyInt(model.OfficeTypeNameID);
            tblDepartment.FundID = GlobalFunction.ReturnEmptyInt(model.FundNameID);
            TOSSDB.Entry(tblDepartment);
            TOSSDB.SaveChanges();
            return PartialView("Department/_UpdateDepartment", model);
        }

        //Delete Department
        public ActionResult DeleteDepartment(FM_ResponsibilityCenter_Department model, int DepartmentID)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            TOSSDB.Signatory_DepartmentTable.Remove(tblDepartment);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region Function

        //Get Add Position Partial View
        public ActionResult Get_AddFunction()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            return PartialView("Function/_AddFunction", model);
        }

        //Position Table Partial View
        public ActionResult Get_FunctionTable()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            List<FunctionList> tbl_Function = new List<FunctionList>();

            var SQLQuery = "SELECT FunctionID,FunctionName,FunctionAbbreviation,FunctionCode,DepartmentName,FundID ,SectorName,dbo.Functions.SubSectorID,OfficeTypeName FROM DB_TOSS.dbo.Functions,Signatory_DepartmentTable,Sector,OfficeType Where Signatory_DepartmentTable.DepartmentID = Functions.DepartmentID AND Sector.SectorID = Functions.SectorID AND OfficeType.OfficeTypeID = Functions.OfficeTypeID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_FunctionsList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Function.Add(new FunctionList()
                        {
                            FunctionID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FunctionName = GlobalFunction.ReturnEmptyString(dr[1]),
                            FunctionAbbreviation = GlobalFunction.ReturnEmptyString(dr[2]),
                            FunctionCode = GlobalFunction.ReturnEmptyString(dr[3]),
                            DepartmentName = GlobalFunction.ReturnEmptyString(dr[4]),
                            Sector = GlobalFunction.ReturnEmptyString(dr[6]),
                            SubSector = GlobalFunction.ReturnEmptyInt(dr[7]),
                            OfficeType = GlobalFunction.ReturnEmptyString(dr[8]),
                            FundID = GlobalFunction.ReturnEmptyInt(dr[5]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFunctionList = tbl_Function.ToList();
            return PartialView("Function/_FunctionTable", model.getFunctionList);
        }

        //Dropdown Department
        public ActionResult GetDynamicDepartment()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            return PartialView("Function/_DynamicDDDepartmentName", model);
        }
        public ActionResult GetDynamicFunctionDepartment(int DepartmentTempID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            model.DepartmentID = DepartmentTempID;
            return PartialView("Function/_DynamicDDDepartmentName", model);
        }

        //Viewing of Fund Title
        public ActionResult GetFundName(int DepartmentID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            Signatory_DepartmentTable tblSector = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            model.FundName = tblSector.Fund.FundName;
            return PartialView("Function/_DynamicLBFundName", model);
        }
        public ActionResult GetSelectedFundName(int DepartmentID, int FundTempID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            Signatory_DepartmentTable tblSector = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            model.FundName = tblSector.Fund.FundName;
            return PartialView("Function/_DynamicLBFundName", model);
        }
        public ActionResult GetDeptOfficeCode(int DepartmentID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            var deptTable = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            model.DepartmentCode = deptTable.DepartmentCode;
            return PartialView("Function/_DynamicLBDepartmentOfficeCode", model);
        }

        //Dropdown SubSector
        public ActionResult GetDynamicFunctionSector()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorName = s.SectorName }), "SectorID", "SectorName");
            return PartialView("Function/_DynamicDDSectorName", model);
        }
        public ActionResult GetSelectedDynamicFunctionSector(int SectorNameTempID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorName = s.SectorName }), "SectorID", "SectorName");
            model.SectorNameID = SectorNameTempID;
            return PartialView("Function/_DynamicDDSectorName", model);
        }

        //Get Dynamic SubSector For Update Partial View
        public ActionResult GetDynamicFunctionSubSector(int SectorID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.SubSectors.Where(a => a.SectorID == SectorID).ToList() select new { SubSectorID = s.SubSectorID, SubSectorName = s.SubSectorName }), "SubSectorID", "SubSectorName");
            return PartialView("Function/_DynamicDDSubSector", model);
        }
        public ActionResult GetSelectedDynamicFunctionSubSector(int SectorID, int SubSectorNameIDTempID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.SubSectors.Where(a => a.SectorID == SectorID).ToList() select new { SubSectorID = s.SubSectorID, SubSectorName = s.SubSectorName }), "SubSectorID", "SubSectorName");
            model.SubSectorNameID = SubSectorNameIDTempID;
            return PartialView("Function/_DynamicDDSubSector", model);
        }

        //Dropdown Office Type
        public ActionResult GetDynamicFunctionOfficeType()
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.OfficeTypes.ToList() select new { OfficeTypeID = s.OfficeTypeID, OfficeTypeName = s.OfficeTypeName }), "OfficeTypeID", "OfficeTypeName");
            return PartialView("Function/_DynamicDDOfficeType", model);
        }
        public ActionResult GetSelectedDynamicFunctionOfficeType(int OfficeTypeNameTempID)
        {
            FM_ResponsibilityCenter_Function model = new FM_ResponsibilityCenter_Function();
            model.FunctionList = new SelectList((from s in TOSSDB.OfficeTypes.ToList() select new { OfficeTypeID = s.OfficeTypeID, OfficeTypeName = s.OfficeTypeName }), "OfficeTypeID", "OfficeTypeName");
            model.OfficeTypeNameID = OfficeTypeNameTempID;
            return PartialView("Function/_DynamicDDOfficeType", model);
        }

        //Add Function
        public JsonResult AddFunction(FM_ResponsibilityCenter_Function model)
        {
            Function tblDepartment = new Function();
            tblDepartment.FunctionName = model.getFunctioncolumns.FunctionName;
            tblDepartment.FunctionCode = model.getFunctioncolumns.FunctionCode;
            tblDepartment.FunctionAbbreviation = model.getFunctioncolumns.FunctionAbbreviation;
            tblDepartment.DepartmentID = model.DepartmentID;
            tblDepartment.SectorID = model.SectorNameID;
            var SubSectorTemp = GlobalFunction.ReturnEmptyInt(model.SubSectorNameID);

            if (SubSectorTemp == 0)
            {
                tblDepartment.SubSectorID = null;
            }
            else
            {
                tblDepartment.SubSectorID = model.SubSectorNameID;
            }
            tblDepartment.OfficeTypeID = model.OfficeTypeNameID;
            TOSSDB.Functions.Add(tblDepartment);
            TOSSDB.SaveChanges();
            return Json(tblDepartment);
        }

        //Get Update SubSector
        public ActionResult Get_UpdateFunction(FM_ResponsibilityCenter_Function model, int FunctionID)
        {
            Function tblFunction = (from e in TOSSDB.Functions where e.FunctionID == FunctionID select e).FirstOrDefault();
            model.getFunctioncolumns.FunctionID = tblFunction.FunctionID;
            model.getFunctioncolumns.FunctionName = tblFunction.FunctionName;
            model.getFunctioncolumns.FunctionAbbreviation = tblFunction.FunctionAbbreviation;
            model.getFunctioncolumns.FunctionCode = tblFunction.FunctionCode;
            model.DepartmentTempID = Convert.ToInt32(tblFunction.DepartmentID);
            model.SectorNameTempID = Convert.ToInt32(tblFunction.SectorID);
            model.SubSectorNameTempID = Convert.ToInt32(tblFunction.SubSectorID);
            model.OfficeTypeNameTempID = Convert.ToInt32(tblFunction.OfficeTypeID);
            return PartialView("Function/_UpdateFunction", model);
        }

        //Update SubSector
        public ActionResult UpdateFunction(FM_ResponsibilityCenter_Function model)
        {
            Function tblDepartment = (from e in TOSSDB.Functions where e.FunctionID == model.getFunctioncolumns.FunctionID select e).FirstOrDefault();
            tblDepartment.FunctionName = model.getFunctioncolumns.FunctionName;
            tblDepartment.FunctionCode = model.getFunctioncolumns.FunctionCode;
            tblDepartment.FunctionAbbreviation = model.getFunctioncolumns.FunctionAbbreviation;
            tblDepartment.DepartmentID = model.DepartmentID;
            tblDepartment.SectorID = model.SectorNameID;
            var SubSectorTemp = GlobalFunction.ReturnEmptyInt(model.SubSectorNameID);

            if (SubSectorTemp == 0)
            {
                tblDepartment.SubSectorID = null;
            }
            else
            {
                tblDepartment.SubSectorID = model.SubSectorNameID;
            }
            tblDepartment.OfficeTypeID = model.OfficeTypeNameID;
            TOSSDB.Entry(tblDepartment);
            TOSSDB.SaveChanges();
            return PartialView("Function/_UpdateFunction", model);
        }
        //Delete Function
        public ActionResult DeleteFunction(FM_ResponsibilityCenter_Function model, int FunctionID)
        {
            Function tblFunction = (from e in TOSSDB.Functions where e.FunctionID == FunctionID select e).FirstOrDefault();
            TOSSDB.Functions.Remove(tblFunction);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Section
        //Get Add Position Partial View
        public ActionResult Get_AddSection()
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            return PartialView("Section/_AddSection", model);
        }
        //Position Table Partial View
        public ActionResult Get_SectionTable()
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            List<SectionList> tbl_Position = new List<SectionList>();

            var SQLQuery = "SELECT SectionID ,SectionName,DepartmentName,FunctionName FROM DB_TOSS.dbo.Sections,Functions,Signatory_DepartmentTable WHERE Signatory_DepartmentTable.DepartmentID = Sections.DepartmentID AND Functions.FunctionID = Sections.FunctionID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SectionList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Position.Add(new SectionList()
                        {
                            SectionID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SectionName = GlobalFunction.ReturnEmptyString(dr[1]),
                            DepartmentName = GlobalFunction.ReturnEmptyString(dr[2]),
                            FunctionName = GlobalFunction.ReturnEmptyString(dr[3])
                        });
                    }
                }
                Connection.Close();
            }
            model.getSectionList = tbl_Position.ToList();
            return PartialView("Section/SectionTable", model.getSectionList);

        }
        //Dropdown Department
        public ActionResult GetSectionDynamicDepartment()
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            model.SectionList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            return PartialView("Section/_DynamicDDDepartmenName", model);
        }
        public ActionResult GetSelectedSectionDynamicDepartment(int DepartmentTempID)
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            model.SectionList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, DepartmentName = s.DepartmentName }), "DepartmentID", "DepartmentName");
            model.DepartmentID = DepartmentTempID;
            return PartialView("Section/_DynamicDDDepartmenName", model);
        }
        //Dropdown Function
        public ActionResult GetSectionDynamicFunction(int DepartmentID)
        {
            FM_ResponsibilityCenter_Section model = new FM_ResponsibilityCenter_Section();
            model.SectionList = new SelectList((from s in TOSSDB.Functions.Where(a => a.DepartmentID == DepartmentID).ToList() select new { FunctionID = s.FunctionID, FunctionName = s.FunctionName }), "FunctionID", "FunctionName");
            return PartialView("Section/_DynamicDDFunctionName", model);
        }
        //Add Function
        public JsonResult AddSection(FM_ResponsibilityCenter_Section model)
        {
            Section tblSection = new Section();
            tblSection.SectionName = model.getSectioncolumns.SectionName;
            tblSection.FunctionID = model.FunctionID;
            tblSection.DepartmentID = model.DepartmentID;
            TOSSDB.Sections.Add(tblSection);
            TOSSDB.SaveChanges();
            return Json(tblSection);
        }
        //Get Update SubSector
        public ActionResult Get_UpdateSection(FM_ResponsibilityCenter_Section model, int SectionID)
        {
            Section tblFunction = (from e in TOSSDB.Sections where e.SectionID == SectionID select e).FirstOrDefault();
            model.getSectioncolumns.SectionID = tblFunction.SectionID;
            model.FunctionTempID = tblFunction.FunctionID;
            model.DepartmentTempID = tblFunction.DepartmentID;
            model.getSectioncolumns.SectionName = tblFunction.SectionName;
            return PartialView("Section/_UpdateSection", model);
        }
        //Update SubSector
        public ActionResult UpdateSection(FM_ResponsibilityCenter_Section model)
        {
            Section tblFunction = (from e in TOSSDB.Sections where e.SectionID == model.getSectioncolumns.SectionID select e).FirstOrDefault();
            tblFunction.SectionName = model.getSectioncolumns.SectionName;
            tblFunction.FunctionID = model.FunctionID;
            tblFunction.DepartmentID = model.DepartmentID;
            TOSSDB.Entry(tblFunction);
            TOSSDB.SaveChanges();
            return PartialView("Section/_UpdateSection", model);
        }
        //Delete Function
        public ActionResult DeleteSection(FM_ResponsibilityCenter_Section model, int SectionID)
        {
            Section tblFunction = (from e in TOSSDB.Sections where e.SectionID == SectionID select e).FirstOrDefault();
            TOSSDB.Sections.Remove(tblFunction);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}