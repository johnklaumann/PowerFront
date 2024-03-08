using PowerFront.Application.Services.Interface;
using PowerFront.DTO;
using PowerFront.Repository;

namespace PowerFront.Services
{
    public class OperatorReportService : IOperatorReportService
    {
        private readonly IOperatorReportRepository _repository;

        public OperatorReportService(IOperatorReportRepository repository)
        {
            _repository = repository;
        }

        public OperatorReportItemsDTO GetOperatorReport(string dateOption, DateTime? fromDate, DateTime? toDate, string website, string device)
        {
            try
            {
                return _repository.GetOperatorReport(dateOption, fromDate, toDate, website, device);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw ex;
            }
        }

        public List<string> GetDistinctDevices()
        {
            try
            {
                return _repository.GetDistinctDevices();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw ex;
            }
        }

        public List<string> GetDistinctWebsites()
        {
            try
            {
                return _repository.GetDistinctWebsites();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw ex;
            }
        }
    }
}
