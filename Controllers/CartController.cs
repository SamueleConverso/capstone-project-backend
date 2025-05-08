using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.Cart;
using Capstone_Project.DTOs.Feed.CartVideogame;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var cartToFind = await _cartService.GetCartByIdAsync(id);

            if (cartToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del cart"
                });
            }

            var cartResponse = new CartDto()
            {
                CartId = cartToFind.CartId,
                IsCheckedOut = cartToFind.IsCheckedOut,
                CreatedAt = cartToFind.CreatedAt,
                UpdatedAt = cartToFind.UpdatedAt,
                IsDeleted = cartToFind.IsDeleted,
                DeletedAt = cartToFind.DeletedAt,
                ApplicationUser = new Cart_ApplicationUserDto()
                {
                    ApplicationUserId = cartToFind.ApplicationUser.Id,
                    FirstName = cartToFind.ApplicationUser.FirstName,
                    LastName = cartToFind.ApplicationUser.LastName,
                    BirthDate = cartToFind.ApplicationUser.BirthDate,
                    Country = cartToFind.ApplicationUser.Country,
                    City = cartToFind.ApplicationUser.City,
                    DisplayName = cartToFind.ApplicationUser.DisplayName,
                    Avatar = cartToFind.ApplicationUser.Avatar,
                    Picture = cartToFind.ApplicationUser.Picture,
                    Cover = cartToFind.ApplicationUser.Cover,
                    IsGamer = cartToFind.ApplicationUser.IsGamer,
                    IsDeveloper = cartToFind.ApplicationUser.IsDeveloper,
                    IsEditor = cartToFind.ApplicationUser.IsEditor,
                    DeveloperRole = cartToFind.ApplicationUser.DeveloperRole,
                    Bio = cartToFind.ApplicationUser.Bio,
                    Title = cartToFind.ApplicationUser.Title,
                    IsHidden = cartToFind.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = cartToFind.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = cartToFind.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = cartToFind.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = cartToFind.ApplicationUser.CreatedAt,
                    UpdatedAt = cartToFind.ApplicationUser.UpdatedAt,
                    IsDeleted = cartToFind.ApplicationUser.IsDeleted,
                    DeletedAt = cartToFind.ApplicationUser.DeletedAt,
                },
                CartVideogames = cartToFind.CartVideogames.Select(cv => new Cart_CartVideogameDto()
                {
                    CartVideogameId = cv.CartVideogameId,
                    Quantity = cv.Quantity,
                    CreatedAt = cv.CreatedAt,
                    UpdatedAt = cv.UpdatedAt,
                    IsDeleted = cv.IsDeleted,
                    DeletedAt = cv.DeletedAt,
                    Videogame = cv.Videogame != null ? new Cart_CartVideogame_VideogameDto()
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
                        DeletedAt = cv.Videogame.DeletedAt,
                        ApplicationUser = cv.Videogame.ApplicationUser != null ? new Cart_CartVideogame_Videogame_ApplicationUserDto()
                        {
                            ApplicationUserId = cv.Videogame.ApplicationUser.Id,
                            FirstName = cv.Videogame.ApplicationUser.FirstName,
                            LastName = cv.Videogame.ApplicationUser.LastName,
                            BirthDate = cv.Videogame.ApplicationUser.BirthDate,
                            Country = cv.Videogame.ApplicationUser.Country,
                            City = cv.Videogame.ApplicationUser.City,
                            DisplayName = cv.Videogame.ApplicationUser.DisplayName,
                            Avatar = cv.Videogame.ApplicationUser.Avatar,
                            Picture = cv.Videogame.ApplicationUser.Picture,
                            Cover = cv.Videogame.ApplicationUser.Cover,
                            IsGamer = cv.Videogame.ApplicationUser.IsGamer,
                            IsDeveloper = cv.Videogame.ApplicationUser.IsDeveloper,
                            IsEditor = cv.Videogame.ApplicationUser.IsEditor,
                            DeveloperRole = cv.Videogame.ApplicationUser.DeveloperRole,
                            Bio = cv.Videogame.ApplicationUser.Bio,
                            Title = cv.Videogame.ApplicationUser.Title,
                            IsHidden = cv.Videogame.ApplicationUser.IsHidden,
                            IsFavouriteListPrivate = cv.Videogame.ApplicationUser.IsFavouriteListPrivate,
                            IsFriendListPrivate = cv.Videogame.ApplicationUser.IsFriendListPrivate,
                            AutoAcceptFriendRequests = cv.Videogame.ApplicationUser.AutoAcceptFriendRequests,
                            CreatedAt = cv.Videogame.ApplicationUser.CreatedAt,
                            UpdatedAt = cv.Videogame.ApplicationUser.UpdatedAt,
                            IsDeleted = cv.Videogame.ApplicationUser.IsDeleted,
                            DeletedAt = cv.Videogame.ApplicationUser.DeletedAt
                        } : null,
                    } : null
                }).ToList()
            };

            return Ok(new
            {
                message = "Cart trovato con successo",
                cart = cartResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCart(Guid id, [FromBody] UpdateCartRequestDto updateCartRequestDto)
        {
            var result = await _cartService.UpdateCartAsync(id, updateCartRequestDto);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella modifica del cart"
                });
            }

            return Ok(new
            {
                message = "Cart aggiornato con successo"
            });
        }
    }
}
