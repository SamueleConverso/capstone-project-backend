using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentLikeController : ControllerBase
    {
        private readonly CommentLikeService _commentLikeService;

        public CommentLikeController(CommentLikeService commentLikeService)
        {
            _commentLikeService = commentLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentLike([FromBody] CreateCommentLikeRequestDto createCommentLikeRequestDto)
        {
            var newCommentLike = new CommentLike
            {
                IsDeleted = false,
                ApplicationUserId = createCommentLikeRequestDto.ApplicationUserId,
                CommentId = createCommentLikeRequestDto.CommentId,
            };

            var result = await _commentLikeService.AddCommentLikeAsync(newCommentLike);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del CommentLike"
                });
            }

            return Ok(new
            {
                message = "CommentLike aggiunto con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCommentLike(Guid id)
        {
            var result = await _commentLikeService.DeleteCommentLikeAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del CommentLike"
                });
            }
            return Ok(new
            {
                message = "CommentLike cancellato con successo"
            });
        }
    }
}
