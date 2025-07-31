using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.EndPoint.WebAPI.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        // For handling 404 errors
        [HttpGet]
        [Route("404")]
        [HttpGet]
        public IActionResult HandleNotFound()
        {
            return NotFound(new
            {
                error = "Not Found",
                message = "Oops! This page does not exist. Please check the URL or contact support."
            });
        }
    }
}
