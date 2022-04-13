using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Philosopher
    {
        public string Name { get; set; }
        public SushiPlate Plate { get; set; }
        public Status Status { get; set; }
        public List<Stick> AvailableSticks { get; set; } = new List<Stick>(2);
        public HashSet<Stick> SticksInHand { get; set; } = new HashSet<Stick>(2);
        public Philosopher(string name)
        {
            Name = name;
        }

        public void SitToTheTable(List<Stick> availableSticks, int sushiNumber)
        {
            Console.WriteLine(Name.ToString() + " sat to the table");
            AvailableSticks = availableSticks;
            Plate = new SushiPlate(sushiNumber);
            Think();
        }

        public bool TryTakeAvailable()
        {
            foreach (var stick in AvailableSticks)
            {
                lock (stick)
                {
                    if (stick.TookedBy != null)
                    {
                        Console.WriteLine(Name.ToString() + " couldn`t take stick " + stick.Id.ToString());
                        foreach (var s in SticksInHand)
                        {
                            s.PutDown();
                        }

                        return false;
                    }

                    TakeInHand(stick);
                }
            }

            var sticks = string.Empty;
            foreach (var stick in SticksInHand)
            {
                sticks += stick.Id.ToString() + " ";
            }

            Console.WriteLine(Name.ToString() + " got sticks " + sticks);
            return true;
        }

        public void TakeInHand(Stick s)
        {
            s.TookedBy = this;
            SticksInHand.Add(s);
        }

        public void TryToEat()
        {
            if (TryTakeAvailable())
            {
                Eat();
            }
            else
            {
                Think();
            }
        }

        public void Eat()
        {
            Status = Status.Eating;
            Console.WriteLine(Name.ToString() + " started eating");
            Plate.Sushies--;
            Thread.Sleep(1000);
            FinishEating();
        }

        public void FinishEating()
        {
            Console.WriteLine(Name.ToString() + " finished eating, on the plate there are more sushies, count - " + Plate.Sushies);
            foreach (var stick in SticksInHand)
            {
                stick.PutDown();
            }

            SticksInHand = new HashSet<Stick>();
            if (Plate.Sushies == 0)
            {
                FinishDinner();
                return;
            }

            Think();
        }

        public void FinishDinner()
        {
            Status = Status.FinishedDinner;
            Console.WriteLine(Name.ToString() + " finished dinner");
        }

        public void Think()
        {
            Status = Status.Thinking;
            Console.WriteLine(Name.ToString() + " started thinking");
            Thread.Sleep(1000);
            TryToEat();
        }
    }
}
