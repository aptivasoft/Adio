using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;

/// <summary>
/// Summary description for AlphaNumericAutoGen
/// </summary>
public class AlphaNumericAutoGen
{
	public AlphaNumericAutoGen()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string NxtKeyCode(string KeyCode)
    {
        byte[] ASCIIValues = ASCIIEncoding.ASCII.GetBytes(KeyCode);
        int StringLength = ASCIIValues.Length;
        bool isAllZed = true;
        bool isAllNine = true;
        //Check if all has ZZZ.... then do nothing just return empty string.

        for (int i = 0; i < StringLength - 1; i++)
        {
            if (ASCIIValues[i] != 90)
            {
                isAllZed = false;
                break;
            }
        }
        if (isAllZed && ASCIIValues[StringLength - 1] == 57)
        {
            ASCIIValues[StringLength - 1] = 64;
        }

        // Check if all has 999... then make it A0
        for (int i = 0; i < StringLength; i++)
        {
            if (ASCIIValues[i] != 57)
            {
                isAllNine = false;
                break;
            }
        }
        if (isAllNine)
        {
            ASCIIValues[StringLength - 1] = 47;
            ASCIIValues[0] = 65;
            for (int i = 1; i < StringLength - 1; i++)
            {
                ASCIIValues[i] = 48;
            }
        }


        for (int i = StringLength; i > 0; i--)
        {
            if (i - StringLength == 0)
            {
                ASCIIValues[i - 1] += 1;
            }
            if (ASCIIValues[i - 1] == 58)
            {
                ASCIIValues[i - 1] = 48;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;
            }
            else if (ASCIIValues[i - 1] == 91)
            {
                ASCIIValues[i - 1] = 65;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;

            }
            else
            {
                break;
            }

        }
        KeyCode = ASCIIEncoding.ASCII.GetString(ASCIIValues);
        return KeyCode;
    }
    public string NxtKeyCod(string KeyCode)
    {
        //int startint = KeyCode.IndexOf("0123456789",0,1);
        StringBuilder sb = new StringBuilder();
        //Regex digitregex = new Regex("^[A-Z])");
        //KeyCode = digitregex.Replace(KeyCode, ""); 

        return KeyCode;
    }
    public string GetRandomString (Random rnd, int length)
    {
        string charPool = "1A2B3C4D5E6F7G8H9I0";
        //ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw xyz1234567890";
        StringBuilder rs = new StringBuilder();        
        rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

        return rs.ToString();
    }

}
