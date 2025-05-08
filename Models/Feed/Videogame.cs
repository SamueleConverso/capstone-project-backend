using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone_Project.Models.Account;

namespace Capstone_Project.Models.Feed
{
    [Table("Videogames")]
    public class Videogame
    {
        [Key]
        public Guid VideogameId
        {
            get; set;
        }

        [Required]
        public required string Title
        {
            get; set;
        }

        public string? Subtitle
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

        [Required]
        public required string Genre
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

        public string? Video
        {
            get; set;
        }

        public string? Link
        {
            get; set;
        }

        public DateTime? ReleaseDate
        {
            get; set;
        }

        public string? Platform
        {
            get; set;
        }

        public int? AgeRating
        {
            get; set;
        }

        public string? Contributors
        {
            get; set;
        }

        public decimal? Price
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }

        public bool? IsAvailableForPurchase
        {
            get; set;
        }

        public string? Country
        {
            get; set;
        }

        public string? City
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

        public ICollection<FavouriteVideogame> FavouriteVideogames
        {
            get; set;
        }

        public ICollection<Review> Reviews
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

        public ICollection<Post> Posts
        {
            get; set;
        }

        public ICollection<CartVideogame> CartVideogames
        {
            get; set;
        }
    }
}
