using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_Project.Models.Feed {
    [Table("Tags")]
    public class Tag {
        [Key]
        public Guid TagId {
            get; set;
        }

        [Required]
        public required string Entry {
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

        public ICollection<PostTag> PostTags {
            get; set;
        }
    }
}
