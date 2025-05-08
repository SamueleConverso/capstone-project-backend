using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_Project.Models.Feed {
    [Table("CartVideogames")]
    public class CartVideogame {
        [Key]
        public Guid CartVideogameId {
            get; set;
        }

        [Required]
        public required int Quantity {
            get; set;
        } = 1;

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
        public required Guid CartId {
            get; set;
        }

        [ForeignKey("CartId")]
        public Cart Cart {
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
