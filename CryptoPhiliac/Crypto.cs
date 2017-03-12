using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CryptoPhiliac  
{
    class Crypto 

    {
        #region
        //Class objects & variables
        private String inputText, outputText;
        private byte[] key, iv, encryptedText, cipherText;
        private ICryptoTransform cipherHandler;

        private RijndaelManaged newRijndael = new RijndaelManaged();
        private CryptoStream cStreamHandler;
        private MemoryStream mStream;
        
        #endregion
        
        public Crypto()
        {
            newRijndael.GenerateKey();
            newRijndael.GenerateIV();
            newRijndael.Mode = CipherMode.CBC;
            newRijndael.Padding = PaddingMode.Zeros;
            key = newRijndael.Key;
            iv = newRijndael.IV;
        }


        #region
        //Getter & Setter Methods
        public string INPUTTEXT
        {
            set { inputText = value; }
        }

        public byte[] CIPHERTEXT
        {
            set { cipherText = value; }
        }

        public string OUTPUT
        {
            get { return outputText; }
        }
        #endregion

        #region
        //Encryption Code
        public void encryptText()
        {
            encryptedText = EncryptTextToBytes();

            outputText = displayText(encryptedText);

            string outputFile = @"C:\temp\encyptedText.bin";
                
            try
            {
                BinaryWriter bWriter = new BinaryWriter(File.OpenWrite(outputFile));
                bWriter.Write(encryptedText);
                bWriter.Flush();
                bWriter.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private byte[] EncryptTextToBytes()
        {
            //Use ICrypostream to create an encyption handler.
            cipherHandler = newRijndael.CreateEncryptor(newRijndael.Key, newRijndael.IV);
            
            //Create streams for encryption
            using (mStream = new MemoryStream())
            {
                using (cStreamHandler = new CryptoStream(mStream, cipherHandler, CryptoStreamMode.Write))
                {
                    using (StreamWriter cipherWriter = new StreamWriter(cStreamHandler))
                    {
                        cipherWriter.Write(inputText);
                    }
                    encryptedText =  mStream.ToArray();
                }             
            }

            return encryptedText;
        }
        #endregion


        #region
        //Decryption Code
        public string decryptBytesToString()
        {
            cipherHandler = newRijndael.CreateDecryptor(newRijndael.Key, newRijndael.IV);

            using (mStream = new MemoryStream(cipherText))
            {
                using (cStreamHandler = new CryptoStream(mStream, cipherHandler, CryptoStreamMode.Read))
                {
                    using (StreamReader cipherReader = new StreamReader(cStreamHandler))
                    {
                        outputText = cipherReader.ReadToEnd();
                    }
                }
            }

            return outputText;
        }
        #endregion

        public string displayText(byte[] arrBytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in arrBytes)
            {
                sb.Append(b.ToString());
            }

            return sb.ToString();
        }
    }
}
