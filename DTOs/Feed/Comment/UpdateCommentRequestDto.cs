using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_Project.DTOs.Feed.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        public required string Text
        {
            get; set;
        }

        public string? Picture
        {
            get; set;
        }

        //public bool? IsDeleted {
        //    get; set;
        //}

        public DateTime? UpdatedAt
        {
            get; set;
        }

        [Required]
        public required string PostId
        {
            get; set;
        }

        public string? ApplicationUserId
        {
            get; set;
        }

        [JsonIgnore]
        public IFormFile? PictureFile { get; set; }
    }
}
