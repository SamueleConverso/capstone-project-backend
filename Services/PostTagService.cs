using System;
using Capstone_Project.Data;
using Capstone_Project.Models.Feed;

namespace Capstone_Project.Services;

public class PostTagService
{
    private ApplicationDbContext _context;

    public PostTagService(ApplicationDbContext context)
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

    public async Task<bool> AddPostTagAsync(PostTag postTag)
    {
        try
        {
            _context.PostTags.Add(postTag);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeletePostTagAsync(Guid id)
    {
        try
        {
            var postTag = await _context.PostTags.FindAsync(id);
            if (postTag == null)
            {
                return false;
            }
            _context.PostTags.Remove(postTag);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
