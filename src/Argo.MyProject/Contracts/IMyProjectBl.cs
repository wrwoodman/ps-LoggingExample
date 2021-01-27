using System;
using System.Threading.Tasks;
using Argo.MyProject.Model;
#pragma warning disable 1591 // XML Comments

// TODO: Every BL file should have a corresponding Interface for dependency injection.
namespace Argo.MyProject.Contracts
{
    /// <summary>
    /// TODO: If you put comments in the interface file, great.  If not, disable the warning if using xml comments. 
    /// </summary>
    public interface IMyProjectBl
    {
        Task<MyProjectDTO> GetDetails(Guid myParam);
    }
}
