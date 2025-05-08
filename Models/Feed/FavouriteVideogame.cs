using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_Project.Models.Feed {
    [Table("FavouriteVideogames")]
    public class FavouriteVideogame {
        [Key]
        public Guid FavouriteVideogameId {
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
        public required Guid FavouriteListId {
            get; set;
        }

        [ForeignKey("FavouriteListId")]
        public FavouriteList FavouriteList {
            get; set;
        }

        [Required]
        public required Guid VideogameId {
            get; set;
        }

        [ForeignKey("VideogameId")]
        public Videogame Videogame {
            get; set;
        }
    }
}
