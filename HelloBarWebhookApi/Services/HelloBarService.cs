using HelloBarWebhookApi.Controllers;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HelloBarWebhookApi.Services
{
    public class HelloBarService : IHelloBarService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HelloBarService> _logger;

        public HelloBarService(
            IConfiguration configuration,
            ILogger<HelloBarService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int InsertHelloBarEmail(string name,string email,string? winningOffer,string? winningOfferCode)
        {
            try
            {
                string connectionString =
                    _configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException(
                        "DefaultConnection is not configured.");

                using SqlConnection connection = new SqlConnection(connectionString);

                using SqlCommand command = new SqlCommand(
                    "[NetFloristMailMaton].[dbo].[InsertEmailMember_VoucherDetails]",
                    connection);

                command.CommandType = CommandType.StoredProcedure;

                // Stored Procedure Parameters
                command.Parameters.AddWithValue("@ReferenceId", "HelloBar");
                command.Parameters.AddWithValue("@EmailAddress", email);
                command.Parameters.AddWithValue("@Username", name);
                command.Parameters.AddWithValue("@FirstName", name);

                // HelloBar Voucher Parameters
                command.Parameters.AddWithValue(
                    "@WinningOffer",
                    (object?)winningOffer ?? DBNull.Value);

                command.Parameters.AddWithValue(
                    "@WinningOfferCode",
                    (object?)winningOfferCode ?? DBNull.Value);

                // Output parameter
                SqlParameter returnValueParameter = new SqlParameter(
                    "@ReturnValue",
                    SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(returnValueParameter);

                connection.Open();
                command.ExecuteNonQuery();

                if (returnValueParameter.Value == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(returnValueParameter.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error inserting Hello Bar email. Email: {Email}",
                    email);

                throw;
            }
        }

        public int InsertHelloBarEmail(string name, string email)
        {
            try
            {
                string connectionString =
                    _configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException(
                        "DefaultConnection is not configured.");

                using SqlConnection connection =
                    new SqlConnection(connectionString);

                using SqlCommand command = new SqlCommand("[NetFloristMailMaton].[dbo].[InsertEmailMember]", connection); command.CommandType = CommandType.StoredProcedure;

                // Stored Procedure Parameters
                command.Parameters.AddWithValue("@ReferenceId", "HelloBar");
                command.Parameters.AddWithValue("@EmailAddress", email);
                command.Parameters.AddWithValue("@Username", name);
                command.Parameters.AddWithValue("@FirstName", name);
                // Output parameter
                SqlParameter returnValueParameter = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.Output };
                command.Parameters.Add(returnValueParameter);
                connection.Open();
                command.ExecuteNonQuery();
                if (returnValueParameter.Value == DBNull.Value) { return 0; }

                return Convert.ToInt32(returnValueParameter.Value);

                //// If your stored procedure is later updated to accept
                //// these parameters, uncomment the following:
                ////
                //// command.Parameters.AddWithValue(
                ////     "@WinningOffer",
                ////     (object?)winningOffer ?? DBNull.Value);
                ////
                //// command.Parameters.AddWithValue(
                ////     "@WinningOfferCode",
                ////     (object?)winningOfferCode ?? DBNull.Value);

                //connection.Open();

                //object? result = command.ExecuteScalar();

                //if (result == null || result == DBNull.Value)
                //{
                //    return 0;
                //}

                //return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error inserting Hello Bar email. Email: {Email}",
                    email);

                throw;
            }
        }
    }
}
