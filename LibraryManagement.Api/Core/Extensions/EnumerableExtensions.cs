using System.Security.Claims;

namespace LibraryManagement.Api.Core.Extensions;

public static class EnumerableExtensions
{
    public static string GetUserId(this IEnumerable<Claim> claims) => claims.Single(p => p.Type == "userId").Value;
    public static DateTimeOffset GetRefreshExpires(this IEnumerable<Claim> claims)
    {
        var single = claims.Single(p => p.Type == "refreshExpires").Value;
        var unixTimeSeconds = long.Parse(single);
        return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
    }

    public static string ToHexString(this IEnumerable<byte> bytes)
    {
        return string.Concat(bytes.Select(p => p.ToString("x2")));
    }
}