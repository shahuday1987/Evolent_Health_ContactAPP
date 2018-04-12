using Dapper;
using DataTrasferObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Repositories
{

    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ContactDBConnection"].ConnectionString;

        public List<ContactDTO> GetContactList()
        {
            List<ContactDTO> lstContactDTO = new List<ContactDTO>();

            IDbConnection dbConnection = null;

            try
            {
                using (dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var result = dbConnection.QueryMultiple(Constant.SP_GetContactList, commandType: CommandType.StoredProcedure);
                    lstContactDTO = result.Read<ContactDTO>().ToList();

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseDbConnection(dbConnection);
            }

            return lstContactDTO;
        }
        private void CloseDbConnection(IDbConnection dbConnection)
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
            }
        }
        public ResponseDTO AddContactDetails(ContactDTO contactDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            IDbConnection dbConnection = null;

            try
            {
                using (dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var param = new DynamicParameters();
                    param.Add("@FirstName", contactDTO.FirstName);
                    param.Add("@LastName", contactDTO.LastName);
                    param.Add("@EmailId", contactDTO.EmailId == null ? "NA" : contactDTO.EmailId);
                    param.Add("@PhoneNumber", contactDTO.PhoneNumber);
                    var result = dbConnection.QueryMultiple(Constant.SP_AddContactDetails, param, commandType: CommandType.StoredProcedure);

                    var ret = result.Read<int>().ToList().Single();
                    responseDTO.IsSuccess = ret > 0 ? true : false;
                    responseDTO.Message = ret > 0 ? "Success" : "Some error occured"; ;
                }
            }
            catch
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = "Some error occured";
                throw;
            }
            finally
            {
                CloseDbConnection(dbConnection);
            }

            return responseDTO;
        }
        public ResponseDTO UpdateContactDetails(ContactDTO contactDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            IDbConnection dbConnection = null;

            try
            {
                using (dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", contactDTO.Id);
                    param.Add("@FirstName", contactDTO.FirstName);
                    param.Add("@LastName", contactDTO.LastName);
                    param.Add("@EmailId", contactDTO.EmailId == null ? "NA" : contactDTO.EmailId);
                    param.Add("@PhoneNumber", contactDTO.PhoneNumber);
                    param.Add("@ActiveStatus", contactDTO.ActiveStatus);
                    var result = dbConnection.QueryMultiple(Constant.SP_UpdateContactDetails, param, commandType: CommandType.StoredProcedure);
                    var ret = result.Read<int>().ToList().Single();
                    responseDTO.IsSuccess = ret > 0 ? true : false;
                    responseDTO.Message = ret > 0 ? "Success" : "Some error occured"; ;
                }
            }
            catch
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = "Some error occured";
                throw;
            }
            finally
            {
                CloseDbConnection(dbConnection);
            }

            return responseDTO;
        }
        public ResponseDTO DeleteContactDetails(ContactDTO contactDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            IDbConnection dbConnection = null;

            try
            {
                using (dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", contactDTO.Id);

                    var result = dbConnection.QueryMultiple(Constant.SP_DeleteContactDetails, param, commandType: CommandType.StoredProcedure);
                    var ret = result.Read<int>().ToList().Single();
                    responseDTO.IsSuccess = ret > 0 ? true : false;
                    responseDTO.Message = ret > 0 ? "Success" : "Some error occured"; ;
                }
            }
            catch
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = "Some error occured";
                throw;
            }
            finally
            {
                CloseDbConnection(dbConnection);
            }

            return responseDTO;
        }


    }
}
