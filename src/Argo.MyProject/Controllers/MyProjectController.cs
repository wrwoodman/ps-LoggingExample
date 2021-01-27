using System;
using System.Threading.Tasks;
using Argo.MyProject.Contracts;
using Argo.MyProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Argo.MyProject.Controllers
{
    /// <summary>
    /// TODO: Describe what this controller does?
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class MyProjectController : ControllerBase
    {
        private readonly IMyProjectBl _myProjectBl;
        private readonly ILogger<MyProjectController> _logger;

        /// <summary>
        /// What do all these variables do?
        /// </summary>
        /// <param name="logger">Class logger for this controller to log events and errors</param>
        /// <param name="myProjectBl">This should be the BL class that this controller will call do do the work.</param>
        public MyProjectController(ILogger<MyProjectController> logger,
            IMyProjectBl myProjectBl)
        {
            _logger = logger;
            _myProjectBl = myProjectBl;
        }

        /// <summary>
        /// TODO: Add a description for your service.
        /// </summary>
        /// <param name="myParam">Add a description for your parameter here.</param>
        /// <returns></returns>
        /// <remarks>Use remarks to add additional information about the service.
        /// Always make controller services async.<br />
        /// Use Task and ActionResult as part of your return type.  If you are returning data, make that part of the action result as shown below.<br/>
        /// Every status that is returned should be defined by the ProducesResponseType attribute. The most common are added to the example below.<br/>
        /// A 400, Bad Request, should be a problem with the call and something that the client should be able to correct.<br/>
        /// A 500, Server Error, should provide details on what went wrong.  This is typically out of the client's ability to correct.<br/>
        /// See https://httpstatuses.com/ for a comprehensive list of codes and definitions.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MyProjectDTO>> GetDetails(Guid myParam)
        {
            try
            {
                var response = await _myProjectBl.GetDetails(myParam);
                // This would be a good place to write out some application specific log message.
                _logger.LogInformation("I did a thing completed.");
                return Ok(response);
            }
            catch (Exception exception)
            {
                // TODO: Set your error message.
                var message = "Failed to do a thing.";
                _logger.LogError(exception, message);  // PostSharp will automatically log the exception.  Use this to add more information.
                // This returns a ProblemDetails object with information on what went wrong.  Read more about it at https://www.intacs.com/how-to-use-the-problemdetails-middleware-in-asp-net-core/
                return Problem(message, HttpContext.Request.Path, StatusCodes.Status500InternalServerError, "Internal Status Error", exception.GetType().ToString());
            }
        }

    }
}
