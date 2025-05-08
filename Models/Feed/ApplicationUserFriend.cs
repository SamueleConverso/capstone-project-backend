using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed {
    [Table("ApplicationUserFriends")]
    public class ApplicationUserFriend {
        [Key]
        public Guid ApplicationUserFriendId {
            get; set;
        }

        [Required]
        public required bool Sent {
            get; set;
        } = true;

        public bool? Accepted {
            get; set;
        }

        public bool? Rejected {
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
        public required Guid FriendListId {
            get; set;
        }

        [ForeignKey("FriendListId")]
        public FriendList FriendList {
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

    }
}
