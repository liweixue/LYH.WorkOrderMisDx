using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LYH.WorkOrder.share
{
    /// <summary>
    /// DecryptEncrypt 的摘要说明
    /// 作用：实现对称加密、解密
    /// </summary>
    public class DecryptEncrypt : IDisposable
    {
        private SymmetricAlgorithm _mobjCryptoService;
        private string _key;

        public void Dispose()
        {
        }

        public DecryptEncrypt()
        {
            _mobjCryptoService = new RijndaelManaged();
            _key = "rrp(%&h70x89H$jgsfgfsI0456Ftma81&fvHrr&&76*h%(12lJ$lhj!y6&(*jkPer44a";
        }

        #region 获得密钥
        /**/
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            var tempKey = _key;
            _mobjCryptoService.GenerateKey();
            var bytTemp = _mobjCryptoService.Key;
            var keyLength = bytTemp.Length;
            if (tempKey.Length > keyLength)
                tempKey = tempKey.Substring(0, keyLength);
            else if (tempKey.Length < keyLength)
                tempKey = tempKey.PadRight(keyLength, ' ');
            return Encoding.ASCII.GetBytes(tempKey);
        }
        #endregion

        #region 获得初始向量
        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIv()
        {
            var tempIv = "@afetj*Ghg7!rNIfsgr95GUqd9gsrb#GG7HBh(urjj6HJ($jhWk7&!hjjri%$hjk";
            _mobjCryptoService.GenerateIV();
            var bytTemp = _mobjCryptoService.IV;
            var ivLength = bytTemp.Length;
            if (tempIv.Length > ivLength)
                tempIv = tempIv.Substring(0, ivLength);
            else if (tempIv.Length < ivLength)
                tempIv = tempIv.PadRight(ivLength, ' ');
            return Encoding.ASCII.GetBytes(tempIv);
        }
        #endregion

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string Encrypto(string source)
        {
            var bytIn = Encoding.UTF8.GetBytes(source);
            var ms = new MemoryStream();
            _mobjCryptoService.Key = GetLegalKey();
            _mobjCryptoService.IV = GetLegalIv();
            //创建对称加密器对象
            var encrypto = _mobjCryptoService.CreateEncryptor();
            //定义将数据流链接到加密转换的流
            var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            var bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        #endregion
    
        #region 解密方法
        /**/
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public string Decrypto(string source)
        {
            var bytIn = Convert.FromBase64String(source);
            var ms = new MemoryStream(bytIn, 0, bytIn.Length);
            _mobjCryptoService.Key = GetLegalKey();
            _mobjCryptoService.IV = GetLegalIv();
            //创建对称解密器对象
            var encrypto = _mobjCryptoService.CreateDecryptor();
            //定义将数据流链接到加密转换的流
            var cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        #endregion
        
    }
}
