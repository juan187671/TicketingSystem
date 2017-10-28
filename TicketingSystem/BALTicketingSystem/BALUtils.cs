using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TicketingSystem.DALTicketingSystem;

namespace TicketingSystem.BALTicketingSystem
{
    public class BALUtils
    {
        public string GetMD5(string pPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalArrayByte = ASCIIEncoding.Default.GetBytes(pPassword);
            Byte[] encodedArrayByte = md5.ComputeHash(originalArrayByte);
            string retorno = BitConverter.ToString(encodedArrayByte);
            return retorno.Replace("-", "");
        }

        internal List<string> GetListOfStatus()
        {
            return new DALUtils().GetListOfStatus();
        }
    }
}