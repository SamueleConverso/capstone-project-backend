using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.PostTag;
using Capstone_Project.DTOs.Feed.Tag;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] CreateTagRequestDto createTagRequestDto)
        {
            var newTag = new Tag
            {
                Entry = createTagRequestDto.Entry,
                IsDeleted = false
            };

            var result = await _tagService.AddTagAsync(newTag);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta del tag"
                });
            }

            return Ok(new
            {
                message = "Tag aggiunto con successo"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tagsList = await _tagService.GetAllTagsAsync();

            if (tagsList == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dei tag",
                    tags = new List<Tag>()
                });
            }

            if (!tagsList.Any())
            {
                return Ok(new
                {
                    message = "Nessun tag trovato",
                    tags = new List<Tag>()
                });
            }

            var tagsResponse = tagsList.Select(t => new TagDto()
            {
                TagId = t.TagId,
                Entry = t.Entry,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                IsDeleted = t.IsDeleted,
                DeletedAt = t.DeletedAt,
                PostTags = t.PostTags.Select(pt => new Tag_PostTagDto()
                {
                    PostTagId = pt.PostTagId,
                    CreatedAt = pt.CreatedAt,
                    UpdatedAt = pt.UpdatedAt,
                    IsDeleted = pt.IsDeleted,
                    DeletedAt = pt.DeletedAt,
                    Post = pt.Post != null ? new Tag_PostTag_PostDto()
                    {
                        PostId = pt.Post.PostId,
                        Text = pt.Post.Text,
                        Picture = pt.Post.Picture,
                        Video = pt.Post.Video,
                        Mood = pt.Post.Mood,
                        IsLookingForGamers = pt.Post.IsLookingForGamers,
                        IsLookingForDevelopers = pt.Post.IsLookingForDevelopers,
                        IsLookingForEditors = pt.Post.IsLookingForEditors,
                        Country = pt.Post.Country,
                        City = pt.Post.City,
                        IsInUserFeed = pt.Post.IsInUserFeed,
                        IsInGameFeed = pt.Post.IsInGameFeed,
                        IsInCommunityFeed = pt.Post.IsInCommunityFeed,
                        IsHidden = pt.Post.IsHidden,
                        Likes = pt.Post.Likes,
                        CreatedAt = pt.Post.CreatedAt,
                        UpdatedAt = pt.Post.UpdatedAt,
                        IsDeleted = pt.Post.IsDeleted,
                        DeletedAt = pt.Post.DeletedAt,
                    } : null
                }).ToList()
            });

            return Ok(new
            {
                message = $"Numero tag trovati: {tagsResponse.Count()}",
                tags = tagsResponse
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var tagToFind = await _tagService.GetTagByIdAsync(id);

            if (tagToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero del tag"
                });
            }

            var tagResponse = new TagDto()
            {
                TagId = tagToFind.TagId,
                Entry = tagToFind.Entry,
                CreatedAt = tagToFind.CreatedAt,
                UpdatedAt = tagToFind.UpdatedAt,
                IsDeleted = tagToFind.IsDeleted,
                DeletedAt = tagToFind.DeletedAt,
                PostTags = tagToFind.PostTags.Select(pt => new Tag_PostTagDto()
                {
                    PostTagId = pt.PostTagId,
                    CreatedAt = pt.CreatedAt,
                    UpdatedAt = pt.UpdatedAt,
                    IsDeleted = pt.IsDeleted,
                    DeletedAt = pt.DeletedAt,
                    Post = pt.Post != null ? new Tag_PostTag_PostDto()
                    {
                        PostId = pt.Post.PostId,
                        Text = pt.Post.Text,
                        Picture = pt.Post.Picture,
                        Video = pt.Post.Video,
                        Mood = pt.Post.Mood,
                        IsLookingForGamers = pt.Post.IsLookingForGamers,
                        IsLookingForDevelopers = pt.Post.IsLookingForDevelopers,
                        IsLookingForEditors = pt.Post.IsLookingForEditors,
                        Country = pt.Post.Country,
                        City = pt.Post.City,
                        IsInUserFeed = pt.Post.IsInUserFeed,
                        IsInGameFeed = pt.Post.IsInGameFeed,
                        IsInCommunityFeed = pt.Post.IsInCommunityFeed,
                        IsHidden = pt.Post.IsHidden,
                        Likes = pt.Post.Likes,
                        CreatedAt = pt.Post.CreatedAt,
                        UpdatedAt = pt.Post.UpdatedAt,
                        IsDeleted = pt.Post.IsDeleted,
                        DeletedAt = pt.Post.DeletedAt,
                    } : null
                }).ToList()
            };

            return Ok(new
            {
                message = "Tag trovato con successo",
                tag = tagResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] UpdateTagRequestDto updateTagRequestDto)
        {
            var result = await _tagService.UpdateTagAsync(id, updateTagRequestDto);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella modifica del tag"
                });
            }

            return Ok(new
            {
                message = "Tag aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var result = await _tagService.DeleteTagAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione del tag"
                });
            }
            return Ok(new
            {
                message = "Tag cancellato con successo"
            });
        }
    }
}
