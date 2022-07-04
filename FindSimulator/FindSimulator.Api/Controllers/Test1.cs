using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    public class Test1:test1
    {
        public test3 test3;

        public Test1(test3 test3)
        {
            this.test3 = test3;
        }

        public  int Get()
        {
            return test3.x;
        }
    }

    public class Test2:test2
    {
        public test3 test3;

        public Test2(test3 test3)
        {
            this.test3 = test3;
        }

        public int Get()
        {
            return test3.x;
        }

    }
    public class Test3 : test3
    {
        public int x { get; set; } 
        public Test3()
        {

            x = new Random().Next(1, 100);
        }
    }



    public interface test3
    {
        public int x { get; set; }
    }
    public interface test1
    {
      public   int Get();
    }
    public interface test2
    {
      public  int Get();
    }
}
