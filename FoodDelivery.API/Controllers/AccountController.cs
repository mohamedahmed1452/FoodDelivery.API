using AutoMapper;
using FoodDelivery.API.Dtos;
using FoodDelivery.API.Errors;
using FoodDelivery.Core.Entities.Identity;
using FoodDelivery.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public AccountController(SignInManager<ApplicationUser> signinManager,
            UserManager<ApplicationUser> userManager,
            IAuthService authService,
            IMapper mapper)
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            this.authService = authService;
            this.mapper = mapper;

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is null) return BadRequest(new ApiResponse(401, "invalid Login"));
            var res = await signinManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!res.Succeeded) return BadRequest(new ApiResponse(401, "invalid Login"));

            var authDto = await authService.CreateTokenWithRefreshAsync(user, userManager);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = authDto.AccessToken,
                RefreshToken = authDto.RefreshToken,
                RefreshTokenExpiresAt = authDto.RefreshTokenExpiresAt
            });


        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                PhoneNumber = registerDto.Phone
            };
            var userCreate = await userManager.CreateAsync(user, registerDto.Password);
            if (!userCreate.Succeeded) return BadRequest(new ApiValidationErrorResponse()
            { Errors = userCreate.Errors.Select(e => e.Description) }
           );
            var authDto = await authService.CreateTokenWithRefreshAsync(user, userManager);

            return Ok(new UserDto()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Token = authDto.AccessToken,
                RefreshToken = authDto.RefreshToken,
                RefreshTokenExpiresAt = authDto.RefreshTokenExpiresAt
            });


        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto dto)
        {
            // Find user by refresh token (simple version)
            var user = userManager.Users.FirstOrDefault(u => u.RefreshToken == dto.RefreshToken);

            if (user == null)
                return Unauthorized("Invalid refresh token");

            if (user.RefreshTokenExpiryTime == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Refresh token expired");

            // Rotate refresh token (recommended)
            var tokens = await authService.CreateTokenWithRefreshAsync(user, userManager);

            return Ok(tokens);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email ?? string.Empty);
            var authDto = await authService.CreateTokenWithRefreshAsync(user, userManager);

            return Ok(new UserDto()
            {
                DisplayName = user?.DisplayName ?? string.Empty,
                Email = user?.Email ?? string.Empty,
                Token = authDto.AccessToken,
                RefreshToken = authDto.RefreshToken,
                RefreshTokenExpiresAt = authDto.RefreshTokenExpiresAt
            });
        }


        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto?>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == email);
            return Ok(mapper.Map<AddressDto>(user?.Address));

        }
        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(e => e.Address).FirstOrDefaultAsync(u => u.Email == email);
            var address = mapper.Map<Address>(addressDto);
            address.Id = user?.Address?.Id ?? 0;
            user?.Address = address;
            var res = await userManager.UpdateAsync(user);
            if (!res.Succeeded) return BadRequest(new ApiValidationErrorResponse()
            {
                Errors = res.Errors.Select(e => e.Description)
            });
            return Ok(addressDto);
        }



        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Revoke()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await userManager.UpdateAsync(user);
            return Ok("Log Out");
        }


    }
}
