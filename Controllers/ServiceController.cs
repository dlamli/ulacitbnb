using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ulacit_bnb.Controllers
{
    [Authorize]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {

    }
}
