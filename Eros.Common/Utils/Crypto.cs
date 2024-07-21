using System.Security.Cryptography;
using System.Text;

namespace Eros.Common.Utils;

public abstract class Crypto
{
  public static string Hash(string input)
  {
    var data = Encoding.UTF8.GetBytes(input);

    var hashBytes = SHA256.HashData(data);

    var builder = new StringBuilder();
    foreach (var t in hashBytes) builder.Append(t.ToString("x2"));

    return builder.ToString();
  }

  public static bool Verify(string input, string hash)
  {
    return Hash(input) == hash;
  }
}
