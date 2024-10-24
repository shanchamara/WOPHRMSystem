using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Reporting.WebForms;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WOPHRMSystem.Context;
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
        readonly ReceiptServices receiptServices = new ReceiptServices();
        readonly JobMasterServices jobMasterServices = new JobMasterServices();

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


        #region Receipt  

        // GET: Report
        public ActionResult ReceiptReportView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReceiptReportPDF(int id)
        {
            var dt = receiptServices.GetReceiptDetailsbyId(id);


            return new ViewAsPdf("ReceiptReportView", dt)
            {
                PageSize = Rotativa.Options.Size.A5,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10)
            };
        }

        #endregion

        #region Listings
        public ActionResult PrintWorkGroup()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportWorkGroup.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<WorkGroupModel> data = new WorkGroupServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }


        public ActionResult PrintWorkType()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportWorkType.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<WorkTypeModel> data = new WorkTypeServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        public ActionResult PrintDesignation()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportDesignation.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<DesignationModel> data = new DesignationServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }


        public ActionResult PrintLocations()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportLocations.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<LocationModel> data = new LocationServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }


        public ActionResult PrintCustomers()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportCustomers.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<CustomerModel> data = new CustomerServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }


        public ActionResult PrintEmployeeHourlyRate()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportEmployeeHourlyRate.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<EmployeeHourlyRateModel> data = new EmployeeServices().GetAllEmployeeHourlyRates();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        public ActionResult PrintEmployees()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportEmployees.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<EmployeeModel> data = new EmployeeServices().GetAll();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")); // Name of the parameter in the RDLC file
            localReport.SetParameters(parameters);

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        #region Work Type
        public ActionResult LaberUtilizationStatementUserAndDateWiseModel()
        {
            var model = new LaberUtilizationStatementWorkTypeAndGroupReportModel
            {
                EmployeeList = new SelectList(employeeServices.GetAllEmployeeASC(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintLaberUtilizationStatementWorkType(LaberUtilizationStatementWorkTypeAndGroupReportModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportLaberUtilizationStatementWorkType.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            var data = new ReportServices().GetAll(model.FromDate.Value.ToString("yyyy/MM/dd"), model.ToDate.Value.ToString("yyyy/MM/dd"), model.FkFromEmployeeId, model.FkToEmployeeId);
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));
            localReport.SetParameters(new ReportParameter("FromDate", (model.FromDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("ToDate", (model.ToDate.Value.ToString("yyyy-MMM-dd"))));


            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>15in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
        #endregion

        #region Group Type
        public ActionResult LaberUtilizationStatementWorkGroupUserAndDateWiseModel()
        {
            var model = new LaberUtilizationStatementWorkTypeAndGroupReportModel
            {
                EmployeeList = new SelectList(employeeServices.GetAllEmployeeASC(), "Id", "Name"),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintLaberUtilizationStatementWorkGroup(LaberUtilizationStatementWorkTypeAndGroupReportModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportLaberUtilizationStatementWorkGroup.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            var data = new ReportServices().GetAll(model.FromDate.Value.ToString("yyyy/MM/dd"), model.ToDate.Value.ToString("yyyy/MM/dd"), model.FkFromEmployeeId, model.FkToEmployeeId);
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));
            localReport.SetParameters(new ReportParameter("FromDate", (model.FromDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("ToDate", (model.ToDate.Value.ToString("yyyy-MMM-dd"))));


            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>15in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
        #endregion

        #region Summary Work Type And Group
        public ActionResult LaberUtilizationSummaryUserAndDateWiseModel()
        {
            var model = new LaberUtilizationStatementWorkTypeAndGroupReportModel
            {
                JObList = new SelectList(jobMasterServices.GetAllDropdownASC(), "Id", "JobCode"),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintLaberUtilizationSummary(LaberUtilizationStatementWorkTypeAndGroupReportModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = "";
            if (model.IsWorkType == true)
            {
                path = Server.MapPath("~/Reports/ReportLaberUtilizationSummaryWorkType.rdlc");
            }
            else
            {
                path = Server.MapPath("~/Reports/ReportLaberUtilizationSummaryWorkGroup.rdlc");
            }

            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            var data = new ReportServices().GetAllSummary(model.FromDate.Value.ToString("yyyy/MM/dd"), model.ToDate.Value.ToString("yyyy/MM/dd"), model.FkFromJObId, model.FkToJobId);
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));
            localReport.SetParameters(new ReportParameter("FromDate", (model.FromDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("ToDate", (model.ToDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("ToJobNo", Convert.ToString(model.FkFromJObId)));
            localReport.SetParameters(new ReportParameter("FromJobNo", Convert.ToString(model.FkToJobId)));


            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>15in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
        #endregion

        public ActionResult PrintLocationsRates()
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportLocationRates.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<VW_CustomerLocationRatesModel> data = new ReportServices().GetAllCustomersLocationRates();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));


            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.5in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        #region EmployeeVisiting LocationRates 

        public ActionResult PrintEmployeeVisitingLocationRatesModel()
        {
            return View();
        }

        public ActionResult PrintEmployeeVisitingLocationRates(VW_EmployeeVisitingRatesModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportEmployeeVisitingLocationRates.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            List<VW_EmployeeVisitingRatesModel> data = new ReportServices().GetEmployeeVisitingLocationRate(model.FromDate.Value.ToString("yyyy/MM/dd"), model.ToDate.Value.ToString("yyyy/MM/dd"));
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));
            localReport.SetParameters(new ReportParameter("FromDate", (model.FromDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("ToDate", (model.ToDate.Value.ToString("yyyy-MMM-dd"))));

            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.5in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        #endregion

        #region JobWise assign Employees and Detail Cost

        public ActionResult JobWiseAssignEmployeesAndDetailCostModel()
        {
            var model = new JObWiseCositingDetailsWithAssignEmployeeModel
            {
                JObList = new SelectList(jobMasterServices.GetAllDropdownASC(), "Id", "JobCode"),
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult JobWiseAssignEmployeesAndDetailCost(JObWiseCositingDetailsWithAssignEmployeeModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportJObWiseCositingDetailsWithAssignEmployee.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            var data = new ReportServices().GetJObWiseCositingDetailsWithAssignEmployee(model.FromDate.Value.ToString("yyyy/MM/dd"), model.Fk_JobMasterId);



            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

            var firstItem = data.FirstOrDefault();
            DateTime? completedDate = firstItem?.CompletedDate;

            localReport.SetParameters(new ReportParameter("CompletedDate",
                completedDate.HasValue
                ? completedDate.Value.ToString("yyyy-MMM-dd")
                : null));

            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));

            localReport.SetParameters(new ReportParameter("ProjectStatus", (data.FirstOrDefault().IsCompleted == false ? "Pending " : "Completed")));
            localReport.SetParameters(new ReportParameter("CompanyName", data.FirstOrDefault().CustomerCode + " " + data.FirstOrDefault().CustomerName));
            localReport.SetParameters(new ReportParameter("commencedDate", (data.FirstOrDefault().StartDate.Value.ToString("yyyy-MMM-dd"))));
            localReport.SetParameters(new ReportParameter("JObNum", data.FirstOrDefault().JobCode));



            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        #endregion


        #region JobWise assign Employees and Detail Cost

        public ActionResult JobCustomerSummaryCostModel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult JobCustomerSummaryCost(JObWiseCositingDetailsWithAssignEmployeeModel model)
        {
            LocalReport localReport = new LocalReport();
            string path = Server.MapPath("~/Reports/ReportJobCustomerSummaryCost.rdlc");
            if (System.IO.File.Exists(path))
            {
                localReport.ReportPath = path;
            }
            else
            {
                return View("Error");
            }

            // Load data
            var data = new ReportServices().GetJobCositingDetailsWithAssignEmployeeSummary(model.FromDate.Value.ToString("yyyy/MM/dd"), model.IsCompleted, model.IsOtherCustomer);



            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", data);
            localReport.DataSources.Add(reportDataSource);

          
            localReport.SetParameters(new ReportParameter("PrintDate", (new CommonResources().LocalDatetime().Date).ToString("yyyy-MMM-dd")));

            localReport.SetParameters(new ReportParameter("ProjectStatus", (data.FirstOrDefault().IsCompleted == false ? "Pending " : "Completed")));
            localReport.SetParameters(new ReportParameter("AsAtDate", (model.FromDate.Value.ToString("yyyy-MMM-dd"))));



            // Render report
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension = "sddssdsdsddsdsd";

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
                "  <MarginTop>0.3in</MarginTop>" +
                "  <MarginLeft>0.5in</MarginLeft>" +
                "  <MarginRight>0.3in</MarginRight>" +
                "  <MarginBottom>0.3in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }


        #endregion

        #endregion

        private DataTable GetReportData()
        {
            // Implement data fetching logic here
            // This could be from a database, API, or another source
            DataTable dt = new DataTable();
            return dt;
        }
    }
}