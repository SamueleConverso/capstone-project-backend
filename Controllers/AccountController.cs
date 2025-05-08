using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Capstone_Project.DTOs.Account;
using System.Text;
using Capstone_Project.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Capstone_Project.Models.Account;
using Capstone_Project.Models.Feed;
using Capstone_Project.Data;

namespace Capstone_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Jwt _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AccountController(IOptions<Jwt> jwtOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _env = env;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDto registerRequestDto)
        {

            if (registerRequestDto.PictureFile != null && registerRequestDto.PictureFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(registerRequestDto.PictureFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await registerRequestDto.PictureFile.CopyToAsync(stream);
                }

                registerRequestDto.Picture = $"/images/{uniqueName}";
            }
            else
            {
                registerRequestDto.PictureFile = null;
            }

            if (registerRequestDto.CoverFile != null && registerRequestDto.CoverFile.Length > 0)
            {

                var imagesFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(registerRequestDto.CoverFile.FileName)}";
                var filePath = Path.Combine(imagesFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await registerRequestDto.CoverFile.CopyToAsync(stream);
                }

                registerRequestDto.Cover = $"/images/{uniqueName}";
            }
            else
            {
                registerRequestDto.CoverFile = null;
            }

            DateOnly parsedDate;
            DateOnly.TryParse(registerRequestDto.BirthDate, out parsedDate);

            bool.TryParse(registerRequestDto.IsGamer, out var isGamerBool);
            bool.TryParse(registerRequestDto.IsDeveloper, out var isDeveloperBool);
            bool.TryParse(registerRequestDto.IsEditor, out var isEditorBool);
            bool.TryParse(registerRequestDto.IsHidden, out var isHiddenBool);
            bool.TryParse(registerRequestDto.IsFavouriteListPrivate, out var isFavouriteListPrivateBool);
            bool.TryParse(registerRequestDto.IsFriendListPrivate, out var isFriendListPrivateBool);
            bool.TryParse(registerRequestDto.AutoAcceptFriendRequests, out var autoAcceptFriendRequestsBool);

            var user = new ApplicationUser
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                BirthDate = parsedDate,
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Email,
                Country = registerRequestDto.Country,
                City = registerRequestDto.City,
                DisplayName = registerRequestDto.DisplayName,
                Avatar = registerRequestDto.Avatar,
                Picture = registerRequestDto.Picture,
                Cover = registerRequestDto.Cover,
                IsGamer = isGamerBool,
                IsDeveloper = isDeveloperBool,
                IsEditor = isEditorBool,
                DeveloperRole = registerRequestDto.DeveloperRole,
                Bio = registerRequestDto.Bio,
                Title = registerRequestDto.Title,
                IsHidden = isHiddenBool,
                IsFavouriteListPrivate = isFavouriteListPrivateBool,
                IsFriendListPrivate = isFriendListPrivateBool,
                AutoAcceptFriendRequests = autoAcceptFriendRequestsBool,
                IsDeleted = false
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Errore nella registrazione dell'utente"
                });
            }

            var userForRole = await _userManager.FindByEmailAsync(user.Email);

            await _userManager.AddToRoleAsync(userForRole, "User");

            var cart = new Cart
            {
                ApplicationUserId = userForRole.Id
            };

            var favouriteList = new FavouriteList
            {
                ApplicationUserId = userForRole.Id
            };

            var friendList = new FriendList
            {
                ApplicationUserId = userForRole.Id
            };

            await _context.Carts.AddAsync(cart);
            await _context.FavouriteList.AddAsync(favouriteList);
            await _context.FriendList.AddAsync(friendList);

            var resultSave = await SaveAsync();

            if (!resultSave)
            {
                return BadRequest(new
                {
                    message = "Errore nella registrazione dell'utente"
                });
            }

            return Ok(new
            {
                message = "Registrazione avvenuta con successo"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto."
                });
            }

            //bool rememberMe = loginRequestDto.RememberMe ?? false;

            await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            // List<Claim> claims = new List<Claim>();

            // //claims.Add(new Claim(ClaimTypes.Email, user.Email));
            // claims.Add(new Claim("email", user.Email));
            // //claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            // claims.Add(new Claim("firstName", $"{user.FirstName}"));
            // claims.Add(new Claim("lastName", $"{user.LastName}"));
            // //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            // claims.Add(new Claim("id", user.Id));

            // claims.Add(new Claim("displayName", user.DisplayName ?? string.Empty));
            // claims.Add(new Claim("avatar", user.Avatar ?? string.Empty));
            // claims.Add(new Claim("picture", user.Picture ?? string.Empty));
            // claims.Add(new Claim("cover", user.Cover ?? string.Empty));
            // claims.Add(new Claim("isGamer", user.IsGamer.ToString() ?? string.Empty));
            // claims.Add(new Claim("isDeveloper", user.IsDeveloper.ToString() ?? string.Empty));
            // claims.Add(new Claim("isEditor", user.IsEditor.ToString() ?? string.Empty));
            // claims.Add(new Claim("developerRole", user.DeveloperRole ?? string.Empty));
            // claims.Add(new Claim("bio", user.Bio ?? string.Empty));
            // claims.Add(new Claim("title", user.Title ?? string.Empty));
            // claims.Add(new Claim("isHidden", user.IsHidden.ToString() ?? string.Empty));
            // claims.Add(new Claim("isFavouriteListPrivate", user.IsFavouriteListPrivate.ToString() ?? string.Empty));
            // claims.Add(new Claim("isFriendListPrivate", user.IsFriendListPrivate.ToString() ?? string.Empty));
            // claims.Add(new Claim("autoAcceptFriendRequests", user.AutoAcceptFriendRequests.ToString() ?? string.Empty));
            // claims.Add(new Claim("isDeleted", user.IsDeleted.ToString() ?? string.Empty));
            // claims.Add(new Claim("deletedAt", user.DeletedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty));
            // claims.Add(new Claim("createdAt", user.CreatedAt.ToString("yyyy-MM-dd") ?? string.Empty));
            // claims.Add(new Claim("updatedAt", user.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty));
            // claims.Add(new Claim("country", user.Country ?? string.Empty));
            // claims.Add(new Claim("city", user.City ?? string.Empty));
            // claims.Add(new Claim("birthDate", user.BirthDate.ToString("yyyy-MM-dd") ?? string.Empty));

            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email ?? string.Empty),
                new Claim("firstName", user.FirstName ?? string.Empty),
                new Claim("lastName", user.LastName ?? string.Empty),
                new Claim("id", user.Id),
                new Claim("displayName", user.DisplayName ?? string.Empty),
                new Claim("avatar", user.Avatar ?? string.Empty),
                new Claim("picture", user.Picture ?? string.Empty),
                new Claim("cover", user.Cover ?? string.Empty),
                new Claim("isGamer", user.IsGamer?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("isDeveloper", user.IsDeveloper?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("isEditor", user.IsEditor?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("developerRole", user.DeveloperRole ?? string.Empty),
                new Claim("bio", user.Bio ?? string.Empty),
                new Claim("title", user.Title ?? string.Empty),
                new Claim("isHidden", user.IsHidden?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("isFavouriteListPrivate", user.IsFavouriteListPrivate?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("isFriendListPrivate", user.IsFriendListPrivate?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("autoAcceptFriendRequests", user.AutoAcceptFriendRequests?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("isDeleted", user.IsDeleted?.ToString().ToLower() ?? "false".ToLower()),
                new Claim("deletedAt", user.DeletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty),
                new Claim("createdAt", user.CreatedAt.ToString("yyyy-MM-dd")),
                new Claim("updatedAt", user.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty),
                new Claim("country", user.Country ?? string.Empty),
                new Claim("city", user.City ?? string.Empty),
                new Claim("birthDate", user.BirthDate.ToString("yyyy-MM-dd") ?? string.Empty)
            };

            //foreach (var role in roles) {
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(_jwtSettings.ExpiresInDays);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, expires: expiry, signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenResponse()
            {
                Token = tokenString,
                Expires = expiry
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Qualcosa è andato storto."
                });
            }

            //await _signInManager.SignOutAsync();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Errore nella cancellazione dell'application user"
                });
            }

            return Ok(new
            {
                message = "Cancellazione dell'application user avvenuta con successo"
            });
        }
    }
}