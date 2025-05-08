using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Tag;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class TagService
{
    private ApplicationDbContext _context;

    public TagService(ApplicationDbContext context)
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

    public async Task<bool> AddTagAsync(Tag tag)
    {
        try
        {
            _context.Tags.Add(tag);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        var tags = new List<Tag>();
        try
        {
            tags = await _context.Tags
                .Include(t => t.PostTags).ThenInclude(pt => pt.Post)
                .ToListAsync();
            return tags;
        }
        catch (Exception ex)
        {
            tags = new List<Tag>();
            Console.WriteLine(ex.Message);
        }
        return tags;
    }

    public async Task<Tag> GetTagByIdAsync(Guid id)
    {
        Tag tag = null;
        try
        {
            tag = await _context.Tags
                .Include(t => t.PostTags).ThenInclude(pt => pt.Post)
                .FirstOrDefaultAsync(t => t.TagId == id);
            return tag;
        }
        catch (Exception ex)
        {
            tag = null;
            Console.WriteLine(ex.Message);
        }
        return tag;
    }

    public async Task<bool> UpdateTagAsync(Guid id, UpdateTagRequestDto updateTagRequestDto)
    {
        try
        {
            var tagToFind = await GetTagByIdAsync(id);

            if (tagToFind == null)
            {
                return false;
            }

            tagToFind.Entry = updateTagRequestDto.Entry;
            tagToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteTagAsync(Guid id)
    {
        try
        {
            var tag = await GetTagByIdAsync(id);
            if (tag == null)
            {
                return false;
            }
            _context.Tags.Remove(tag);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
