namespace ENTiger.Security;

/// <summary>
/// A simple contract for AES encryption and decryption operations.
/// </summary>
internal interface IAesGcmCrypto
{
    /// <summary>
    /// Encrypts a given plain text with the provided key.
    /// </summary>
    /// <param name="plainText">Plain text to encrypt.</param>
    /// <param name="key">The encryption key.</param>
    /// <returns>Base64-encoded ciphertext.</returns>
    string Encrypt(string plainText, byte[] key);

    /// <summary>
    /// Decrypts a given cipher text with the provided key.
    /// </summary>
    /// <param name="cipherText">Cipher text to decrypt (Base64-encoded string).</param>
    /// <param name="key">The decryption key.</param>
    /// <returns>Decrypted plain text.</returns>
    string Decrypt(string cipherText, byte[] key);
}