using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class SushiPlate
    {
        public Guid Id { get; set; }
        public int Sushies { get; set; }

        public SushiPlate(int n)
        {
            Sushies = n;
            Id = Guid.NewGuid();
        }
    }
}
