using System;
using System.Threading.Tasks;
using Argo.MyProject.Contracts;
using Argo.MyProject.Model;
using Microsoft.Extensions.Logging;

// TODO: Make as many BL classes that you need to perform business logic work.  If you configure PostSharp to log public methods, 
// then any method marked public or protected will have the call in and return value logged. (Assuming the GlobalAspect file is unchanged.)
namespace Argo.MyProject.Bl
{
    /// <summary>
    /// What is the purpose of this BL class
    /// </summary>
    public class MyProjectBl : IMyProjectBl
    {
        private static Random _random = new Random();
        private readonly ILogger<MyProjectBl> _logger;

        /// <summary>
        /// Describe your parameters
        /// </summary>
        /// <param name="logger">This initiates the logger</param>
        public MyProjectBl(ILogger<MyProjectBl> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Here is the awesome Bl method.  Describe what it does.
        /// </summary>
        /// <param name="myParam">What does this parameter do?</param>
        /// <returns></returns>
        public async Task<MyProjectDTO> GetDetails(Guid myParam)
        {
            MyProjectDTO results = null;
            await Task.Run(() => results= new MyProjectDTO
            {
                DataId = myParam,
                DataPoint1 = "I am a string",
                HasMoreData = Convert.ToBoolean(_random.Next(0,1)),
                NextId = _random.Next(1,1000),
                PIIData = "This is Sensitive Data"
            });
            _logger.LogInformation("I did a thing."); // This should go to the Trace file.

            return results;
        }
    }
}
