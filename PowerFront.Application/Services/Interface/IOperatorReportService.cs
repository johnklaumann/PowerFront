using PowerFront.DTO;

namespace PowerFront.Application.Services.Interface
{
    public interface IOperatorReportService
    {
        OperatorReportItemsDTO GetOperatorReport(string dateOption, DateTime? fromDate, DateTime? toDate, string website, string device);

        List<string> GetDistinctDevices();

        List<string> GetDistinctWebsites();
    }
}
