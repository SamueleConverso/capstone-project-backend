using System;
using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Review;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services;

public class ReviewService
{
    private ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ReviewService(ApplicationDbContext context, IWebHostEnvironment env)
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

    public async Task<bool> AddReviewAsync(Review review)
    {
        try
        {
            _context.Reviews.Add(review);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        var reviews = new List<Review>();
        try
        {
            reviews = await _context.Reviews
                .Include(r => r.Videogame).ThenInclude(v => v.ApplicationUser)
                .Include(r => r.ApplicationUser)
                .ToListAsync();
            return reviews;
        }
        catch (Exception ex)
        {
            reviews = new List<Review>();
            Console.WriteLine(ex.Message);
        }
        return reviews;
    }

    public async Task<Review> GetReviewByIdAsync(Guid id)
    {
        Review review = null;
        try
        {
            review = await _context.Reviews
                .Include(r => r.Videogame).ThenInclude(v => v.ApplicationUser)
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
            return review;
        }
        catch (Exception ex)
        {
            review = null;
            Console.WriteLine(ex.Message);
        }
        return review;
    }

    public async Task<bool> UpdateReviewAsync(Guid id, UpdateReviewRequestDto updateReviewRequestDto)
    {
        try
        {
            var reviewToFind = await GetReviewByIdAsync(id);

            if (reviewToFind == null)
            {
                return false;
            }

            if (updateReviewRequestDto.PictureFile != null && updateReviewRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateReviewRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateReviewRequestDto.PictureFile.CopyToAsync(stream);
                }

                updateReviewRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                updateReviewRequestDto.Picture = null;
            }

            int.TryParse(updateReviewRequestDto.Rating, out var ratingInt);
            bool.TryParse(updateReviewRequestDto.Recommend, out var recommendBool);

            reviewToFind.Title = updateReviewRequestDto.Title;
            reviewToFind.Text = updateReviewRequestDto.Text;
            reviewToFind.Rating = ratingInt;
            reviewToFind.Recommend = recommendBool;
            reviewToFind.Picture = updateReviewRequestDto.Picture;
            reviewToFind.Video = updateReviewRequestDto.Video;
            reviewToFind.Link = updateReviewRequestDto.Link;
            reviewToFind.UpdatedAt = DateTime.Now;

            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteReviewAsync(Guid id)
    {
        try
        {
            var review = await GetReviewByIdAsync(id);
            if (review == null)
            {
                return false;
            }
            _context.Reviews.Remove(review);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
