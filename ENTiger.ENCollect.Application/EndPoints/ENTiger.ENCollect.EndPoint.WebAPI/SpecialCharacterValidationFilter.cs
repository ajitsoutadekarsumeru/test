using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.EndPoint.WebAPI
{
    public class SpecialCharacterValidationFilter : ActionFilterAttribute
    {
        //regular expression that allows: English,Digits,Special chars \{\}\[\]:,.\-\\_\""<>\@+/=
        //Devanagari (Hindi, Marathi),Tamil,Telugu,Kannada,Malayalam,
        private static readonly string SpecialCharacterPattern = @"^([a-zA-Z0-9\u0900-\u097F\u0B80-\u0BFF\u0C00-\u0C7F\u0C80-\u0CFF\u0D00-\u0D7F \{\}\[\]:,.\-\\_\""<>\@+/=]|<<|>>)+$";

        // Override for the action execution
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments;
            // Only process the body if it's a POST or PUT request
            if (model == null || model.Count == 0)
            {
                await next(); // Proceed if no content in the body
                return;
            }
            foreach (var entry in model)
            {
                // Skip file validation
                if (entry.Value is IFormFile)
                {
                    continue;
                }
                // Check if the value is a string
                if (entry.Value is string value)
                {
                    // If invalid characters are found, return the error
                    if (!Regex.IsMatch(value, SpecialCharacterPattern))
                    {
                        // Throw error if special characters are found
                        context.Result = CreateErrorResponse(context, "Special characters are not allowed in the input");
                        return;
                    }
                }
                else if (entry.Value != null)
                {
                    // If the value is not a string, try to convert it to string and check for special characters
                    string valueString = Convert.ToString(entry.Value) ?? string.Empty;
                    if (entry.Value is not string && entry.Value.GetType().IsClass)
                    {
                        // For complex objects, convert to JSON string using JSON serialization
                        valueString = JsonConvert.SerializeObject(entry.Value);
                    }

                    // Check for special characters in the string representation of the object
                    if (!Regex.IsMatch(valueString, SpecialCharacterPattern)
                        // Block script tag explicitly
                        || Regex.IsMatch(valueString, @"(?i)<\s*script\b[^>]*>(.*?)<\s*/\s*script\s*>"))
                    {
                        // Throw error if special characters are found
                        context.Result = CreateErrorResponse(context, "Special characters are not allowed in the input");
                        return;
                    }

                    //Optional: Block Other Dangerous HTML elements like<iframe>, < object >, < embed >, < style >, etc.
                    //string[] blockedTags = { "script", "iframe", "object", "embed", "style" };
                    //foreach (var tag in blockedTags)
                    //{
                    //    var tagPattern = $@"(?i)<\s*{tag}\b[^>]*>(.*?)<\s*/\s*{tag}\s*>";
                    //    if (Regex.IsMatch(valueString, tagPattern))
                    //    {
                    //        context.Result = CreateErrorResponse(context, $"{tag} tags are not allowed in the input");
                    //        return;
                    //    }
                    //}
                }
            }

            // Proceed with the next action
            await next();
        }

        // Method for creating a standardized error response
        private IActionResult CreateErrorResponse(ActionExecutingContext context, string errorMessage)
        {
            var standardResponse = new
            {
                status = "error",
                message = "One or more validation errors occurred.",
                code = StatusCodes.Status400BadRequest,
                errors = new { Error = new JArray(errorMessage) },
                metadata = new
                {
                    timestamp = DateTime.UtcNow.ToString("o"),
                    traceId = context.HttpContext.TraceIdentifier
                }
            };

            return new JsonResult(standardResponse)
            {
                StatusCode = 400
            };
        }
    }
}
