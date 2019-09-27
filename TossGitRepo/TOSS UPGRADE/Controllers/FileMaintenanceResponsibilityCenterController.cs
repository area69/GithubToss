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



        #endregion
    }
}