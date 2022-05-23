using Microsoft.AspNetCore.Mvc;

namespace Training.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorldController : Controller
{
    public HelloWorldController()
    {

    }

    public string Index()
    {
        return "Hello World! Patate";
    }
}
