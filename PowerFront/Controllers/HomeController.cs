using Microsoft.AspNetCore.Mvc;
using OperatorReport.Models;
using PowerFront.Application.Services.Interface;
using PowerFront.Mapping;

namespace OperatorReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOperatorReportService _operatorReportService;

        public HomeController(IOperatorReportService operatorReportService)
        {
            _operatorReportService = operatorReportService;
        }
        public ActionResult Index(string dateOption, DateTime? fromDate, DateTime? toDate, string website, string device)
        {
            OperatorReportItems ProductivityReport = new OperatorReportItems();
            ProductivityReport.OperatorProductivity = new List<OperatorReportViewModel>();

            ViewBag.Message = "Operator Productivity Report";

            ProductivityReport = OperatorReportMapper.MapToViewModel(_operatorReportService.GetOperatorReport(dateOption, fromDate, toDate, website, device));

            return View(ProductivityReport.OperatorProductivity);
        }
    }
}