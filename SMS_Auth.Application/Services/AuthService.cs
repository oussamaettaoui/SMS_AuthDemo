using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SMS_Auth.Application.Features.AuthFeatures.Command.Commands;
using SMS_Auth.Application.IServices;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Props
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        #endregion
        #region Contructor
        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _response = new();
        }
        #endregion
        #region Login Method
        public async Task<LoginResponseDto> Login(LoginRequestCommand loginRequestCommand)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginRequestCommand.Email, loginRequestCommand.Password, false, false);
            if (result.Succeeded)
            {
                User user = await _userManager.FindByEmailAsync(loginRequestCommand.Email);
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                string userRole = userRoles.FirstOrDefault();
                string token = await _tokenService.GenerateJwtToken(user);
                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token,
                    UserRole = userRole
                };
                
                return loginResponseDto;
            }
            return null;
        }
        #endregion
        #region Register Method
        public async Task<IdentityResult> Register(RegisterRequestCommand registerRequestCommand)
        {
            User user = new User { UserName = registerRequestCommand.Email, Email = registerRequestCommand.Email, FirstName = registerRequestCommand.FirstName, LastName = registerRequestCommand.LastName };
            IdentityResult result = await _userManager.CreateAsync(user, registerRequestCommand.Password);
            if (result.Succeeded)
            {
                // check user role if exists
                bool roleExists = await _roleManager.RoleExistsAsync(registerRequestCommand.Role.ToLower());
                // if the role does not exist, create it
                if (!roleExists)
                {
                    IdentityRole userRole = new IdentityRole(registerRequestCommand.Role.ToLower());
                    await _roleManager.CreateAsync(userRole);
                }
                // assign the user to the specified role
                IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(user, registerRequestCommand.Role);
                if (addToRoleResult.Succeeded)
                {
                    return addToRoleResult;
                }
                await _userManager.DeleteAsync(user);
                return addToRoleResult;
            }
            return null;
        }
        #endregion
    }
}
