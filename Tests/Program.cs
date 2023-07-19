using System;
using System.Collections.Generic;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            A a=new C();
            Console.WriteLine(typeof(A).IsAssignableFrom(typeof(C)));
        }

        public class A
        {
            public virtual void M()
            {
                Console.WriteLine("A");
            }
        }
        public class B:A
        {
            public override void M()
            {
                base.M();
                Console.WriteLine("B");
            }
        }
        public class C:B
        {
            public override void M()
            {
                var a = this as A;
                a.M();
                Console.WriteLine("C");
            }
        }
    }
 
}