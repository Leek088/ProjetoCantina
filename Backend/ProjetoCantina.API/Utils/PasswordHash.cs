using System.Security.Cryptography;

namespace ProjetoCantina.API.Utils;

public static class PasswordHash
{
    private const int SALT_BYTE_SIZE = 24;
    private const int HASH_BYTE_SIZE = 24;
    private const int PBKDF2_ITERATIONS = 1000;

    private const int ITERATION_INDEX = 0;
    private const int SALT_INDEX = 1;
    private const int PBKDF2_INDEX = 2;

    public static string CreateHash(string password)
    {
        // Generate a random salt
#pragma warning disable SYSLIB0023 // O tipo ou membro é obsoleto
        RNGCryptoServiceProvider csprng = new();
#pragma warning restore SYSLIB0023 // O tipo ou membro é obsoleto

        byte[] salt = new byte[SALT_BYTE_SIZE];
        csprng.GetBytes(salt);

        // Hash the password and encode the parameters
        byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
        return PBKDF2_ITERATIONS + ":" +
            Convert.ToBase64String(salt) + ":" +
            Convert.ToBase64String(hash);
    }

    public static bool ValidatePassword(string password, string correctHash)
    {
        // Extract the parameters from the hash
        char[] delimiter = [':'];
        string[] split = correctHash.Split(delimiter);
        int iterations = Int32.Parse(split[ITERATION_INDEX]);
        byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
        byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

        byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
        return SlowEquals(hash, testHash);
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
            diff |= (uint)(a[i] ^ b[i]);
        return diff == 0;
    }

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
    {
#pragma warning disable SYSLIB0041 // O tipo ou membro é obsoleto
        Rfc2898DeriveBytes pbkdf2 = new(password, salt)
        {
#pragma warning restore SYSLIB0041 // O tipo ou membro é obsoleto

            IterationCount = iterations
        };
        return pbkdf2.GetBytes(outputBytes);
    }
}
