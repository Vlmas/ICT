using System;

namespace Problem1
{
    class Program
    {
        public static int[] RunningSum(int[] nums)
        {
            int[] res = new int[nums.Length];

            for(int i=0;i<nums.Length;i++)
            {
                res[i] = nums[i];

                if(i != 0)
                {
                    res[i] += res[i - 1];
                }
            }

            return res;
        }

        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] ar = new int[n];

            string elem = Console.ReadLine();
            string[] elems = elem.Split(" ");

            for(int i=0;i<ar.Length;i++)
            {
                ar[i] = int.Parse(elems[i]);
            }

            int[] res = RunningSum(ar);

            foreach(var i in res)
            {
                Console.Write(i + " ");
            }
        }
    }
}
