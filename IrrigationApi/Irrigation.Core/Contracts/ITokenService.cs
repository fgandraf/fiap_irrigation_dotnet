using System.Security.Claims;
using Irrigation.Core.Models;

namespace Irrigation.Core.Contracts;

public interface ITokenService
{
    string GenerateToken(User user);
}