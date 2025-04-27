using System.Security.Cryptography;
using System.Text;

namespace Quicktup.Util;

public static class Cripto
{
    private static readonly string Key = ConfigurationVariables.CriptoKey;
    private static readonly string InitVector = ConfigurationVariables.CriptoInitVector;
    
    public static string Encrypt(string text)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = Encoding.UTF8.GetBytes(InitVector);

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        MemoryStream ms = new();
        CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
        using(StreamWriter sw = new(cs))
            sw.Write(text);
        
        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string hash)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = Encoding.UTF8.GetBytes(InitVector);

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using MemoryStream ms = new(Convert.FromBase64String(hash));
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new(cs);
        return sr.ReadToEnd();
    }
}