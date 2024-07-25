﻿using Microsoft.AspNetCore.Identity;
using SMS_Auth.Application.Features.AuthFeatures.Command.Commands;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestCommand loginRequestCommand);
        Task<IdentityResult> Register(RegisterRequestCommand registerRequestCommand);
    }
}
