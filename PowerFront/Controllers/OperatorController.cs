using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OperatorReport.Models;
using PowerFront.Application.Services.Interface;
using PowerFront.Mapping;

namespace PowerFront.API.Controllers
{
    [ApiController]
    [Route("OperatorController")]
    public class OperatorReportController : Controller
    {
        private readonly IOperatorReportService _operatorReportService;

        public OperatorReportController(IOperatorReportService operatorReportService)
        {
            _operatorReportService = operatorReportService;
        }

        [HttpGet("GetOperator")]
        public ActionResult Get(string? dateOption, DateTime? fromDate, DateTime? toDate, string? website, string? device)
        {
            try
            {
                OperatorReportItems ProductivityReport = new OperatorReportItems();
                ProductivityReport.OperatorProductivity = new List<OperatorReportViewModel>();

                ProductivityReport = OperatorReportMapper.MapToViewModel(_operatorReportService.GetOperatorReport(dateOption, fromDate, toDate, website, device));

                return PartialView("_OperatorReportTableRows", ProductivityReport.OperatorProductivity);
            }
            catch (Exception)
            {
                // Log or handle the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetDistinctWebsites")]
        public IActionResult GetDistinctWebsites()
        {
            try
            {
                var websites = _operatorReportService.GetDistinctWebsites();
                return Ok(websites);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetDistinctDevices")]
        public IActionResult GetDistinctDevices()
        {
            try
            {
                var devices = _operatorReportService.GetDistinctDevices();
                return Ok(devices);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel(string? dateOption, DateTime? fromDate, DateTime? toDate, string? website, string? device)
        {
            OperatorReportItems data = OperatorReportMapper.MapToViewModel(_operatorReportService.GetOperatorReport(dateOption, fromDate, toDate, website, device));

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Operator Report");
                var currentRow = 1;
                
                worksheet.Cell(currentRow, 1).Value = "S.No.";
                worksheet.Cell(currentRow, 2).Value = "Operator Name";

                worksheet.Cell(currentRow, 3).Value = "Proactive Sent";
                worksheet.Cell(currentRow, 4).Value = "Proactive Answered";
                worksheet.Cell(currentRow, 5).Value = "Proactive Response Rate";
                worksheet.Cell(currentRow, 6).Value = "Reactive Received";
                worksheet.Cell(currentRow, 7).Value = "Reactive Answered";
                worksheet.Cell(currentRow, 8).Value = "Reactive Response Rate";
                worksheet.Cell(currentRow, 9).Value = "Total Chat Length";
                worksheet.Cell(currentRow, 10).Value = "Average Chat Length";

                foreach (var item in data.OperatorProductivity)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.ID;
                    worksheet.Cell(currentRow, 2).Value = item.Name;
                    worksheet.Cell(currentRow, 3).Value = item.ProactiveSent;
                    worksheet.Cell(currentRow, 4).Value = item.ProactiveAnswered;
                    worksheet.Cell(currentRow, 5).Value = item.ProactiveResponseRate;
                    worksheet.Cell(currentRow, 6).Value = item.ReactiveReceived;
                    worksheet.Cell(currentRow, 7).Value = item.ReactiveAnswered;
                    worksheet.Cell(currentRow, 8).Value = item.ReactiveResponseRate;
                    worksheet.Cell(currentRow, 9).Value = item.TotalChatLength;
                    worksheet.Cell(currentRow, 10).Value = item.AverageChatLength;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OperatorReport.xlsx");
                }
            }
        }
    }
}
