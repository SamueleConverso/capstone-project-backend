using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Post {
    public class UpdatePostResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
