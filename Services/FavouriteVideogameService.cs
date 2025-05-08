using System;
using Capstone_Project.Data;
using Capstone_Project.Models.Feed;

namespace Capstone_Project.Services;

public class FavouriteVideogameService
{
    private ApplicationDbContext _context;

    public FavouriteVideogameService(ApplicationDbContext context)
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

    public async Task<bool> AddFavouriteVideogameAsync(FavouriteVideogame favouriteVideogame)
    {
        try
        {
            _context.FavouriteVideogames.Add(favouriteVideogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteFavouriteVideogameAsync(Guid id)
    {
        try
        {
            var favouriteVideogame = await _context.FavouriteVideogames.FindAsync(id);
            if (favouriteVideogame == null)
            {
                return false;
            }
            _context.FavouriteVideogames.Remove(favouriteVideogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
