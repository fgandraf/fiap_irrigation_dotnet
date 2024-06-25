using Irrigation.Core.Models;

namespace Irrigation.Core.Contracts.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}