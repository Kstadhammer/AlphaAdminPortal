using Business.Interfaces;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        var list = await _userManager.Users.ToListAsync();
        var members = list.Select(x => new Member
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            JobTitle = x.JobTitle,
            Email = x.Email,
            Phone = x.PhoneNumber,
        });
        return members;
    }
}
