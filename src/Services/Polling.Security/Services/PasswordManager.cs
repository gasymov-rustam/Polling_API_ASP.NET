using Polling.Security.Constants;
using System.Security.Cryptography;

namespace Polling.Security.Services
{
    public class PasswordManager : IPasswordManager
    {
        public string Secure(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SecurityConstants._saltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                SecurityConstants._iterations,
                SecurityConstants._algorithm,
                SecurityConstants._keySize
            );

            return string.Join(
                SecurityConstants.segmentDelimiter,
                Convert.ToHexString(key),
                Convert.ToHexString(salt),
                SecurityConstants._iterations,
                SecurityConstants._algorithm
            );
        }

        public bool Validate(string password, string passwordHash)
        {
            var segments = passwordHash.Split(SecurityConstants.segmentDelimiter);
            var key = Convert.FromHexString(segments[0]);
            var salt = Convert.FromHexString(segments[1]);
            var iterations = int.Parse(segments[2]);
            var algorithm = new HashAlgorithmName(segments[3]);

            var inputSecretKey = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                algorithm,
                key.Length
            );

            return key.SequenceEqual(inputSecretKey);
        }
    }
}
