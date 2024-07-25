using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SMS_Auth.Application.Features.AuthFeatures.Command.Commands;
using SMS_Auth.Application.IServices;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Features.AuthFeatures.Command.Handlers
{
    public class RegisterRequestCommandHandler : IRequestHandler<RegisterRequestCommand, IdentityResult>
    {
        private readonly IAuthService _authService;
        public RegisterRequestCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IdentityResult> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                IdentityResult addToRoleResult = await _authService.Register(request);
                return addToRoleResult;
            }
            catch(Exception ex){
                throw new Exception("Error handler : " +ex.ToString());
            }
        }
    }
}
