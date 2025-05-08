using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.ApplicationUserFriend;
using Capstone_Project.DTOs.Feed.Cart;
using Capstone_Project.DTOs.Feed.CartVideogame;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.DTOs.Feed.Community;
using Capstone_Project.DTOs.Feed.CommunityApplicationUser;
using Capstone_Project.DTOs.Feed.FavouriteList;
using Capstone_Project.DTOs.Feed.FavouriteVideogame;
using Capstone_Project.DTOs.Feed.FriendList;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.DTOs.Feed.Review;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Account;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserService _applicationUserService;

        public ApplicationUserController(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplicationUsers()
        {
            var applicationUsersList = await _applicationUserService.GetAllApplicationUsersAsync();

            if (applicationUsersList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero degli application user",
                    applicationUsers = new List<ApplicationUser>()
                });
            }

            if (!applicationUsersList.Any())
            {
                return Ok(new
                {
                    message = "Nessun application user trovato",
                    applicationUsers = new List<ApplicationUser>()
                });
            }

            var applicationUsersResponse = applicationUsersList.Select(a => new ApplicationUserDto
            {
                ApplicationUserId = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
                Country = a.Country,
                City = a.City,
                DisplayName = a.DisplayName,
                Avatar = a.Avatar,
                Picture = a.Picture,
                Cover = a.Cover,
                IsGamer = a.IsGamer,
                IsDeveloper = a.IsDeveloper,
                IsEditor = a.IsEditor,
                DeveloperRole = a.DeveloperRole,
                Bio = a.Bio,
                Title = a.Title,
                IsHidden = a.IsHidden,
                IsFavouriteListPrivate = a.IsFavouriteListPrivate,
                IsFriendListPrivate = a.IsFriendListPrivate,
                AutoAcceptFriendRequests = a.AutoAcceptFriendRequests,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                IsDeleted = a.IsDeleted,
                DeletedAt = a.DeletedAt,
                Posts = a.Posts?.Select(p => new ApplicationUser_PostDto
                {
                    PostId = p.PostId,
                    Text = p.Text,
                    Picture = p.Picture,
                    Video = p.Video,
                    Mood = p.Mood,
                    IsLookingForGamers = p.IsLookingForGamers,
                    IsLookingForDevelopers = p.IsLookingForDevelopers,
                    IsLookingForEditors = p.IsLookingForEditors,
                    Country = p.Country,
                    City = p.City,
                    IsInUserFeed = p.IsInUserFeed,
                    IsInGameFeed = p.IsInGameFeed,
                    IsInCommunityFeed = p.IsInCommunityFeed,
                    IsHidden = p.IsHidden,
                    Likes = p.PostLikes?.Count ?? 0,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted,
                    DeletedAt = p.DeletedAt,
                    PostLikes = p.PostLikes?.Select(pl => new ApplicationUser_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser == null ? null : new ApplicationUser_Post_PostLike_ApplicationUserDto
                        {
                            ApplicationUserId = pl.ApplicationUser.Id,
                            FirstName = pl.ApplicationUser.FirstName,
                            LastName = pl.ApplicationUser.LastName,
                            BirthDate = pl.ApplicationUser.BirthDate,
                            Country = pl.ApplicationUser.Country,
                            City = pl.ApplicationUser.City,
                            DisplayName = pl.ApplicationUser.DisplayName,
                            Avatar = pl.ApplicationUser.Avatar,
                            Picture = pl.ApplicationUser.Picture,
                            Cover = pl.ApplicationUser.Cover,
                            IsGamer = pl.ApplicationUser.IsGamer,
                            IsDeveloper = pl.ApplicationUser.IsDeveloper,
                            IsEditor = pl.ApplicationUser.IsEditor,
                            DeveloperRole = pl.ApplicationUser.DeveloperRole,
                            Bio = pl.ApplicationUser.Bio,
                            Title = pl.ApplicationUser.Title,
                            IsHidden = pl.ApplicationUser.IsHidden
                        }
                    }).ToList() ?? new List<ApplicationUser_Post_PostLikeDto>()
                }).ToList() ?? new List<ApplicationUser_PostDto>(),
                Comments = a.Comments?.Select(c => new ApplicationUser_CommentDto
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    Picture = c.Picture,
                    Likes = c.CommentLikes?.Count ?? 0,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt,
                    CommentLikes = c.CommentLikes?.Select(cl => new ApplicationUser_Comment_CommentLikeDto
                    {
                        CommentLikeId = cl.CommentLikeId,
                        CreatedAt = cl.CreatedAt,
                        UpdatedAt = cl.UpdatedAt,
                        IsDeleted = cl.IsDeleted,
                        DeletedAt = cl.DeletedAt,
                        ApplicationUser = cl.ApplicationUser == null ? null : new ApplicationUser_Comment_CommentLike_ApplicationUserDto
                        {
                            ApplicationUserId = cl.ApplicationUser.Id,
                            FirstName = cl.ApplicationUser.FirstName,
                            LastName = cl.ApplicationUser.LastName,
                            BirthDate = cl.ApplicationUser.BirthDate,
                            Country = cl.ApplicationUser.Country,
                            City = cl.ApplicationUser.City,
                            DisplayName = cl.ApplicationUser.DisplayName,
                            Avatar = cl.ApplicationUser.Avatar,
                            Picture = cl.ApplicationUser.Picture,
                            Cover = cl.ApplicationUser.Cover,
                            IsGamer = cl.ApplicationUser.IsGamer,
                            IsDeveloper = cl.ApplicationUser.IsDeveloper,
                            IsEditor = cl.ApplicationUser.IsEditor,
                            DeveloperRole = cl.ApplicationUser.DeveloperRole,
                            Bio = cl.ApplicationUser.Bio,
                            Title = cl.ApplicationUser.Title,
                            IsHidden = cl.ApplicationUser.IsHidden
                        }
                    }).ToList() ?? new List<ApplicationUser_Comment_CommentLikeDto>()
                }).ToList() ?? new List<ApplicationUser_CommentDto>(),
                PostLikes = a.PostLikes?.Select(pl => new ApplicationUser_PostLikeDto
                {
                    PostLikeId = pl.PostLikeId,
                    CreatedAt = pl.CreatedAt,
                    UpdatedAt = pl.UpdatedAt,
                    IsDeleted = pl.IsDeleted,
                    DeletedAt = pl.DeletedAt
                }).ToList() ?? new List<ApplicationUser_PostLikeDto>(),
                CommentLikes = a.CommentLikes?.Select(cl => new ApplicationUser_CommentLikeDto
                {
                    CommentLikeId = cl.CommentLikeId,
                    CreatedAt = cl.CreatedAt,
                    UpdatedAt = cl.UpdatedAt,
                    IsDeleted = cl.IsDeleted,
                    DeletedAt = cl.DeletedAt
                }).ToList() ?? new List<ApplicationUser_CommentLikeDto>(),
                Videogames = a.Videogames?.Select(v => new ApplicationUser_VideogameDto
                {
                    VideogameId = v.VideogameId,
                    Title = v.Title,
                    Subtitle = v.Subtitle,
                    Description = v.Description,
                    ExtraDescription = v.ExtraDescription,
                    Genre = v.Genre,
                    Picture = v.Picture,
                    Cover = v.Cover,
                    Video = v.Video,
                    Link = v.Link,
                    ReleaseDate = v.ReleaseDate,
                    Platform = v.Platform,
                    AgeRating = v.AgeRating,
                    Contributors = v.Contributors,
                    Price = v.Price,
                    IsHidden = v.IsHidden,
                    IsAvailableForPurchase = v.IsAvailableForPurchase,
                    Country = v.Country,
                    City = v.City,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt,
                    IsDeleted = v.IsDeleted,
                    DeletedAt = v.DeletedAt
                }).ToList() ?? new List<ApplicationUser_VideogameDto>(),
                Reviews = a.Reviews?.Select(r => new ApplicationUser_ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Title = r.Title,
                    Text = r.Text,
                    Rating = r.Rating,
                    Recommend = r.Recommend,
                    Picture = r.Picture,
                    Video = r.Video,
                    Link = r.Link,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    IsDeleted = r.IsDeleted,
                    DeletedAt = r.DeletedAt
                }).ToList() ?? new List<ApplicationUser_ReviewDto>(),
                Communities = a.Communities?.Select(c => new ApplicationUser_CommunityDto
                {
                    CommunityId = c.CommunityId,
                    Type = c.Type,
                    Name = c.Name,
                    ExtraName = c.ExtraName,
                    Description = c.Description,
                    ExtraDescription = c.ExtraDescription,
                    Picture = c.Picture,
                    Cover = c.Cover,
                    Link = c.Link,
                    IsPrivate = c.IsPrivate,
                    IsHidden = c.IsHidden,
                    MaxMembers = c.MaxMembers,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt
                }).ToList() ?? new List<ApplicationUser_CommunityDto>(),
                CommunityApplicationUsers = a.CommunityApplicationUsers?.Select(ca => new ApplicationUser_CommunityApplicationUserDto
                {
                    CommunityApplicationUserId = ca.CommunityApplicationUserId,
                    CreatedAt = ca.CreatedAt,
                    UpdatedAt = ca.UpdatedAt,
                    IsDeleted = ca.IsDeleted,
                    DeletedAt = ca.DeletedAt,
                    Community = ca.Community == null ? null : new ApplicationUser_CommunityDto
                    {
                        CommunityId = ca.Community.CommunityId,
                        Type = ca.Community.Type,
                        Name = ca.Community.Name,
                        ExtraName = ca.Community.ExtraName,
                        Description = ca.Community.Description,
                        ExtraDescription = ca.Community.ExtraDescription,
                        Picture = ca.Community.Picture,
                        Cover = ca.Community.Cover,
                        Link = ca.Community.Link,
                        IsPrivate = ca.Community.IsPrivate,
                        IsHidden = ca.Community.IsHidden,
                        MaxMembers = ca.Community.MaxMembers,
                        CreatedAt = ca.Community.CreatedAt,
                        UpdatedAt = ca.Community.UpdatedAt,
                        IsDeleted = ca.Community.IsDeleted,
                        DeletedAt = ca.Community.DeletedAt
                    }
                }).ToList() ?? new List<ApplicationUser_CommunityApplicationUserDto>(),
                ApplicationUserFriends = a.ApplicationUserFriends?.Select(uf => new ApplicationUser_ApplicationUserFriendDto
                {
                    ApplicationUserFriendId = uf.ApplicationUserFriendId,
                    Sent = uf.Sent,
                    Accepted = uf.Accepted,
                    Rejected = uf.Rejected,
                    CreatedAt = uf.CreatedAt,
                    UpdatedAt = uf.UpdatedAt,
                    IsDeleted = uf.IsDeleted,
                    DeletedAt = uf.DeletedAt,
                    FriendList = uf.FriendList == null ? null : new ApplicationUser_ApplicationUserFriend_FriendListDto
                    {
                        FriendListId = uf.FriendList.FriendListId,
                        CreatedAt = uf.FriendList.CreatedAt,
                        UpdatedAt = uf.FriendList.UpdatedAt,
                        IsDeleted = uf.FriendList.IsDeleted,
                        DeletedAt = uf.FriendList.DeletedAt,
                        ApplicationUser = uf.FriendList.ApplicationUser == null ? null : new ApplicationUser_ApplicationUserDto
                        {
                            ApplicationUserId = uf.FriendList.ApplicationUser.Id,
                            FirstName = uf.FriendList.ApplicationUser.FirstName,
                            LastName = uf.FriendList.ApplicationUser.LastName,
                            BirthDate = uf.FriendList.ApplicationUser.BirthDate,
                            Country = uf.FriendList.ApplicationUser.Country,
                            City = uf.FriendList.ApplicationUser.City,
                            DisplayName = uf.FriendList.ApplicationUser.DisplayName,
                            Avatar = uf.FriendList.ApplicationUser.Avatar,
                            Picture = uf.FriendList.ApplicationUser.Picture,
                            Cover = uf.FriendList.ApplicationUser.Cover,
                            IsGamer = uf.FriendList.ApplicationUser.IsGamer,
                            IsDeveloper = uf.FriendList.ApplicationUser.IsDeveloper,
                            IsEditor = uf.FriendList.ApplicationUser.IsEditor,
                            DeveloperRole = uf.FriendList.ApplicationUser.DeveloperRole,
                            Bio = uf.FriendList.ApplicationUser.Bio,
                            Title = uf.FriendList.ApplicationUser.Title,
                            IsHidden = uf.FriendList.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = uf.FriendList.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = uf.FriendList.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = uf.FriendList.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = uf.FriendList.ApplicationUser.CreatedAt,
                            UpdatedAt = uf.FriendList.ApplicationUser.UpdatedAt,
                            IsDeleted = uf.FriendList.ApplicationUser.IsDeleted,
                            DeletedAt = uf.FriendList.ApplicationUser.DeletedAt
                        }
                    }
                }).ToList() ?? new List<ApplicationUser_ApplicationUserFriendDto>(),
                Cart = a.Cart == null ? null : new ApplicationUser_CartDto
                {
                    CartId = a.Cart.CartId,
                    IsCheckedOut = a.Cart.IsCheckedOut,
                    CreatedAt = a.Cart.CreatedAt,
                    UpdatedAt = a.Cart.UpdatedAt,
                    IsDeleted = a.Cart.IsDeleted,
                    DeletedAt = a.Cart.DeletedAt,
                    CartVideogames = a.Cart.CartVideogames?.Select(cv => new ApplicationUser_CartVideogameDto
                    {
                        CartVideogameId = cv.CartVideogameId,
                        Quantity = cv.Quantity,
                        CreatedAt = cv.CreatedAt,
                        UpdatedAt = cv.UpdatedAt,
                        IsDeleted = cv.IsDeleted,
                        DeletedAt = cv.DeletedAt,
                        Videogame = cv.Videogame == null ? null : new ApplicationUser_VideogameDto
                        {
                            VideogameId = cv.Videogame.VideogameId,
                            Title = cv.Videogame.Title,
                            Subtitle = cv.Videogame.Subtitle,
                            Description = cv.Videogame.Description,
                            ExtraDescription = cv.Videogame.ExtraDescription,
                            Genre = cv.Videogame.Genre,
                            Picture = cv.Videogame.Picture,
                            Cover = cv.Videogame.Cover,
                            Video = cv.Videogame.Video,
                            Link = cv.Videogame.Link,
                            ReleaseDate = cv.Videogame.ReleaseDate,
                            Platform = cv.Videogame.Platform,
                            AgeRating = cv.Videogame.AgeRating,
                            Contributors = cv.Videogame.Contributors,
                            Price = cv.Videogame.Price,
                            IsHidden = cv.Videogame.IsHidden,
                            IsAvailableForPurchase = cv.Videogame.IsAvailableForPurchase,
                            Country = cv.Videogame.Country,
                            City = cv.Videogame.City,
                            CreatedAt = cv.Videogame.CreatedAt,
                            UpdatedAt = cv.Videogame.UpdatedAt,
                            IsDeleted = cv.Videogame.IsDeleted,
                            DeletedAt = cv.Videogame.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_CartVideogameDto>()
                },
                FavouriteList = a.FavouriteList == null ? null : new ApplicationUser_FavouriteListDto
                {
                    FavouriteListId = a.FavouriteList.FavouriteListId,
                    CreatedAt = a.FavouriteList.CreatedAt,
                    UpdatedAt = a.FavouriteList.UpdatedAt,
                    IsDeleted = a.FavouriteList.IsDeleted,
                    DeletedAt = a.FavouriteList.DeletedAt,
                    FavouriteVideogames = a.FavouriteList.FavouriteVideogames?.Select(fv => new ApplicationUser_FavouriteVideogameDto
                    {
                        FavouriteVideogameId = fv.FavouriteVideogameId,
                        CreatedAt = fv.CreatedAt,
                        UpdatedAt = fv.UpdatedAt,
                        IsDeleted = fv.IsDeleted,
                        DeletedAt = fv.DeletedAt,
                        Videogame = fv.Videogame == null ? null : new ApplicationUser_VideogameDto
                        {
                            VideogameId = fv.Videogame.VideogameId,
                            Title = fv.Videogame.Title,
                            Subtitle = fv.Videogame.Subtitle,
                            Description = fv.Videogame.Description,
                            ExtraDescription = fv.Videogame.ExtraDescription,
                            Genre = fv.Videogame.Genre,
                            Picture = fv.Videogame.Picture,
                            Cover = fv.Videogame.Cover,
                            Video = fv.Videogame.Video,
                            Link = fv.Videogame.Link,
                            ReleaseDate = fv.Videogame.ReleaseDate,
                            Platform = fv.Videogame.Platform,
                            AgeRating = fv.Videogame.AgeRating,
                            Contributors = fv.Videogame.Contributors,
                            Price = fv.Videogame.Price,
                            IsHidden = fv.Videogame.IsHidden,
                            IsAvailableForPurchase = fv.Videogame.IsAvailableForPurchase,
                            Country = fv.Videogame.Country,
                            City = fv.Videogame.City,
                            CreatedAt = fv.Videogame.CreatedAt,
                            UpdatedAt = fv.Videogame.UpdatedAt,
                            IsDeleted = fv.Videogame.IsDeleted,
                            DeletedAt = fv.Videogame.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_FavouriteVideogameDto>()
                },
                FriendList = a.FriendList == null ? null : new ApplicationUser_FriendListDto
                {
                    FriendListId = a.FriendList.FriendListId,
                    CreatedAt = a.FriendList.CreatedAt,
                    UpdatedAt = a.FriendList.UpdatedAt,
                    IsDeleted = a.FriendList.IsDeleted,
                    DeletedAt = a.FriendList.DeletedAt,
                    ApplicationUserFriends = a.FriendList.ApplicationUserFriends?.Select(uf => new ApplicationUser_FriendList_ApplicationUserFriendDto
                    {
                        ApplicationUserFriendId = uf.ApplicationUserFriendId,
                        Sent = uf.Sent,
                        Accepted = uf.Accepted,
                        Rejected = uf.Rejected,
                        CreatedAt = uf.CreatedAt,
                        UpdatedAt = uf.UpdatedAt,
                        IsDeleted = uf.IsDeleted,
                        DeletedAt = uf.DeletedAt,
                        ApplicationUser = uf.ApplicationUser == null ? null : new ApplicationUser_ApplicationUserDto
                        {
                            ApplicationUserId = uf.ApplicationUser.Id,
                            FirstName = uf.ApplicationUser.FirstName,
                            LastName = uf.ApplicationUser.LastName,
                            BirthDate = uf.ApplicationUser.BirthDate,
                            Country = uf.ApplicationUser.Country,
                            City = uf.ApplicationUser.City,
                            DisplayName = uf.ApplicationUser.DisplayName,
                            Avatar = uf.ApplicationUser.Avatar,
                            Picture = uf.ApplicationUser.Picture,
                            Cover = uf.ApplicationUser.Cover,
                            IsGamer = uf.ApplicationUser.IsGamer,
                            IsDeveloper = uf.ApplicationUser.IsDeveloper,
                            IsEditor = uf.ApplicationUser.IsEditor,
                            DeveloperRole = uf.ApplicationUser.DeveloperRole,
                            Bio = uf.ApplicationUser.Bio,
                            Title = uf.ApplicationUser.Title,
                            IsHidden = uf.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = uf.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = uf.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = uf.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = uf.ApplicationUser.CreatedAt,
                            UpdatedAt = uf.ApplicationUser.UpdatedAt,
                            IsDeleted = uf.ApplicationUser.IsDeleted,
                            DeletedAt = uf.ApplicationUser.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_FriendList_ApplicationUserFriendDto>()
                }
            });

            return Ok(new
            {
                message = $"Numero application user trovati: {applicationUsersResponse.Count()}",
                applicationUsers = applicationUsersResponse
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUserById(string id)
        {
            var applicationUserToFind = await _applicationUserService.GetApplicationUserByIdAsync(id);

            if (applicationUserToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dell'application user"
                });
            }

            var applicationUserResponse = new ApplicationUserDto()
            {
                ApplicationUserId = applicationUserToFind.Id,
                FirstName = applicationUserToFind.FirstName,
                LastName = applicationUserToFind.LastName,
                BirthDate = applicationUserToFind.BirthDate,
                Country = applicationUserToFind.Country,
                City = applicationUserToFind.City,
                DisplayName = applicationUserToFind.DisplayName,
                Avatar = applicationUserToFind.Avatar,
                Picture = applicationUserToFind.Picture,
                Cover = applicationUserToFind.Cover,
                IsGamer = applicationUserToFind.IsGamer,
                IsDeveloper = applicationUserToFind.IsDeveloper,
                IsEditor = applicationUserToFind.IsEditor,
                DeveloperRole = applicationUserToFind.DeveloperRole,
                Bio = applicationUserToFind.Bio,
                Title = applicationUserToFind.Title,
                IsHidden = applicationUserToFind.IsHidden,
                IsFavouriteListPrivate = applicationUserToFind.IsFavouriteListPrivate,
                IsFriendListPrivate = applicationUserToFind.IsFriendListPrivate,
                AutoAcceptFriendRequests = applicationUserToFind.AutoAcceptFriendRequests,
                CreatedAt = applicationUserToFind.CreatedAt,
                UpdatedAt = applicationUserToFind.UpdatedAt,
                IsDeleted = applicationUserToFind.IsDeleted,
                DeletedAt = applicationUserToFind.DeletedAt,
                Posts = applicationUserToFind.Posts?.Select(p => new ApplicationUser_PostDto
                {
                    PostId = p.PostId,
                    Text = p.Text,
                    Picture = p.Picture,
                    Video = p.Video,
                    Mood = p.Mood,
                    IsLookingForGamers = p.IsLookingForGamers,
                    IsLookingForDevelopers = p.IsLookingForDevelopers,
                    IsLookingForEditors = p.IsLookingForEditors,
                    Country = p.Country,
                    City = p.City,
                    IsInUserFeed = p.IsInUserFeed,
                    IsInGameFeed = p.IsInGameFeed,
                    IsInCommunityFeed = p.IsInCommunityFeed,
                    IsHidden = p.IsHidden,
                    Likes = p.PostLikes?.Count ?? 0,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted,
                    DeletedAt = p.DeletedAt,
                    PostLikes = p.PostLikes?.Select(pl => new ApplicationUser_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser == null ? null : new ApplicationUser_Post_PostLike_ApplicationUserDto
                        {
                            ApplicationUserId = pl.ApplicationUser.Id,
                            FirstName = pl.ApplicationUser.FirstName,
                            LastName = pl.ApplicationUser.LastName,
                            BirthDate = pl.ApplicationUser.BirthDate,
                            Country = pl.ApplicationUser.Country,
                            City = pl.ApplicationUser.City,
                            DisplayName = pl.ApplicationUser.DisplayName,
                            Avatar = pl.ApplicationUser.Avatar,
                            Picture = pl.ApplicationUser.Picture,
                            Cover = pl.ApplicationUser.Cover,
                            IsGamer = pl.ApplicationUser.IsGamer,
                            IsDeveloper = pl.ApplicationUser.IsDeveloper,
                            IsEditor = pl.ApplicationUser.IsEditor,
                            DeveloperRole = pl.ApplicationUser.DeveloperRole,
                            Bio = pl.ApplicationUser.Bio,
                            Title = pl.ApplicationUser.Title,
                            IsHidden = pl.ApplicationUser.IsHidden
                        }
                    }).ToList() ?? new List<ApplicationUser_Post_PostLikeDto>()
                }).ToList() ?? new List<ApplicationUser_PostDto>(),
                Comments = applicationUserToFind.Comments?.Select(c => new ApplicationUser_CommentDto
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    Picture = c.Picture,
                    Likes = c.CommentLikes?.Count ?? 0,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt,
                    CommentLikes = c.CommentLikes?.Select(cl => new ApplicationUser_Comment_CommentLikeDto
                    {
                        CommentLikeId = cl.CommentLikeId,
                        CreatedAt = cl.CreatedAt,
                        UpdatedAt = cl.UpdatedAt,
                        IsDeleted = cl.IsDeleted,
                        DeletedAt = cl.DeletedAt,
                        ApplicationUser = cl.ApplicationUser == null ? null : new ApplicationUser_Comment_CommentLike_ApplicationUserDto
                        {
                            ApplicationUserId = cl.ApplicationUser.Id,
                            FirstName = cl.ApplicationUser.FirstName,
                            LastName = cl.ApplicationUser.LastName,
                            BirthDate = cl.ApplicationUser.BirthDate,
                            Country = cl.ApplicationUser.Country,
                            City = cl.ApplicationUser.City,
                            DisplayName = cl.ApplicationUser.DisplayName,
                            Avatar = cl.ApplicationUser.Avatar,
                            Picture = cl.ApplicationUser.Picture,
                            Cover = cl.ApplicationUser.Cover,
                            IsGamer = cl.ApplicationUser.IsGamer,
                            IsDeveloper = cl.ApplicationUser.IsDeveloper,
                            IsEditor = cl.ApplicationUser.IsEditor,
                            DeveloperRole = cl.ApplicationUser.DeveloperRole,
                            Bio = cl.ApplicationUser.Bio,
                            Title = cl.ApplicationUser.Title,
                            IsHidden = cl.ApplicationUser.IsHidden
                        }
                    }).ToList() ?? new List<ApplicationUser_Comment_CommentLikeDto>()
                }).ToList() ?? new List<ApplicationUser_CommentDto>(),
                PostLikes = applicationUserToFind.PostLikes?.Select(pl => new ApplicationUser_PostLikeDto
                {
                    PostLikeId = pl.PostLikeId,
                    CreatedAt = pl.CreatedAt,
                    UpdatedAt = pl.UpdatedAt,
                    IsDeleted = pl.IsDeleted,
                    DeletedAt = pl.DeletedAt
                }).ToList() ?? new List<ApplicationUser_PostLikeDto>(),
                CommentLikes = applicationUserToFind.CommentLikes?.Select(cl => new ApplicationUser_CommentLikeDto
                {
                    CommentLikeId = cl.CommentLikeId,
                    CreatedAt = cl.CreatedAt,
                    UpdatedAt = cl.UpdatedAt,
                    IsDeleted = cl.IsDeleted,
                    DeletedAt = cl.DeletedAt
                }).ToList() ?? new List<ApplicationUser_CommentLikeDto>(),
                Videogames = applicationUserToFind.Videogames?.Select(v => new ApplicationUser_VideogameDto
                {
                    VideogameId = v.VideogameId,
                    Title = v.Title,
                    Subtitle = v.Subtitle,
                    Description = v.Description,
                    ExtraDescription = v.ExtraDescription,
                    Genre = v.Genre,
                    Picture = v.Picture,
                    Cover = v.Cover,
                    Video = v.Video,
                    Link = v.Link,
                    ReleaseDate = v.ReleaseDate,
                    Platform = v.Platform,
                    AgeRating = v.AgeRating,
                    Contributors = v.Contributors,
                    Price = v.Price,
                    IsHidden = v.IsHidden,
                    IsAvailableForPurchase = v.IsAvailableForPurchase,
                    Country = v.Country,
                    City = v.City,
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt,
                    IsDeleted = v.IsDeleted,
                    DeletedAt = v.DeletedAt
                }).ToList() ?? new List<ApplicationUser_VideogameDto>(),
                Reviews = applicationUserToFind.Reviews?.Select(r => new ApplicationUser_ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Title = r.Title,
                    Text = r.Text,
                    Rating = r.Rating,
                    Recommend = r.Recommend,
                    Picture = r.Picture,
                    Video = r.Video,
                    Link = r.Link,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    IsDeleted = r.IsDeleted,
                    DeletedAt = r.DeletedAt
                }).ToList() ?? new List<ApplicationUser_ReviewDto>(),
                Communities = applicationUserToFind.Communities?.Select(c => new ApplicationUser_CommunityDto
                {
                    CommunityId = c.CommunityId,
                    Type = c.Type,
                    Name = c.Name,
                    ExtraName = c.ExtraName,
                    Description = c.Description,
                    ExtraDescription = c.ExtraDescription,
                    Picture = c.Picture,
                    Cover = c.Cover,
                    Link = c.Link,
                    IsPrivate = c.IsPrivate,
                    IsHidden = c.IsHidden,
                    MaxMembers = c.MaxMembers,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt
                }).ToList() ?? new List<ApplicationUser_CommunityDto>(),
                CommunityApplicationUsers = applicationUserToFind.CommunityApplicationUsers?.Select(ca => new ApplicationUser_CommunityApplicationUserDto
                {
                    CommunityApplicationUserId = ca.CommunityApplicationUserId,
                    CreatedAt = ca.CreatedAt,
                    UpdatedAt = ca.UpdatedAt,
                    IsDeleted = ca.IsDeleted,
                    DeletedAt = ca.DeletedAt,
                    Community = ca.Community == null ? null : new ApplicationUser_CommunityDto
                    {
                        CommunityId = ca.Community.CommunityId,
                        Type = ca.Community.Type,
                        Name = ca.Community.Name,
                        ExtraName = ca.Community.ExtraName,
                        Description = ca.Community.Description,
                        ExtraDescription = ca.Community.ExtraDescription,
                        Picture = ca.Community.Picture,
                        Cover = ca.Community.Cover,
                        Link = ca.Community.Link,
                        IsPrivate = ca.Community.IsPrivate,
                        IsHidden = ca.Community.IsHidden,
                        MaxMembers = ca.Community.MaxMembers,
                        CreatedAt = ca.Community.CreatedAt,
                        UpdatedAt = ca.Community.UpdatedAt,
                        IsDeleted = ca.Community.IsDeleted,
                        DeletedAt = ca.Community.DeletedAt
                    }
                }).ToList() ?? new List<ApplicationUser_CommunityApplicationUserDto>(),
                ApplicationUserFriends = applicationUserToFind.ApplicationUserFriends?.Select(uf => new ApplicationUser_ApplicationUserFriendDto
                {
                    ApplicationUserFriendId = uf.ApplicationUserFriendId,
                    Sent = uf.Sent,
                    Accepted = uf.Accepted,
                    Rejected = uf.Rejected,
                    CreatedAt = uf.CreatedAt,
                    UpdatedAt = uf.UpdatedAt,
                    IsDeleted = uf.IsDeleted,
                    DeletedAt = uf.DeletedAt,
                    FriendList = uf.FriendList == null ? null : new ApplicationUser_ApplicationUserFriend_FriendListDto
                    {
                        FriendListId = uf.FriendList.FriendListId,
                        CreatedAt = uf.FriendList.CreatedAt,
                        UpdatedAt = uf.FriendList.UpdatedAt,
                        IsDeleted = uf.FriendList.IsDeleted,
                        DeletedAt = uf.FriendList.DeletedAt,
                        ApplicationUser = uf.FriendList.ApplicationUser == null ? null : new ApplicationUser_ApplicationUserDto
                        {
                            ApplicationUserId = uf.FriendList.ApplicationUser.Id,
                            FirstName = uf.FriendList.ApplicationUser.FirstName,
                            LastName = uf.FriendList.ApplicationUser.LastName,
                            BirthDate = uf.FriendList.ApplicationUser.BirthDate,
                            Country = uf.FriendList.ApplicationUser.Country,
                            City = uf.FriendList.ApplicationUser.City,
                            DisplayName = uf.FriendList.ApplicationUser.DisplayName,
                            Avatar = uf.FriendList.ApplicationUser.Avatar,
                            Picture = uf.FriendList.ApplicationUser.Picture,
                            Cover = uf.FriendList.ApplicationUser.Cover,
                            IsGamer = uf.FriendList.ApplicationUser.IsGamer,
                            IsDeveloper = uf.FriendList.ApplicationUser.IsDeveloper,
                            IsEditor = uf.FriendList.ApplicationUser.IsEditor,
                            DeveloperRole = uf.FriendList.ApplicationUser.DeveloperRole,
                            Bio = uf.FriendList.ApplicationUser.Bio,
                            Title = uf.FriendList.ApplicationUser.Title,
                            IsHidden = uf.FriendList.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = uf.FriendList.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = uf.FriendList.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = uf.FriendList.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = uf.FriendList.ApplicationUser.CreatedAt,
                            UpdatedAt = uf.FriendList.ApplicationUser.UpdatedAt,
                            IsDeleted = uf.FriendList.ApplicationUser.IsDeleted,
                            DeletedAt = uf.FriendList.ApplicationUser.DeletedAt
                        }
                    }
                }).ToList() ?? new List<ApplicationUser_ApplicationUserFriendDto>(),
                Cart = applicationUserToFind.Cart == null ? null : new ApplicationUser_CartDto
                {
                    CartId = applicationUserToFind.Cart.CartId,
                    IsCheckedOut = applicationUserToFind.Cart.IsCheckedOut,
                    CreatedAt = applicationUserToFind.Cart.CreatedAt,
                    UpdatedAt = applicationUserToFind.Cart.UpdatedAt,
                    IsDeleted = applicationUserToFind.Cart.IsDeleted,
                    DeletedAt = applicationUserToFind.Cart.DeletedAt,
                    CartVideogames = applicationUserToFind.Cart.CartVideogames?.Select(cv => new ApplicationUser_CartVideogameDto
                    {
                        CartVideogameId = cv.CartVideogameId,
                        Quantity = cv.Quantity,
                        CreatedAt = cv.CreatedAt,
                        UpdatedAt = cv.UpdatedAt,
                        IsDeleted = cv.IsDeleted,
                        DeletedAt = cv.DeletedAt,
                        Videogame = cv.Videogame == null ? null : new ApplicationUser_VideogameDto
                        {
                            VideogameId = cv.Videogame.VideogameId,
                            Title = cv.Videogame.Title,
                            Subtitle = cv.Videogame.Subtitle,
                            Description = cv.Videogame.Description,
                            ExtraDescription = cv.Videogame.ExtraDescription,
                            Genre = cv.Videogame.Genre,
                            Picture = cv.Videogame.Picture,
                            Cover = cv.Videogame.Cover,
                            Video = cv.Videogame.Video,
                            Link = cv.Videogame.Link,
                            ReleaseDate = cv.Videogame.ReleaseDate,
                            Platform = cv.Videogame.Platform,
                            AgeRating = cv.Videogame.AgeRating,
                            Contributors = cv.Videogame.Contributors,
                            Price = cv.Videogame.Price,
                            IsHidden = cv.Videogame.IsHidden,
                            IsAvailableForPurchase = cv.Videogame.IsAvailableForPurchase,
                            Country = cv.Videogame.Country,
                            City = cv.Videogame.City,
                            CreatedAt = cv.Videogame.CreatedAt,
                            UpdatedAt = cv.Videogame.UpdatedAt,
                            IsDeleted = cv.Videogame.IsDeleted,
                            DeletedAt = cv.Videogame.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_CartVideogameDto>()
                },
                FavouriteList = applicationUserToFind.FavouriteList == null ? null : new ApplicationUser_FavouriteListDto
                {
                    FavouriteListId = applicationUserToFind.FavouriteList.FavouriteListId,
                    CreatedAt = applicationUserToFind.FavouriteList.CreatedAt,
                    UpdatedAt = applicationUserToFind.FavouriteList.UpdatedAt,
                    IsDeleted = applicationUserToFind.FavouriteList.IsDeleted,
                    DeletedAt = applicationUserToFind.FavouriteList.DeletedAt,
                    FavouriteVideogames = applicationUserToFind.FavouriteList.FavouriteVideogames?.Select(fv => new ApplicationUser_FavouriteVideogameDto
                    {
                        FavouriteVideogameId = fv.FavouriteVideogameId,
                        CreatedAt = fv.CreatedAt,
                        UpdatedAt = fv.UpdatedAt,
                        IsDeleted = fv.IsDeleted,
                        DeletedAt = fv.DeletedAt,
                        Videogame = fv.Videogame == null ? null : new ApplicationUser_VideogameDto
                        {
                            VideogameId = fv.Videogame.VideogameId,
                            Title = fv.Videogame.Title,
                            Subtitle = fv.Videogame.Subtitle,
                            Description = fv.Videogame.Description,
                            ExtraDescription = fv.Videogame.ExtraDescription,
                            Genre = fv.Videogame.Genre,
                            Picture = fv.Videogame.Picture,
                            Cover = fv.Videogame.Cover,
                            Video = fv.Videogame.Video,
                            Link = fv.Videogame.Link,
                            ReleaseDate = fv.Videogame.ReleaseDate,
                            Platform = fv.Videogame.Platform,
                            AgeRating = fv.Videogame.AgeRating,
                            Contributors = fv.Videogame.Contributors,
                            Price = fv.Videogame.Price,
                            IsHidden = fv.Videogame.IsHidden,
                            IsAvailableForPurchase = fv.Videogame.IsAvailableForPurchase,
                            Country = fv.Videogame.Country,
                            City = fv.Videogame.City,
                            CreatedAt = fv.Videogame.CreatedAt,
                            UpdatedAt = fv.Videogame.UpdatedAt,
                            IsDeleted = fv.Videogame.IsDeleted,
                            DeletedAt = fv.Videogame.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_FavouriteVideogameDto>()
                },
                FriendList = applicationUserToFind.FriendList == null ? null : new ApplicationUser_FriendListDto
                {
                    FriendListId = applicationUserToFind.FriendList.FriendListId,
                    CreatedAt = applicationUserToFind.FriendList.CreatedAt,
                    UpdatedAt = applicationUserToFind.FriendList.UpdatedAt,
                    IsDeleted = applicationUserToFind.FriendList.IsDeleted,
                    DeletedAt = applicationUserToFind.FriendList.DeletedAt,
                    ApplicationUserFriends = applicationUserToFind.FriendList.ApplicationUserFriends?.Select(uf => new ApplicationUser_FriendList_ApplicationUserFriendDto
                    {
                        ApplicationUserFriendId = uf.ApplicationUserFriendId,
                        Sent = uf.Sent,
                        Accepted = uf.Accepted,
                        Rejected = uf.Rejected,
                        CreatedAt = uf.CreatedAt,
                        UpdatedAt = uf.UpdatedAt,
                        IsDeleted = uf.IsDeleted,
                        DeletedAt = uf.DeletedAt,
                        ApplicationUser = uf.ApplicationUser == null ? null : new ApplicationUser_ApplicationUserDto
                        {
                            ApplicationUserId = uf.ApplicationUser.Id,
                            FirstName = uf.ApplicationUser.FirstName,
                            LastName = uf.ApplicationUser.LastName,
                            BirthDate = uf.ApplicationUser.BirthDate,
                            Country = uf.ApplicationUser.Country,
                            City = uf.ApplicationUser.City,
                            DisplayName = uf.ApplicationUser.DisplayName,
                            Avatar = uf.ApplicationUser.Avatar,
                            Picture = uf.ApplicationUser.Picture,
                            Cover = uf.ApplicationUser.Cover,
                            IsGamer = uf.ApplicationUser.IsGamer,
                            IsDeveloper = uf.ApplicationUser.IsDeveloper,
                            IsEditor = uf.ApplicationUser.IsEditor,
                            DeveloperRole = uf.ApplicationUser.DeveloperRole,
                            Bio = uf.ApplicationUser.Bio,
                            Title = uf.ApplicationUser.Title,
                            IsHidden = uf.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = uf.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = uf.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = uf.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = uf.ApplicationUser.CreatedAt,
                            UpdatedAt = uf.ApplicationUser.UpdatedAt,
                            IsDeleted = uf.ApplicationUser.IsDeleted,
                            DeletedAt = uf.ApplicationUser.DeletedAt
                        }
                    }).ToList() ?? new List<ApplicationUser_FriendList_ApplicationUserFriendDto>()
                }
            };

            return Ok(new
            {
                message = "Application user trovato con successo",
                applicationUser = applicationUserResponse,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationUser(string id, [FromForm] UpdateApplicationUserRequestDto updateApplicationUserRequestDto)
        {
            var result = await _applicationUserService.UpdateApplicationUserAsync(id, updateApplicationUserRequestDto);

            if (!result)
            {
                return BadRequest(new UpdateApplicationUserResponseDto
                {
                    Message = "Errore nella modifica dell'application user"
                });
            }

            return Ok(new UpdateApplicationUserResponseDto
            {
                Message = "Application user aggiornato con successo"
            });
        }
    }
}
