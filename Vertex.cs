using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WariantBZad5KamilŁozowski
{
    class Vertex
    {
        public int Numer { get; set; }
        public int Waga { get; set; }
        public int Odleglosc { get; set; }
        public Vertex(int numer, int odleglosc, int waga)
        {
            this.Numer = numer;
            this.Waga = waga;
            this.Odleglosc = odleglosc;
        }
    }
}
