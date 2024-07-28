using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Rotativa;
using System;
using System.IO;
using System.Web.Mvc;
using WOPHRMSystem.Helps;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class ReportController : Controller
    {



        readonly JobReportServices _ClientService = new JobReportServices();
        readonly EmployeeServices employeeServices = new EmployeeServices();
        readonly ProformaInvoiceHeadServices proformaInvoiceHead = new ProformaInvoiceHeadServices();
        readonly InvoiceHeadServices invoiceHead = new InvoiceHeadServices();

        #region Job Details 
        // GET: Report
        public ActionResult JObDetailsReportView()
        {
            return View();
        }

        public ActionResult JObDetailsReportModel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JObDetailsReportViewPDF(JobMasterForReportModel jobMasterForReportModel)
        {
            var dt = _ClientService.GetAllJobDetails(jobMasterForReportModel.IsPartner, jobMasterForReportModel.IsManager, jobMasterForReportModel.IsCompleted, jobMasterForReportModel.ISPending, jobMasterForReportModel.ReportGenaratedDate.Value.ToString("yyyy/MM/dd"));

            ViewBag.IsManger = jobMasterForReportModel.IsManager == true ? "Managers" : "Partners";
            ViewBag.StartDate = jobMasterForReportModel.ReportGenaratedDate.Value.ToShortDateString();
            return new ViewAsPdf("JObDetailsReportView", dt)
            {
                CustomSwitches = "--orientation Landscape", // Note: "Landscape" should be uppercase
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }

        [HttpGet]
        public ActionResult JObDetailsReportViewExport(JobMasterForReportModel jobMasterForReportModel)
        {
            // Simulated grouped data (replace with your actual grouped data retrieval)
            var dt = _ClientService.GetAllJobDetails(jobMasterForReportModel.IsPartner, jobMasterForReportModel.IsManager, jobMasterForReportModel.IsCompleted, jobMasterForReportModel.ISPending, jobMasterForReportModel.ReportGenaratedDate.Value.ToString("yyyy/MM/dd"));

            var IsManger = jobMasterForReportModel.IsManager == true ? "Managers" : "Partners";

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(IsManger);

            // Headers
            worksheet.Cell(1, 1).Value = "Job No";
            worksheet.Cell(1, 2).Value = "Narration";
            worksheet.Cell(1, 3).Value = "Customer Code";
            worksheet.Cell(1, 4).Value = "Customer Name";
            worksheet.Cell(1, 5).Value = "Commenced Date";
            worksheet.Cell(1, 6).Value = "Due Date";
            worksheet.Cell(1, 7).Value = "Preview Value";

            // Data
            int row = 2;
            foreach (var group in dt)
            {
                foreach (var item in group)
                {
                    worksheet.Cell(row, 1).Value = item.JobCode;
                    worksheet.Cell(row, 2).Value = item.Narration;
                    worksheet.Cell(row, 3).Value = item.CustomerCode;
                    worksheet.Cell(row, 4).Value = item.CustomerName;
                    worksheet.Cell(row, 5).Value = item.StartDate.Value.ToShortDateString();
                    worksheet.Cell(row, 6).Value = item.DueDate.Value.ToShortDateString();
                    worksheet.Cell(row, 7).Value = item.PreViewvalue;
                    // Add more columns if needed
                    row++;
                }
            }

            // Add print date to the worksheet
            worksheet.Cell(row + 2, 1).Value = "Print Date:";
            worksheet.Cell(row + 2, 2).Value = DateTime.Now.ToString("yyyy-MM-dd");
            // Auto-fit columns
            worksheet.Columns().AdjustToContents();
            // Save the workbook to a stream
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Return the Excel file as a byte array
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JObDetailsReportViewExport.xlsx");
        }


        #endregion


        #region Job Cost Calculator 

        public ActionResult JObCalculatorCostReportModel()
        {
            var model = new JobMasterForCalculatorJobCostModel()
            {
                PartnerList = new SelectList(employeeServices.GetAllIsPartner(), "Id", "Name"),
            };
            return View(model);
        }

        public ActionResult JObCalculatorCostReportView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JObCalculatorCostReportViewPDF(JobMasterForCalculatorJobCostModel jobMasterForReportModel)
        {
            var dt = _ClientService.GetAllJobCalculatorJobCost(jobMasterForReportModel.StartDate.Value.ToString("yyyy/MM/dd"), jobMasterForReportModel.DueDate.Value.ToString("yyyy/MM/dd"), jobMasterForReportModel.PartnerFrom, jobMasterForReportModel.PartnerTo, jobMasterForReportModel.IsReActivate);

            ViewBag.FromDate = jobMasterForReportModel.StartDate.Value.ToShortDateString();
            ViewBag.ToDate = jobMasterForReportModel.DueDate.Value.ToShortDateString();

            return new ViewAsPdf("JObCalculatorCostReportView", dt)
            {
                CustomSwitches = "--orientation Landscape", // Note: "Landscape" should be uppercase
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }


        [HttpGet]
        public ActionResult JObCalculatorCostReportExport(JobMasterForCalculatorJobCostModel jobMasterForReportModel)
        {
            // Simulated grouped data (replace with your actual grouped data retrieval)
            var dt = _ClientService.GetAllJobCalculatorJobCost(jobMasterForReportModel.StartDate.Value.ToString("yyyy/MM/dd"), jobMasterForReportModel.DueDate.Value.ToString("yyyy/MM/dd"), jobMasterForReportModel.PartnerFrom, jobMasterForReportModel.PartnerTo, jobMasterForReportModel.IsReActivate);
            var FromDate = jobMasterForReportModel.StartDate.Value.ToShortDateString();
            var ToDate = jobMasterForReportModel.DueDate.Value.ToShortDateString();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet");

            // Headers
            worksheet.Cell(1, 1).Value = "Job No";
            worksheet.Cell(1, 2).Value = "Narration";
            worksheet.Cell(1, 3).Value = "Customer Code";
            worksheet.Cell(1, 4).Value = "Customer Name";
            worksheet.Cell(1, 5).Value = "Commenced Date";
            worksheet.Cell(1, 6).Value = "Due Date";
            worksheet.Cell(1, 7).Value = "Preview Value";
            worksheet.Cell(1, 8).Value = "Actual Value";
            worksheet.Cell(1, 9).Value = "Budget Value";
            worksheet.Cell(1, 10).Value = "Variance Value";

            // Data
            int row = 2;
            foreach (var group in dt)
            {
                row++;




                worksheet.Cell(row, 1).Value = "Group Key:";
                worksheet.Cell(row, 2).Value = group.Key;
                worksheet.Cell(row, 3).Value = "From Date:";
                worksheet.Cell(row, 4).Value = FromDate;
                worksheet.Cell(row, 5).Value = "To Date:";
                worksheet.Cell(row, 6).Value = ToDate;

                row++;
                row++;

                foreach (var item in group)
                {
                    worksheet.Cell(row, 1).Value = item.JobCode;
                    worksheet.Cell(row, 2).Value = item.Narration;
                    worksheet.Cell(row, 3).Value = item.CustomerCode;
                    worksheet.Cell(row, 4).Value = item.CustomerName;
                    worksheet.Cell(row, 5).Value = item.StartDate.Value.ToShortDateString();
                    worksheet.Cell(row, 6).Value = item.DueDate.Value.ToShortDateString();
                    worksheet.Cell(row, 7).Value = item.PreViewvalue;
                    worksheet.Cell(row, 8).Value = item.ActualValue;
                    worksheet.Cell(row, 9).Value = item.BudgetValue;
                    worksheet.Cell(row, 10).Value = item.VarianceValue;
                    // Add more columns if needed
                    row++;
                }
            }

            // Add print date to the worksheet
            worksheet.Cell(row + 2, 1).Value = "Print Date:";
            worksheet.Cell(row + 2, 2).Value = DateTime.Now.ToString("yyyy-MM-dd");
            // Auto-fit columns
            worksheet.Columns().AdjustToContents();
            // Save the workbook to a stream
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Return the Excel file as a byte array
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JObDetailsReportViewExport.xlsx");
        }


        #endregion


        #region Proforma Invoice 

        // GET: Report
        public ActionResult ProformaInvoiceReportView()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ProformaInvoiceReportPDF(int id)
        {
            var dt = proformaInvoiceHead.GetAllTransferedInvoiceForPrint(id);

            decimal amount = Convert.ToDecimal(dt.TotalReceivedAmount);
            string AmountInWords = CommonResources.ConvertAmountToWords(amount);

            ViewBag.AmountInWords = AmountInWords;
            //ViewBag.StartDate = jobMasterForReportModel.ReportGenaratedDate.Value.ToShortDateString();
            return new ViewAsPdf("ProformaInvoiceReportView", dt)
            {
                PageSize = Rotativa.Options.Size.A5,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }

        #endregion

        #region  SuspendedInvoice 

        // GET: Report
        public ActionResult InvoiceReportView()
        {
            return View();
        }


        [HttpGet]
        public ActionResult InvoiceReportPDF(int id)
        {
            var dt = invoiceHead.GetAllTransferedInvoiceForPrint(id);
            var dtbody = invoiceHead.GetAllTransferedInvoiceForPrintbody(id);


            decimal amount = Convert.ToDecimal(dt.TotalReceivedAmount);
            decimal amountplusNBT = Convert.ToDecimal(dt.TotalAmount + dt.ValueNBT);

            string GrandAmountInWords = CommonResources.ConvertAmountToWords(amount);
            string AmountInWords = CommonResources.ConvertAmountToWords(amountplusNBT);


            var model = new InvoicePrintModel()
            {
                InvoiceBodyModels = dtbody,
                Invoicehead1 = dt

            };
            ViewBag.GrandAmountInWords = GrandAmountInWords;
            ViewBag.AmountInWords = AmountInWords;
            return new ViewAsPdf("InvoiceReportView", model)
            {
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }

        #endregion


        #region  TAXInvoice 

        // GET: Report
        public ActionResult TAXInvoiceReportView()
        {
            return View();
        }


        [HttpGet]
        public ActionResult TAXInvoiceReportPDF(int id)
        {
            var dt = invoiceHead.GetAllTransferedInvoiceForPrint(id);
            var dtbody = invoiceHead.GetAllTransferedInvoiceForPrintbody(id);


            decimal amount = Convert.ToDecimal(dt.TotalReceivedAmount);
            decimal amountplusNBT = Convert.ToDecimal(dt.TotalAmount + dt.ValueNBT);

            string GrandAmountInWords = CommonResources.ConvertAmountToWords(amount);
            string AmountInWords = CommonResources.ConvertAmountToWords(amountplusNBT);


            var model = new InvoicePrintModel()
            {
                InvoiceBodyModels = dtbody,
                Invoicehead1 = dt

            };
            ViewBag.GrandAmountInWords = GrandAmountInWords;
            ViewBag.AmountInWords = AmountInWords;
            return new ViewAsPdf("TAXInvoiceReportView", model)
            {
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }

        #endregion
    }
}