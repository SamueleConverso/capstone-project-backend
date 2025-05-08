using Capstone_Project.DTOs.Feed.PostTag;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly PostTagService _postTagService;

        public PostTagController(PostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostTag([FromBody] CreatePostTagRequestDto createPostTagRequestDto)
        {
            var newPostTag = new PostTag
            {
                IsDeleted = false,
                PostId = createPostTagRequestDto.PostId,
                TagId = createPostTagRequestDto.TagId,
            };

            var result = await _postTagService.AddPostTagAsync(newPostTag);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del PostTag"
                });
            }

            return Ok(new
            {
                message = "PostTag aggiunto con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePostTag(Guid id)
        {
            var result = await _postTagService.DeletePostTagAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del PostTag"
                });
            }
            return Ok(new
            {
                message = "PostTag cancellato con successo"
            });
        }
    }
}
