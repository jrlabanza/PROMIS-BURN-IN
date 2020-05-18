using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BI_PROMIS.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;


namespace BI_PROMIS.Controllers
{
    public class GET_FORM_ID //POST DATA
    {
        public int id { get; set; }

    }

    public class FOL_DATA
    {
        public string machineID { get; set; }
        public string machinePF { get; set; }
        public string pkgLine { get; set; }
        public string stsOwner { get; set; }
        public string stsDes { get; set; }
        public string prodName { get; set; }
        public string Prod_Line { get; set; }
        public string lotNo { get; set; }
        public double uph { get; set; }
        public int bin1 { get; set; }
        public string waferID { get; set; }
        public string pkgType { get; set; }
        public string remarks { get; set; }
        public string group { get; set; }
        public string user { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
        public string date3 { get; set; }
        public string process { get; set; }
        public string set_temp { get; set; }
        public string actual_temp { get; set; }
        public string set_timer_1 { get; set; }
        public string set_timer_2 { get; set; }
        public string voltage_set { get; set; }
        public string chamber_condition { get; set; }
        public string wafer_size { get; set; }
        public string sampling_qty { get; set; }
        public string bi_aging_board_name { get; set; }
        public string device_qty_per_loading { get; set; }
        public string item_isolation { get; set; }
        public string aging_tray_no { get; set; }
        public string ps_1 { get; set; }
        public string ps_2 { get; set; }
        public string ps_3 { get; set; }
        public string clk_1 { get; set; }
        public string clk_2 { get; set; }
        public string clk_3 { get; set; }
        public string clk_4 { get; set; }
        public string clk_5 { get; set; }
        public string clk_6 { get; set; }
        public string clk_7 { get; set; }
        public string clk_8 { get; set; }
        public string no_of_running_slot { get; set; }
        public string max_no_of_slot { get; set; }
        public string burn_in_board_cleaning { get; set; }
        public string handlerModel { get; set; }
        public string Handler_ID { get; set; }
        public string track_in { get; set; }
        public string track_out { get; set; }

    }
    public class promisController : BaseController
    {
        BIMod promisObject = new BIMod();
        // for testing queries
        public JsonResult testing()
        {
            var results = promisObject.export_to_excel();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public FileResult ExportToExcel(FormCollection data)
        {
            DateTime now = DateTime.Now;
            //var SD = DateAdd(DateInterval.Day, 7 * (WW - 1) + Day1, NYD);
            //var ED = DateAdd(DateInterval.Day, 7 * (WW) + Day1 - 1, NYD);
            //txtStartDate.Text = Format(SD, "dd-MMM-yyyy");
            //txtEndDate.Text = Format(ED, "dd-MMM-yyyy");

            // Reference code:
            // https: //docs.microsoft.com/en-us/office/open-xml/working-with-sheets 


            string logsDir = "DummyFiles/Excel/";
            var nowTime = DateTime.Now.ToString("yyMMddHHmmssffftt");
            string newFileName = nowTime + ".xlsx";

            var FileVirtualPath = Server.MapPath("~/App_Data/" + logsDir + newFileName);

            try
            {
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(FileVirtualPath, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "mySheet"
                };
                sheets.Append(sheet);


                // Get the sheetData cell table.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();


                // ############################
                // Header row
                // ############################

                Row headerRow;
                headerRow = new Row()
                {
                    RowIndex = 1
                };
                sheetData.Append(headerRow);

                headerRow.Append(new Cell()
                {
                    CellReference = "A1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("WORK WEEK")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "B1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("DATE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "C1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("START DATETINE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "D1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("END DATETINE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "E1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("DURATION")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "F1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("SHIFT")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "G1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TESTER ID")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "H1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TESTER MODEL")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "I1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("HANDLER ID")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "J1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("HANDLER MODEL")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "K1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TEST RES")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "L1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("STATUS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "M1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("STATUS OWNER")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "N1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("COMMENTS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "O1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("DEVICE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "P1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("LOT ID")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "Q1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("UPH")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "R1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("RUNNING SITES")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "S1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("MAX SITES")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "T1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("BIN1")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "U1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("EMPLOYEE NAME")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "V1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("INDEX TIME")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "W1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TEST TIME")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "X1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("CONSUMPTION RATE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "Y1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("LB NAME")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "Z1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("DATALOGS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AA1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("SITE FAILING")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AB1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("OPEN/SHORT")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AC1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("CHARAC")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AD1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TEST STAGE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AE1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("TEMP")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AF1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("EXPECTED OUTPUT/LOSS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AG1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("EARNED HOURS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AH1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("ROOT CAUSE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AI1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("DT TYPE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AJ1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("PACKAGE LINE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AK1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("CHANGE POINT")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AL1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("GROUPS")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AM1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("ITEM ISOLATION")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AN1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("PACKAGE TYPE")
                });////

                headerRow.Append(new Cell()
                {
                    CellReference = "AO1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("LSG REPAIR TYPE")
                });

                // Save and Close the document.
                workbookpart.Workbook.Save();
                spreadsheetDocument.Close();
            }
            catch (Exception ex)
            {
                // Log error
                //this.LogTransactions("html", this.GV_error_log_dir + "" + this.GV_error_log_filename,
                //                    ex.Message, "ExportToExcel");
            }


            return this.Download(newFileName, logsDir, newFileName);

        }

        public ActionResult biStatus()
        {
            return View();
        }

        public JsonResult show_filtered_machine_BI(string statOwner, string machine, string machineID)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_filtered_machine_BI(statOwner, machine, machineID);

            return Json(results);
        }

        public JsonResult get_bi_data()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_bi_machines();

            return Json(results);
        }

        public JsonResult show_statusOwner()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_statusowner();

            return Json(results);
        }

        public JsonResult show_testerPF()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_testerPF();

            return Json(results);
        }

        public JsonResult show_testerID_onload()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_testerID_onload();

            return Json(results);
        }

        public JsonResult show_testerID(string pfid)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.show_testerID(pfid);

            return Json(results);
        }

        //Update Form

        public JsonResult show_data(GET_FORM_ID data)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.show_Data(data.id);

            return Json(results);

        }

        public JsonResult get_status_owner()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_status_owner();

            return Json(results);
        }

        public JsonResult get_status(string stsOwner)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_status(stsOwner);

            return Json(results);
        }

        public JsonResult get_uph(string machinePF, string family)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.get_uph(machinePF, family);

            return Json(results);
        }

        public JsonResult get_uph_check(string testerPF, string handlerPF, string family)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.get_uph_check(testerPF, handlerPF, family);

            return Json(results);
        }

        public JsonResult get_package_data(string id)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.get_package_type(id);

            return Json(results);

        }

        public JsonResult get_handler(string handlerModel)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_handler(handlerModel);

            return Json(results);
        }

        public JsonResult get_handler_mode(string handlerID)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.get_handler_onLoad(handlerID);

            return Json(results);
        }

        public JsonResult get_prod_line()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_prod_line();

            return Json(results);
        }

        public JsonResult get_device()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_device();

            return Json(results);
        }

        //Validation

        public JsonResult get_user()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_all_user();

            return Json(results);
        }

        public JsonResult hash_test(string pass)
        {

            var results = this.GetHashMD5(pass);
            return Json(results);
        }

        public JsonResult check_user(string user, string pass)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.check_user(user, pass);

            return Json(results);

        }

        public JsonResult check_lot(FOL_DATA access)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.check_lot(access.machineID, access.lotNo);

            return Json(results);

        }

        public JsonResult check_track_by_machine(FOL_DATA access)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.check_track_by_machine(access.machineID);

            return Json(results);

        }

        public JsonResult check_track_by_lot(FOL_DATA access)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results = promisObject.check_track_by_lot(access.lotNo);

            return Json(results);

        }


        // Export to Excel

        public JsonResult export_allmachines()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.export_allmachines();

            return Json(results);
        }

        //Posting

        [HttpPost] //Posting in C#
        [ValidateInput(true)] // Checks if inputs are true
        public JsonResult update_machine(FOL_DATA access)
        {

            IDictionary<string, string> results = new Dictionary<string, string>();


            promisObject.insert_Data(access.machineID, access.machinePF, access.stsOwner, access.stsDes, access.prodName, access.lotNo, access.pkgType, access.remarks, access.group, access.user,
                access.date3, access.pkgLine, access.waferID, access.set_temp, access.actual_temp, access.set_timer_1, access.set_timer_2, access.voltage_set, access.chamber_condition,
                access.wafer_size, access.sampling_qty, access.bi_aging_board_name, access.device_qty_per_loading, access.item_isolation, access.handlerModel, access.aging_tray_no,
                access.ps_1, access.ps_2, access.ps_3, access.clk_1, access.clk_2, access.clk_3, access.clk_4, access.clk_5, access.clk_6, access.clk_7, access.clk_8, access.no_of_running_slot, access.max_no_of_slot, access.burn_in_board_cleaning, access.Handler_ID);

            promisObject.insert_Data_History(access.Handler_ID, access.stsDes, access.machineID, access.prodName, access.remarks, access.user, access.date1, access.date2, access.set_temp, access.actual_temp,
                access.set_timer_1, access.set_timer_2, access.voltage_set, access.chamber_condition, access.wafer_size, access.sampling_qty, access.bi_aging_board_name, access.device_qty_per_loading,
                access.item_isolation, access.handlerModel, access.aging_tray_no, access.ps_1, access.ps_2, access.ps_3, access.clk_1, access.clk_2, access.clk_3, access.clk_4, access.clk_5, access.clk_6, access.clk_7, access.clk_8, access.no_of_running_slot,
                access.max_no_of_slot, access.burn_in_board_cleaning);

            results["done"] = "TRUE";
            results["msg"] = "<strong class='success'>UPDATE SUCCESSFULLY</strong>";
            return Json(results);

        }

        [HttpPost] //Posting in C#
        [ValidateInput(true)] // Checks if inputs are true
        public JsonResult track_lot(FOL_DATA access)
        {

            IDictionary<string, string> results = new Dictionary<string, string>();


            promisObject.track_lot(access.machineID, access.track_in, access.track_out, access.lotNo, access.user);

            //if (access.track_out == "1")
            //{
            //    promisObject.pbft_reset(access.machineID);
            //}

            results["done"] = "TRUE";
            results["msg"] = "<strong class='success'>UPDATE TRACK DATA SUCCESSFULLY</strong>";
            return Json(results);

        }

        //[HttpPost] //Posting in C#
        //[ValidateInput(true)] // Checks if inputs are true
        //public JsonResult update_track_lot(FOL_DATA access)
        //{

        //    IDictionary<string, string> results = new Dictionary<string, string>();


        //    promisObject.track_lot(access.machineID, access.track_in, access.track_out, access.lotNo, access.user);

        //    if (access.track_out == "1")
        //    {
        //        promisObject.pbft_reset(access.machineID);
        //    }

        //    results["done"] = "TRUE";
        //    results["msg"] = "<strong class='success'>UPDATE TRACK DATA SUCCESSFULLY</strong>";
        //    return Json(results);

        //}

        public JsonResult get_bi_family()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            results = promisObject.get_bi_family();

            return Json(results);
        }
    }
}
