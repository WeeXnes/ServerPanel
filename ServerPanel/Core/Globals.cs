using System.Security.Cryptography;

namespace ServerPanel.Core;


public static class Globals
{
    public static List<VirtualMachine> globalVms = new List<VirtualMachine>();
    public static string AdminPassword = "";
    public static string ValidToken = "";
    
    
    public static string panel_title = "";
    
    
    
    
    public static string GenerateSecureToken(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[length];
            rng.GetBytes(tokenData);
            return BitConverter.ToString(tokenData).Replace("-", "").ToLower();
        }
    }
}