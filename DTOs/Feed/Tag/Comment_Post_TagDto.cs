using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Tag {
    public class Comment_Post_TagDto {
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
        }

        public DateTime? DeletedAt {
            get; set;
        }
    }
}
