using LAB1consumer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LAB1consumer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HttpClient _httpClient;

        public DepartmentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5216/api/");
        }

        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Department");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<Department>>();
                return View(data);
            }
            return View(new List<Department>());
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Department/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Department>();
                return View(data);
            }
            return NotFound();
        }

       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Department", department);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Department/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Department>();
                return View(data);
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Department/{id}", department);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Department department)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var jsonPatch = new JsonPatchDocument<Department>();
        //        jsonPatch.Replace(d => d.name, department.name); // Only update the name

        //        var response = await _httpClient.PatchAsJsonAsync($"Department/{id}", jsonPatch);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(department);
        //}


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Department/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Department>();
                return View(data);
            }
            return NotFound();
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Department/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
    }

}
