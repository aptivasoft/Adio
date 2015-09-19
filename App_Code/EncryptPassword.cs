using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Security.Cryptography;
using NLog;

/// <summary>
/// Summary description for EncryptPassword
/// </summary>
public class EncryptPassword
{
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

	public EncryptPassword()
	{ }
    public string EncryptText(string stringtoEncrypt, string Password)
    {
        string EncryptedData = "";
        try
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(stringtoEncrypt);

            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            EncryptedData = Convert.ToBase64String(CipherBytes);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        // Return encrypted string.
        return EncryptedData;
    } 
}
