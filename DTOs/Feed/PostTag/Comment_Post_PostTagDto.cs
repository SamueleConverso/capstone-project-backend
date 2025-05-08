using Capstone_Project.DTOs.Feed.Tag;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.PostTag {
    public class Comment_Post_PostTagDto {
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
        }

        public DateTime? DeletedAt {
            get; set;
        }

        public Comment_Post_TagDto Tag {
            get; set;
        }
    }
}
