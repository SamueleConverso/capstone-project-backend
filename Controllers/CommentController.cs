using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.CommentLike;
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
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly IWebHostEnvironment _env;

        public CommentController(CommentService commentService, IWebHostEnvironment env)
        {
            _commentService = commentService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] CreateCommentRequestDto createCommentRequestDto)
        {

            if (createCommentRequestDto.PictureFile != null && createCommentRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createCommentRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createCommentRequestDto.PictureFile.CopyToAsync(stream);
                }

                createCommentRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                createCommentRequestDto.Picture = null;
            }

            Guid.TryParse(createCommentRequestDto.PostId, out var postIdGuid);

            var newComment = new Comment
            {
                Text = createCommentRequestDto.Text,
                Picture = createCommentRequestDto.Picture,
                IsDeleted = false,
                PostId = postIdGuid,
                ApplicationUserId = createCommentRequestDto.ApplicationUserId,
            };

            var result = await _commentService.AddCommentAsync(newComment);

            if (!result)
            {
                return BadRequest(new CreateCommentResponseDto
                {
                    Message = "Errore nell'aggiunta del comment"
                });
            }

            return Ok(new CreateCommentResponseDto
            {
                Message = "Comment aggiunto con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commentsList = await _commentService.GetAllCommentsAsync();

            if (commentsList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dei comment",
                    comments = new List<Comment>()
                });
            }

            if (!commentsList.Any())
            {
                return Ok(new
                {
                    message = "Nessun comment trovato",
                    comments = new List<Comment>()
                });
            }

            var commentsResponse = commentsList.Select(c => new CommentDto()
            {
                CommentId = c.CommentId,
                Text = c.Text,
                Picture = c.Picture,
                Likes = c.Likes,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsDeleted = c.IsDeleted,
                DeletedAt = c.DeletedAt,
                CommentLikes = c.CommentLikes.Select(cl => new Comment_CommentLikeDto
                {
                    CommentLikeId = cl.CommentLikeId,
                    CreatedAt = cl.CreatedAt,
                    UpdatedAt = cl.UpdatedAt,
                    IsDeleted = cl.IsDeleted,
                    DeletedAt = cl.DeletedAt,
                    ApplicationUser = cl.ApplicationUser != null ? new Comment_CommentLike_ApplicationUserDto
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
                Post = new Comment_PostDto
                {
                    PostId = c.Post.PostId,
                    Text = c.Post.Text,
                    Picture = c.Post.Picture,
                    Video = c.Post.Video,
                    Mood = c.Post.Mood,
                    IsLookingForGamers = c.Post.IsLookingForGamers,
                    IsLookingForDevelopers = c.Post.IsLookingForDevelopers,
                    IsLookingForEditors = c.Post.IsLookingForEditors,
                    Country = c.Post.Country,
                    City = c.Post.City,
                    IsInUserFeed = c.Post.IsInUserFeed,
                    IsInGameFeed = c.Post.IsInGameFeed,
                    IsInCommunityFeed = c.Post.IsInCommunityFeed,
                    IsHidden = c.Post.IsHidden,
                    Likes = c.Post.Likes,
                    CreatedAt = c.Post.CreatedAt,
                    UpdatedAt = c.Post.UpdatedAt,
                    IsDeleted = c.Post.IsDeleted,
                    DeletedAt = c.Post.DeletedAt,
                    PostLikes = c.Post.PostLikes.Select(pl => new Comment_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser != null ? new Comment_PostLike_ApplicationUserDto
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
                    PostTags = c.Post.PostTags.Select(pt => new Comment_Post_PostTagDto
                    {
                        PostTagId = pt.PostTagId,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        IsDeleted = pt.IsDeleted,
                        DeletedAt = pt.DeletedAt,
                        Tag = new Comment_Post_TagDto
                        {
                            TagId = pt.Tag.TagId,
                            Entry = pt.Tag.Entry,
                            CreatedAt = pt.Tag.CreatedAt,
                            UpdatedAt = pt.Tag.UpdatedAt,
                            IsDeleted = pt.Tag.IsDeleted,
                            DeletedAt = pt.Tag.DeletedAt
                        }
                    }).ToList(),
                    ApplicationUser = c.Post.ApplicationUser != null ? new Comment_Post_ApplicationUserDto
                    {
                        ApplicationUserId = c.Post.ApplicationUser.Id,
                        FirstName = c.Post.ApplicationUser.FirstName,
                        LastName = c.Post.ApplicationUser.LastName,
                        BirthDate = c.Post.ApplicationUser.BirthDate,
                        Country = c.Post.ApplicationUser.Country,
                        City = c.Post.ApplicationUser.City,
                        DisplayName = c.Post.ApplicationUser.DisplayName,
                        Avatar = c.Post.ApplicationUser.Avatar,
                        Picture = c.Post.ApplicationUser.Picture,
                        Cover = c.Post.ApplicationUser.Cover,
                        IsGamer = c.Post.ApplicationUser.IsGamer,
                        IsDeveloper = c.Post.ApplicationUser.IsDeveloper,
                        IsEditor = c.Post.ApplicationUser.IsEditor,
                        DeveloperRole = c.Post.ApplicationUser.DeveloperRole,
                        Bio = c.Post.ApplicationUser.Bio,
                        Title = c.Post.ApplicationUser.Title,
                        IsHidden = c.Post.ApplicationUser.IsHidden
                    } : null,
                    Videogame = c.Post.Videogame != null ? new Comment_Post_VideogameDto
                    {
                        VideogameId = c.Post.Videogame.VideogameId,
                        Title = c.Post.Videogame.Title,
                        Subtitle = c.Post.Videogame.Subtitle,
                        Description = c.Post.Videogame.Description,
                        ExtraDescription = c.Post.Videogame.ExtraDescription,
                        Genre = c.Post.Videogame.Genre,
                        Picture = c.Post.Videogame.Picture,
                        Cover = c.Post.Videogame.Cover,
                        Video = c.Post.Videogame.Video,
                        Link = c.Post.Videogame.Link,
                        ReleaseDate = c.Post.Videogame.ReleaseDate,
                        Platform = c.Post.Videogame.Platform,
                        AgeRating = c.Post.Videogame.AgeRating,
                        Contributors = c.Post.Videogame.Contributors,
                        Price = c.Post.Videogame.Price,
                        IsHidden = c.Post.Videogame.IsHidden,
                        IsAvailableForPurchase = c.Post.Videogame.IsAvailableForPurchase,
                        Country = c.Post.Videogame.Country,
                        City = c.Post.Videogame.City,
                        CreatedAt = c.Post.Videogame.CreatedAt,
                        UpdatedAt = c.Post.Videogame.UpdatedAt,
                        IsDeleted = c.Post.Videogame.IsDeleted,
                        DeletedAt = c.Post.Videogame.DeletedAt,
                        ApplicationUser = c.Post.Videogame.ApplicationUser != null ? new Comment_Post_Videogame_ApplicationUserDto
                        {
                            ApplicationUserId = c.Post.Videogame.ApplicationUser.Id,
                            FirstName = c.Post.Videogame.ApplicationUser.FirstName,
                            LastName = c.Post.Videogame.ApplicationUser.LastName,
                            BirthDate = c.Post.Videogame.ApplicationUser.BirthDate,
                            Country = c.Post.Videogame.ApplicationUser.Country,
                            City = c.Post.Videogame.ApplicationUser.City,
                            DisplayName = c.Post.Videogame.ApplicationUser.DisplayName,
                            Avatar = c.Post.Videogame.ApplicationUser.Avatar,
                            Picture = c.Post.Videogame.ApplicationUser.Picture,
                            Cover = c.Post.Videogame.ApplicationUser.Cover,
                            IsGamer = c.Post.Videogame.ApplicationUser.IsGamer,
                            IsDeveloper = c.Post.Videogame.ApplicationUser.IsDeveloper,
                            IsEditor = c.Post.Videogame.ApplicationUser.IsEditor,
                            DeveloperRole = c.Post.Videogame.ApplicationUser.DeveloperRole,
                            Bio = c.Post.Videogame.ApplicationUser.Bio,
                            Title = c.Post.Videogame.ApplicationUser.Title,
                            IsHidden = c.Post.Videogame.ApplicationUser.IsHidden
                        } : null
                    } : null,
                },
                ApplicationUser = c.ApplicationUser != null ? new Comment_ApplicationUserDto
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
            });

            return Ok(new
            {
                message = $"Numero comment trovati: {commentsResponse.Count()}",
                comments = commentsResponse
            });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var commentToFind = await _commentService.GetCommentByIdAsync(id);

            if (commentToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del comment"
                });
            }

            var commentResponse = new CommentDto()
            {
                CommentId = commentToFind.CommentId,
                Text = commentToFind.Text,
                Picture = commentToFind.Picture,
                Likes = commentToFind.Likes,
                CreatedAt = commentToFind.CreatedAt,
                UpdatedAt = commentToFind.UpdatedAt,
                IsDeleted = commentToFind.IsDeleted,
                DeletedAt = commentToFind.DeletedAt,
                CommentLikes = commentToFind.CommentLikes.Select(cl => new Comment_CommentLikeDto
                {
                    CommentLikeId = cl.CommentLikeId,
                    CreatedAt = cl.CreatedAt,
                    UpdatedAt = cl.UpdatedAt,
                    IsDeleted = cl.IsDeleted,
                    DeletedAt = cl.DeletedAt,
                    ApplicationUser = cl.ApplicationUser != null ? new Comment_CommentLike_ApplicationUserDto
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
                Post = new Comment_PostDto
                {
                    PostId = commentToFind.Post.PostId,
                    Text = commentToFind.Post.Text,
                    Picture = commentToFind.Post.Picture,
                    Video = commentToFind.Post.Video,
                    Mood = commentToFind.Post.Mood,
                    IsLookingForGamers = commentToFind.Post.IsLookingForGamers,
                    IsLookingForDevelopers = commentToFind.Post.IsLookingForDevelopers,
                    IsLookingForEditors = commentToFind.Post.IsLookingForEditors,
                    Country = commentToFind.Post.Country,
                    City = commentToFind.Post.City,
                    IsInUserFeed = commentToFind.Post.IsInUserFeed,
                    IsInGameFeed = commentToFind.Post.IsInGameFeed,
                    IsInCommunityFeed = commentToFind.Post.IsInCommunityFeed,
                    IsHidden = commentToFind.Post.IsHidden,
                    Likes = commentToFind.Post.Likes,
                    CreatedAt = commentToFind.Post.CreatedAt,
                    UpdatedAt = commentToFind.Post.UpdatedAt,
                    IsDeleted = commentToFind.Post.IsDeleted,
                    DeletedAt = commentToFind.Post.DeletedAt,
                    PostLikes = commentToFind.Post.PostLikes.Select(pl => new Comment_Post_PostLikeDto
                    {
                        PostLikeId = pl.PostLikeId,
                        CreatedAt = pl.CreatedAt,
                        UpdatedAt = pl.UpdatedAt,
                        IsDeleted = pl.IsDeleted,
                        DeletedAt = pl.DeletedAt,
                        ApplicationUser = pl.ApplicationUser != null ? new Comment_PostLike_ApplicationUserDto
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
                    PostTags = commentToFind.Post.PostTags.Select(pt => new Comment_Post_PostTagDto
                    {
                        PostTagId = pt.PostTagId,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        IsDeleted = pt.IsDeleted,
                        DeletedAt = pt.DeletedAt,
                        Tag = new Comment_Post_TagDto
                        {
                            TagId = pt.Tag.TagId,
                            Entry = pt.Tag.Entry,
                            CreatedAt = pt.Tag.CreatedAt,
                            UpdatedAt = pt.Tag.UpdatedAt,
                            IsDeleted = pt.Tag.IsDeleted,
                            DeletedAt = pt.Tag.DeletedAt
                        }
                    }).ToList(),
                    ApplicationUser = commentToFind.Post.ApplicationUser != null ? new Comment_Post_ApplicationUserDto
                    {
                        ApplicationUserId = commentToFind.Post.ApplicationUser.Id,
                        FirstName = commentToFind.Post.ApplicationUser.FirstName,
                        LastName = commentToFind.Post.ApplicationUser.LastName,
                        BirthDate = commentToFind.Post.ApplicationUser.BirthDate,
                        Country = commentToFind.Post.ApplicationUser.Country,
                        City = commentToFind.Post.ApplicationUser.City,
                        DisplayName = commentToFind.Post.ApplicationUser.DisplayName,
                        Avatar = commentToFind.Post.ApplicationUser.Avatar,
                        Picture = commentToFind.Post.ApplicationUser.Picture,
                        Cover = commentToFind.Post.ApplicationUser.Cover,
                        IsGamer = commentToFind.Post.ApplicationUser.IsGamer,
                        IsDeveloper = commentToFind.Post.ApplicationUser.IsDeveloper,
                        IsEditor = commentToFind.Post.ApplicationUser.IsEditor,
                        DeveloperRole = commentToFind.Post.ApplicationUser.DeveloperRole,
                        Bio = commentToFind.Post.ApplicationUser.Bio,
                        Title = commentToFind.Post.ApplicationUser.Title,
                        IsHidden = commentToFind.Post.ApplicationUser.IsHidden
                    } : null,
                    Videogame = commentToFind.Post.Videogame != null ? new Comment_Post_VideogameDto
                    {
                        VideogameId = commentToFind.Post.Videogame.VideogameId,
                        Title = commentToFind.Post.Videogame.Title,
                        Subtitle = commentToFind.Post.Videogame.Subtitle,
                        Description = commentToFind.Post.Videogame.Description,
                        ExtraDescription = commentToFind.Post.Videogame.ExtraDescription,
                        Genre = commentToFind.Post.Videogame.Genre,
                        Picture = commentToFind.Post.Videogame.Picture,
                        Cover = commentToFind.Post.Videogame.Cover,
                        Video = commentToFind.Post.Videogame.Video,
                        Link = commentToFind.Post.Videogame.Link,
                        ReleaseDate = commentToFind.Post.Videogame.ReleaseDate,
                        Platform = commentToFind.Post.Videogame.Platform,
                        AgeRating = commentToFind.Post.Videogame.AgeRating,
                        Contributors = commentToFind.Post.Videogame.Contributors,
                        Price = commentToFind.Post.Videogame.Price,
                        IsHidden = commentToFind.Post.Videogame.IsHidden,
                        IsAvailableForPurchase = commentToFind.Post.Videogame.IsAvailableForPurchase,
                        Country = commentToFind.Post.Videogame.Country,
                        City = commentToFind.Post.Videogame.City,
                        CreatedAt = commentToFind.Post.Videogame.CreatedAt,
                        UpdatedAt = commentToFind.Post.Videogame.UpdatedAt,
                        IsDeleted = commentToFind.Post.Videogame.IsDeleted,
                        DeletedAt = commentToFind.Post.Videogame.DeletedAt,
                        ApplicationUser = commentToFind.Post.Videogame.ApplicationUser != null ? new Comment_Post_Videogame_ApplicationUserDto
                        {
                            ApplicationUserId = commentToFind.Post.Videogame.ApplicationUser.Id,
                            FirstName = commentToFind.Post.Videogame.ApplicationUser.FirstName,
                            LastName = commentToFind.Post.Videogame.ApplicationUser.LastName,
                            BirthDate = commentToFind.Post.Videogame.ApplicationUser.BirthDate,
                            Country = commentToFind.Post.Videogame.ApplicationUser.Country,
                            City = commentToFind.Post.Videogame.ApplicationUser.City,
                            DisplayName = commentToFind.Post.Videogame.ApplicationUser.DisplayName,
                            Avatar = commentToFind.Post.Videogame.ApplicationUser.Avatar,
                            Picture = commentToFind.Post.Videogame.ApplicationUser.Picture,
                            Cover = commentToFind.Post.Videogame.ApplicationUser.Cover,
                            IsGamer = commentToFind.Post.Videogame.ApplicationUser.IsGamer,
                            IsDeveloper = commentToFind.Post.Videogame.ApplicationUser.IsDeveloper,
                            IsEditor = commentToFind.Post.Videogame.ApplicationUser.IsEditor,
                            DeveloperRole = commentToFind.Post.Videogame.ApplicationUser.DeveloperRole,
                            Bio = commentToFind.Post.Videogame.ApplicationUser.Bio,
                            Title = commentToFind.Post.Videogame.ApplicationUser.Title,
                            IsHidden = commentToFind.Post.Videogame.ApplicationUser.IsHidden
                        } : null
                    } : null,
                },
                ApplicationUser = commentToFind.ApplicationUser != null ? new Comment_ApplicationUserDto
                {
                    ApplicationUserId = commentToFind.ApplicationUser.Id,
                    FirstName = commentToFind.ApplicationUser.FirstName,
                    LastName = commentToFind.ApplicationUser.LastName,
                    BirthDate = commentToFind.ApplicationUser.BirthDate,
                    Country = commentToFind.ApplicationUser.Country,
                    City = commentToFind.ApplicationUser.City,
                    DisplayName = commentToFind.ApplicationUser.DisplayName,
                    Avatar = commentToFind.ApplicationUser.Avatar,
                    Picture = commentToFind.ApplicationUser.Picture,
                    Cover = commentToFind.ApplicationUser.Cover,
                    IsGamer = commentToFind.ApplicationUser.IsGamer,
                    IsDeveloper = commentToFind.ApplicationUser.IsDeveloper,
                    IsEditor = commentToFind.ApplicationUser.IsEditor,
                    DeveloperRole = commentToFind.ApplicationUser.DeveloperRole,
                    Bio = commentToFind.ApplicationUser.Bio,
                    Title = commentToFind.ApplicationUser.Title,
                    IsHidden = commentToFind.ApplicationUser.IsHidden
                } : null
            };

            if (commentResponse.CommentLikes != null)
            {
                return Ok(new
                {
                    message = "Comment trovato con successo",
                    comment = commentResponse,
                    likes = commentResponse.CommentLikes.Count > 0 ? commentResponse.CommentLikes.Count : 0,
                });
            }

            return Ok(new
            {
                message = "Comment trovato con successo",
                comment = commentResponse,
                likes = 0
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromForm] UpdateCommentRequestDto updateCommentRequestDto)
        {
            var result = await _commentService.UpdateCommentAsync(id, updateCommentRequestDto);

            if (!result)
            {
                return BadRequest(new UpdateCommentResponseDto
                {
                    Message = "Errore nella modifica del comment"
                });
            }

            return Ok(new UpdateCommentResponseDto
            {
                Message = "Comment aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del comment"
                });
            }
            return Ok(new
            {
                message = "Comment cancellato con successo"
            });
        }
    }
}
