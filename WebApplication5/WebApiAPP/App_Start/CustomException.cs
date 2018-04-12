using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApiAPP.App_Start
{
    public class CustomException : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {

            context.Response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

        }

    }
}