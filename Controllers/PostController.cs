using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.DTOs.Feed.Community;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.DTOs.Feed.PostTag;
using Capstone_Project.DTOs.Feed.Tag;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IWebHostEnvironment _env;

        public PostController(PostService postService, IWebHostEnvironment env)
        {
            _postService = postService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] CreatePostRequestDto createPostRequestDto)
        {

            if (createPostRequestDto.PictureFile != null && createPostRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createPostRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createPostRequestDto.PictureFile.CopyToAsync(stream);
                }

                createPostRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                createPostRequestDto.Picture = null;
            }

            bool.TryParse(createPostRequestDto.IsLookingForGamers, out var isLookingForGamersBool);
            bool.TryParse(createPostRequestDto.IsLookingForDevelopers, out var isLookingForDevelopersBool);
            bool.TryParse(createPostRequestDto.IsLookingForEditors, out var isLookingForEditorsBool);
            bool.TryParse(createPostRequestDto.IsInUserFeed, out var isInUserFeedBool);
            bool.TryParse(createPostRequestDto.IsInGameFeed, out var isInGameFeedBool);
            bool.TryParse(createPostRequestDto.IsInCommunityFeed, out var isInCommunityFeedBool);
            bool.TryParse(createPostRequestDto.IsHidden, out var isHiddenBool);

            Guid? videogameIdGuid = null;
            if (createPostRequestDto.VideogameId != null && createPostRequestDto.VideogameId != "")
            {
                Guid.TryParse(createPostRequestDto.VideogameId, out var videogameIdGuidToParse);
                videogameIdGuid = videogameIdGuidToParse;
            }

            Guid? communityIdGuid = null;
            if (createPostRequestDto.CommunityId != null && createPostRequestDto.CommunityId != "")
            {
                Guid.TryParse(createPostRequestDto.CommunityId, out var communityIdGuidToParse);
                communityIdGuid = communityIdGuidToParse;
            }

            var newPost = new Post
            {
                Text = createPostRequestDto.Text,
                Picture = createPostRequestDto.Picture,
                Video = createPostRequestDto.Video,
                Mood = createPostRequestDto.Mood,
                IsLookingForGamers = isLookingForGamersBool,
                IsLookingForDevelopers = isLookingForDevelopersBool,
                IsLookingForEditors = isLookingForEditorsBool,
                Country = createPostRequestDto.Country,
                City = createPostRequestDto.City,
                IsInUserFeed = isInUserFeedBool,
                IsInGameFeed = isInGameFeedBool,
                IsInCommunityFeed = isInCommunityFeedBool,
                IsHidden = isHiddenBool,
                IsDeleted = false,
                ApplicationUserId = createPostRequestDto.ApplicationUserId,
                VideogameId = videogameIdGuid,
                CommunityId = communityIdGuid,
            };

            var result = await _postService.AddPostAsync(newPost);

            if (!result)
            {
                return BadRequest(new CreatePostResponseDto
                {
                    Message = "Errore nell'aggiunta del post"
                });
            }

            return Ok(new CreatePostResponseDto
            {
                Message = "Post aggiunto con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var postsList = await _postService.GetAllPostsAsync();

            if (postsList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dei post",
                    posts = new List<Post>()
                });
            }

            if (!postsList.Any())
            {
                return Ok(new
                {
                    message = "Nessun post trovato",
                    posts = new List<Post>()
                });
            }

            var postsResponse = postsList.Select(p => new PostDto()
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
                PostLikes = p.PostLikes.Select(pl => new Post_PostLikeDto
                {
                    PostLikeId = pl.PostLikeId,
                    CreatedAt = pl.CreatedAt,
                    UpdatedAt = pl.UpdatedAt,
                    IsDeleted = pl.IsDeleted,
                    DeletedAt = pl.DeletedAt,
                    ApplicationUser = pl.ApplicationUser != null ? new Post_PostLike_ApplicationUserDto
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
                    } : null
                }).ToList(),
                Comments = p.Comments.Select(c => new Post_CommentDto
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    Picture = c.Picture,
                    Likes = c.Likes,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt,
                    CommentLikes = c.CommentLikes.Select(cl => new Post_CommentLikeDto
                    {
                        CommentLikeId = cl.CommentLikeId,
                        CreatedAt = cl.CreatedAt,
                        UpdatedAt = cl.UpdatedAt,
                        IsDeleted = cl.IsDeleted,
                        DeletedAt = cl.DeletedAt,
                        ApplicationUser = cl.ApplicationUser != null ? new Post_CommentLike_ApplicationUserDto
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
                        } : null
                    }).ToList(),
                    ApplicationUser = c.ApplicationUser != null ? new Post_Comment_ApplicationUserDto
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
                        IsHidden = c.ApplicationUser.IsHidden
                    } : null
                }).ToList(),
                PostTags = p.PostTags.Select(pt => new Post_PostTagDto
                {
                    PostTagId = pt.PostTagId,
                    CreatedAt = pt.CreatedAt,
                    UpdatedAt = pt.UpdatedAt,
                    IsDeleted = pt.IsDeleted,
                    DeletedAt = pt.DeletedAt,
                    Tag = new Post_TagDto
                    {
                        TagId = pt.Tag.TagId,
                        Entry = pt.Tag.Entry,
                        CreatedAt = pt.Tag.CreatedAt,
                        UpdatedAt = pt.Tag.UpdatedAt,
                        IsDeleted = pt.Tag.IsDeleted,
                        DeletedAt = pt.Tag.DeletedAt
                    }
                }).ToList(),
                ApplicationUser = p.ApplicationUser != null ? new Post_ApplicationUserDto
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
                    IsHidden = p.ApplicationUser.IsHidden
                } : null,
                Videogame = p.Videogame != null ? new Post_VideogameDto
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
                    ApplicationUser = p.Videogame.ApplicationUser != null ? new Post_Videogame_ApplicationUserDto
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
                        IsHidden = p.Videogame.ApplicationUser.IsHidden
                    } : null
                } : null,
                Community = p.Community != null ? new Post_CommunityDto
                {
                    CommunityId = p.Community.CommunityId,
                    Type = p.Community.Type,
                    Name = p.Community.Name,
                    ExtraName = p.Community.ExtraName,
                    Description = p.Community.Description,
                    ExtraDescription = p.Community.ExtraDescription,
                    Picture = p.Community.Picture,
                    Cover = p.Community.Cover,
                    Link = p.Community.Link,
                    IsPrivate = p.Community.IsPrivate,
                    IsHidden = p.Community.IsHidden,
                    MaxMembers = p.Community.MaxMembers,
                    CreatedAt = p.Community.CreatedAt,
                    UpdatedAt = p.Community.UpdatedAt,
                    IsDeleted = p.Community.IsDeleted,
                    DeletedAt = p.Community.DeletedAt,
                    ApplicationUser = p.Community.ApplicationUser != null ? new Post_Community_ApplicationUserDto
                    {
                        ApplicationUserId = p.Community.ApplicationUser.Id,
                        FirstName = p.Community.ApplicationUser.FirstName,
                        LastName = p.Community.ApplicationUser.LastName,
                        BirthDate = p.Community.ApplicationUser.BirthDate,
                        Country = p.Community.ApplicationUser.Country,
                        City = p.Community.ApplicationUser.City,
                        DisplayName = p.Community.ApplicationUser.DisplayName,
                        Avatar = p.Community.ApplicationUser.Avatar,
                        Picture = p.Community.ApplicationUser.Picture,
                        Cover = p.Community.ApplicationUser.Cover,
                        IsGamer = p.Community.ApplicationUser.IsGamer,
                        IsDeveloper = p.Community.ApplicationUser.IsDeveloper,
                        IsEditor = p.Community.ApplicationUser.IsEditor,
                        DeveloperRole = p.Community.ApplicationUser.DeveloperRole,
                        Bio = p.Community.ApplicationUser.Bio,
                        Title = p.Community.ApplicationUser.Title,
                        IsHidden = p.Community.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = p.Community.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = p.Community.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = p.Community.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = p.Community.ApplicationUser.CreatedAt,
                        UpdatedAt = p.Community.ApplicationUser.UpdatedAt,
                        IsDeleted = p.Community.ApplicationUser.IsDeleted,
                        DeletedAt = p.Community.ApplicationUser.DeletedAt,
                    } : null
                } : null
            });

            return Ok(new
            {
                message = $"Numero post trovati: {postsResponse.Count()}",
                posts = postsResponse
            });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var postToFind = await _postService.GetPostByIdAsync(id);

            if (postToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del post"
                });
            }

            var postResponse = new PostDto()
            {
                PostId = postToFind.PostId,
                Text = postToFind.Text,
                Picture = postToFind.Picture,
                Video = postToFind.Video,
                Mood = postToFind.Mood,
                IsLookingForGamers = postToFind.IsLookingForGamers,
                IsLookingForDevelopers = postToFind.IsLookingForDevelopers,
                IsLookingForEditors = postToFind.IsLookingForEditors,
                Country = postToFind.Country,
                City = postToFind.City,
                IsInUserFeed = postToFind.IsInUserFeed,
                IsInGameFeed = postToFind.IsInGameFeed,
                IsInCommunityFeed = postToFind.IsInCommunityFeed,
                IsHidden = postToFind.IsHidden,
                Likes = postToFind.Likes,
                CreatedAt = postToFind.CreatedAt,
                UpdatedAt = postToFind.UpdatedAt,
                IsDeleted = postToFind.IsDeleted,
                DeletedAt = postToFind.DeletedAt,
                PostLikes = postToFind.PostLikes.Select(pl => new Post_PostLikeDto
                {
                    PostLikeId = pl.PostLikeId,
                    CreatedAt = pl.CreatedAt,
                    UpdatedAt = pl.UpdatedAt,
                    IsDeleted = pl.IsDeleted,
                    DeletedAt = pl.DeletedAt,
                    ApplicationUser = pl.ApplicationUser != null ? new Post_PostLike_ApplicationUserDto
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
                    } : null
                }).ToList(),
                Comments = postToFind.Comments.Select(c => new Post_CommentDto
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    Picture = c.Picture,
                    Likes = c.Likes,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt,
                    CommentLikes = c.CommentLikes.Select(cl => new Post_CommentLikeDto
                    {
                        CommentLikeId = cl.CommentLikeId,
                        CreatedAt = cl.CreatedAt,
                        UpdatedAt = cl.UpdatedAt,
                        IsDeleted = cl.IsDeleted,
                        DeletedAt = cl.DeletedAt,
                        ApplicationUser = cl.ApplicationUser != null ? new Post_CommentLike_ApplicationUserDto
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
                        } : null
                    }).ToList(),
                    ApplicationUser = c.ApplicationUser != null ? new Post_Comment_ApplicationUserDto
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
                        IsHidden = c.ApplicationUser.IsHidden
                    } : null
                }).ToList(),
                PostTags = postToFind.PostTags.Select(pt => new Post_PostTagDto
                {
                    PostTagId = pt.PostTagId,
                    CreatedAt = pt.CreatedAt,
                    UpdatedAt = pt.UpdatedAt,
                    IsDeleted = pt.IsDeleted,
                    DeletedAt = pt.DeletedAt,
                    Tag = new Post_TagDto
                    {
                        TagId = pt.Tag.TagId,
                        Entry = pt.Tag.Entry,
                        CreatedAt = pt.Tag.CreatedAt,
                        UpdatedAt = pt.Tag.UpdatedAt,
                        IsDeleted = pt.Tag.IsDeleted,
                        DeletedAt = pt.Tag.DeletedAt
                    }
                }).ToList(),
                ApplicationUser = postToFind.ApplicationUser != null ? new Post_ApplicationUserDto
                {
                    ApplicationUserId = postToFind.ApplicationUser.Id,
                    FirstName = postToFind.ApplicationUser.FirstName,
                    LastName = postToFind.ApplicationUser.LastName,
                    BirthDate = postToFind.ApplicationUser.BirthDate,
                    Country = postToFind.ApplicationUser.Country,
                    City = postToFind.ApplicationUser.City,
                    DisplayName = postToFind.ApplicationUser.DisplayName,
                    Avatar = postToFind.ApplicationUser.Avatar,
                    Picture = postToFind.ApplicationUser.Picture,
                    Cover = postToFind.ApplicationUser.Cover,
                    IsGamer = postToFind.ApplicationUser.IsGamer,
                    IsDeveloper = postToFind.ApplicationUser.IsDeveloper,
                    IsEditor = postToFind.ApplicationUser.IsEditor,
                    DeveloperRole = postToFind.ApplicationUser.DeveloperRole,
                    Bio = postToFind.ApplicationUser.Bio,
                    Title = postToFind.ApplicationUser.Title,
                    IsHidden = postToFind.ApplicationUser.IsHidden
                } : null,
                Videogame = postToFind.Videogame != null ? new Post_VideogameDto
                {
                    VideogameId = postToFind.Videogame.VideogameId,
                    Title = postToFind.Videogame.Title,
                    Subtitle = postToFind.Videogame.Subtitle,
                    Description = postToFind.Videogame.Description,
                    ExtraDescription = postToFind.Videogame.ExtraDescription,
                    Genre = postToFind.Videogame.Genre,
                    Picture = postToFind.Videogame.Picture,
                    Cover = postToFind.Videogame.Cover,
                    Video = postToFind.Videogame.Video,
                    Link = postToFind.Videogame.Link,
                    ReleaseDate = postToFind.Videogame.ReleaseDate,
                    Platform = postToFind.Videogame.Platform,
                    AgeRating = postToFind.Videogame.AgeRating,
                    Contributors = postToFind.Videogame.Contributors,
                    Price = postToFind.Videogame.Price,
                    IsHidden = postToFind.Videogame.IsHidden,
                    IsAvailableForPurchase = postToFind.Videogame.IsAvailableForPurchase,
                    Country = postToFind.Videogame.Country,
                    City = postToFind.Videogame.City,
                    CreatedAt = postToFind.Videogame.CreatedAt,
                    UpdatedAt = postToFind.Videogame.UpdatedAt,
                    IsDeleted = postToFind.Videogame.IsDeleted,
                    DeletedAt = postToFind.Videogame.DeletedAt,
                    ApplicationUser = postToFind.Videogame.ApplicationUser != null ? new Post_Videogame_ApplicationUserDto
                    {
                        ApplicationUserId = postToFind.Videogame.ApplicationUser.Id,
                        FirstName = postToFind.Videogame.ApplicationUser.FirstName,
                        LastName = postToFind.Videogame.ApplicationUser.LastName,
                        BirthDate = postToFind.Videogame.ApplicationUser.BirthDate,
                        Country = postToFind.Videogame.ApplicationUser.Country,
                        City = postToFind.Videogame.ApplicationUser.City,
                        DisplayName = postToFind.Videogame.ApplicationUser.DisplayName,
                        Avatar = postToFind.Videogame.ApplicationUser.Avatar,
                        Picture = postToFind.Videogame.ApplicationUser.Picture,
                        Cover = postToFind.Videogame.ApplicationUser.Cover,
                        IsGamer = postToFind.Videogame.ApplicationUser.IsGamer,
                        IsDeveloper = postToFind.Videogame.ApplicationUser.IsDeveloper,
                        IsEditor = postToFind.Videogame.ApplicationUser.IsEditor,
                        DeveloperRole = postToFind.Videogame.ApplicationUser.DeveloperRole,
                        Bio = postToFind.Videogame.ApplicationUser.Bio,
                        Title = postToFind.Videogame.ApplicationUser.Title,
                        IsHidden = postToFind.Videogame.ApplicationUser.IsHidden
                    } : null
                } : null,
                Community = postToFind.Community != null ? new Post_CommunityDto
                {
                    CommunityId = postToFind.Community.CommunityId,
                    Type = postToFind.Community.Type,
                    Name = postToFind.Community.Name,
                    ExtraName = postToFind.Community.ExtraName,
                    Description = postToFind.Community.Description,
                    ExtraDescription = postToFind.Community.ExtraDescription,
                    Picture = postToFind.Community.Picture,
                    Cover = postToFind.Community.Cover,
                    Link = postToFind.Community.Link,
                    IsPrivate = postToFind.Community.IsPrivate,
                    IsHidden = postToFind.Community.IsHidden,
                    MaxMembers = postToFind.Community.MaxMembers,
                    CreatedAt = postToFind.Community.CreatedAt,
                    UpdatedAt = postToFind.Community.UpdatedAt,
                    IsDeleted = postToFind.Community.IsDeleted,
                    DeletedAt = postToFind.Community.DeletedAt,
                    ApplicationUser = postToFind.Community.ApplicationUser != null ? new Post_Community_ApplicationUserDto
                    {
                        ApplicationUserId = postToFind.Community.ApplicationUser.Id,
                        FirstName = postToFind.Community.ApplicationUser.FirstName,
                        LastName = postToFind.Community.ApplicationUser.LastName,
                        BirthDate = postToFind.Community.ApplicationUser.BirthDate,
                        Country = postToFind.Community.ApplicationUser.Country,
                        City = postToFind.Community.ApplicationUser.City,
                        DisplayName = postToFind.Community.ApplicationUser.DisplayName,
                        Avatar = postToFind.Community.ApplicationUser.Avatar,
                        Picture = postToFind.Community.ApplicationUser.Picture,
                        Cover = postToFind.Community.ApplicationUser.Cover,
                        IsGamer = postToFind.Community.ApplicationUser.IsGamer,
                        IsDeveloper = postToFind.Community.ApplicationUser.IsDeveloper,
                        IsEditor = postToFind.Community.ApplicationUser.IsEditor,
                        DeveloperRole = postToFind.Community.ApplicationUser.DeveloperRole,
                        Bio = postToFind.Community.ApplicationUser.Bio,
                        Title = postToFind.Community.ApplicationUser.Title,
                        IsHidden = postToFind.Community.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = postToFind.Community.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = postToFind.Community.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = postToFind.Community.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = postToFind.Community.ApplicationUser.CreatedAt,
                        UpdatedAt = postToFind.Community.ApplicationUser.UpdatedAt,
                        IsDeleted = postToFind.Community.ApplicationUser.IsDeleted,
                        DeletedAt = postToFind.Community.ApplicationUser.DeletedAt,
                    } : null
                } : null
            };

            if (postResponse.PostLikes != null)
            {
                return Ok(new
                {
                    message = "Post trovato con successo",
                    post = postResponse,
                    likes = postResponse.PostLikes.Count > 0 ? postResponse.PostLikes.Count : 0,
                });
            }

            return Ok(new
            {
                message = "Post trovato con successo",
                post = postResponse,
                likes = 0
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromForm] UpdatePostRequestDto updatePostRequestDto)
        {
            var result = await _postService.UpdatePostAsync(id, updatePostRequestDto);

            if (!result)
            {
                return BadRequest(new UpdatePostResponseDto
                {
                    Message = "Errore nella modifica del post"
                });
            }

            return Ok(new UpdatePostResponseDto
            {
                Message = "Post aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del post"
                });
            }
            return Ok(new
            {
                message = "Post cancellato con successo"
            });
        }
    }
}
