using System;

namespace RestClientBookServiceCore
{

    class Program
    {
        static void Main(string[] args)
        {
            TestBookServiceJSON tjson = new TestBookServiceJSON();
            tjson.RunTestJSON();

        }
    }
}
