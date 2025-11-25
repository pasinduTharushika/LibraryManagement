using System.Security.Cryptography;
using System.Text;

public static class HashHelper
{
    public static string ToMd5Hash(this string input)
    {
        using var md5 = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(bytes);

        return Convert.ToHexString(hashBytes); // returns uppercase hex string
    }
}
