using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

/// <summary>
/// Password hashing for admin credentials: bcrypt (BouncyCastle) with legacy PBKDF2 and plain-text verify for migration.
/// </summary>
public static class PasswordHelper {

    public const string Pbkdf2HashPrefix = "PBKDF2$";
    private const int Pbkdf2SaltSize = 16;
    private const int Pbkdf2KeySize = 32;
    private const int Pbkdf2Iterations = 100000;
    private const int BcryptWorkFactor = 12;

    public static string HashPassword(string password) {
        if (password == null) {
            throw new ArgumentNullException("password");
        }
        var chars = password.ToCharArray();
        try {
            var random = new SecureRandom();
            return OpenBsdBCrypt.Generate(chars, random, BcryptWorkFactor);
        }
        finally {
            Array.Clear(chars, 0, chars.Length);
        }
    }

    /// <summary>
    /// Verifies against bcrypt, legacy PBKDF2$... or legacy plain-text.
    /// </summary>
    public static bool Verify(string password, string stored) {
        if (string.IsNullOrEmpty(stored) || password == null) {
            return false;
        }
        if (stored.StartsWith("$2", StringComparison.Ordinal)) {
            var chars = password.ToCharArray();
            try {
                return OpenBsdBCrypt.CheckPassword(stored, chars);
            }
            finally {
                Array.Clear(chars, 0, chars.Length);
            }
        }
        if (stored.StartsWith(Pbkdf2HashPrefix, StringComparison.Ordinal)) {
            return VerifyPbkdf2(password, stored);
        }
        return string.Equals(password, stored, StringComparison.Ordinal);
    }

    private static bool VerifyPbkdf2(string password, string stored) {
        try {
            var body = stored.Substring(Pbkdf2HashPrefix.Length);
            var parts = body.Split('$');
            if (parts.Length != 3) {
                return false;
            }
            var iterations = int.Parse(parts[0]);
            var salt = FromB64(parts[1]);
            var expected = FromB64(parts[2]);
            if (salt == null || expected == null) {
                return false;
            }
            var key = Pbkdf2(password, salt, iterations, expected.Length);
            return FixedTimeEquals(key, expected);
        }
        catch {
            return false;
        }
    }

    private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int keyByteCount) {
        using (var derive = new Rfc2898DeriveBytes(password, salt, iterations)) {
            return derive.GetBytes(keyByteCount);
        }
    }

    private static byte[] FromB64(string s) {
        try {
            return Convert.FromBase64String(s);
        }
        catch {
            return null;
        }
    }

    private static bool FixedTimeEquals(byte[] a, byte[] b) {
        if (a == null || b == null || a.Length != b.Length) {
            return false;
        }
        var diff = 0;
        for (var i = 0; i < a.Length; i++) {
            diff |= a[i] ^ b[i];
        }
        return diff == 0;
    }
}
