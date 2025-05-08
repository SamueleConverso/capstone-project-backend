using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services
{
    public class CommentService
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CommentService(ApplicationDbContext context, IWebHostEnvironment env)
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

        public async Task<bool> AddCommentAsync(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = new List<Comment>();
            try
            {
                comments = await _context.Comments
                    .Include(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.PostTags).ThenInclude(pt => pt.Tag)
                    .Include(c => c.Post).ThenInclude(p => p.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                    .Include(c => c.ApplicationUser)
                    .ToListAsync();
                return comments;
            }
            catch (Exception ex)
            {
                comments = new List<Comment>();
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            Comment comment = null;
            try
            {
                comment = await _context.Comments
                    .Include(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.PostTags).ThenInclude(pt => pt.Tag)
                    .Include(c => c.Post).ThenInclude(p => p.ApplicationUser)
                    .Include(c => c.Post).ThenInclude(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.CommentId == id);
                return comment;
            }
            catch (Exception ex)
            {
                comment = null;
                Console.WriteLine(ex.Message);
            }
            return comment;
        }

        public async Task<bool> UpdateCommentAsync(Guid id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            try
            {
                var commentToFind = await GetCommentByIdAsync(id);

                if (commentToFind == null)
                {
                    return false;
                }

                if (updateCommentRequestDto.PictureFile != null && updateCommentRequestDto.PictureFile.Length > 0)
                {

                    var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateCommentRequestDto.PictureFile.FileName)}";
                    var filePath = Path.Combine(imagesFolder, uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateCommentRequestDto.PictureFile.CopyToAsync(stream);
                    }

                    updateCommentRequestDto.Picture = $"/images/{uniqueName}";
                }
                else
                {
                    updateCommentRequestDto.Picture = null;
                }

                Guid.TryParse(updateCommentRequestDto.PostId, out var postIdGuid);

                commentToFind.Text = updateCommentRequestDto.Text;
                commentToFind.Picture = updateCommentRequestDto.Picture;
                commentToFind.UpdatedAt = DateTime.Now;
                commentToFind.PostId = postIdGuid;
                commentToFind.ApplicationUserId = updateCommentRequestDto.ApplicationUserId;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCommentAsync(Guid id)
        {
            try
            {
                var comment = await GetCommentByIdAsync(id);
                if (comment == null)
                {
                    return false;
                }
                _context.Comments.Remove(comment);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
