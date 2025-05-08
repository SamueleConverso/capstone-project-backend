using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Comment {
    public class CreateCommentResponseDto {
        [Required]
        public required string Message {
            get; set;
        }
    }
}
