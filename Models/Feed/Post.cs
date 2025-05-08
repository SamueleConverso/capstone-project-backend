using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public Guid PostId
        {
            get; set;
        }

        [Required]
        public required string Text
        {
            get; set;
        }

        public string? Picture
        {
            get; set;
        }

        public string? Video
        {
            get; set;
        }

        public string? Mood
        {
            get; set;
        }

        public bool? IsLookingForGamers
        {
            get; set;
        }

        public bool? IsLookingForDevelopers
        {
            get; set;
        }

        public bool? IsLookingForEditors
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

        public bool? IsInUserFeed
        {
            get; set;
        }

        public bool? IsInGameFeed
        {
            get; set;
        }

        public bool? IsInCommunityFeed
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }

        public int? Likes
        {
            get; set;
        } = 0;

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

        public ICollection<PostLike> PostLikes
        {
            get; set;
        }

        public ICollection<Comment> Comments
        {
            get; set;
        }

        public ICollection<PostTag> PostTags
        {
            get; set;
        }

        public string? ApplicationUserId
        {
            get; set;
        }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser
        {
            get; set;
        }

        public Guid? VideogameId
        {
            get; set;
        }

        [ForeignKey("VideogameId")]
        public Videogame Videogame
        {
            get; set;
        }

        public Guid? CommunityId { get; set; }

        [ForeignKey("CommunityId")]
        public Community Community { get; set; }
    }
}
