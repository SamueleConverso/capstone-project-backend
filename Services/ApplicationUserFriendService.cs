using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.ApplicationUserFriend;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class ApplicationUserFriendService
{
    private ApplicationDbContext _context;

    public ApplicationUserFriendService(ApplicationDbContext context)
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

    public async Task<bool> AddApplicationUserFriendAsync(ApplicationUserFriend applicationUserFriend)
    {
        try
        {
            _context.ApplicationUserFriends.Add(applicationUserFriend);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<ApplicationUserFriend> GetApplicationUserFriendByIdAsync(Guid id)
    {
        ApplicationUserFriend applicationUserFriend = null;
        try
        {
            applicationUserFriend = await _context.ApplicationUserFriends
                .Include(auf => auf.FriendList).ThenInclude(fl => fl.ApplicationUser)
                .Include(auf => auf.ApplicationUser)
                .FirstOrDefaultAsync(auf => auf.ApplicationUserFriendId == id);
            return applicationUserFriend;
        }
        catch (Exception ex)
        {
            applicationUserFriend = null;
            Console.WriteLine(ex.Message);
        }
        return applicationUserFriend;
    }

    public async Task<bool> UpdateApplicationUserFriendAsync(Guid id, UpdateApplicationUserFriendRequestDto updateApplicationUserFriendRequestDto)
    {
        try
        {
            var applicationUserFriendToFind = await GetApplicationUserFriendByIdAsync(id);

            if (applicationUserFriendToFind == null)
            {
                return false;
            }

            applicationUserFriendToFind.Accepted = updateApplicationUserFriendRequestDto.Accepted;
            applicationUserFriendToFind.Rejected = updateApplicationUserFriendRequestDto.Rejected;
            applicationUserFriendToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteApplicationUserFriendAsync(Guid id)
    {
        try
        {
            var applicationUserFriend = await GetApplicationUserFriendByIdAsync(id);
            if (applicationUserFriend == null)
            {
                return false;
            }
            _context.ApplicationUserFriends.Remove(applicationUserFriend);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
