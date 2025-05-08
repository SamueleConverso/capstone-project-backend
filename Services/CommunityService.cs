using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Community;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class CommunityService
{
    private ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CommunityService(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
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

    public async Task<bool> AddCommunityAsync(Community community)
    {
        try
        {
            _context.Communities.Add(community);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<Community>> GetAllCommunitiesAsync()
    {
        var communities = new List<Community>();
        try
        {
            communities = await _context.Communities
                .Include(c => c.ApplicationUser)
                .Include(c => c.CommunityApplicationUsers).ThenInclude(ca => ca.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Comments).ThenInclude(c => c.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                .ToListAsync();
            return communities;
        }
        catch (Exception ex)
        {
            communities = new List<Community>();
            Console.WriteLine(ex.Message);
        }
        return communities;
    }

    public async Task<Community> GetCommunityByIdAsync(Guid id)
    {
        Community community = null;
        try
        {
            community = await _context.Communities
                .Include(c => c.ApplicationUser)
                .Include(c => c.CommunityApplicationUsers).ThenInclude(ca => ca.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Comments).ThenInclude(c => c.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                .Include(c => c.Posts).ThenInclude(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                .FirstOrDefaultAsync(c => c.CommunityId == id);
            return community;
        }
        catch (Exception ex)
        {
            community = null;
            Console.WriteLine(ex.Message);
        }
        return community;
    }

    public async Task<bool> UpdateCommunityAsync(Guid id, UpdateCommunityRequestDto updateCommunityRequestDto)
    {
        try
        {
            var communityToFind = await GetCommunityByIdAsync(id);

            if (communityToFind == null)
            {
                return false;
            }

            if (updateCommunityRequestDto.PictureFile != null && updateCommunityRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateCommunityRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateCommunityRequestDto.PictureFile.CopyToAsync(stream);
                }

                updateCommunityRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                updateCommunityRequestDto.Picture = null;
            }

            if (updateCommunityRequestDto.CoverFile != null && updateCommunityRequestDto.CoverFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateCommunityRequestDto.CoverFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateCommunityRequestDto.CoverFile.CopyToAsync(stream);
                }

                updateCommunityRequestDto.Cover = $"/images/{uniqueName}";
            }
            else
            {
                updateCommunityRequestDto.Cover = null;
            }

            bool.TryParse(updateCommunityRequestDto.IsPrivate, out var isPrivateBool);
            bool.TryParse(updateCommunityRequestDto.IsHidden, out var isHiddenBool);
            int.TryParse(updateCommunityRequestDto.MaxMembers, out var maxMembersInt);

            communityToFind.Type = updateCommunityRequestDto.Type;
            communityToFind.Name = updateCommunityRequestDto.Name;
            communityToFind.ExtraName = updateCommunityRequestDto.ExtraName;
            communityToFind.Description = updateCommunityRequestDto.Description;
            communityToFind.ExtraDescription = updateCommunityRequestDto.ExtraDescription;
            communityToFind.Picture = updateCommunityRequestDto.Picture;
            communityToFind.Cover = updateCommunityRequestDto.Cover;
            communityToFind.Link = updateCommunityRequestDto.Link;
            communityToFind.IsPrivate = isPrivateBool;
            communityToFind.IsHidden = isHiddenBool;
            communityToFind.MaxMembers = maxMembersInt;
            communityToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCommunityAsync(Guid id)
    {
        try
        {
            var community = await GetCommunityByIdAsync(id);
            if (community == null)
            {
                return false;
            }
            _context.Communities.Remove(community);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
