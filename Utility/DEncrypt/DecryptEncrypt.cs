using System.Text;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
namespace Utility.DEncrypt
{
    /// <summary>
    /// DecryptEncrypt 的摘要说明
    /// 作用：实现对称加密、解密
    /// </summary>
    public class DecryptEncrypt
    {
        private SymmetricAlgorithm mobjCryptoService;
        private string Key;

        public DecryptEncrypt()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "rrp(%&h70x89H$jgsfgfsI0456Ftma81&fvHrr&&76*h%(12lJ$lhj!y6&(*jkPer44a";
        }

        #region 获得密钥
        /**/
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string _TempKey = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (_TempKey.Length > KeyLength)
                _TempKey = _TempKey.Substring(0, KeyLength);
            else if (_TempKey.Length < KeyLength)
                _TempKey = _TempKey.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(_TempKey);
        }
        #endregion

        #region 获得初始向量
        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string _TempIV = "@afetj*Ghg7!rNIfsgr95GUqd9gsrb#GG7HBh(urjj6HJ($jhWk7&!hjjri%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (_TempIV.Length > IVLength)
                _TempIV = _TempIV.Substring(0, IVLength);
            else if (_TempIV.Length < IVLength)
                _TempIV = _TempIV.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(_TempIV);
        }
        #endregion

        #region 加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            //创建对称加密器对象
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        #endregion

        #region 解密方法
        /**/
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            //创建对称解密器对象
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        #endregion

    }
}
