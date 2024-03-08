using PowerFront.DTO;

namespace PowerFront.Repository
{
    public interface IOperatorReportRepository
    {
        OperatorReportItemsDTO GetOperatorReport(string dateOption, DateTime? fromDate, DateTime? toDate, string website, string device);

        List<string> GetDistinctWebsites();

        List<string> GetDistinctDevices();
    }
}
