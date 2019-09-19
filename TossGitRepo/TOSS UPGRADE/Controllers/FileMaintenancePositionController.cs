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
using TOSS_UPGRADE.Models.FM_Position;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenancePositionController : Controller
    {
        // GET: FileMaintenancePosition  
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        //Get Add Position Partial View
        public ActionResult Get_AddPosition()
        {
            FM_Position_Position model = new FM_Position_Position();
            return PartialView("_AddPosition", model);
        }

        //Position Table Partial View
        public ActionResult Get_PositionTable()
        {
            FM_Position_Position model = new FM_Position_Position();
            List<PositionList> tbl_Position = new List<PositionList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Signatory_PositionTable order by PositionName asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PositionList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Position.Add(new PositionList()
                        {
                            PositionID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            PositionName = GlobalFunction.ReturnEmptyString(dr[1]),
                            PositionCode = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getPositionList = tbl_Position.ToList();
            return PartialView("_PositionTable", model.getPositionList);

        }

        //Get Position Update
        public ActionResult Get_UpdatePosition(FM_Position_Position model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            model.getPositioncolumns.PositionID = tblPosition.PositionID;
            model.getPositioncolumns.PositionName = tblPosition.PositionName;
            model.getPositioncolumns.PositionCode = tblPosition.PositionCode;
            return PartialView("_UpdatePosition", model);
        }

        //Add Position
        public JsonResult AddPosition(FM_Position_Position model)
        {
            Signatory_PositionTable tblPositionName = new Signatory_PositionTable();
            tblPositionName.PositionName = GlobalFunction.ReturnEmptyString(model.getPositioncolumns.PositionName);
            tblPositionName.PositionCode = GlobalFunction.ReturnEmptyString(model.getPositioncolumns.PositionCode);
            TOSSDB.Signatory_PositionTable.Add(tblPositionName);
            TOSSDB.SaveChanges();
            return Json(tblPositionName);
        }

        public ActionResult UpdatePosition(FM_Position_Position model)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == model.getPositioncolumns.PositionID select e).FirstOrDefault();
            tblPosition.PositionName = model.getPositioncolumns.PositionName;
            tblPosition.PositionCode = model.getPositioncolumns.PositionCode;
            TOSSDB.Entry(tblPosition);
            TOSSDB.SaveChanges();
            return PartialView("_UpdatePosition", model);
        }

        //Delete Position Table
        public ActionResult DeletePosition(FM_Position_Position model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            TOSSDB.Signatory_PositionTable.Remove(tblPosition);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}