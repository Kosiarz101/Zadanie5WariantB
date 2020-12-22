using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WariantBZad5KamilŁozowski
{
    class Vertex
    {
        public int NumerSkrzyzowania { get; set; }
        public int Waga { get; set; }
        public int Odleglosc { get; set; }
        public int NumerUlicy { get; set; }
        public Vertex(int numerSkrzyzowania, int odleglosc, int waga, int numerUlicy)
        {
            this.NumerSkrzyzowania = numerSkrzyzowania;
            this.Waga = waga;
            this.Odleglosc = odleglosc;
            this.NumerUlicy = numerUlicy;
        }
    }
}
