using System.Security.Cryptography;
using System.Text;

namespace LibraryManagement.Api.Core;

public static class CryptographyTools
{
    public static byte[] GetBytes(byte[] bytes, byte[]? salt = null, int iterations = 10_000, int size = 64)
    {
        salt ??= [];
        return Rfc2898DeriveBytes.Pbkdf2(bytes, salt, iterations, HashAlgorithmName.SHA512, size);
    }

    public static byte[] GetBytes(string text, byte[]? salt = null, int iterations = 10_000, int size = 64)
    {
        return GetBytes(Encoding.UTF8.GetBytes(text), salt, iterations, size);
    }
}