using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Feed;
using Microsoft.AspNetCore.Identity;

namespace Capstone_Project.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName
        {
            get; set;
        }

        [Required]
        public required string LastName
        {
            get; set;
        }

        [Required]
        public required DateOnly BirthDate
        {
            get; set;
        }

        public string? Country
        {
            get; set;
        }

        public string? City
        {
            get; set;
        }

        public string? DisplayName
        {
            get; set;
        }

        public string? Avatar
        {
            get; set;
        }

        public string? Picture
        {
            get; set;
        }

        public string? Cover
        {
            get; set;
        }

        public bool? IsGamer
        {
            get; set;
        }

        public bool? IsDeveloper
        {
            get; set;
        }

        public bool? IsEditor
        {
            get; set;
        }

        public string? DeveloperRole
        {
            get; set;
        }

        public string? Bio
        {
            get; set;
        }

        public string? Title
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }

        public bool? IsFavouriteListPrivate
        {
            get; set;
        }

        public bool? IsFriendListPrivate
        {
            get; set;
        }

        public bool? AutoAcceptFriendRequests
        {
            get; set;
        } = false;

        [Required]
        public DateTime CreatedAt
        {
            get; set;
        }

        public DateTime? UpdatedAt
        {
            get; set;
        }

        public bool? IsDeleted
        {
            get; set;
        } = false;

        public DateTime? DeletedAt
        {
            get; set;
        }

        public ICollection<Post> Posts
        {
            get; set;
        }

        public ICollection<Comment> Comments
        {
            get; set;
        }

        public ICollection<PostLike> PostLikes
        {
            get; set;
        }

        public ICollection<CommentLike> CommentLikes
        {
            get; set;
        }

        public ICollection<Videogame> Videogames
        {
            get; set;
        }

        public ICollection<Review> Reviews
        {
            get; set;
        }

        public ICollection<Community> Communities
        {
            get; set;
        }

        public ICollection<CommunityApplicationUser> CommunityApplicationUsers
        {
            get; set;
        }

        public ICollection<ApplicationUserFriend> ApplicationUserFriends
        {
            get; set;
        }

        [InverseProperty("ApplicationUser")]
        public Cart Cart
        {
            get; set;
        }

        [InverseProperty("ApplicationUser")]
        public FavouriteList FavouriteList
        {
            get; set;
        }

        [InverseProperty("ApplicationUser")]
        public FriendList FriendList
        {
            get; set;
        }

        public ICollection<ApplicationUserRole> ApplicationUserRoles
        {
            get; set;
        }
    }
}