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
using TOSS_UPGRADE.Models.FM_OfficeType;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceOfficeTypeController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenanceOfficeType
        public ActionResult Index()
        {
            return View();
        }

        #region Office Type
        //Table Internal Revenue Allotment
        public ActionResult Get_OfficeTypeTable()
        {
            FM_OfficeType_OfficeType model = new FM_OfficeType_OfficeType();
            List<OfficeTypeList> tbl_OfficeType = new List<OfficeTypeList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.OfficeType";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_OfficeTypeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_OfficeType.Add(new OfficeTypeList()
                        {
                            OfficeTypeID = Convert.ToInt32(dr[0]),
                            OfficeTypeName = GlobalFunction.ReturnEmptyString(dr[1]),
                            OfficeTypeCode = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getOfficeTypeList = tbl_OfficeType.ToList();
            return PartialView("_OfficeTypeTable", model.getOfficeTypeList);
        }

        //Get Add Internal Revenue Allotment Partial View
        public ActionResult Get_AddOfficeType()
        {
            FM_OfficeType_OfficeType model = new FM_OfficeType_OfficeType();
            return PartialView("_AddOfficeType", model);
        }

        //Add Internal Revenue Allotment
        public JsonResult AddOfficeType(FM_OfficeType_OfficeType model)
        {
            OfficeType tblOfficeType = new OfficeType();
            tblOfficeType.OfficeTypeName = model.getOfficeTypecolumns.OfficeTypeName;
            tblOfficeType.OfficeTypeCode = model.getOfficeTypecolumns.OfficeTypeCode;
            TOSSDB.OfficeTypes.Add(tblOfficeType);
            TOSSDB.SaveChanges();
            return Json(tblOfficeType);
        }

        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdateOfficeType(FM_OfficeType_OfficeType model, int OfficeTypeID)
        {
            OfficeType tblOfficeType = (from e in TOSSDB.OfficeTypes where e.OfficeTypeID == OfficeTypeID select e).FirstOrDefault();
            model.getOfficeTypecolumns.OfficeTypeID = tblOfficeType.OfficeTypeID;
            model.getOfficeTypecolumns.OfficeTypeName = tblOfficeType.OfficeTypeName;
            model.getOfficeTypecolumns.OfficeTypeCode = tblOfficeType.OfficeTypeCode;
            return PartialView("_UpdateOfficeType", model);
        }

        //Update Internal Revenue Allotment
        public ActionResult UpdateOfficeType(FM_OfficeType_OfficeType model)
        {
            OfficeType tblOfficeType = (from e in TOSSDB.OfficeTypes where e.OfficeTypeID == model.getOfficeTypecolumns.OfficeTypeID select e).FirstOrDefault();
            tblOfficeType.OfficeTypeName = model.getOfficeTypecolumns.OfficeTypeName;
            tblOfficeType.OfficeTypeCode = model.getOfficeTypecolumns.OfficeTypeCode;
            TOSSDB.Entry(tblOfficeType);
            TOSSDB.SaveChanges();
            return PartialView("_UpdateOfficeType", model);
        }

        //Delete Internal Revenue Allotment
        public ActionResult DeleteOfficeType(FM_OfficeType_OfficeType model, int OfficeTypeID)
        {
            OfficeType tblOfficeType = (from e in TOSSDB.OfficeTypes where e.OfficeTypeID == OfficeTypeID select e).FirstOrDefault();
            TOSSDB.OfficeTypes.Remove(tblOfficeType);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}