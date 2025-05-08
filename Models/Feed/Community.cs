using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed
{
    [Table("Communities")]
    public class Community
    {
        [Key]
        public Guid CommunityId
        {
            get; set;
        }

        [Required]
        public required string Type
        {
            get; set;
        }

        [Required]
        public required string Name
        {
            get; set;
        }

        public string? ExtraName
        {
            get; set;
        }

        [Required]
        public required string Description
        {
            get; set;
        }

        public string? ExtraDescription
        {
            get; set;
        }

        public string? Picture
        {
            get; set;
        }

        public string? Cover
        {
            get; set;
        }

        public string? Link
        {
            get; set;
        }

        public bool? IsPrivate
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }

        [Required]
        public required int MaxMembers
        {
            get; set;
        } = 2;

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

        public string? ApplicationUserId
        {
            get; set;
        }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser
        {
            get; set;
        }

        public ICollection<CommunityApplicationUser> CommunityApplicationUsers
        {
            get; set;
        }

        public ICollection<Post> Posts { get; set; }
    }
}
