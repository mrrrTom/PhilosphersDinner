using ConsoleApp1.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static System.Collections.Specialized.BitVector32;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dinner has started");
            var sticks = new List<Stick>();
            for (int i = 0; i < 10; i++)
            {
                var stick = new Stick(i.ToString());
                sticks.Add(stick);
            }

            var philosophers = new List<Philosopher>() { new Philosopher("Ivan"), new Philosopher("Sergey"), new Philosopher("Evgenii"), new Philosopher("Alex"), new Philosopher("Oleg"), new Philosopher("Vilizar"), new Philosopher("Yuri"), new Philosopher("Constantine"), new Philosopher("Ilia"), new Philosopher("Max") };

            var tasks = new List<Task>();
            
            for (int i = 0; i < 10; i++)
            {
                var leftStickIndex = (i + 9) % 10;
                var rightStickIndex = (i + 1) % 10;
                var stickForP = new List<Stick> { sticks[leftStickIndex], sticks[rightStickIndex] };
                var p = philosophers[i];
                var t = new Task(() => p.SitToTheTable(stickForP, 10));
                tasks.Add(t);
                t.Start();
            }
            
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Dinner has finished");
        }
    }
}
