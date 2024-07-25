using MediatR;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Features.AuthFeatures.Command.Commands
{
    public class LoginRequestCommand : IRequest<LoginResponseDto>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
