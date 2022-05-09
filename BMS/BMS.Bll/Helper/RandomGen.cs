using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Bll.Helper
{
    public static class RandomGen
    {
        private static Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        public static string GenerateStr(int length = 6)
        {
            var charArray = Enumerable.Range(0, length).Select(x => chars[random.Next(0, chars.Length - 1)]);
            return string.Join("", charArray);
        }
    }
}
