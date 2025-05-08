using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private readonly PostLikeService _postLikeService;

        public PostLikeController(PostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostLike([FromBody] CreatePostLikeRequestDto createPostLikeRequestDto)
        {
            var newPostLike = new PostLike
            {
                IsDeleted = false,
                ApplicationUserId = createPostLikeRequestDto.ApplicationUserId,
                PostId = createPostLikeRequestDto.PostId,
            };

            var result = await _postLikeService.AddPostLikeAsync(newPostLike);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del PostLike"
                });
            }

            return Ok(new
            {
                message = "PostLike aggiunto con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePostLike(Guid id)
        {
            var result = await _postLikeService.DeletePostLikeAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del PostLike"
                });
            }
            return Ok(new
            {
                message = "PostLike cancellato con successo"
            });
        }
    }
}
