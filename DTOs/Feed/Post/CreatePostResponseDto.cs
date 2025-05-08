using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Post {
    public class CreatePostResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
