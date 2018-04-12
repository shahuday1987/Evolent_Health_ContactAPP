using DataTrasferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Helpers;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        private readonly Helper helper;

        public AccountController()
        {
            helper = new Helper();
        }
        // GET: Account
        public ActionResult Index()
        {
            ////   throw new EntryPointNotFoundException() ;
            //var test = helper.GetContactList();
            return View();
        }

        [HttpGet]
        public ActionResult ContactList()
        {
            // Thread.Sleep(5000);
            List<ContactDTO> contactDTO = helper.GetContactList();
            return PartialView("_contactList", contactDTO);
        }
        [HttpGet]
        public ActionResult CreateContactList()
        {
            // Thread.Sleep(5000); 

            return PartialView("_createNew", new ContactDTO());
        }

        [HttpPost]
        public ActionResult CreateContactDetails(ContactDTO contactDTO)
        {
            ResponseDTO ret = new ResponseDTO { IsSuccess = false };
            if (ModelState.IsValid)
            {
                ret = helper.CreateNewContact(contactDTO);
                List<ContactDTO> contactDTOlst = helper.GetContactList();
                return PartialView("_contactList", contactDTOlst);
            }
            else
            {
                return this.Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Something goes wrong, please try again later"
                });
            }


        }
        [HttpPost]
        public ActionResult DeleteContact(string id)
        {

            ResponseDTO ret = new ResponseDTO { IsSuccess = false };
            if (ModelState.IsValid)
            {
                ContactDTO contactDTO = new ContactDTO { Id = Convert.ToInt32(id) };
                ret = helper.DeleteContactDetails(contactDTO);
                List<ContactDTO> contactDTOlst = helper.GetContactList();
                return PartialView("_contactList", contactDTOlst);
            }
            else
            {
                return this.Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Something goes wrong, please try again later"
                });
            }

        }



        [HttpGet]
        public ActionResult UpdateContactList(string id)
        {
            ContactDTO contactDTO = helper.GetContactList().Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();
            return PartialView("_updateContact", contactDTO != null ? contactDTO : new ContactDTO());
        }
        [HttpPost]
        public ActionResult UpdateDetails(ContactDTO contactDTO)
        {
            ResponseDTO ret = new ResponseDTO { IsSuccess = false };
            if (ModelState.IsValid)
            {

                ret = helper.UpdateContact(contactDTO);
                List<ContactDTO> contactDTOlst = helper.GetContactList();
                return PartialView("_contactList", contactDTOlst);
            }
            else
            {
                return this.Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Something goes wrong, please try again later"
                });
            }
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            //logger will come here
            var model = new HandleErrorInfo(filterContext.Exception, "AccountController", "Index");

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };

        }
    }
}