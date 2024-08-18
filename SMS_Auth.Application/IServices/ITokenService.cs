using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
        bool IsTokenExpired(string token);
    }
}
