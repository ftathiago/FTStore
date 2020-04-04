using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Routing;

namespace FTStore.Web.Tests
{
    public class ProductControllerFixture
    {
        public ControllerContext RequestWithoutFile(string fileName)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", fileName);
            var formFileCollection = new FormFileCollection();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), formFileCollection);
            var actx = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            var ControllerContext = new ControllerContext();
            return new ControllerContext(actx);
        }

        public ControllerContext RequestWithFile(string fileName)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", fileName);
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });
            var actx = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            var ControllerContext = new ControllerContext();
            return new ControllerContext(actx);
        }
    }
}
