using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexControllerBridge<TController> : FlexController where TController : FlexControllerBridge<TController>
    {
        protected readonly ILogger<TController> _logger;
        protected readonly IRateLimiter _rateLimiter;
        public FlexControllerBridge(ILogger<TController> logger, IRateLimiter rateLimiter) : base()
        {
            _logger = logger;
            _rateLimiter = rateLimiter;
        }

        protected IActionResult Guard<TDto>(TDto value) where TDto : DtoBridge
        {
            if (value == null)
            {
                ModelState.AddModelError("requesterror", $"{nameof(TDto)} can not be empty");
                _logger.LogDebug($"{nameof(TDto)} can not be empty - {DateTime.Now}");
                return ControllerContext.ReturnFlexBadRequestError();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        protected IActionResult Guard(object value, string modelName)
        {
            if (value == null)
            {
                var errorMessage = $"{modelName} can not be empty";
                ModelState.AddModelError("requesterror", errorMessage);
                _logger.LogDebug(errorMessage + " " + DateTime.Now.ToString());
                return ControllerContext.ReturnFlexBadRequestError();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return null;
        }

        public IActionResult RateLimit<TInputDto>(TInputDto value, string apiName) where TInputDto : DtoBridge
        {
            var validationResult = Guard(value, nameof(TInputDto));
            if (validationResult != null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            // 1) Populate UserId into TInputDto
            value.EnrichWithAppContext();

            // 2) Extract userId from the context
            var userId = value.GetAppContext().UserId;

            try
            {
                // 3) Enforce rate limit
                _rateLimiter.EnforceRateLimit(apiName, userId);                
            }
            catch (RateLimitExceededException ex)
            {
                _logger.LogError("RateLimitExceededException : " +  ex.Message);
                // Returning 429 Too Many Requests with a message
                ModelState.AddModelError("Error", "Too many requests. Please try again later.");
                return new ObjectResult(ModelState)
                {
                    StatusCode = StatusCodes.Status429TooManyRequests
                };
            }
            return null;
        }

        public async Task<IActionResult> RunService<TInputDto>(
                    Int32 ReturnStatusCode, TInputDto value, Func<TInputDto, Task<CommandResult>> processMethod) where TInputDto : DtoBridge
        {
            var validationResult = Guard(value, nameof(TInputDto));
            if (validationResult != null)
            {
                return validationResult;
            }

            value.EnrichWithAppContext();

            var cmdResult = await processMethod(value);

            if (cmdResult.Status != Status.Success)
            {
                return ProcessCmdErrorResult(cmdResult);
            }

            return StatusCode(ReturnStatusCode, cmdResult.result);
        }

        public IActionResult RunQueryPagedService<TParams, TDto>(
                    TParams parameters, Func<TParams, FlexiPagedList<TDto>> processMethod) where TDto : class, new() where TParams : PagedQueryParamsDtoBridge
        {
            var validationResult = Guard(parameters, nameof(TParams));
            if (validationResult != null)
            {
                return validationResult;
            }

            parameters.EnrichWithAppContext();

            var result = processMethod(parameters);
            ProcessPagination(result);

            return Ok(result);
        }

        private void ProcessPagination<TDto>(FlexiPagedList<TDto> result) where TDto : class, new()
        {
            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        }

        protected IActionResult RunQuerySingleService<TParameters, TResult>(TParameters parameters,
                    Func<TParameters, TResult> processMethod) where TParameters : DtoBridge where TResult : class, new()
        {
            parameters.EnrichWithAppContext();

            // Execute the query
            var queryResult = processMethod(parameters);

            // Return the result based on the query outcome
            return queryResult != null ? Ok(queryResult) : NoContent();
        }

        public IActionResult RunQueryListService<TParams, TDto>(
                    TParams parameters, Func<TParams, IEnumerable<TDto>> processMethod) where TParams : DtoBridge where TDto : class, new()
        {
            var validationResult = Guard(parameters, nameof(DtoBridge));
            if (validationResult != null)
            {
                return validationResult;
            }

            parameters.EnrichWithAppContext();

            var queryResult = processMethod(parameters);

            if (queryResult != null) //&& queryResult.Count() > 0)
                return Ok(queryResult);
            else
                return NoContent();
        }
               

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ProcessCmdErrorResult(CommandResult cmdResult)
        {
            ModelState.ComposeFlexBadRequestError(cmdResult.Errors());
            return ControllerContext.ReturnFlexBadRequestError();
        }

        //-------------Async Methods----------------------------------------
        public async Task<IActionResult> RunQueryListServiceAsync<TParams, TDto>(
                    TParams parameters, Func<TParams, Task<IEnumerable<TDto>>> processMethod) where TParams : DtoBridge where TDto : class, new()
        {
            var validationResult = Guard(parameters, nameof(DtoBridge));
            if (validationResult != null)
            {
                return validationResult;
            }

            parameters.EnrichWithAppContext();

            var queryResult = processMethod(parameters);

            if (queryResult?.Result != null)
                return Ok(queryResult?.Result);
            else
                return NoContent();
        }

        public async Task<IActionResult> RunQueryPagedServiceAsync<TParams, TDto>(
                    TParams parameters, Func<TParams, Task<FlexiPagedList<TDto>>> processMethod) where TDto : class, new() where TParams : PagedQueryParamsDtoBridge
        {
            var validationResult = Guard(parameters, nameof(TParams));
            if (validationResult != null)
            {
                return validationResult;
            }

            parameters.EnrichWithAppContext();

            var result = processMethod(parameters);
            ProcessPagination(result?.Result);

            return Ok(result?.Result);
        }

        protected async Task<IActionResult> RunQuerySingleServiceAsync<TParameters, TResult>(TParameters parameters,
                    Func<TParameters, Task<TResult>> processMethod) where TParameters : DtoBridge where TResult : class, new()
        {
            parameters.EnrichWithAppContext();

            // Execute the query
            var queryResult = processMethod(parameters);

            // Return the result based on the query outcome
            return queryResult?.Result != null ? Ok(queryResult?.Result) : NoContent();
        }
    }
}