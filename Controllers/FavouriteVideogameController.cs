using Capstone_Project.DTOs.Feed.FavouriteVideogame;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteVideogameController : ControllerBase
    {
        private readonly FavouriteVideogameService _favouriteVideogameService;

        public FavouriteVideogameController(FavouriteVideogameService favouriteVideogameService)
        {
            _favouriteVideogameService = favouriteVideogameService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteVideogame([FromBody] CreateFavouriteVideogameRequestDto createFavouriteVideogameRequestDto)
        {
            var newFavouriteVideogame = new FavouriteVideogame
            {
                IsDeleted = false,
                FavouriteListId = createFavouriteVideogameRequestDto.FavouriteListId,
                VideogameId = createFavouriteVideogameRequestDto.VideogameId,
            };

            var result = await _favouriteVideogameService.AddFavouriteVideogameAsync(newFavouriteVideogame);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del FavouriteVideogame"
                });
            }

            return Ok(new
            {
                message = "FavouriteVideogame aggiunto con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFavouriteVideogame(Guid id)
        {
            var result = await _favouriteVideogameService.DeleteFavouriteVideogameAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del FavouriteVideogame"
                });
            }
            return Ok(new
            {
                message = "FavouriteVideogame cancellato con successo"
            });
        }
    }
}
