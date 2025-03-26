using Domain.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjects();
}
