using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAB1consumer.Models;

public class ProjectController : Controller
{
    private readonly HttpClient _httpClient;

    public ProjectController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5216/api/");
    }

    
    public async Task<IActionResult> Index()
    {
        var projects = await _httpClient.GetFromJsonAsync<List<Project>>("Project");
        return View(projects);
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var project = await _httpClient.GetFromJsonAsync<Project>($"Project/{id}");
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    
    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await GetDepartmentsAsync();
        ViewBag.Employees = await GetEmployeesAsync();
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project project)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("Project", project);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        // Refetch Departments and Employees in case of validation failure
        ViewBag.Departments = await GetDepartmentsAsync();
        ViewBag.Employees = await GetEmployeesAsync();
        return View(project);
    }

   
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _httpClient.GetFromJsonAsync<Project>($"Project/{id}");
        if (project == null)
        {
            return NotFound();
        }
        ViewBag.Departments = await GetDepartmentsAsync();
        ViewBag.Employees = await GetEmployeesAsync();
        return View(project);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Project project)
    {
        if (id != project.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"Project/{id}", project);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.Departments = await GetDepartmentsAsync();
        ViewBag.Employees = await GetEmployeesAsync();
        return View(project);
    }

    
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _httpClient.GetFromJsonAsync<Project>($"Project/{id}");
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"Project/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    // Helper Methods to Fetch Departments and Employees
    private async Task<List<Department>> GetDepartmentsAsync()
    {
        var response = await _httpClient.GetAsync("Department");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Department>>();
        }
        return new List<Department>();
    }

    private async Task<List<Employee>> GetEmployeesAsync()
    {
        var response = await _httpClient.GetAsync("Employee");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Employee>>();
        }
        return new List<Employee>();
    }
}
