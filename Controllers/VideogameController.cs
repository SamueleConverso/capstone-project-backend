using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.Review;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogameController : ControllerBase
    {
        private readonly VideogameService _videogameService;
        private readonly IWebHostEnvironment _env;

        public VideogameController(VideogameService videogameService, IWebHostEnvironment env)
        {
            _videogameService = videogameService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddVideogame([FromForm] CreateVideogameRequestDto createVideogameRequestDto)
        {

            if (createVideogameRequestDto.PictureFile != null && createVideogameRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createVideogameRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createVideogameRequestDto.PictureFile.CopyToAsync(stream);
                }

                createVideogameRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                createVideogameRequestDto.Picture = null;
            }

            if (createVideogameRequestDto.CoverFile != null && createVideogameRequestDto.CoverFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createVideogameRequestDto.CoverFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createVideogameRequestDto.CoverFile.CopyToAsync(stream);
                }

                createVideogameRequestDto.Cover = $"/images/{uniqueName}";
            }
            else
            {
                createVideogameRequestDto.Cover = null;
            }

            DateTime parsedDate;
            DateTime.TryParse(createVideogameRequestDto.ReleaseDate, out parsedDate);

            int.TryParse(createVideogameRequestDto.AgeRating, out var ageRatingInt);
            decimal.TryParse(createVideogameRequestDto.Price, out var priceDecimal);
            bool.TryParse(createVideogameRequestDto.IsHidden, out var isHiddenBool);
            bool.TryParse(createVideogameRequestDto.IsAvailableForPurchase, out var isAvailableForPurchaseBool);

            var newVideogame = new Videogame
            {
                Title = createVideogameRequestDto.Title,
                Subtitle = createVideogameRequestDto.Subtitle,
                Description = createVideogameRequestDto.Description,
                ExtraDescription = createVideogameRequestDto.ExtraDescription,
                Genre = createVideogameRequestDto.Genre,
                Picture = createVideogameRequestDto.Picture,
                Cover = createVideogameRequestDto.Cover,
                Video = createVideogameRequestDto.Video,
                Link = createVideogameRequestDto.Link,
                ReleaseDate = parsedDate,
                Platform = createVideogameRequestDto.Platform,
                AgeRating = ageRatingInt,
                Contributors = createVideogameRequestDto.Contributors,
                Price = priceDecimal,
                IsHidden = isHiddenBool,
                IsAvailableForPurchase = isAvailableForPurchaseBool,
                Country = createVideogameRequestDto.Country,
                City = createVideogameRequestDto.City,
                IsDeleted = false,
                ApplicationUserId = createVideogameRequestDto.ApplicationUserId,
            };

            var result = await _videogameService.AddVideogameAsync(newVideogame);

            if (!result)
            {
                return BadRequest(new CreateVideogameResponseDto
                {
                    Message = "Errore nell'aggiunta del videogame"
                });
            }

            return Ok(new CreateVideogameResponseDto
            {
                Message = "Videogame aggiunto con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideogames()
        {
            var videogamesList = await _videogameService.GetAllVideogamesAsync();

            if (videogamesList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dei videogame",
                    videogames = new List<Videogame>()
                });
            }

            if (!videogamesList.Any())
            {
                return Ok(new
                {
                    message = "Nessun videogame trovato",
                    videogames = new List<Videogame>()
                });
            }
            var videogamesResponse = videogamesList.Select(v => new VideogameDto()
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
                DeletedAt = v.DeletedAt,
                ApplicationUser = v.ApplicationUser != null ? new Videogame_ApplicationUserDto
                {
                    ApplicationUserId = v.ApplicationUser.Id,
                    FirstName = v.ApplicationUser.FirstName,
                    LastName = v.ApplicationUser.LastName,
                    BirthDate = v.ApplicationUser.BirthDate,
                    Country = v.ApplicationUser.Country,
                    City = v.ApplicationUser.City,
                    DisplayName = v.ApplicationUser.DisplayName,
                    Avatar = v.ApplicationUser.Avatar,
                    Picture = v.ApplicationUser.Picture,
                    Cover = v.ApplicationUser.Cover,
                    IsGamer = v.ApplicationUser.IsGamer,
                    IsDeveloper = v.ApplicationUser.IsDeveloper,
                    IsEditor = v.ApplicationUser.IsEditor,
                    DeveloperRole = v.ApplicationUser.DeveloperRole,
                    Bio = v.ApplicationUser.Bio,
                    Title = v.ApplicationUser.Title,
                    IsHidden = v.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = v.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = v.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = v.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = v.ApplicationUser.CreatedAt,
                    UpdatedAt = v.ApplicationUser.UpdatedAt,
                    IsDeleted = v.ApplicationUser.IsDeleted,
                    DeletedAt = v.ApplicationUser.DeletedAt,
                } : null,
                Reviews = v.Reviews.Select(r => new Videogame_ReviewDto
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
                    DeletedAt = r.DeletedAt,
                    ApplicationUser = r.ApplicationUser != null ? new Videogame_Review_ApplicationUserDto
                    {
                        ApplicationUserId = r.ApplicationUser.Id,
                        FirstName = r.ApplicationUser.FirstName,
                        LastName = r.ApplicationUser.LastName,
                        BirthDate = r.ApplicationUser.BirthDate,
                        Country = r.ApplicationUser.Country,
                        City = r.ApplicationUser.City,
                        DisplayName = r.ApplicationUser.DisplayName,
                        Avatar = r.ApplicationUser.Avatar,
                        Picture = r.ApplicationUser.Picture,
                        Cover = r.ApplicationUser.Cover,
                        IsGamer = r.ApplicationUser.IsGamer,
                        IsDeveloper = r.ApplicationUser.IsDeveloper,
                        IsEditor = r.ApplicationUser.IsEditor,
                        DeveloperRole = r.ApplicationUser.DeveloperRole,
                        Bio = r.ApplicationUser.Bio,
                        Title = r.ApplicationUser.Title,
                        IsHidden = r.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = r.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = r.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = r.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = r.ApplicationUser.CreatedAt,
                        UpdatedAt = r.ApplicationUser.UpdatedAt,
                        IsDeleted = r.ApplicationUser.IsDeleted,
                        DeletedAt = r.ApplicationUser.DeletedAt,
                    } : null,
                }).ToList(),
                Posts = v.Posts.Select(p => new Videogame_PostDto
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
                    ApplicationUser = p.ApplicationUser != null ? new Videogame_Post_ApplicationUserDto
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
                    } : null
                }).ToList()
            });

            return Ok(new
            {
                message = $"Numero videogame trovati: {videogamesResponse.Count()}",
                videogames = videogamesResponse
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetVideogameById(Guid id)
        {
            var videogameToFind = await _videogameService.GetVideogameByIdAsync(id);

            if (videogameToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del videogame"
                });
            }

            var videogameResponse = new VideogameDto()
            {
                VideogameId = videogameToFind.VideogameId,
                Title = videogameToFind.Title,
                Subtitle = videogameToFind.Subtitle,
                Description = videogameToFind.Description,
                ExtraDescription = videogameToFind.ExtraDescription,
                Genre = videogameToFind.Genre,
                Picture = videogameToFind.Picture,
                Cover = videogameToFind.Cover,
                Video = videogameToFind.Video,
                Link = videogameToFind.Link,
                ReleaseDate = videogameToFind.ReleaseDate,
                Platform = videogameToFind.Platform,
                AgeRating = videogameToFind.AgeRating,
                Contributors = videogameToFind.Contributors,
                Price = videogameToFind.Price,
                IsHidden = videogameToFind.IsHidden,
                IsAvailableForPurchase = videogameToFind.IsAvailableForPurchase,
                Country = videogameToFind.Country,
                City = videogameToFind.City,
                CreatedAt = videogameToFind.CreatedAt,
                UpdatedAt = videogameToFind.UpdatedAt,
                IsDeleted = videogameToFind.IsDeleted,
                DeletedAt = videogameToFind.DeletedAt,
                ApplicationUser = videogameToFind.ApplicationUser != null ? new Videogame_ApplicationUserDto
                {
                    ApplicationUserId = videogameToFind.ApplicationUser.Id,
                    FirstName = videogameToFind.ApplicationUser.FirstName,
                    LastName = videogameToFind.ApplicationUser.LastName,
                    BirthDate = videogameToFind.ApplicationUser.BirthDate,
                    Country = videogameToFind.ApplicationUser.Country,
                    City = videogameToFind.ApplicationUser.City,
                    DisplayName = videogameToFind.ApplicationUser.DisplayName,
                    Avatar = videogameToFind.ApplicationUser.Avatar,
                    Picture = videogameToFind.ApplicationUser.Picture,
                    Cover = videogameToFind.ApplicationUser.Cover,
                    IsGamer = videogameToFind.ApplicationUser.IsGamer,
                    IsDeveloper = videogameToFind.ApplicationUser.IsDeveloper,
                    IsEditor = videogameToFind.ApplicationUser.IsEditor,
                    DeveloperRole = videogameToFind.ApplicationUser.DeveloperRole,
                    Bio = videogameToFind.ApplicationUser.Bio,
                    Title = videogameToFind.ApplicationUser.Title,
                    IsHidden = videogameToFind.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = videogameToFind.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = videogameToFind.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = videogameToFind.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = videogameToFind.ApplicationUser.CreatedAt,
                    UpdatedAt = videogameToFind.ApplicationUser.UpdatedAt,
                    IsDeleted = videogameToFind.ApplicationUser.IsDeleted,
                    DeletedAt = videogameToFind.ApplicationUser.DeletedAt,
                } : null,
                Reviews = videogameToFind.Reviews.Select(r => new Videogame_ReviewDto
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
                    DeletedAt = r.DeletedAt,
                    ApplicationUser = r.ApplicationUser != null ? new Videogame_Review_ApplicationUserDto
                    {
                        ApplicationUserId = r.ApplicationUser.Id,
                        FirstName = r.ApplicationUser.FirstName,
                        LastName = r.ApplicationUser.LastName,
                        BirthDate = r.ApplicationUser.BirthDate,
                        Country = r.ApplicationUser.Country,
                        City = r.ApplicationUser.City,
                        DisplayName = r.ApplicationUser.DisplayName,
                        Avatar = r.ApplicationUser.Avatar,
                        Picture = r.ApplicationUser.Picture,
                        Cover = r.ApplicationUser.Cover,
                        IsGamer = r.ApplicationUser.IsGamer,
                        IsDeveloper = r.ApplicationUser.IsDeveloper,
                        IsEditor = r.ApplicationUser.IsEditor,
                        DeveloperRole = r.ApplicationUser.DeveloperRole,
                        Bio = r.ApplicationUser.Bio,
                        Title = r.ApplicationUser.Title,
                        IsHidden = r.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = r.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = r.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = r.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = r.ApplicationUser.CreatedAt,
                        UpdatedAt = r.ApplicationUser.UpdatedAt,
                        IsDeleted = r.ApplicationUser.IsDeleted,
                        DeletedAt = r.ApplicationUser.DeletedAt,
                    } : null,
                }).ToList(),
                Posts = videogameToFind.Posts.Select(p => new Videogame_PostDto
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
                    ApplicationUser = p.ApplicationUser != null ? new Videogame_Post_ApplicationUserDto
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
                    } : null
                }).ToList()
            };

            return Ok(new
            {
                message = "Videogame trovato con successo",
                videogame = videogameResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateVideogame(Guid id, [FromForm] UpdateVideogameRequestDto updateVideogameRequestDto)
        {
            var result = await _videogameService.UpdateVideogameAsync(id, updateVideogameRequestDto);

            if (!result)
            {
                return BadRequest(new UpdateVideogameResponseDto
                {
                    Message = "Errore nella modifica del videogame"
                });
            }

            return Ok(new UpdateVideogameResponseDto
            {
                Message = "Videogame aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteVideogame(Guid id)
        {
            var result = await _videogameService.DeleteVideogameAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del videogame"
                });
            }
            return Ok(new
            {
                message = "Videogame cancellato con successo"
            });
        }
    }
}
