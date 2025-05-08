using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed
{
    [Table("FriendList")]
    public class FriendList
    {
        [Key]
        public Guid FriendListId
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
        } = false;

        public DateTime? DeletedAt
        {
            get; set;
        }

        [Required]
        public required string ApplicationUserId
        {
            get; set;
        }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser
        {
            get; set;
        }

        public ICollection<ApplicationUserFriend> ApplicationUserFriends
        {
            get; set;
        }
    }
}
