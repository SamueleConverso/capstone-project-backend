using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Cart;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class CartService
{
    private ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
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

    public async Task<Cart> GetCartByIdAsync(Guid id)
    {
        Cart cart = null;
        try
        {
            cart = await _context.Carts
                .Include(c => c.ApplicationUser)
                .Include(c => c.CartVideogames).ThenInclude(cv => cv.Videogame).ThenInclude(v => v.ApplicationUser)
                .FirstOrDefaultAsync(c => c.CartId == id);
            return cart;
        }
        catch (Exception ex)
        {
            cart = null;
            Console.WriteLine(ex.Message);
        }
        return cart;
    }

    public async Task<bool> UpdateCartAsync(Guid id, UpdateCartRequestDto updateCartRequestDto)
    {
        try
        {
            var cartToFind = await GetCartByIdAsync(id);

            if (cartToFind == null)
            {
                return false;
            }

            cartToFind.IsCheckedOut = updateCartRequestDto.IsCheckedOut;
            cartToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
