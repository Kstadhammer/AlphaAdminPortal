namespace Business.Interfaces;

using Domain.Models;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembers();
}
