using System.Security.Claims;

namespace LibraryManagement.Api.Core.Extensions;

public static class EnumerableExtensions
{
    public static string GetUserId(this IEnumerable<Claim> claims) => claims.Single(p => p.Type == "userId").Value;

    public static string ToHexString(this IEnumerable<byte> bytes)
    {
        return string.Concat(bytes.Select(p => p.ToString("x2")));
    }
}