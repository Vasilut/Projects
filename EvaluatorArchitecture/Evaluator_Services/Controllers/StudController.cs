using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [Route("api/Stud/OpenRandomProcess")]
        [HttpGet]
        public string OpenRandomProcess()
        {
            string sProcessPath = @"C:\sorta\grase.exe";
            ProcessStartInfo psiStartInfo = new ProcessStartInfo()
            {
                FileName = sProcessPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            Process proc = new Process()
            {
                StartInfo = psiStartInfo
            };

            string sReturnValue = string.Empty;

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                sReturnValue += (line + Environment.NewLine);
            }

            return sReturnValue;
        }

    }
}
