using Microsoft.AspNetCore.Mvc;
using NetEscapades.AspNetCore.SecurityHeaders;

namespace SecurityHeadersMiddlewareWebSite;

public class HomeController : ControllerBase
{
    [HttpGet("/api/index")]
    public IActionResult Index()
    {
        return Content("/api/index", "text/html");
    }

    [HttpGet("/api/custom"), SecurityHeadersPolicy("CustomHeader")]
    public string Custom() => "Hello Controller!";
}