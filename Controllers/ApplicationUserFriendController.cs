using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.ApplicationUserFriend;
using Capstone_Project.DTOs.Feed.FriendList;
using Capstone_Project.Models.Feed;
using Capstone_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserFriendController : ControllerBase
    {
        private readonly ApplicationUserFriendService _applicationUserFriendService;

        public ApplicationUserFriendController(ApplicationUserFriendService applicationUserFriendService)
        {
            _applicationUserFriendService = applicationUserFriendService;
        }

        [HttpPost]
        public async Task<IActionResult> AddApplicationUserFriend([FromBody] CreateApplicationUserFriendRequestDto createApplicationUserFriendRequestDto)
        {
            var newApplicationUserFriend = new ApplicationUserFriend
            {
                Sent = true,
                Accepted = createApplicationUserFriendRequestDto.Accepted,
                IsDeleted = false,
                FriendListId = createApplicationUserFriendRequestDto.FriendListId,
                ApplicationUserId = createApplicationUserFriendRequestDto.ApplicationUserId
            };

            var result = await _applicationUserFriendService.AddApplicationUserFriendAsync(newApplicationUserFriend);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nell'aggiunta dell'ApplicationUserFriend"
                });
            }

            return Ok(new
            {
                message = "ApplicationUserFriend aggiunto con successo"
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetApplicationUserFriendById(Guid id)
        {
            var applicationUserFriendToFind = await _applicationUserFriendService.GetApplicationUserFriendByIdAsync(id);

            if (applicationUserFriendToFind == null)
            {
                return BadRequest(new
                {
                    message = "Errore nel recupero dell'ApplicationUserFriend"
                });
            }

            var applicationUserFriendResponse = new ApplicationUserFriendDto()
            {
                ApplicationUserFriendId = applicationUserFriendToFind.ApplicationUserFriendId,
                Sent = applicationUserFriendToFind.Sent,
                Accepted = applicationUserFriendToFind.Accepted,
                Rejected = applicationUserFriendToFind.Rejected,
                CreatedAt = applicationUserFriendToFind.CreatedAt,
                UpdatedAt = applicationUserFriendToFind.UpdatedAt,
                IsDeleted = applicationUserFriendToFind.IsDeleted,
                DeletedAt = applicationUserFriendToFind.DeletedAt,
                FriendList = applicationUserFriendToFind.FriendList != null ? new ApplicationUserFriend_FriendListDto()
                {
                    FriendListId = applicationUserFriendToFind.FriendList.FriendListId,
                    CreatedAt = applicationUserFriendToFind.FriendList.CreatedAt,
                    UpdatedAt = applicationUserFriendToFind.FriendList.UpdatedAt,
                    IsDeleted = applicationUserFriendToFind.FriendList.IsDeleted,
                    DeletedAt = applicationUserFriendToFind.FriendList.DeletedAt,
                    ApplicationUser = applicationUserFriendToFind.FriendList.ApplicationUser != null ? new ApplicationUserFriend_FriendList_ApplicationUserDto()
                    {
                        ApplicationUserId = applicationUserFriendToFind.FriendList.ApplicationUser.Id,
                        FirstName = applicationUserFriendToFind.FriendList.ApplicationUser.FirstName,
                        LastName = applicationUserFriendToFind.FriendList.ApplicationUser.LastName,
                        BirthDate = applicationUserFriendToFind.FriendList.ApplicationUser.BirthDate,
                        Country = applicationUserFriendToFind.FriendList.ApplicationUser.Country,
                        City = applicationUserFriendToFind.FriendList.ApplicationUser.City,
                        DisplayName = applicationUserFriendToFind.FriendList.ApplicationUser.DisplayName,
                        Avatar = applicationUserFriendToFind.FriendList.ApplicationUser.Avatar,
                        Picture = applicationUserFriendToFind.FriendList.ApplicationUser.Picture,
                        Cover = applicationUserFriendToFind.FriendList.ApplicationUser.Cover,
                        IsGamer = applicationUserFriendToFind.FriendList.ApplicationUser.IsGamer,
                        IsDeveloper = applicationUserFriendToFind.FriendList.ApplicationUser.IsDeveloper,
                        IsEditor = applicationUserFriendToFind.FriendList.ApplicationUser.IsEditor,
                        DeveloperRole = applicationUserFriendToFind.FriendList.ApplicationUser.DeveloperRole,
                        Bio = applicationUserFriendToFind.FriendList.ApplicationUser.Bio,
                        Title = applicationUserFriendToFind.FriendList.ApplicationUser.Title,
                        IsHidden = applicationUserFriendToFind.FriendList.ApplicationUser.IsHidden,
                        IsFavouriteListPrivate = applicationUserFriendToFind.FriendList.ApplicationUser.IsFavouriteListPrivate,
                        IsFriendListPrivate = applicationUserFriendToFind.FriendList.ApplicationUser.IsFriendListPrivate,
                        AutoAcceptFriendRequests = applicationUserFriendToFind.FriendList.ApplicationUser.AutoAcceptFriendRequests,
                        CreatedAt = applicationUserFriendToFind.FriendList.ApplicationUser.CreatedAt,
                        UpdatedAt = applicationUserFriendToFind.FriendList.ApplicationUser.UpdatedAt,
                        IsDeleted = applicationUserFriendToFind.FriendList.ApplicationUser.IsDeleted,
                        DeletedAt = applicationUserFriendToFind.FriendList.ApplicationUser.DeletedAt
                    } : null
                } : null,
                ApplicationUser = applicationUserFriendToFind.ApplicationUser != null ? new ApplicationUserFriend_ApplicationUserDto()
                {
                    ApplicationUserId = applicationUserFriendToFind.ApplicationUser.Id,
                    FirstName = applicationUserFriendToFind.ApplicationUser.FirstName,
                    LastName = applicationUserFriendToFind.ApplicationUser.LastName,
                    BirthDate = applicationUserFriendToFind.ApplicationUser.BirthDate,
                    Country = applicationUserFriendToFind.ApplicationUser.Country,
                    City = applicationUserFriendToFind.ApplicationUser.City,
                    DisplayName = applicationUserFriendToFind.ApplicationUser.DisplayName,
                    Avatar = applicationUserFriendToFind.ApplicationUser.Avatar,
                    Picture = applicationUserFriendToFind.ApplicationUser.Picture,
                    Cover = applicationUserFriendToFind.ApplicationUser.Cover,
                    IsGamer = applicationUserFriendToFind.ApplicationUser.IsGamer,
                    IsDeveloper = applicationUserFriendToFind.ApplicationUser.IsDeveloper,
                    IsEditor = applicationUserFriendToFind.ApplicationUser.IsEditor,
                    DeveloperRole = applicationUserFriendToFind.ApplicationUser.DeveloperRole,
                    Bio = applicationUserFriendToFind.ApplicationUser.Bio,
                    Title = applicationUserFriendToFind.ApplicationUser.Title,
                    IsHidden = applicationUserFriendToFind.ApplicationUser.IsHidden,
                    IsFavouriteListPrivate = applicationUserFriendToFind.ApplicationUser.IsFavouriteListPrivate,
                    IsFriendListPrivate = applicationUserFriendToFind.ApplicationUser.IsFriendListPrivate,
                    AutoAcceptFriendRequests = applicationUserFriendToFind.ApplicationUser.AutoAcceptFriendRequests,
                    CreatedAt = applicationUserFriendToFind.ApplicationUser.CreatedAt,
                    UpdatedAt = applicationUserFriendToFind.ApplicationUser.UpdatedAt,
                    IsDeleted = applicationUserFriendToFind.ApplicationUser.IsDeleted,
                    DeletedAt = applicationUserFriendToFind.ApplicationUser.DeletedAt
                } : null
            };

            return Ok(new
            {
                message = "ApplicationUserFriend trovato con successo",
                applicationUserFriend = applicationUserFriendResponse
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateApplicationUserFriend(Guid id, [FromBody] UpdateApplicationUserFriendRequestDto updateApplicationUserFriendRequestDto)
        {
            var result = await _applicationUserFriendService.UpdateApplicationUserFriendAsync(id, updateApplicationUserFriendRequestDto);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella modifica dell'ApplicationUserFriend"
                });
            }

            return Ok(new
            {
                message = "ApplicationUserFriend aggiornato con successo"
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteApplicationUserFriend(Guid id)
        {
            var result = await _applicationUserFriendService.DeleteApplicationUserFriendAsync(id);
            if (!result)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione dell'ApplicationUserFriend"
                });
            }
            return Ok(new
            {
                message = "ApplicationUserFriend cancellato con successo"
            });
        }
    }
}
