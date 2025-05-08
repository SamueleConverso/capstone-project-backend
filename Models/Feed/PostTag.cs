using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_Project.Models.Feed {
    [Table("PostTags")]
    public class PostTag {
        [Key]
        public Guid PostTagId {
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
        public required Guid PostId {
            get; set;
        }

        [ForeignKey("PostId")]
        public Post Post {
            get; set;
        }

        [Required]
        public required Guid TagId {
            get; set;
        }

        [ForeignKey("TagId")]
        public Tag Tag {
            get; set;
        }
    }
}
