using System.Security.Cryptography;

namespace Polling.Security.Constants
{
    public static class SecurityConstants
    {
        public const int _saltSize = 16;
        public const int _keySize = 32;
        public const int _iterations = 100000;
        public static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;
        public const char segmentDelimiter = ':';
    }
}
