using BusinessServiceLayer.Account;
using DataTrasferObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAPP.App_Start;
using WebApiAPP.Logging;

namespace WebApiAPP.Controllers
{
    public class ContactController : ApiController
    { 
        private IContact _objContact;
        private readonly ILogAdapter _logger;
        public ContactController(IContact objContact)
        {
            _objContact = objContact;
            _logger = LoggerFactory.GetLogger();
        }

        [ActionName("GetContactList")]
        [HttpGet]
        //[CustomException]
        public List<ContactDTO> GetContactList()
        {
            try
            {
                return _objContact.GetContactList();
            }
            catch (Exception ex)
            {
                _logger.WriteMessage("ContactController:GetContactList", NLogAdapter.LogLevel.ERROR, ex);
                return new List<ContactDTO>();
            }

             
        }

      
        
        [HttpPost]
        [Route("api/Contact/AddContactDetails")]
        public ResponseDTO AddContactDetails(ContactDTO contactDTO)
        {
            try
            {
                return _objContact.AddContactDetails(contactDTO);
            }
            catch (Exception ex)
            {
                _logger.WriteMessage("ContactController:AddContactDetails", NLogAdapter.LogLevel.ERROR, ex);
                return new ResponseDTO { IsSuccess = false };

            }

        }
        [HttpPost]       
        [Route("api/Contact/UpdateContactDetails")]
        public ResponseDTO UpdateContactDetails(ContactDTO contactDTO)
        {
            try
            {
                return _objContact.UpdateContactDetails(contactDTO);
            }
            catch (Exception ex)
            {
                _logger.WriteMessage("ContactController:UpdateContactDetails", NLogAdapter.LogLevel.ERROR, ex);
                return new ResponseDTO { IsSuccess = false };

            }

        }
        [HttpPost]
        [Route("api/Contact/DeleteContactDetails")]
        public ResponseDTO DeleteContactDetails(ContactDTO contactDTO)
        {
            try
            {
                return _objContact.DeleteContactDetails(contactDTO);
            }
            catch (Exception ex)
            {
                _logger.WriteMessage("ContactController:DeleteContactDetails", NLogAdapter.LogLevel.ERROR, ex);
                return new ResponseDTO { IsSuccess = false };

            }

        }

    }
}
