using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Stick
    {
        public string Id { get; set; }
        public Philosopher TookedBy { get; set; }
        public List<Philosopher> Owners { get; set; } = new List<Philosopher>();


        public Stick(string id)
        {
            Id = id;
        }

        public void PutDown()
        {
            foreach (var owner in Owners)
            {
                owner.TryToEat();
            }

            TookedBy = null;
            Owners = new List<Philosopher>();
        }
    }
}
