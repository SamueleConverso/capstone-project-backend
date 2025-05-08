using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUserFriend;
using Capstone_Project.DTOs.Feed.Cart;
using Capstone_Project.DTOs.Feed.Comment;
using Capstone_Project.DTOs.Feed.CommentLike;
using Capstone_Project.DTOs.Feed.Community;
using Capstone_Project.DTOs.Feed.CommunityApplicationUser;
using Capstone_Project.DTOs.Feed.FavouriteList;
using Capstone_Project.DTOs.Feed.FriendList;
using Capstone_Project.DTOs.Feed.Post;
using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.DTOs.Feed.Review;
using Capstone_Project.DTOs.Feed.Videogame;

namespace Capstone_Project.DTOs.Feed.ApplicationUser;

public class ApplicationUserDto
{
    [Required]
    public required string ApplicationUserId
    {
        get; set;
    }

    [Required]
    public required string FirstName
    {
        get; set;
    }

    [Required]
    public required string LastName
    {
        get; set;
    }

    [Required]
    public required DateOnly BirthDate
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

    public string? DisplayName
    {
        get; set;
    }

    public string? Avatar
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

    public bool? IsGamer
    {
        get; set;
    }

    public bool? IsDeveloper
    {
        get; set;
    }

    public bool? IsEditor
    {
        get; set;
    }

    public string? DeveloperRole
    {
        get; set;
    }

    public string? Bio
    {
        get; set;
    }

    public string? Title
    {
        get; set;
    }

    public bool? IsHidden
    {
        get; set;
    }

    public bool? IsFavouriteListPrivate
    {
        get; set;
    }

    public bool? IsFriendListPrivate
    {
        get; set;
    }

    public bool? AutoAcceptFriendRequests
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
    }

    public DateTime? DeletedAt
    {
        get; set;
    }

    public ICollection<ApplicationUser_PostDto> Posts
    {
        get; set;
    }

    public ICollection<ApplicationUser_CommentDto> Comments
    {
        get; set;
    }

    public ICollection<ApplicationUser_PostLikeDto> PostLikes
    {
        get; set;
    }

    public ICollection<ApplicationUser_CommentLikeDto> CommentLikes
    {
        get; set;
    }

    public ICollection<ApplicationUser_VideogameDto> Videogames
    {
        get; set;
    }

    public ICollection<ApplicationUser_ReviewDto> Reviews
    {
        get; set;
    }

    public ICollection<ApplicationUser_CommunityDto> Communities
    {
        get; set;
    }

    public ICollection<ApplicationUser_CommunityApplicationUserDto> CommunityApplicationUsers
    {
        get; set;
    }

    public ICollection<ApplicationUser_ApplicationUserFriendDto> ApplicationUserFriends
    {
        get; set;
    }

    public ApplicationUser_CartDto Cart
    {
        get; set;
    }

    public ApplicationUser_FavouriteListDto FavouriteList
    {
        get; set;
    }

    public ApplicationUser_FriendListDto FriendList
    {
        get; set;
    }
}
