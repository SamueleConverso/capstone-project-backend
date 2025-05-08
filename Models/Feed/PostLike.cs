using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed {
    [Table("PostLikes")]
    public class PostLike {
        [Key]
        public Guid PostLikeId {
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
        } = false;

        public DateTime? DeletedAt {
            get; set;
        }

        [Required]
        public required string ApplicationUserId {
            get; set;
        }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser {
            get; set;
        }

        [Required]
        public required Guid PostId {
            get; set;
        }

        [ForeignKey("PostId")]
        public Post Post {
            get; set;
        }
    }
}
