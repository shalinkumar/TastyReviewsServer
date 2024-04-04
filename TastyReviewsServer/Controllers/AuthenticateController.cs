using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TastyReviewsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,IMapper? mapper = null) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;
        private IMapper? _mapper = mapper;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user =  _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false).GetAwaiter().GetResult();
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);
            if (user != null && await _userManager.HasPasswordAsync(user))
            {
                var userRoles =  _userManager.GetRolesAsync(user).ConfigureAwait(false).GetAwaiter().GetResult();
                var role = userRoles.FirstOrDefault();

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
                }


                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    role = userRoles.FirstOrDefault()
                });

            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists =  _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false).GetAwaiter().GetResult();
            if (userExists != null)
                return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" }));

            var user = _mapper.Map<ApplicationUser>(model);
            
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);
            
            var result = _userManager.CreateAsync(user).ConfigureAwait(false).GetAwaiter().GetResult();
            //"User creation failed! Please check user details and try again."
            if (!result.Succeeded)
                return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." }));

            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status200OK, new ResponseModel { Status = "Success", Message = "User created." }));

        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists =  _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false).GetAwaiter().GetResult();
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });
            
            var user = _mapper.Map<ApplicationUser>(model);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);

            var result =  _userManager.CreateAsync(user).ConfigureAwait(false).GetAwaiter().GetResult();
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (! _roleManager.RoleExistsAsync(UserRoles.Admin).ConfigureAwait(false).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).ConfigureAwait(false).GetAwaiter().GetResult();
            if (! _roleManager.RoleExistsAsync(UserRoles.User).ConfigureAwait(false).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(UserRoles.User)).ConfigureAwait(false).GetAwaiter().GetResult();

            if ( _roleManager.RoleExistsAsync(UserRoles.Admin).ConfigureAwait(false).GetAwaiter().GetResult())
            {
                 _userManager.AddToRoleAsync(user, UserRoles.Admin).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseModel { Status = "Success", Message = "User created." });
            //return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-owner-user")]
        public async Task<IActionResult> RegisterOwnerUser([FromBody] RegisterModel model)
        {
            var userExists = _userManager.FindByNameAsync(model.UserName).ConfigureAwait(false).GetAwaiter().GetResult();
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });


            var user = _mapper.Map<ApplicationUser>(model);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);

            var result = _userManager.CreateAsync(user).ConfigureAwait(false).GetAwaiter().GetResult();
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!_roleManager.RoleExistsAsync(UserRoles.Owner).ConfigureAwait(false).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Owner)).ConfigureAwait(false).GetAwaiter().GetResult();

            if (_roleManager.RoleExistsAsync(UserRoles.Owner).ConfigureAwait(false).GetAwaiter().GetResult())
            {
                _userManager.AddToRoleAsync(user, UserRoles.Owner).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseModel { Status = "Success", Message = "User created." });            
        }

    }
}
