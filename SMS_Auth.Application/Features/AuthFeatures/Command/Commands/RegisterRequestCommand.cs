using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Features.AuthFeatures.Command.Commands
{
    public class RegisterRequestCommand : IRequest<IdentityResult>
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
