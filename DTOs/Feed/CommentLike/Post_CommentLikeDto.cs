using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.CommentLike {
    public class Post_CommentLikeDto {
        public Guid CommentLikeId {
            get; set;
        }

        [Required]
        public DateTime CreatedAt {
            get; set;
        }

        public DateTime? UpdatedAt {
            get; set;
        }

        public bool? IsDeleted {
            get; set;
        }

        public DateTime? DeletedAt {
            get; set;
        }

        public Post_CommentLike_ApplicationUserDto ApplicationUser {
            get; set;
        }
    }
}
