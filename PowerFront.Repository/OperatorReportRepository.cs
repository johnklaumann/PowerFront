using PowerFront.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PowerFront.Repository
{
    public class OperatorReportRepository : IOperatorReportRepository
    {
        private readonly string _connectionString;

        public OperatorReportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OperatorReportItemsDTO GetOperatorReport(string dateOption, DateTime? fromDate, DateTime? toDate, string website, string device)
        {
            OperatorReportItemsDTO productivityReport = new OperatorReportItemsDTO();
            productivityReport.OperatorProductivity = new List<OperatorReportViewModelDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlcomm = new SqlCommand("dbo.OperatorProductivity", conn);
                    sqlcomm.CommandType = CommandType.StoredProcedure;

                    // Add parameters based on the filters
                    sqlcomm.Parameters.AddWithValue("@DateOption", dateOption ?? string.Empty);
                    sqlcomm.Parameters.AddWithValue("@FromDate", fromDate.HasValue ? fromDate.Value : (object)DBNull.Value);
                    sqlcomm.Parameters.AddWithValue("@ToDate", toDate.HasValue ? toDate.Value : (object)DBNull.Value);
                    sqlcomm.Parameters.AddWithValue("@Website", website ?? string.Empty);
                    sqlcomm.Parameters.AddWithValue("@Device", device ?? string.Empty);

                    conn.Open();
                    using (SqlDataReader dr = sqlcomm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OperatorReportViewModelDTO opVM = new OperatorReportViewModelDTO();
                            opVM.ID = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                            opVM.Name = dr[1].ToString();
                            opVM.ProactiveAnswered = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0;
                            opVM.ProactiveSent = dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0;
                            opVM.ProactiveResponseRate = dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0;
                            opVM.ReactiveAnswered = dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0;
                            opVM.ReactiveReceived = dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0;
                            opVM.ReactiveResponseRate = dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0;
                            opVM.AverageChatLength = dr[8].ToString();
                            opVM.TotalChatLength = dr[9].ToString();
                            productivityReport.OperatorProductivity.Add(opVM);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                //throw ex;
            }

            return productivityReport;
        }

        public List<string> GetDistinctWebsites()
        {
            List<string> websites = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlcomm = new SqlCommand("SELECT DISTINCT Website FROM conversation", conn);
                    conn.Open();
                    using (SqlDataReader dr = sqlcomm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string website = dr["Website"].ToString();
                            if (!string.IsNullOrEmpty(website))
                            {
                                websites.Add(website);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                // throw ex;
            }

            return websites;
        }

        public List<string> GetDistinctDevices()
        {
            List<string> devices = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlcomm = new SqlCommand("SELECT DISTINCT Device FROM visitor", conn);
                    conn.Open();
                    using (SqlDataReader dr = sqlcomm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string device = dr["Device"].ToString();
                            if (!string.IsNullOrEmpty(device))
                            {
                                devices.Add(device);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                // throw ex;
            }

            return devices;
        }

    }
}
