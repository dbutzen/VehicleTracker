using DTB.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DTB.VehicleTracker.Web.Controllers
{
    public class VehicleController : Controller
    {

        // GET: VehicleController
        public ActionResult Index()
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            
            response = client.GetAsync("Vehicle").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Vehicle> vehicles = items.ToObject<List<Vehicle>>();

            return View(vehicles);
        }

        private static HttpClient InitializeClient()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44343/api/");
            //client.BaseAddress = new Uri("https://vehicletrackerapi.azurewebsites.net/api/");
            return client;
        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle)
        {
            try
            {
                HttpResponseMessage response;
                string result;
                dynamic items;


                HttpClient client = InitializeClient();

                response = client.GetAsync("Vehicle/" + vehicle.ColorName).Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Vehicle> vehicles = items.ToObject<List<Vehicle>>();

                
                return View(nameof(Index), vehicles);
            }
            catch (Exception ex)
            {
                return View(vehicle);
            }
        }

        

        // GET: VehicleController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpResponseMessage response;
            string result;
            dynamic item;


            HttpClient client = InitializeClient();

            response = client.GetAsync("Vehicle/" + id).Result;
            result = response.Content.ReadAsStringAsync().Result;
            item = JsonConvert.DeserializeObject(result);
            Vehicle vehicle = item.ToObject<Vehicle>();

            return View(vehicle);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Vehicle vehicle)
        {
            try
            {
                HttpClient client = InitializeClient();
                string serializedObject = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Vehicle/" + vehicle.Id, content).Result;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(vehicle);
            }
        }
    }
}
