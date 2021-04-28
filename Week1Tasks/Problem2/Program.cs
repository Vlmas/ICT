using System;

namespace Problem2
{
    class Program
    {
        public static string DefangIPaddr(string address)
        {
            string res = "";

            for(int i = 0; i < address.Length; i++)
            {
                if(address[i] == '.')
                {
                    res += "[.]";
                } 
                else
                {
                    res += address[i];
                }
                
            }

            return res;
        }

        static void Main(string[] args)
        {
            string address = Console.ReadLine();

            Console.Write(DefangIPaddr(address));
        }
    }
}
