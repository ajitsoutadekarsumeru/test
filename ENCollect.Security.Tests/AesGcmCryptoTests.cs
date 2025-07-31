using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using ENCollect.Security;

namespace ENCollect.Security.Tests
{
    public class AesGcmCryptoTests
    {
        // Example 16-byte key (128-bit). 
        // In production, retrieve from a secure store or config. 
        private readonly byte[] _testKey = new byte[]
        {
            0x01, 0x02, 0x03, 0x04,
            0x05, 0x06, 0x07, 0x08,
            0x09, 0x0A, 0x0B, 0x0C,
            0x0D, 0x0E, 0x0F, 0x10
        };

        private readonly AesGcmCrypto _crypto;

        public AesGcmCryptoTests()
        {
            _crypto = new AesGcmCrypto();
        }

        [Fact]
        public void EncryptDecrypt_ValidInputs_ReturnsOriginal()
        {
            // Arrange
            string plainText = "Hello, AES-GCM!";

            // Act
            string cipher = _crypto.Encrypt(plainText, _testKey);
            string decrypted = _crypto.Decrypt(cipher, _testKey);

            // Assert
            Assert.Equal(plainText, decrypted);
        }

        [Fact]
        public void Decrypt_InvalidBase64_ThrowsFormatException()
        {
            // Arrange
            string invalidBase64 = "#NOT_BASE64#";

            // Act & Assert
            Assert.Throws<FormatException>(() => _crypto.Decrypt(invalidBase64, _testKey));
        }

        [Fact]
        public void Decrypt_TooShortData_ThrowsCryptographicException()
        {
            // Arrange
            // We'll produce a Base64 string that decodes to only a couple of bytes,
            // too small to even read [IV size + tag size + IV + ciphertext].
            byte[] tinyBytes = new byte[] { 0x01, 0x02 }; 
            string base64Tiny = Convert.ToBase64String(tinyBytes);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _crypto.Decrypt(base64Tiny, _testKey));
        }

        [Fact]
        public void Decrypt_WrongKey_ThrowsCryptographicException()
        {
            // Arrange
            string plainText = "Sensitive Data";
            string cipher = _crypto.Encrypt(plainText, _testKey);

            // A different key of the same length
            byte[] wrongKey = new byte[]
            {
                0x99, 0x88, 0x77, 0x66,
                0x55, 0x44, 0x33, 0x22,
                0x11, 0x00, 0xAA, 0xBB,
                0xCC, 0xDD, 0xEE, 0xFF
            };

            // Act & Assert
            // .NET AES-GCM typically throws CryptographicException if final auth check fails
            Assert.Throws<AuthenticationTagMismatchException>(() => _crypto.Decrypt(cipher, wrongKey));
        }

        [Fact]
        public void Decrypt_TamperedData_ThrowsCryptographicException()
        {
            // Arrange
            string plainText = "Test Tampering!";
            string cipher = _crypto.Encrypt(plainText, _testKey);

            byte[] rawData = Convert.FromBase64String(cipher);

            // We'll flip one byte in the [ciphertext+tag] region. 
            // The first 2 bytes are ivSize & tagSize, next 16 bytes is IV, so start flipping after that
            int flipIndex = 1 + 1 + 16; // skip 1 byte ivSize, 1 byte tagSize, 16 bytes IV
            if (flipIndex < rawData.Length)
            {
                rawData[flipIndex] ^= 0xFF; // flip bits
            }

            string tamperedCipher = Convert.ToBase64String(rawData);

            // Act & Assert
            // GCM's auth check should fail
            Assert.Throws<AuthenticationTagMismatchException>(() => _crypto.Decrypt(tamperedCipher, _testKey));
        }
    }
}
