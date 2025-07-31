using System;
using System.Security.Cryptography;
using System.Text;
using ENTiger.Security;

namespace ENCollect.Security
{
    public enum CipherMode
    {
        CBC,
        GCM
    }

    public enum Padding
    {
        NoPadding,
        PKCS7
    }

    /// <summary>
    /// A stateless AES-GCM implementation using .NET's AesGcm.
    /// 
    /// Key is passed per call (no constructor parameters for key).
    /// By default, it uses a 12-byte IV (nonce) and a 16-byte (128-bit) tag.
    /// 
    /// NOTE: The Angular code uses a 16-byte IV on the front-end. (as on 27th Dec 2024)
    ///       We've switched to 12 bytes here for compliance with standard GCM usage.
    ///       TODO: The Angular code must also shift to a 12-byte IV so both sides match.
    /// </summary>
    public class AesGcmCrypto : IAesGcmCrypto
    {
        /// <summary>
        /// We now fix the IV (nonce) size at 12 bytes (96 bits), 
        /// which is the recommended size for AES-GCM in most .NET frameworks.
        /// </summary>
        private const int AesIvSize = 12; 

        /// <summary>
        /// We keep a 16-byte tag (128-bit), which is standard for GCM.
        /// </summary>
        private const int GcmTagSize = 16;  

        /// <summary>
        /// Encrypts the given plaintext using .NET AesGcm, 
        /// storing output in the custom format:
        ///   [1 byte: ivSize][1 byte: tagSize][iv][ciphertext+tag]
        /// then returns the result as a Base64 string.
        /// 
        /// Key is provided as a UTF-8 encoded byte array.
        /// </summary>
        public string Encrypt(string plainText, byte[] key)
        {
            // 1) Generate a random 12-byte IV
            var iv = GenerateRandomIv(AesIvSize);

            // 2) Convert plaintext to bytes
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            // 3) Prepare arrays for ciphertext and tag
            byte[] ciphertext = new byte[plainBytes.Length];
            byte[] tag = new byte[GcmTagSize];

            // 4) Encrypt with AesGcm (tagSize = 16 bytes)
            using (var aesGcm = new AesGcm(key, 16))
            {
                aesGcm.Encrypt(iv, plainBytes, ciphertext, tag);
            }

            // 5) Pack into [1 byte ivSize][1 byte tagSize][iv][ciphertext+tag]
            var packed = PackCipherData(ciphertext, tag, iv);

            // 6) Convert to Base64
            return Convert.ToBase64String(packed);
        }

        /// <summary>
        /// Decrypts a Base64 string in the format 
        ///   [1 byte ivSize][1 byte tagSize][iv][ciphertext+tag],
        /// using .NET's AesGcm under the hood.
        /// Key is provided as a UTF-8 encoded byte array.
        /// </summary>
        public string Decrypt(string cipherText, byte[] key)
        {
            // 1) Base64 decode
            var data = Convert.FromBase64String(cipherText);

            // 2) Unpack into (encryptedBytes, iv, tagSize)
            var (encryptedBytes, iv, tagSize) = UnpackCipherData(data);

            // 3) Separate out ciphertext vs. tag
            int cipherLen = encryptedBytes.Length - tagSize;
            if (cipherLen < 0)
            {
                throw new CryptographicException("Invalid cipher/tag data length.");
            }

            byte[] ciphertext = new byte[cipherLen];
            byte[] tag = new byte[tagSize];

            Buffer.BlockCopy(encryptedBytes, 0, ciphertext, 0, cipherLen);
            Buffer.BlockCopy(encryptedBytes, cipherLen, tag, 0, tagSize);

            // 4) Decrypt
            byte[] plainBytes = new byte[cipherLen];
            using (var aesGcm = new AesGcm(key, 16)) 
            {
                aesGcm.Decrypt(iv, ciphertext, tag, plainBytes);
            }

            // 5) Convert back to string (UTF-8)
            return Encoding.UTF8.GetString(plainBytes);
        }

        // -------------------------------------------------------
        // Private Helpers
        // -------------------------------------------------------

        /// <summary>
        /// Generates a random IV of the given size (in bytes) 
        /// using a cryptographically secure RNG.
        /// </summary>
        private static byte[] GenerateRandomIv(int size)
        {
            byte[] iv = new byte[size];
            RandomNumberGenerator.Fill(iv);
            return iv;
        }

        /// <summary>
        /// Packs data into [1 byte: ivSize][1 byte: tagSize][iv][ciphertext+tag].
        /// 
        /// Currently uses a 12-byte IV and 16-byte tag by default, 
        /// but the first 2 bytes store the actual lengths. 
        /// </summary>
        private static byte[] PackCipherData(byte[] ciphertext, byte[] tag, byte[] iv)
        {
            byte ivSize = (byte)iv.Length;     // e.g. 12 
            byte tagSize = (byte)tag.Length;   // e.g. 16

            int finalSize = 1 + 1 + iv.Length + ciphertext.Length + tag.Length;
            byte[] finalData = new byte[finalSize];
            int index = 0;

            // Write ivSize, then tagSize
            finalData[index++] = ivSize;
            finalData[index++] = tagSize;

            // Write the IV
            Buffer.BlockCopy(iv, 0, finalData, index, iv.Length);
            index += iv.Length;

            // Write [ciphertext + tag]
            Buffer.BlockCopy(ciphertext, 0, finalData, index, ciphertext.Length);
            index += ciphertext.Length;

            Buffer.BlockCopy(tag, 0, finalData, index, tag.Length);

            return finalData;
        }

        /// <summary>
        /// Unpacks the raw byte array into (cipher+tag, iv, tagSize).
        /// Format: [1 byte ivSize][1 byte tagSize][IV][ciphertext+tag]
        /// 
        /// The returned 'encryptedBytes' is the concatenation of ciphertext+tag,
        /// so you'll need to slice out the final 'tagSize' bytes yourself.
        /// </summary>
        private static (byte[] encryptedBytes, byte[] iv, byte tagSize) UnpackCipherData(byte[] rawData)
        {
            int index = 0;
            byte ivSize = rawData[index++];
            byte tagSize = rawData[index++];

            byte[] iv = new byte[ivSize];
            Buffer.BlockCopy(rawData, index, iv, 0, ivSize);
            index += ivSize;

            byte[] encryptedBytes = new byte[rawData.Length - index];
            Buffer.BlockCopy(rawData, index, encryptedBytes, 0, encryptedBytes.Length);

            return (encryptedBytes, iv, tagSize);
        }
    }
}
