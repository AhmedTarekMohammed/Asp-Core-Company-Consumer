using LAB1consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB1consumer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5216/api/");
        }

        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Employee");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return View(data);
            }
            return View(new List<Employee>());
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"Employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Employee>();
                return View(data);
            }
            return NotFound();
        }

        //// GET: Employee/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Employee/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("Employee", employee);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(employee);
        //}

        
        public async Task<IActionResult> Create()
        {
            var departments = await GetDepartmentsAsync();
            ViewBag.Departments = departments;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Employee", employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // fe Moshkela . Refetch departments for the view.
            var departments = await GetDepartmentsAsync();
            ViewBag.Departments = departments;
            return View(employee);
        }

        
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await GetDepartmentsAsync();
            return Json(departments);
        }

        
        public async Task<IActionResult> GetProjectsByDepartment(int id)
        {
            var response = await _httpClient.GetAsync($"Department/{id}");
            if (response.IsSuccessStatusCode)
            {
                var department = await response.Content.ReadFromJsonAsync<Department>();
                return Json(department.projectsNames);
            }
            return Json(new List<string>());
        }

        private async Task<List<Department>> GetDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("Department");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Department>>();
            }
            return new List<Department>();
        }




        //// GET: Employee/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var response = await _httpClient.GetAsync($"Employee/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var data = await response.Content.ReadFromJsonAsync<Employee>();
        //        return View(data);
        //    }
        //    return NotFound();
        //}

        //// POST: Employee/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _httpClient.PutAsJsonAsync($"Employee/{id}", employee);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(employee);
        //}


        
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();

                // Fetch departments to populate the dropdown
                var departments = await GetDepartmentsAsync();
                ViewBag.Departments = departments;

                return View(employee);
            }
            return NotFound();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Employee/{id}", employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // fe moshkela. Refetch departments for the view.
            var departments = await GetDepartmentsAsync();
            ViewBag.Departments = departments;

            return View(employee);
        }


        
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Employee>();
                return View(data);
            }
            return NotFound();
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
    }

}
