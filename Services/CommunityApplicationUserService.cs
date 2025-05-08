using System;
using Capstone_Project.Data;
using Capstone_Project.Models.Feed;

namespace Capstone_Project.Services;

public class CommunityApplicationUserService
{
    private ApplicationDbContext _context;

    public CommunityApplicationUserService(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<bool> SaveAsync()
    {
        try
        {
            var rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> AddCommunityApplicationUserAsync(CommunityApplicationUser communityApplicationUser)
    {
        try
        {
            _context.CommunityApplicationUsers.Add(communityApplicationUser);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCommunityApplicationUserAsync(Guid id)
    {
        try
        {
            var communityApplicationUser = await _context.CommunityApplicationUsers.FindAsync(id);
            if (communityApplicationUser == null)
            {
                return false;
            }
            _context.CommunityApplicationUsers.Remove(communityApplicationUser);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
