using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Evaluator_Services.Controllers
{
    public class StudController : ApiController
    {
        
        public IEnumerable<string> Get()
        {
            IEnumerable<string> rezult = new string[] { "Luci", "Toader", "Iulica" };
            return rezult;
        }

        [Route("api/Stud/Get2")]
        [HttpGet]
        public IEnumerable<string> Get2()
        {
            IEnumerable<string> rezult = new string[] { "Luci", "Toader2", "Iulica" };
            return rezult;
        }

        [Route("api/Stud/Another")]
        [HttpGet]
        public IEnumerable<string> Another()
        {
            IEnumerable<string> rezult = new string[] { "Luci", "Toader3", "Iulica" };
            return rezult;
        }

    }
}
