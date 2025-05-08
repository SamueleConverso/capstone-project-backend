using System.Text.Json.Serialization;

namespace Capstone_Project.DTOs.Account {
    public class RegisterRequestDto {
        public required string FirstName {
            get; set;
        }

        public required string LastName {
            get; set;
        }

        public required string BirthDate {
            get; set;
        }

        public required string Email {
            get; set;
        }

        public required string Password {
            get; set;
        }

        public string? Country {
            get; set;
        }

        public string? City {
            get; set;
        }

        public string? DisplayName {
            get; set;
        }

        public string? Avatar {
            get; set;
        }

        public string? Picture {
            get; set;
        }

        public string? Cover {
            get; set;
        }

        public string? IsGamer {
            get; set;
        }

        public string? IsDeveloper {
            get; set;
        }

        public string? IsEditor {
            get; set;
        }

        public string? DeveloperRole {
            get; set;
        }

        public string? Bio {
            get; set;
        }

        public string? Title {
            get; set;
        }

        public string? IsHidden {
            get; set;
        }

        public string? IsFavouriteListPrivate {
            get; set;
        }

        public string? IsFriendListPrivate {
            get; set;
        }

        public string? AutoAcceptFriendRequests {
            get; set;
        }

        public bool? IsDeleted {
            get; set;
        } = false;

        [JsonIgnore]
        public IFormFile? PictureFile {
            get; set;
        }

        [JsonIgnore]
        public IFormFile? CoverFile {
            get; set;
        }
    }
}
