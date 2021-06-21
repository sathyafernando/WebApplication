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
    public class PostController : Controller
    {
        Helper _helper = new Helper();
        HttpClient client;

        public PostController()
        {
            client = _helper.Initial();
        }
        public async Task<IActionResult> Index()
        {
            var postList = new List<Post>();
            var response = await client.GetAsync("Post");

            if (response.IsSuccessStatusCode)
            {
                var readPost = response.Content.ReadAsStringAsync().Result;
                postList = JsonConvert.DeserializeObject<List<Post>>(readPost);
            }

            return View(postList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = new Post();

            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync($"Post/{id}");
            if (response.IsSuccessStatusCode)
            {
                var readPost = response.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<Post>(readPost);
            }

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Post post = new Post();
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync($"Post/{id}");
            if (response.IsSuccessStatusCode)
            {
                var readPost = response.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<Post>(readPost);
            }

            return View(post);

        }

        public async Task<IActionResult> Delete(int id)
        {
            await client.DeleteAsync($"Post/{id}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            Post post = new Post();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {

            if (ModelState.IsValid)
            {
                string jsonString = JsonConvert.SerializeObject(post);
                using (HttpContent httpContent = new StringContent(jsonString))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await client.PutAsync("Post", httpContent);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
    }
}
