using MediatR;
using SMS_Auth.Application.Features.AuthFeatures.Command.Commands;
using SMS_Auth.Application.IServices;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Features.AuthFeatures.Command.Handlers
{
    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponseDto>
    {
        private readonly IAuthService _authService;
        public LoginRequestCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginResponseDto> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LoginResponseDto loginResponseDto= await _authService.Login(request);
                return loginResponseDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Hanlder : "+ex.ToString());
            }
        }
    }
}
