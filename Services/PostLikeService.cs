using System;
using Capstone_Project.Data;
using Capstone_Project.Models.Feed;

namespace Capstone_Project.Services;

public class PostLikeService
{
    private ApplicationDbContext _context;

    public PostLikeService(ApplicationDbContext context)
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

    public async Task<bool> AddPostLikeAsync(PostLike postLike)
    {
        try
        {
            _context.PostLikes.Add(postLike);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeletePostLikeAsync(Guid id)
    {
        try
        {
            var postLike = await _context.PostLikes.FindAsync(id);
            if (postLike == null)
            {
                return false;
            }
            _context.PostLikes.Remove(postLike);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
