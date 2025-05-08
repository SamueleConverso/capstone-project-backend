using Capstone_Project.DTOs.Feed.Cart;
using Capstone_Project.DTOs.Feed.CartVideogame;
using Capstone_Project.DTOs.Feed.Videogame;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartVideogameController : ControllerBase
    {
        private readonly CartVideogameService _cartVideogameService;

        public CartVideogameController(CartVideogameService cartVideogameService)
        {
            _cartVideogameService = cartVideogameService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartVideogame([FromBody] CreateCartVideogameRequestDto createCartVideogameRequestDto)
        {
            var newCartVideogame = new CartVideogame
            {
                Quantity = createCartVideogameRequestDto.Quantity,
                IsDeleted = false,
                CartId = createCartVideogameRequestDto.CartId,
                VideogameId = createCartVideogameRequestDto.VideogameId,
            };

            var result = await _cartVideogameService.AddCartVideogameAsync(newCartVideogame);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del CartVideogame"
                });
            }

            return Ok(new
            {
                message = "CartVideogame aggiunto con successo"
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCartVideogameById(Guid id)
        {
            var cartVideogameToFind = await _cartVideogameService.GetCartVideogameByIdAsync(id);

            if (cartVideogameToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del CartVideogame"
                });
            }

            var cartVideogameResponse = new CartVideogameDto()
            {
                CartVideogameId = cartVideogameToFind.CartVideogameId,
                Quantity = cartVideogameToFind.Quantity,
                CreatedAt = cartVideogameToFind.CreatedAt,
                UpdatedAt = cartVideogameToFind.UpdatedAt,
                IsDeleted = cartVideogameToFind.IsDeleted,
                DeletedAt = cartVideogameToFind.DeletedAt,
                Cart = new CartVideogame_CartDto()
                {
                    CartId = cartVideogameToFind.Cart.CartId,
                    IsCheckedOut = cartVideogameToFind.Cart.IsCheckedOut,
                    CreatedAt = cartVideogameToFind.Cart.CreatedAt,
                    UpdatedAt = cartVideogameToFind.Cart.UpdatedAt,
                    IsDeleted = cartVideogameToFind.Cart.IsDeleted,
                    DeletedAt = cartVideogameToFind.Cart.DeletedAt,
                },
                Videogame = new CartVideogame_VideogameDto()
                {
                    VideogameId = cartVideogameToFind.Videogame.VideogameId,
                    Title = cartVideogameToFind.Videogame.Title,
                    Subtitle = cartVideogameToFind.Videogame.Subtitle,
                    Description = cartVideogameToFind.Videogame.Description,
                    ExtraDescription = cartVideogameToFind.Videogame.ExtraDescription,
                    Genre = cartVideogameToFind.Videogame.Genre,
                    Picture = cartVideogameToFind.Videogame.Picture,
                    Cover = cartVideogameToFind.Videogame.Cover,
                    Video = cartVideogameToFind.Videogame.Video,
                    Link = cartVideogameToFind.Videogame.Link,
                    ReleaseDate = cartVideogameToFind.Videogame.ReleaseDate,
                    Platform = cartVideogameToFind.Videogame.Platform,
                    AgeRating = cartVideogameToFind.Videogame.AgeRating,
                    Contributors = cartVideogameToFind.Videogame.Contributors,
                    Price = cartVideogameToFind.Videogame.Price,
                    IsHidden = cartVideogameToFind.Videogame.IsHidden,
                    IsAvailableForPurchase = cartVideogameToFind.Videogame.IsAvailableForPurchase,
                    Country = cartVideogameToFind.Videogame.Country,
                    City = cartVideogameToFind.Videogame.City,
                    CreatedAt = cartVideogameToFind.Videogame.CreatedAt,
                    UpdatedAt = cartVideogameToFind.Videogame.UpdatedAt,
                    IsDeleted = cartVideogameToFind.Videogame.IsDeleted,
                    DeletedAt = cartVideogameToFind.Videogame.DeletedAt,
                }
            };

            return Ok(new
            {
                message = "CartVideogame trovato con successo",
                cartVideogame = cartVideogameResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCartVideogame(Guid id, [FromBody] UpdateCartVideogameRequestDto updateCartVideogameRequestDto)
        {
            var result = await _cartVideogameService.UpdateCartVideogameAsync(id, updateCartVideogameRequestDto);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella modifica del CartVideogame"
                });
            }

            return Ok(new
            {
                message = "CartVideogame aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCartVideogame(Guid id)
        {
            var result = await _cartVideogameService.DeleteCartVideogameAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del CartVideogame"
                });
            }
            return Ok(new
            {
                message = "CartVideogame cancellato con successo"
            });
        }
    }
}
