using Capstone_Project.Data;
using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.Models.Account;
using Capstone_Project.Models.Feed;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Services
{
    public class ApplicationUserService
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ApplicationUserService(ApplicationDbContext context, IWebHostEnvironment env)
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

        public async Task<List<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            var applicationUsers = new List<ApplicationUser>();
            try
            {
                applicationUsers = await _context.ApplicationUsers
                    .Include(a => a.Posts).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(a => a.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(a => a.PostLikes)
                    .Include(a => a.CommentLikes)
                    .Include(a => a.Videogames)
                    .Include(a => a.Reviews)
                    .Include(a => a.Communities)
                    .Include(a => a.CommunityApplicationUsers).ThenInclude(c => c.Community)
                    .Include(a => a.ApplicationUserFriends).ThenInclude(uf => uf.FriendList).ThenInclude(fl => fl.ApplicationUser)
                    .Include(a => a.Cart).ThenInclude(c => c.CartVideogames).ThenInclude(cv => cv.Videogame)
                    .Include(a => a.FavouriteList).ThenInclude(f => f.FavouriteVideogames).ThenInclude(fv => fv.Videogame)
                    .Include(a => a.FriendList).ThenInclude(f => f.ApplicationUserFriends).ThenInclude(uf => uf.ApplicationUser)
                    .ToListAsync();
                return applicationUsers;
            }
            catch (Exception ex)
            {
                applicationUsers = new List<ApplicationUser>();
                Console.WriteLine(ex.Message);
            }
            return applicationUsers;
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string id)
        {
            ApplicationUser applicationUser = null;
            try
            {
                applicationUser = await _context.ApplicationUsers
                    .Include(a => a.Posts).ThenInclude(p => p.PostLikes).ThenInclude(pl => pl.ApplicationUser)
                    .Include(a => a.Comments).ThenInclude(c => c.CommentLikes).ThenInclude(cl => cl.ApplicationUser)
                    .Include(a => a.PostLikes)
                    .Include(a => a.CommentLikes)
                    .Include(a => a.Videogames)
                    .Include(a => a.Reviews)
                    .Include(a => a.Communities)
                    .Include(a => a.CommunityApplicationUsers).ThenInclude(c => c.Community)
                    .Include(a => a.ApplicationUserFriends).ThenInclude(uf => uf.FriendList).ThenInclude(fl => fl.ApplicationUser)
                    .Include(a => a.Cart).ThenInclude(c => c.CartVideogames).ThenInclude(cv => cv.Videogame)
                    .Include(a => a.FavouriteList).ThenInclude(f => f.FavouriteVideogames).ThenInclude(fv => fv.Videogame)
                    .Include(a => a.FriendList).ThenInclude(f => f.ApplicationUserFriends).ThenInclude(uf => uf.ApplicationUser)
                    .FirstOrDefaultAsync(a => a.Id == id);
                return applicationUser;
            }
            catch (Exception ex)
            {
                applicationUser = null;
                Console.WriteLine(ex.Message);
            }
            return applicationUser;
        }

        public async Task<bool> UpdateApplicationUserAsync(string id, UpdateApplicationUserRequestDto updateApplicationUserRequestDto)
        {
            try
            {
                var applicationUserToFind = await GetApplicationUserByIdAsync(id);

                if (applicationUserToFind == null)
                {
                    return false;
                }

                if (updateApplicationUserRequestDto.PictureFile != null && updateApplicationUserRequestDto.PictureFile.Length > 0)
                {

                    var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateApplicationUserRequestDto.PictureFile.FileName)}";
                    var filePath = Path.Combine(imagesFolder, uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateApplicationUserRequestDto.PictureFile.CopyToAsync(stream);
                    }

                    updateApplicationUserRequestDto.Picture = $"/images/{uniqueName}";
                }
                else
                {
                    updateApplicationUserRequestDto.Picture = null;
                }

                if (updateApplicationUserRequestDto.CoverFile != null && updateApplicationUserRequestDto.CoverFile.Length > 0)
                {

                    var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(updateApplicationUserRequestDto.CoverFile.FileName)}";
                    var filePath = Path.Combine(imagesFolder, uniqueName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateApplicationUserRequestDto.CoverFile.CopyToAsync(stream);
                    }

                    updateApplicationUserRequestDto.Cover = $"/images/{uniqueName}";
                }
                else
                {
                    updateApplicationUserRequestDto.Cover = null;
                }

                DateOnly parsedDate;
                DateOnly.TryParse(updateApplicationUserRequestDto.BirthDate, out parsedDate);

                bool.TryParse(updateApplicationUserRequestDto.IsGamer, out var isGamerBool);
                bool.TryParse(updateApplicationUserRequestDto.IsDeveloper, out var isDeveloperBool);
                bool.TryParse(updateApplicationUserRequestDto.IsEditor, out var isEditorBool);
                bool.TryParse(updateApplicationUserRequestDto.IsHidden, out var isHiddenBool);
                bool.TryParse(updateApplicationUserRequestDto.IsFavouriteListPrivate, out var isFavouriteListPrivateBool);
                bool.TryParse(updateApplicationUserRequestDto.IsFriendListPrivate, out var isFriendListPrivateBool);
                bool.TryParse(updateApplicationUserRequestDto.AutoAcceptFriendRequests, out var autoAcceptFriendRequestsBool);

                applicationUserToFind.FirstName = updateApplicationUserRequestDto.FirstName;
                applicationUserToFind.LastName = updateApplicationUserRequestDto.LastName;
                applicationUserToFind.BirthDate = parsedDate;
                applicationUserToFind.Country = updateApplicationUserRequestDto.Country;
                applicationUserToFind.City = updateApplicationUserRequestDto.City;
                applicationUserToFind.DisplayName = updateApplicationUserRequestDto.DisplayName;
                applicationUserToFind.Avatar = updateApplicationUserRequestDto.Avatar;
                applicationUserToFind.Picture = updateApplicationUserRequestDto.Picture;
                applicationUserToFind.Cover = updateApplicationUserRequestDto.Cover;
                applicationUserToFind.IsGamer = isGamerBool;
                applicationUserToFind.IsDeveloper = isDeveloperBool;
                applicationUserToFind.IsEditor = isEditorBool;
                applicationUserToFind.DeveloperRole = updateApplicationUserRequestDto.DeveloperRole;
                applicationUserToFind.Bio = updateApplicationUserRequestDto.Bio;
                applicationUserToFind.Title = updateApplicationUserRequestDto.Title;
                applicationUserToFind.IsHidden = isHiddenBool;
                applicationUserToFind.IsFavouriteListPrivate = isFavouriteListPrivateBool;
                applicationUserToFind.IsFriendListPrivate = isFriendListPrivateBool;
                applicationUserToFind.AutoAcceptFriendRequests = autoAcceptFriendRequestsBool;
                applicationUserToFind.UpdatedAt = DateTime.Now;

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
