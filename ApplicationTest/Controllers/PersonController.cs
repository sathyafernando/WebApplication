using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApplicationTest.Helpers;
using ApplicationTestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApplicationTest.Controllers
{
    public class PersonController : Controller
    {
        Helper _helper = new Helper();
        HttpClient client;

        public PersonController()
        {
            client = _helper.Initial();
        }
        public async Task<IActionResult> Index()
        {
            var personList = new List<Person>();
            var response = await client.GetAsync("Person");

            if (response.IsSuccessStatusCode)
            {
                var readPerson = response.Content.ReadAsStringAsync().Result;
                personList = JsonConvert.DeserializeObject<List<Person>>(readPerson);
            }

            return View(personList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = new Person();

            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync($"Person/{id}");
            if (response.IsSuccessStatusCode)
            {
                var personRead = response.Content.ReadAsStringAsync().Result;
                person = JsonConvert.DeserializeObject<Person>(personRead);
            }

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        public async Task<IActionResult> Edit(int id)
        {
            Person person = new Person();
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync($"Person/{id}");
            if (response.IsSuccessStatusCode)
            {
                var personRead = response.Content.ReadAsStringAsync().Result;
                person = JsonConvert.DeserializeObject<Person>(personRead);
            }

            return View(person);

        }

        public async Task<IActionResult> Delete(int id)
        {
            await client.DeleteAsync($"Person/{id}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            Person person = new Person();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {

            if (ModelState.IsValid)
            {
                string jsonString = JsonConvert.SerializeObject(person);
                using (HttpContent httpContent = new StringContent(jsonString))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await client.PutAsync("Person", httpContent);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}
