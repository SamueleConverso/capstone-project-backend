using System;
using Capstone_Project.Data;
using Capstone_Project.Models.Feed;

namespace Capstone_Project.Services;

public class CommentLikeService
{
    private ApplicationDbContext _context;

    public CommentLikeService(ApplicationDbContext context)
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

    public async Task<bool> AddCommentLikeAsync(CommentLike commentLike)
    {
        try
        {
            _context.CommentLikes.Add(commentLike);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCommentLikeAsync(Guid id)
    {
        try
        {
            var commentLike = await _context.CommentLikes.FindAsync(id);
            if (commentLike == null)
            {
                return false;
            }
            _context.CommentLikes.Remove(commentLike);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
