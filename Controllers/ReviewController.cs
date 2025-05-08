using Capstone_Project.DTOs.Feed.ApplicationUser;
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
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly IWebHostEnvironment _env;

        public ReviewController(ReviewService reviewService, IWebHostEnvironment env)
        {
            _reviewService = reviewService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromForm] CreateReviewRequestDto createReviewRequestDto)
        {

            if (createReviewRequestDto.PictureFile != null && createReviewRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(createReviewRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createReviewRequestDto.PictureFile.CopyToAsync(stream);
                }

                createReviewRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                createReviewRequestDto.Picture = null;
            }

            int.TryParse(createReviewRequestDto.Rating, out var ratingInt);
            bool.TryParse(createReviewRequestDto.Recommend, out var recommendBool);
            //Guid.TryParse(createReviewRequestDto.VideogameId, out var videogameIdGuid);

            Guid? videogameIdGuid = null;
            if (createReviewRequestDto.VideogameId != null && createReviewRequestDto.VideogameId != "")
            {
                Guid.TryParse(createReviewRequestDto.VideogameId, out var videogameIdGuidToParse);
                videogameIdGuid = videogameIdGuidToParse;
            }

            var newReview = new Review
            {
                Title = createReviewRequestDto.Title,
                Text = createReviewRequestDto.Text,
                Rating = ratingInt,
                Recommend = recommendBool,
                Picture = createReviewRequestDto.Picture,
                Video = createReviewRequestDto.Video,
                Link = createReviewRequestDto.Link,
                IsDeleted = false,
                VideogameId = videogameIdGuid,
                ApplicationUserId = createReviewRequestDto.ApplicationUserId
            };

            var result = await _reviewService.AddReviewAsync(newReview);

            if (!result)
            {
                return BadRequest(new CreateReviewResponseDto
                {
                    Message = "Errore nell'aggiunta della review"
                });
            }

            return Ok(new CreateReviewResponseDto
            {
                Message = "Review aggiunta con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviewsList = await _reviewService.GetAllReviewsAsync();

            if (reviewsList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero delle reviews",
                    reviews = new List<Review>()
                });
            }

            if (!reviewsList.Any())
            {
                return Ok(new
                {
                    message = "Nessuna review trovata",
                    reviews = new List<Review>()
                });
            }
            var reviewsResponse = reviewsList.Select(r => new ReviewDto()
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
                Videogame = r.Videogame != null ? new Review_VideogameDto
                {
                    VideogameId = r.Videogame.VideogameId,
                    Title = r.Videogame.Title,
                    Subtitle = r.Videogame.Subtitle,
                    Description = r.Videogame.Description,
                    ExtraDescription = r.Videogame.ExtraDescription,
                    Genre = r.Videogame.Genre,
                    Picture = r.Videogame.Picture,
                    Cover = r.Videogame.Cover,
                    Video = r.Videogame.Video,
                    Link = r.Videogame.Link,
                    ReleaseDate = r.Videogame.ReleaseDate,
                    Platform = r.Videogame.Platform,
                    AgeRating = r.Videogame.AgeRating,
                    Contributors = r.Videogame.Contributors,
                    Price = r.Videogame.Price,
                    IsHidden = r.Videogame.IsHidden,
                    IsAvailableForPurchase = r.Videogame.IsAvailableForPurchase,
                    Country = r.Videogame.Country,
                    City = r.Videogame.City,
                    CreatedAt = r.Videogame.CreatedAt,
                    UpdatedAt = r.Videogame.UpdatedAt,
                    IsDeleted = r.Videogame.IsDeleted,
                    DeletedAt = r.Videogame.DeletedAt,
                    ApplicationUser = r.Videogame.ApplicationUser != null ? new Review_Videogame_ApplicationUserDto
                    {
                        ApplicationUserId = r.Videogame.ApplicationUser.Id,
                        FirstName = r.Videogame.ApplicationUser.FirstName,
                        LastName = r.Videogame.ApplicationUser.LastName,
                        BirthDate = r.Videogame.ApplicationUser.BirthDate,
                        Country = r.Videogame.ApplicationUser.Country,
                        City = r.Videogame.ApplicationUser.City,
                        DisplayName = r.Videogame.ApplicationUser.DisplayName,
                        Avatar = r.Videogame.ApplicationUser.Avatar,
                        Picture = r.Videogame.ApplicationUser.Picture,
                        Cover = r.Videogame.ApplicationUser.Cover,
                        IsGamer = r.Videogame.ApplicationUser.IsGamer,
                        IsDeveloper = r.Videogame.ApplicationUser.IsDeveloper,
                        IsEditor = r.Videogame.ApplicationUser.IsEditor,
                        DeveloperRole = r.Videogame.ApplicationUser.DeveloperRole,
                        Bio = r.Videogame.ApplicationUser.Bio,
                        Title = r.Videogame.ApplicationUser.Title,
                        IsHidden = r.Videogame.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = r.Videogame.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = r.Videogame.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = r.Videogame.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = r.Videogame.ApplicationUser.CreatedAt,
                        UpdatedAt = r.Videogame.ApplicationUser.UpdatedAt,
                        IsDeleted = r.Videogame.ApplicationUser.IsDeleted,
                        DeletedAt = r.Videogame.ApplicationUser.DeletedAt,
                    } : null,
                } : null,
                ApplicationUser = r.ApplicationUser != null ? new Review_ApplicationUserDto
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
                    DeletedAt = r.ApplicationUser.DeletedAt
                } : null
            });

            return Ok(new
            {
                message = $"Numero review trovate: {reviewsResponse.Count()}",
                reviews = reviewsResponse
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var reviewToFind = await _reviewService.GetReviewByIdAsync(id);

            if (reviewToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero della review"
                });
            }

            var reviewResponse = new ReviewDto()
            {
                ReviewId = reviewToFind.ReviewId,
                Title = reviewToFind.Title,
                Text = reviewToFind.Text,
                Rating = reviewToFind.Rating,
                Recommend = reviewToFind.Recommend,
                Picture = reviewToFind.Picture,
                Video = reviewToFind.Video,
                Link = reviewToFind.Link,
                CreatedAt = reviewToFind.CreatedAt,
                UpdatedAt = reviewToFind.UpdatedAt,
                IsDeleted = reviewToFind.IsDeleted,
                DeletedAt = reviewToFind.DeletedAt,
                Videogame = reviewToFind.Videogame != null ? new Review_VideogameDto
                {
                    VideogameId = reviewToFind.Videogame.VideogameId,
                    Title = reviewToFind.Videogame.Title,
                    Subtitle = reviewToFind.Videogame.Subtitle,
                    Description = reviewToFind.Videogame.Description,
                    ExtraDescription = reviewToFind.Videogame.ExtraDescription,
                    Genre = reviewToFind.Videogame.Genre,
                    Picture = reviewToFind.Videogame.Picture,
                    Cover = reviewToFind.Videogame.Cover,
                    Video = reviewToFind.Videogame.Video,
                    Link = reviewToFind.Videogame.Link,
                    ReleaseDate = reviewToFind.Videogame.ReleaseDate,
                    Platform = reviewToFind.Videogame.Platform,
                    AgeRating = reviewToFind.Videogame.AgeRating,
                    Contributors = reviewToFind.Videogame.Contributors,
                    Price = reviewToFind.Videogame.Price,
                    IsHidden = reviewToFind.Videogame.IsHidden,
                    IsAvailableForPurchase = reviewToFind.Videogame.IsAvailableForPurchase,
                    Country = reviewToFind.Videogame.Country,
                    City = reviewToFind.Videogame.City,
                    CreatedAt = reviewToFind.Videogame.CreatedAt,
                    UpdatedAt = reviewToFind.Videogame.UpdatedAt,
                    IsDeleted = reviewToFind.Videogame.IsDeleted,
                    DeletedAt = reviewToFind.Videogame.DeletedAt,
                    ApplicationUser = reviewToFind.Videogame.ApplicationUser != null ? new Review_Videogame_ApplicationUserDto
                    {
                        ApplicationUserId = reviewToFind.Videogame.ApplicationUser.Id,
                        FirstName = reviewToFind.Videogame.ApplicationUser.FirstName,
                        LastName = reviewToFind.Videogame.ApplicationUser.LastName,
                        BirthDate = reviewToFind.Videogame.ApplicationUser.BirthDate,
                        Country = reviewToFind.Videogame.ApplicationUser.Country,
                        City = reviewToFind.Videogame.ApplicationUser.City,
                        DisplayName = reviewToFind.Videogame.ApplicationUser.DisplayName,
                        Avatar = reviewToFind.Videogame.ApplicationUser.Avatar,
                        Picture = reviewToFind.Videogame.ApplicationUser.Picture,
                        Cover = reviewToFind.Videogame.ApplicationUser.Cover,
                        IsGamer = reviewToFind.Videogame.ApplicationUser.IsGamer,
                        IsDeveloper = reviewToFind.Videogame.ApplicationUser.IsDeveloper,
                        IsEditor = reviewToFind.Videogame.ApplicationUser.IsEditor,
                        DeveloperRole = reviewToFind.Videogame.ApplicationUser.DeveloperRole,
                        Bio = reviewToFind.Videogame.ApplicationUser.Bio,
                        Title = reviewToFind.Videogame.ApplicationUser.Title,
                        IsHidden = reviewToFind.Videogame.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = reviewToFind.Videogame.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = reviewToFind.Videogame.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = reviewToFind.Videogame.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = reviewToFind.Videogame.ApplicationUser.CreatedAt,
                        UpdatedAt = reviewToFind.Videogame.ApplicationUser.UpdatedAt,
                        IsDeleted = reviewToFind.Videogame.ApplicationUser.IsDeleted,
                        DeletedAt = reviewToFind.Videogame.ApplicationUser.DeletedAt,
                    } : null,
                } : null,
                ApplicationUser = reviewToFind.ApplicationUser != null ? new Review_ApplicationUserDto
                {
                    ApplicationUserId = reviewToFind.ApplicationUser.Id,
                    FirstName = reviewToFind.ApplicationUser.FirstName,
                    LastName = reviewToFind.ApplicationUser.LastName,
                    BirthDate = reviewToFind.ApplicationUser.BirthDate,
                    Country = reviewToFind.ApplicationUser.Country,
                    City = reviewToFind.ApplicationUser.City,
                    DisplayName = reviewToFind.ApplicationUser.DisplayName,
                    Avatar = reviewToFind.ApplicationUser.Avatar,
                    Picture = reviewToFind.ApplicationUser.Picture,
                    Cover = reviewToFind.ApplicationUser.Cover,
                    IsGamer = reviewToFind.ApplicationUser.IsGamer,
                    IsDeveloper = reviewToFind.ApplicationUser.IsDeveloper,
                    IsEditor = reviewToFind.ApplicationUser.IsEditor,
                    DeveloperRole = reviewToFind.ApplicationUser.DeveloperRole,
                    Bio = reviewToFind.ApplicationUser.Bio,
                    Title = reviewToFind.ApplicationUser.Title,
                    IsHidden = reviewToFind.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = reviewToFind.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = reviewToFind.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = reviewToFind.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = reviewToFind.ApplicationUser.CreatedAt,
                    UpdatedAt = reviewToFind.ApplicationUser.UpdatedAt,
                    IsDeleted = reviewToFind.ApplicationUser.IsDeleted,
                    DeletedAt = reviewToFind.ApplicationUser.DeletedAt
                } : null
            };

            return Ok(new
            {
                message = "Review trovata con successo",
                review = reviewResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromForm] UpdateReviewRequestDto updateReviewRequestDto)
        {
            var result = await _reviewService.UpdateReviewAsync(id, updateReviewRequestDto);

            if (!result)
            {
                return BadRequest(new UpdateReviewResponseDto
                {
                    Message = "Errore nella modifica della review"
                });
            }

            return Ok(new UpdateReviewResponseDto
            {
                Message = "Review aggiornata con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione della review"
                });
            }
            return Ok(new
            {
                message = "Review cancellata con successo"
            });
        }
    }
}
