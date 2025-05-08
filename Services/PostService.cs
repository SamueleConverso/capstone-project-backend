using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services
{
    public class PostService
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostService(ApplicationDbContext context, IWebHostEnvironment env)
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

        public async Task<bool> AddPostAsync(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = new List<Post>();
            try
            {
                posts = await _context.Posts
                    .Include(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(p => p.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(p => p.Comments).ThenInclude(c => c.ApplicationUser)
                    .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                    .Include(p => p.Community).ThenInclude(c => c.ApplicationUser)
                    .ToListAsync();
                return posts;
            }
            catch (Exception ex)
            {
                posts = new List<Post>();
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            Post post = null;
            try
            {
                post = await _context.Posts
                    .Include(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(p => p.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(p => p.Comments).ThenInclude(c => c.ApplicationUser)
                    .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Videogame).ThenInclude(v => v.ApplicationUser)
                    .Include(p => p.Community).ThenInclude(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(p => p.PostId == id);
                return post;
            }
            catch (Exception ex)
            {
                post = null;
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        public async Task<bool> UpdatePostAsync(Guid id, UpdatePostRequestDto updatePostRequestDto)
        {
            try
            {
                var postToFind = await GetPostByIdAsync(id);

                if (postToFind == null)
                {
                    return false;
                }

                if (updatePostRequestDto.PictureFile != null && updatePostRequestDto.PictureFile.Length > 0)
                {

                    var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updatePostRequestDto.PictureFile.FileName)}";
                    var filePath = Path.Combine(imagesFolder, uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updatePostRequestDto.PictureFile.CopyToAsync(stream);
                    }

                    updatePostRequestDto.Picture = $"/images/{uniqueName}";
                }
                else
                {
                    updatePostRequestDto.Picture = null;
                }

                bool.TryParse(updatePostRequestDto.IsLookingForGamers, out var isLookingForGamersBool);
                bool.TryParse(updatePostRequestDto.IsLookingForDevelopers, out var isLookingForDevelopersBool);
                bool.TryParse(updatePostRequestDto.IsLookingForEditors, out var isLookingForEditorsBool);
                bool.TryParse(updatePostRequestDto.IsInUserFeed, out var isInUserFeedBool);
                bool.TryParse(updatePostRequestDto.IsInGameFeed, out var isInGameFeedBool);
                bool.TryParse(updatePostRequestDto.IsInCommunityFeed, out var isInCommunityFeedBool);
                bool.TryParse(updatePostRequestDto.IsHidden, out var isHiddenBool);

                Guid? videogameIdGuid = null;
                if (updatePostRequestDto.VideogameId != null && updatePostRequestDto.VideogameId != "")
                {
                    Guid.TryParse(updatePostRequestDto.VideogameId, out var videogameIdGuidToParse);
                    videogameIdGuid = videogameIdGuidToParse;
                }

                Guid? communityIdGuid = null;
                if (updatePostRequestDto.CommunityId != null && updatePostRequestDto.CommunityId != "")
                {
                    Guid.TryParse(updatePostRequestDto.CommunityId, out var communityIdGuidToParse);
                    communityIdGuid = communityIdGuidToParse;
                }

                postToFind.Text = updatePostRequestDto.Text;
                postToFind.Picture = updatePostRequestDto.Picture;
                postToFind.Video = updatePostRequestDto.Video;
                postToFind.Mood = updatePostRequestDto.Mood;
                postToFind.IsLookingForGamers = isLookingForGamersBool;
                postToFind.IsLookingForDevelopers = isLookingForDevelopersBool;
                postToFind.IsLookingForEditors = isLookingForEditorsBool;
                postToFind.Country = updatePostRequestDto.Country;
                postToFind.City = updatePostRequestDto.City;
                //postToFind.IsInUserFeed = updatePostRequestDto.IsInUserFeed;
                //postToFind.IsInGameFeed = updatePostRequestDto.IsInGameFeed;
                //postToFind.IsInCommunityFeed = updatePostRequestDto.IsInCommunityFeed;
                postToFind.IsHidden = isHiddenBool;
                postToFind.UpdatedAt = DateTime.Now;
                postToFind.VideogameId = videogameIdGuid;
                postToFind.CommunityId = communityIdGuid;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(Guid id)
        {
            try
            {
                var post = await GetPostByIdAsync(id);
                if (post == null)
                {
                    return false;
                }
                _context.Posts.Remove(post);
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
