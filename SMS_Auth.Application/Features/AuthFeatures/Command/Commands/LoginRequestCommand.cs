using System.ComponentModel.DataAnnotations;
using MediatR;
using SMS_Auth.Domain.Dtos;

namespace SMS_Auth.Application.Features.AuthFeatures.Command.Commands
{
    public class LoginRequestCommand : IRequest<LoginResponseDto>
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}