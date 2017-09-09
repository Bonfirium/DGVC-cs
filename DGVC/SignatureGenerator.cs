using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TestProject
{
    static class SignatureGenerator
    {
        public static string Generate(string s)
        {
            byte[] data = new UTF8Encoding().GetBytes(s);
            SHA256 result = new SHA256Managed();
            result.ComputeHash(data);
            string result1 = new UTF8Encoding().GetString(result.ComputeHash(data));
            return result1;
        }
        static string GenerateByValues(string username, DateTime data, string commitId)
        {
            string s = username + ";" + data.ToString() + ";" + commitId;
            return Generate(s);
        }
    }
}
