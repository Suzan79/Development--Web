using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;



namespace SimpleModelsAndRelations.Controllers
{
  public partial class HomeController : Controller
  {
    private readonly ProjectNameOptions _projectNameOptions;
    

    public HomeController(IOptions<ProjectNameOptions> projectNameOptions )
    {
      _projectNameOptions = projectNameOptions.Value;
    }

    [Route("")]
    [HttpGet("Home/{*slug}")] 
    [HttpGet("Home/Index/{*slug}")]
    [HttpGet("{*slug}")]
    public IActionResult Index(string slug)
    {
      
      ViewData["Page"] = "Home/Index";
      ViewData["ProjectName"] = _projectNameOptions.Value;
      return View();
    }  
  }
}
