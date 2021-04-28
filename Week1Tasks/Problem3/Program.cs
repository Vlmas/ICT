using System;

namespace Problem3
{
    class Program
    {
        public static int NumberOfSteps(int num)
        {
            int cnt = 0;

            while(num != 0)
            {
                if(num%2==0)
                {
                    num /= 2;
                }
                else
                {
                    num -= 1;
                }
                cnt++;
            }

            return cnt;
        }

        static void Main(string[] args)
        {
            int num = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(NumberOfSteps(num));
        }
    }
}
