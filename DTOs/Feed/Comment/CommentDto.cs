using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.Comment
{
    public class CommentDto
    {
        public Guid CommentId
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

        public int? Likes
        {
            get; set;
        }

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
        }

        public DateTime? DeletedAt
        {
            get; set;
        }

        public ICollection<Comment_CommentLikeDto> CommentLikes
        {
            get; set;
        }

        public Comment_PostDto Post
        {
            get; set;
        }

        public Comment_ApplicationUserDto ApplicationUser
        {
            get; set;
        }
    }
}
