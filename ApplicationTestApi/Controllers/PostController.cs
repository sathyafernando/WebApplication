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

namespace ApplicationTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        Helper _helper = new Helper();
        HttpClient client;


        [HttpGet("")]
        public async Task<string> Details()
        {
            var response = await client.GetAsync("Post");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return null;
        }

        [HttpGet("{id}")]
        public async Task<string> Detail(int id)
        {
            var response = await client.GetAsync($"Post/{id}");
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;

            }

            return null;

        }

        [HttpPost]
        public async Task<IActionResult> Save(Person model)
        {
            string jsonString = JsonConvert.SerializeObject(model);
            using (HttpContent httpContent = new StringContent(jsonString))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await client.PutAsync("Person", httpContent);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await client.DeleteAsync($"Post/{id}");
            return Ok();
        }
    }
}
