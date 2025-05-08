using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class VideogameService
{
    private ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public VideogameService(ApplicationDbContext context, IWebHostEnvironment env)
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

    public async Task<bool> AddVideogameAsync(Videogame videogame)
    {
        try
        {
            _context.Videogames.Add(videogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<Videogame>> GetAllVideogamesAsync()
    {
        var videogames = new List<Videogame>();
        try
        {
            videogames = await _context.Videogames
                .Include(v => v.ApplicationUser)
                .Include(v => v.Reviews).ThenInclude(r => r.ApplicationUser)
                .Include(v => v.Posts).ThenInclude(p => p.ApplicationUser)
                .ToListAsync();
            return videogames;
        }
        catch (Exception ex)
        {
            videogames = new List<Videogame>();
            Console.WriteLine(ex.Message);
        }
        return videogames;
    }

    public async Task<Videogame> GetVideogameByIdAsync(Guid id)
    {
        Videogame videogame = null;
        try
        {
            videogame = await _context.Videogames
                .Include(v => v.ApplicationUser)
                .Include(v => v.Reviews).ThenInclude(r => r.ApplicationUser)
                .Include(v => v.Posts).ThenInclude(p => p.ApplicationUser)
                .FirstOrDefaultAsync(v => v.VideogameId == id);
            return videogame;
        }
        catch (Exception ex)
        {
            videogame = null;
            Console.WriteLine(ex.Message);
        }
        return videogame;
    }

    public async Task<bool> UpdateVideogameAsync(Guid id, UpdateVideogameRequestDto updateVideogameRequestDto)
    {
        try
        {
            var videogameToFind = await GetVideogameByIdAsync(id);

            if (videogameToFind == null)
            {
                return false;
            }

            if (updateVideogameRequestDto.PictureFile != null && updateVideogameRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateVideogameRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateVideogameRequestDto.PictureFile.CopyToAsync(stream);
                }

                updateVideogameRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                updateVideogameRequestDto.Picture = null;
            }

            if (updateVideogameRequestDto.CoverFile != null && updateVideogameRequestDto.CoverFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateVideogameRequestDto.CoverFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateVideogameRequestDto.CoverFile.CopyToAsync(stream);
                }

                updateVideogameRequestDto.Cover = $"/images/{uniqueName}";
            }
            else
            {
                updateVideogameRequestDto.Cover = null;
            }

            DateTime parsedDate;
            DateTime.TryParse(updateVideogameRequestDto.ReleaseDate, out parsedDate);

            int.TryParse(updateVideogameRequestDto.AgeRating, out var ageRatingInt);
            decimal.TryParse(updateVideogameRequestDto.Price, out var priceDecimal);
            bool.TryParse(updateVideogameRequestDto.IsHidden, out var isHiddenBool);
            bool.TryParse(updateVideogameRequestDto.IsAvailableForPurchase, out var isAvailableForPurchaseBool);

            videogameToFind.Title = updateVideogameRequestDto.Title;
            videogameToFind.Subtitle = updateVideogameRequestDto.Subtitle;
            videogameToFind.Description = updateVideogameRequestDto.Description;
            videogameToFind.ExtraDescription = updateVideogameRequestDto.ExtraDescription;
            videogameToFind.Genre = updateVideogameRequestDto.Genre;
            videogameToFind.Picture = updateVideogameRequestDto.Picture;
            videogameToFind.Cover = updateVideogameRequestDto.Cover;
            videogameToFind.Video = updateVideogameRequestDto.Video;
            videogameToFind.Link = updateVideogameRequestDto.Link;
            videogameToFind.ReleaseDate = parsedDate;
            videogameToFind.Platform = updateVideogameRequestDto.Platform;
            videogameToFind.AgeRating = ageRatingInt;
            videogameToFind.Contributors = updateVideogameRequestDto.Contributors;
            videogameToFind.Price = priceDecimal;
            videogameToFind.IsHidden = isHiddenBool;
            videogameToFind.IsAvailableForPurchase = isAvailableForPurchaseBool;
            videogameToFind.Country = updateVideogameRequestDto.Country;
            videogameToFind.City = updateVideogameRequestDto.City;
            videogameToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteVideogameAsync(Guid id)
    {
        try
        {
            var videogame = await GetVideogameByIdAsync(id);
            if (videogame == null)
            {
                return false;
            }
            _context.Videogames.Remove(videogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
