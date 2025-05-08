using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.CartVideogame;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class CartVideogameService
{
    private ApplicationDbContext _context;

    public CartVideogameService(ApplicationDbContext context)
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

    public async Task<bool> AddCartVideogameAsync(CartVideogame cartVideogame)
    {
        try
        {
            _context.CartVideogames.Add(cartVideogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<CartVideogame> GetCartVideogameByIdAsync(Guid id)
    {
        CartVideogame cartVideogame = null;
        try
        {
            cartVideogame = await _context.CartVideogames
                .Include(cv => cv.Cart)
                .Include(cv => cv.Videogame)
                .FirstOrDefaultAsync(cv => cv.CartVideogameId == id);
            return cartVideogame;
        }
        catch (Exception ex)
        {
            cartVideogame = null;
            Console.WriteLine(ex.Message);
        }
        return cartVideogame;
    }

    public async Task<bool> UpdateCartVideogameAsync(Guid id, UpdateCartVideogameRequestDto updateCartVideogameRequestDto)
    {
        try
        {
            var cartVideogameToFind = await GetCartVideogameByIdAsync(id);

            if (cartVideogameToFind == null)
            {
                return false;
            }

            cartVideogameToFind.Quantity = updateCartVideogameRequestDto.Quantity;
            cartVideogameToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCartVideogameAsync(Guid id)
    {
        try
        {
            var cartVideogame = await GetCartVideogameByIdAsync(id);
            if (cartVideogame == null)
            {
                return false;
            }
            _context.CartVideogames.Remove(cartVideogame);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
