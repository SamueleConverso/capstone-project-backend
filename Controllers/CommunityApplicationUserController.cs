using Capstone_Project.DTOs.Feed.CommunityApplicationUser;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityApplicationUserController : ControllerBase
    {
        private readonly CommunityApplicationUserService _communityApplicationUserService;

        public CommunityApplicationUserController(CommunityApplicationUserService communityApplicationUserService)
        {
            _communityApplicationUserService = communityApplicationUserService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCommunityApplicationUser([FromBody] CreateCommunityApplicationUserRequestDto createCommunityApplicationUserRequestDto)
        {
            var newCommunityApplicationUser = new CommunityApplicationUser
            {
                IsDeleted = false,
                ApplicationUserId = createCommunityApplicationUserRequestDto.ApplicationUserId,
                CommunityId = createCommunityApplicationUserRequestDto.CommunityId,
            };

            var result = await _communityApplicationUserService.AddCommunityApplicationUserAsync(newCommunityApplicationUser);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del CommunityApplicationUser"
                });
            }

            return Ok(new
            {
                message = "CommunityApplicationUser aggiunto con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCommunityApplicationUser(Guid id)
        {
            var result = await _communityApplicationUserService.DeleteCommunityApplicationUserAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del CommunityApplicationUser"
                });
            }
            return Ok(new
            {
                message = "CommunityApplicationUser cancellato con successo"
            });
        }
    }
}
