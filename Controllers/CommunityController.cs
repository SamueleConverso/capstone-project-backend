using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.DTOs.Feed.Community;
using Capstone_Project.DTOs.Feed.CommunityApplicationUser;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly CommunityService _communityService;
        private readonly IWebHostEnvironment _env;

        public CommunityController(CommunityService communityService, IWebHostEnvironment env)
        {
            _communityService = communityService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddCommunity([FromForm] CreateCommunityRequestDto createCommunityRequestDto)
        {

            if (createCommunityRequestDto.PictureFile != null && createCommunityRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createCommunityRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createCommunityRequestDto.PictureFile.CopyToAsync(stream);
                }

                createCommunityRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                createCommunityRequestDto.Picture = null;
            }

            if (createCommunityRequestDto.CoverFile != null && createCommunityRequestDto.CoverFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createCommunityRequestDto.CoverFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createCommunityRequestDto.CoverFile.CopyToAsync(stream);
                }

                createCommunityRequestDto.Cover = $"/images/{uniqueName}";
            }
            else
            {
                createCommunityRequestDto.Cover = null;
            }

            bool.TryParse(createCommunityRequestDto.IsPrivate, out var isPrivateBool);
            bool.TryParse(createCommunityRequestDto.IsHidden, out var isHiddenBool);
            int.TryParse(createCommunityRequestDto.MaxMembers, out var maxMembersInt);

            var newCommunity = new Community
            {
                Type = createCommunityRequestDto.Type,
                Name = createCommunityRequestDto.Name,
                ExtraName = createCommunityRequestDto.ExtraName,
                Description = createCommunityRequestDto.Description,
                ExtraDescription = createCommunityRequestDto.ExtraDescription,
                Picture = createCommunityRequestDto.Picture,
                Cover = createCommunityRequestDto.Cover,
                Link = createCommunityRequestDto.Link,
                IsPrivate = isPrivateBool,
                IsHidden = isHiddenBool,
                MaxMembers = maxMembersInt,
                IsDeleted = false,
                ApplicationUserId = createCommunityRequestDto.ApplicationUserId,
            };

            var result = await _communityService.AddCommunityAsync(newCommunity);

            if (!result)
            {
                return BadRequest(new CreateCommunityResponseDto
                {
                    Message = "Errore nell'aggiunta della community"
                });
            }

            return Ok(new CreateCommunityResponseDto
            {
                Message = "Community aggiunta con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommunities()
        {
            var communitiesList = await _communityService.GetAllCommunitiesAsync();

            if (communitiesList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero delle community",
                    communites = new List<Community>()
                });
            }

            if (!communitiesList.Any())
            {
                return Ok(new
                {
                    message = "Nessuna community trovata",
                    communities = new List<Community>()
                });
            }
            var communitiesResponse = communitiesList.Select(c => new CommunityDto()
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
                DeletedAt = c.DeletedAt,
                ApplicationUser = c.ApplicationUser != null ? new Community_ApplicationUserDto
                {
                    ApplicationUserId = c.ApplicationUser.Id,
                    FirstName = c.ApplicationUser.FirstName,
                    LastName = c.ApplicationUser.LastName,
                    BirthDate = c.ApplicationUser.BirthDate,
                    Country = c.ApplicationUser.Country,
                    City = c.ApplicationUser.City,
                    DisplayName = c.ApplicationUser.DisplayName,
                    Avatar = c.ApplicationUser.Avatar,
                    Picture = c.ApplicationUser.Picture,
                    Cover = c.ApplicationUser.Cover,
                    IsGamer = c.ApplicationUser.IsGamer,
                    IsDeveloper = c.ApplicationUser.IsDeveloper,
                    IsEditor = c.ApplicationUser.IsEditor,
                    DeveloperRole = c.ApplicationUser.DeveloperRole,
                    Bio = c.ApplicationUser.Bio,
                    Title = c.ApplicationUser.Title,
                    IsHidden = c.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = c.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = c.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = c.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = c.ApplicationUser.CreatedAt,
                    UpdatedAt = c.ApplicationUser.UpdatedAt,
                    IsDeleted = c.ApplicationUser.IsDeleted,
                    DeletedAt = c.ApplicationUser.DeletedAt,
                } : null,
                CommunityApplicationUsers = c.CommunityApplicationUsers.Select(ca => new Community_CommunityApplicationUserDto
                {
                    CommunityApplicationUserId = ca.CommunityApplicationUserId,
                    CreatedAt = ca.CreatedAt,
                    UpdatedAt = ca.UpdatedAt,
                    IsDeleted = ca.IsDeleted,
                    DeletedAt = ca.DeletedAt,
                    ApplicationUser = ca.ApplicationUser != null ? new Community_ApplicationUserDto
                    {
                        ApplicationUserId = ca.ApplicationUser.Id,
                        FirstName = ca.ApplicationUser.FirstName,
                        LastName = ca.ApplicationUser.LastName,
                        BirthDate = ca.ApplicationUser.BirthDate,
                        Country = ca.ApplicationUser.Country,
                        City = ca.ApplicationUser.City,
                        DisplayName = ca.ApplicationUser.DisplayName,
                        Avatar = ca.ApplicationUser.Avatar,
                        Picture = ca.ApplicationUser.Picture,
                        Cover = ca.ApplicationUser.Cover,
                        IsGamer = ca.ApplicationUser.IsGamer,
                        IsDeveloper = ca.ApplicationUser.IsDeveloper,
                        IsEditor = ca.ApplicationUser.IsEditor,
                        DeveloperRole = ca.ApplicationUser.DeveloperRole,
                        Bio = ca.ApplicationUser.Bio,
                        Title = ca.ApplicationUser.Title,
                        IsHidden = ca.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = ca.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = ca.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = ca.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = ca.ApplicationUser.CreatedAt,
                        UpdatedAt = ca.ApplicationUser.UpdatedAt,
                        IsDeleted = ca.ApplicationUser.IsDeleted,
                        DeletedAt = ca.ApplicationUser.DeletedAt,
                    } : null
                }).ToList(),
                Posts = c.Posts.Select(p => new Community_PostDto
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
                    Likes = p.Likes,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted,
                    DeletedAt = p.DeletedAt,
                    PostLikes = p.PostLikes.Select(pl => new Community_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser != null ? new Community_Post_PostLike_ApplicationUserDto
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
                            IsHidden = pl.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = pl.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = pl.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = pl.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = pl.ApplicationUser.CreatedAt,
                            UpdatedAt = pl.ApplicationUser.UpdatedAt,
                            IsDeleted = pl.ApplicationUser.IsDeleted,
                            DeletedAt = pl.ApplicationUser.DeletedAt
                        } : null
                    }).ToList(),
                    ApplicationUser = p.ApplicationUser != null ? new Community_Post_ApplicationUserDto
                    {
                        ApplicationUserId = p.ApplicationUser.Id,
                        FirstName = p.ApplicationUser.FirstName,
                        LastName = p.ApplicationUser.LastName,
                        BirthDate = p.ApplicationUser.BirthDate,
                        Country = p.ApplicationUser.Country,
                        City = p.ApplicationUser.City,
                        DisplayName = p.ApplicationUser.DisplayName,
                        Avatar = p.ApplicationUser.Avatar,
                        Picture = p.ApplicationUser.Picture,
                        Cover = p.ApplicationUser.Cover,
                        IsGamer = p.ApplicationUser.IsGamer,
                        IsDeveloper = p.ApplicationUser.IsDeveloper,
                        IsEditor = p.ApplicationUser.IsEditor,
                        DeveloperRole = p.ApplicationUser.DeveloperRole,
                        Bio = p.ApplicationUser.Bio,
                        Title = p.ApplicationUser.Title,
                        IsHidden = p.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = p.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = p.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = p.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = p.ApplicationUser.CreatedAt,
                        UpdatedAt = p.ApplicationUser.UpdatedAt,
                        IsDeleted = p.ApplicationUser.IsDeleted,
                        DeletedAt = p.ApplicationUser.DeletedAt
                    } : null,
                    Comments = p.Comments.Select(c => new Community_Post_CommentDto
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,
                        Picture = c.Picture,
                        Likes = c.Likes,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        IsDeleted = c.IsDeleted,
                        DeletedAt = c.DeletedAt,
                        ApplicationUser = c.ApplicationUser != null ? new Community_Post_Comment_ApplicationUserDto
                        {
                            ApplicationUserId = c.ApplicationUser.Id,
                            FirstName = c.ApplicationUser.FirstName,
                            LastName = c.ApplicationUser.LastName,
                            BirthDate = c.ApplicationUser.BirthDate,
                            Country = c.ApplicationUser.Country,
                            City = c.ApplicationUser.City,
                            DisplayName = c.ApplicationUser.DisplayName,
                            Avatar = c.ApplicationUser.Avatar,
                            Picture = c.ApplicationUser.Picture,
                            Cover = c.ApplicationUser.Cover,
                            IsGamer = c.ApplicationUser.IsGamer,
                            IsDeveloper = c.ApplicationUser.IsDeveloper,
                            IsEditor = c.ApplicationUser.IsEditor,
                            DeveloperRole = c.ApplicationUser.DeveloperRole,
                            Bio = c.ApplicationUser.Bio,
                            Title = c.ApplicationUser.Title,
                            IsHidden = c.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = c.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = c.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = c.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = c.ApplicationUser.CreatedAt,
                            UpdatedAt = c.ApplicationUser.UpdatedAt,
                            IsDeleted = c.ApplicationUser.IsDeleted,
                            DeletedAt = c.ApplicationUser.DeletedAt
                        } : null,
                        CommentLikes = c.CommentLikes.Select(cl => new Community_Post_Comment_CommentLikeDto
                        {
                            CommentLikeId = cl.CommentLikeId,
                            CreatedAt = cl.CreatedAt,
                            UpdatedAt = cl.UpdatedAt,
                            IsDeleted = cl.IsDeleted,
                            DeletedAt = cl.DeletedAt,
                            ApplicationUser = cl.ApplicationUser != null ? new Community_Post_Comment_CommentLike_ApplicationUserDto
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
                                IsHidden = cl.ApplicationUser.IsHidden,
                                IsFavouriteListPrivate = cl.ApplicationUser.IsFavouriteListPrivate,
                                IsFriendListPrivate = cl.ApplicationUser.IsFriendListPrivate,
                                AutoAcceptFriendRequests = cl.ApplicationUser.AutoAcceptFriendRequests,
                                CreatedAt = cl.ApplicationUser.CreatedAt,
                                UpdatedAt = cl.ApplicationUser.UpdatedAt,
                                IsDeleted = cl.ApplicationUser.IsDeleted,
                                DeletedAt = cl.ApplicationUser.DeletedAt
                            } : null
                        }).ToList()
                    }).ToList(),
                    Videogame = p.Videogame != null ? new Community_Post_VideogameDto
                    {
                        VideogameId = p.Videogame.VideogameId,
                        Title = p.Videogame.Title,
                        Subtitle = p.Videogame.Subtitle,
                        Description = p.Videogame.Description,
                        ExtraDescription = p.Videogame.ExtraDescription,
                        Genre = p.Videogame.Genre,
                        Picture = p.Videogame.Picture,
                        Cover = p.Videogame.Cover,
                        Video = p.Videogame.Video,
                        Link = p.Videogame.Link,
                        ReleaseDate = p.Videogame.ReleaseDate,
                        Platform = p.Videogame.Platform,
                        AgeRating = p.Videogame.AgeRating,
                        Contributors = p.Videogame.Contributors,
                        Price = p.Videogame.Price,
                        IsHidden = p.Videogame.IsHidden,
                        IsAvailableForPurchase = p.Videogame.IsAvailableForPurchase,
                        Country = p.Videogame.Country,
                        City = p.Videogame.City,
                        CreatedAt = p.Videogame.CreatedAt,
                        UpdatedAt = p.Videogame.UpdatedAt,
                        IsDeleted = p.Videogame.IsDeleted,
                        DeletedAt = p.Videogame.DeletedAt,
                        ApplicationUser = p.Videogame.ApplicationUser != null ? new Community_Post_Videogame_ApplicationUserDto
                        {
                            ApplicationUserId = p.Videogame.ApplicationUser.Id,
                            FirstName = p.Videogame.ApplicationUser.FirstName,
                            LastName = p.Videogame.ApplicationUser.LastName,
                            BirthDate = p.Videogame.ApplicationUser.BirthDate,
                            Country = p.Videogame.ApplicationUser.Country,
                            City = p.Videogame.ApplicationUser.City,
                            DisplayName = p.Videogame.ApplicationUser.DisplayName,
                            Avatar = p.Videogame.ApplicationUser.Avatar,
                            Picture = p.Videogame.ApplicationUser.Picture,
                            Cover = p.Videogame.ApplicationUser.Cover,
                            IsGamer = p.Videogame.ApplicationUser.IsGamer,
                            IsDeveloper = p.Videogame.ApplicationUser.IsDeveloper,
                            IsEditor = p.Videogame.ApplicationUser.IsEditor,
                            DeveloperRole = p.Videogame.ApplicationUser.DeveloperRole,
                            Bio = p.Videogame.ApplicationUser.Bio,
                            Title = p.Videogame.ApplicationUser.Title,
                            IsHidden = p.Videogame.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = p.Videogame.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = p.Videogame.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = p.Videogame.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = p.Videogame.ApplicationUser.CreatedAt,
                            UpdatedAt = p.Videogame.ApplicationUser.UpdatedAt,
                            IsDeleted = p.Videogame.ApplicationUser.IsDeleted,
                            DeletedAt = p.Videogame.ApplicationUser.DeletedAt
                        } : null
                    } : null
                }).ToList()
            });

            return Ok(new
            {
                message = $"Numero community trovate: {communitiesResponse.Count()}",
                communities = communitiesResponse
            });

        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCommunityById(Guid id)
        {
            var communityToFind = await _communityService.GetCommunityByIdAsync(id);

            if (communityToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero della community"
                });
            }

            var communityResponse = new CommunityDto()
            {
                CommunityId = communityToFind.CommunityId,
                Type = communityToFind.Type,
                Name = communityToFind.Name,
                ExtraName = communityToFind.ExtraName,
                Description = communityToFind.Description,
                ExtraDescription = communityToFind.ExtraDescription,
                Picture = communityToFind.Picture,
                Cover = communityToFind.Cover,
                Link = communityToFind.Link,
                IsPrivate = communityToFind.IsPrivate,
                IsHidden = communityToFind.IsHidden,
                MaxMembers = communityToFind.MaxMembers,
                CreatedAt = communityToFind.CreatedAt,
                UpdatedAt = communityToFind.UpdatedAt,
                IsDeleted = communityToFind.IsDeleted,
                DeletedAt = communityToFind.DeletedAt,
                ApplicationUser = communityToFind.ApplicationUser != null ? new Community_ApplicationUserDto
                {
                    ApplicationUserId = communityToFind.ApplicationUser.Id,
                    FirstName = communityToFind.ApplicationUser.FirstName,
                    LastName = communityToFind.ApplicationUser.LastName,
                    BirthDate = communityToFind.ApplicationUser.BirthDate,
                    Country = communityToFind.ApplicationUser.Country,
                    City = communityToFind.ApplicationUser.City,
                    DisplayName = communityToFind.ApplicationUser.DisplayName,
                    Avatar = communityToFind.ApplicationUser.Avatar,
                    Picture = communityToFind.ApplicationUser.Picture,
                    Cover = communityToFind.ApplicationUser.Cover,
                    IsGamer = communityToFind.ApplicationUser.IsGamer,
                    IsDeveloper = communityToFind.ApplicationUser.IsDeveloper,
                    IsEditor = communityToFind.ApplicationUser.IsEditor,
                    DeveloperRole = communityToFind.ApplicationUser.DeveloperRole,
                    Bio = communityToFind.ApplicationUser.Bio,
                    Title = communityToFind.ApplicationUser.Title,
                    IsHidden = communityToFind.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = communityToFind.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = communityToFind.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = communityToFind.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = communityToFind.ApplicationUser.CreatedAt,
                    UpdatedAt = communityToFind.ApplicationUser.UpdatedAt,
                    IsDeleted = communityToFind.ApplicationUser.IsDeleted,
                    DeletedAt = communityToFind.ApplicationUser.DeletedAt,
                } : null,
                CommunityApplicationUsers = communityToFind.CommunityApplicationUsers.Select(ca => new Community_CommunityApplicationUserDto
                {
                    CommunityApplicationUserId = ca.CommunityApplicationUserId,
                    CreatedAt = ca.CreatedAt,
                    UpdatedAt = ca.UpdatedAt,
                    IsDeleted = ca.IsDeleted,
                    DeletedAt = ca.DeletedAt,
                    ApplicationUser = ca.ApplicationUser != null ? new Community_ApplicationUserDto
                    {
                        ApplicationUserId = ca.ApplicationUser.Id,
                        FirstName = ca.ApplicationUser.FirstName,
                        LastName = ca.ApplicationUser.LastName,
                        BirthDate = ca.ApplicationUser.BirthDate,
                        Country = ca.ApplicationUser.Country,
                        City = ca.ApplicationUser.City,
                        DisplayName = ca.ApplicationUser.DisplayName,
                        Avatar = ca.ApplicationUser.Avatar,
                        Picture = ca.ApplicationUser.Picture,
                        Cover = ca.ApplicationUser.Cover,
                        IsGamer = ca.ApplicationUser.IsGamer,
                        IsDeveloper = ca.ApplicationUser.IsDeveloper,
                        IsEditor = ca.ApplicationUser.IsEditor,
                        DeveloperRole = ca.ApplicationUser.DeveloperRole,
                        Bio = ca.ApplicationUser.Bio,
                        Title = ca.ApplicationUser.Title,
                        IsHidden = ca.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = ca.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = ca.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = ca.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = ca.ApplicationUser.CreatedAt,
                        UpdatedAt = ca.ApplicationUser.UpdatedAt,
                        IsDeleted = ca.ApplicationUser.IsDeleted,
                        DeletedAt = ca.ApplicationUser.DeletedAt,
                    } : null
                }).ToList(),
                Posts = communityToFind.Posts.Select(p => new Community_PostDto
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
                    Likes = p.Likes,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted,
                    DeletedAt = p.DeletedAt,
                    PostLikes = p.PostLikes.Select(pl => new Community_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser != null ? new Community_Post_PostLike_ApplicationUserDto
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
                            IsHidden = pl.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = pl.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = pl.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = pl.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = pl.ApplicationUser.CreatedAt,
                            UpdatedAt = pl.ApplicationUser.UpdatedAt,
                            IsDeleted = pl.ApplicationUser.IsDeleted,
                            DeletedAt = pl.ApplicationUser.DeletedAt
                        } : null
                    }).ToList(),
                    ApplicationUser = p.ApplicationUser != null ? new Community_Post_ApplicationUserDto
                    {
                        ApplicationUserId = p.ApplicationUser.Id,
                        FirstName = p.ApplicationUser.FirstName,
                        LastName = p.ApplicationUser.LastName,
                        BirthDate = p.ApplicationUser.BirthDate,
                        Country = p.ApplicationUser.Country,
                        City = p.ApplicationUser.City,
                        DisplayName = p.ApplicationUser.DisplayName,
                        Avatar = p.ApplicationUser.Avatar,
                        Picture = p.ApplicationUser.Picture,
                        Cover = p.ApplicationUser.Cover,
                        IsGamer = p.ApplicationUser.IsGamer,
                        IsDeveloper = p.ApplicationUser.IsDeveloper,
                        IsEditor = p.ApplicationUser.IsEditor,
                        DeveloperRole = p.ApplicationUser.DeveloperRole,
                        Bio = p.ApplicationUser.Bio,
                        Title = p.ApplicationUser.Title,
                        IsHidden = p.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = p.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = p.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = p.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = p.ApplicationUser.CreatedAt,
                        UpdatedAt = p.ApplicationUser.UpdatedAt,
                        IsDeleted = p.ApplicationUser.IsDeleted,
                        DeletedAt = p.ApplicationUser.DeletedAt
                    } : null,
                    Comments = p.Comments.Select(c => new Community_Post_CommentDto
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,
                        Picture = c.Picture,
                        Likes = c.Likes,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        IsDeleted = c.IsDeleted,
                        DeletedAt = c.DeletedAt,
                        ApplicationUser = c.ApplicationUser != null ? new Community_Post_Comment_ApplicationUserDto
                        {
                            ApplicationUserId = c.ApplicationUser.Id,
                            FirstName = c.ApplicationUser.FirstName,
                            LastName = c.ApplicationUser.LastName,
                            BirthDate = c.ApplicationUser.BirthDate,
                            Country = c.ApplicationUser.Country,
                            City = c.ApplicationUser.City,
                            DisplayName = c.ApplicationUser.DisplayName,
                            Avatar = c.ApplicationUser.Avatar,
                            Picture = c.ApplicationUser.Picture,
                            Cover = c.ApplicationUser.Cover,
                            IsGamer = c.ApplicationUser.IsGamer,
                            IsDeveloper = c.ApplicationUser.IsDeveloper,
                            IsEditor = c.ApplicationUser.IsEditor,
                            DeveloperRole = c.ApplicationUser.DeveloperRole,
                            Bio = c.ApplicationUser.Bio,
                            Title = c.ApplicationUser.Title,
                            IsHidden = c.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = c.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = c.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = c.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = c.ApplicationUser.CreatedAt,
                            UpdatedAt = c.ApplicationUser.UpdatedAt,
                            IsDeleted = c.ApplicationUser.IsDeleted,
                            DeletedAt = c.ApplicationUser.DeletedAt
                        } : null,
                        CommentLikes = c.CommentLikes.Select(cl => new Community_Post_Comment_CommentLikeDto
                        {
                            CommentLikeId = cl.CommentLikeId,
                            CreatedAt = cl.CreatedAt,
                            UpdatedAt = cl.UpdatedAt,
                            IsDeleted = cl.IsDeleted,
                            DeletedAt = cl.DeletedAt,
                            ApplicationUser = cl.ApplicationUser != null ? new Community_Post_Comment_CommentLike_ApplicationUserDto
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
                                IsHidden = cl.ApplicationUser.IsHidden,
                                IsFavouriteListPrivate = cl.ApplicationUser.IsFavouriteListPrivate,
                                IsFriendListPrivate = cl.ApplicationUser.IsFriendListPrivate,
                                AutoAcceptFriendRequests = cl.ApplicationUser.AutoAcceptFriendRequests,
                                CreatedAt = cl.ApplicationUser.CreatedAt,
                                UpdatedAt = cl.ApplicationUser.UpdatedAt,
                                IsDeleted = cl.ApplicationUser.IsDeleted,
                                DeletedAt = cl.ApplicationUser.DeletedAt
                            } : null
                        }).ToList()
                    }).ToList(),
                    Videogame = p.Videogame != null ? new Community_Post_VideogameDto
                    {
                        VideogameId = p.Videogame.VideogameId,
                        Title = p.Videogame.Title,
                        Subtitle = p.Videogame.Subtitle,
                        Description = p.Videogame.Description,
                        ExtraDescription = p.Videogame.ExtraDescription,
                        Genre = p.Videogame.Genre,
                        Picture = p.Videogame.Picture,
                        Cover = p.Videogame.Cover,
                        Video = p.Videogame.Video,
                        Link = p.Videogame.Link,
                        ReleaseDate = p.Videogame.ReleaseDate,
                        Platform = p.Videogame.Platform,
                        AgeRating = p.Videogame.AgeRating,
                        Contributors = p.Videogame.Contributors,
                        Price = p.Videogame.Price,
                        IsHidden = p.Videogame.IsHidden,
                        IsAvailableForPurchase = p.Videogame.IsAvailableForPurchase,
                        Country = p.Videogame.Country,
                        City = p.Videogame.City,
                        CreatedAt = p.Videogame.CreatedAt,
                        UpdatedAt = p.Videogame.UpdatedAt,
                        IsDeleted = p.Videogame.IsDeleted,
                        DeletedAt = p.Videogame.DeletedAt,
                        ApplicationUser = p.Videogame.ApplicationUser != null ? new Community_Post_Videogame_ApplicationUserDto
                        {
                            ApplicationUserId = p.Videogame.ApplicationUser.Id,
                            FirstName = p.Videogame.ApplicationUser.FirstName,
                            LastName = p.Videogame.ApplicationUser.LastName,
                            BirthDate = p.Videogame.ApplicationUser.BirthDate,
                            Country = p.Videogame.ApplicationUser.Country,
                            City = p.Videogame.ApplicationUser.City,
                            DisplayName = p.Videogame.ApplicationUser.DisplayName,
                            Avatar = p.Videogame.ApplicationUser.Avatar,
                            Picture = p.Videogame.ApplicationUser.Picture,
                            Cover = p.Videogame.ApplicationUser.Cover,
                            IsGamer = p.Videogame.ApplicationUser.IsGamer,
                            IsDeveloper = p.Videogame.ApplicationUser.IsDeveloper,
                            IsEditor = p.Videogame.ApplicationUser.IsEditor,
                            DeveloperRole = p.Videogame.ApplicationUser.DeveloperRole,
                            Bio = p.Videogame.ApplicationUser.Bio,
                            Title = p.Videogame.ApplicationUser.Title,
                            IsHidden = p.Videogame.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = p.Videogame.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = p.Videogame.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = p.Videogame.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = p.Videogame.ApplicationUser.CreatedAt,
                            UpdatedAt = p.Videogame.ApplicationUser.UpdatedAt,
                            IsDeleted = p.Videogame.ApplicationUser.IsDeleted,
                            DeletedAt = p.Videogame.ApplicationUser.DeletedAt
                        } : null
                    } : null
                }).ToList()
            };

            return Ok(new
            {
                message = "Community trovata con successo",
                community = communityResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCommunity(Guid id, [FromForm] UpdateCommunityRequestDto updateCommunityRequestDto)
        {
            var result = await _communityService.UpdateCommunityAsync(id, updateCommunityRequestDto);

            if (!result)
            {
                return BadRequest(new UpdateCommunityResponseDto
                {
                    Message = "Errore nella modifica della community"
                });
            }

            return Ok(new UpdateCommunityResponseDto
            {
                Message = "Community aggiornata con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCommunity(Guid id)
        {
            var result = await _communityService.DeleteCommunityAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione della community"
                });
            }
            return Ok(new
            {
                message = "Community cancellata con successo"
            });
        }
    }
}
