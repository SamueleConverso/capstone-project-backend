using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public Guid ReviewId
        {
            get; set;
        }

        [Required]
        public required string Title
        {
            get; set;
        }

        [Required]
        public required string Text
        {
            get; set;
        }

        [Required]
        [Range(1, 10)]
        public required int Rating
        {
            get; set;
        }

        [Required]
        public required bool Recommend
        {
            get; set;
        }

        public string? Picture
        {
            get; set;
        }

        public string? Video
        {
            get; set;
        }

        public string? Link
        {
            get; set;
        }

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

        public Guid? VideogameId
        {
            get; set;
        }

        [ForeignKey("VideogameId")]
        public Videogame Videogame
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
    }
}
