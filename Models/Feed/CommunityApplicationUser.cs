using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed {
    [Table("CommunityApplicationUsers")]
    public class CommunityApplicationUser {
        [Key]
        public Guid CommunityApplicationUserId {
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
        public required Guid CommunityId {
            get; set;
        }

        [ForeignKey("CommunityId")]
        public Community Community {
            get; set;
        }
    }
}
