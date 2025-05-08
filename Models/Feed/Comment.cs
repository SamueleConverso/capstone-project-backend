using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed {
    [Table("Comments")]
    public class Comment {
        [Key]
        public Guid CommentId {
            get; set;
        }

        [Required]
        public required string Text {
            get; set;
        }

        public string? Picture {
            get; set;
        }

        public int? Likes {
            get; set;
        } = 0;

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

        public ICollection<CommentLike> CommentLikes {
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

        public string? ApplicationUserId {
            get; set;
        }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser {
            get; set;
        }
    }
}
