using ENTiger.ENCollect;
using System.Security.Cryptography;
using System.Text;

namespace ENCollect.Security;

/// <summary>
/// LEGACY code: Class has been renamed temporarily.
///  It should be retired once AesGcmCrypto rolls out and is stable
/// </summary>
public class LegacyAesCrypto
{
    //private static readonly string EncryptionIv = AppConfigManager.AppSettings["EncryptionIV"] != null
    //    ? AppConfigManager.AppSettings["EncryptionIV"].ToString()
    //    : string.Empty;

    //public static string DecryptStringAes(string cipherText, string Key)
    //{
    //    var keybytes = Encoding.UTF8.GetBytes(Key);
    //    var iv = Encoding.UTF8.GetBytes(EncryptionIv);

    //    var encrypted = Convert.FromBase64String(cipherText);
    //    var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
    //    return string.Format(decriptedFromJavascript);
    //}

    //private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    //{
    //    // Check arguments.
    //    if (cipherText == null || cipherText.Length <= 0)
    //    {
    //        throw new ArgumentNullException("cipherText");
    //    }

    //    if (key == null || key.Length <= 0)
    //    {
    //        throw new ArgumentNullException("key");
    //    }

    //    if (iv == null || iv.Length <= 0)
    //    {
    //        throw new ArgumentNullException("key");
    //    }

    //    // Declare the string used to hold
    //    // the decrypted text.
    //    string plaintext = null;

    //    // Create an RijndaelManaged object
    //    // with the specified key and IV.
    //    using (var rijAlg = new RijndaelManaged())
    //    {
    //        //Settings
    //        rijAlg.Mode = System.Security.Cryptography.CipherMode.CBC;
    //        rijAlg.Padding = PaddingMode.PKCS7; //.PKCS7;
    //        rijAlg.FeedbackSize = 128;

    //        rijAlg.Key = key;
    //        rijAlg.IV = iv;

    //        // Create a decrytor to perform the stream transform.
    //        var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
    //        try
    //        {
    //            // Create the streams used for decryption.
    //            using (var msDecrypt = new MemoryStream(cipherText))
    //            {
    //                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {
    //                    using (var srDecrypt = new StreamReader(csDecrypt))
    //                    {
    //                        // Read the decrypted bytes from the decrypting stream
    //                        // and place them in a string.
    //                        plaintext = srDecrypt.ReadToEnd();
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            plaintext = "keyError";
    //        }
    //    }

    //    return plaintext;
    //}
}