using System;

namespace TestingPrinciples
{
    class Human
    {
        private string fullName;
        private string university;
        public Human(string fullName,string university) 
        {
            this.fullName = fullName;
            this.university = university;
        }

        public override string ToString()
        {
            return $"{this.fullName} studies at {this.university}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string name, university;
            string[] line = Console.ReadLine().Split(' ');
            name = line[0];
            university = line[1];

            Human human = new Human(name,university);

            Console.WriteLine(human);
        }
    }
}
