using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace EvaluatorArchitecture.Controllers
{
    public class StudController : Controller
    {
        private readonly string apiMethodsUrl = "http://localhost:43554/";
        // GET: Stud
        public ActionResult Index()
        {
            IEnumerable<string> rezult = new string[] { "Null" };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiMethodsUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync("api/Stud/Another").Result;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                }
                else
                {
                    ViewBag.result = "Eroare tata";
                }
            }

            return View();
        }

        public ActionResult GetExampleSource()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiMethodsUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
                HttpResponseMessage response = client.GetAsync("api/Stud/OpenRandomProcess").Result;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.result = response.Content.ReadAsAsync<string>().Result;
                }
                else
                {
                    ViewBag.result = "Eroare tata";
                }
            }
            return View();
        }
    }
}