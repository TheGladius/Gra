using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Gra
{
    public abstract class Klocki
    {
        Wspolrzedne aktualne_wsp; //aktualne wspolrzedne klocka w danej chwili
        public int obrocenie; //stan obrocenia, kazdy klocek ma ich 4, poza klockiem O (kwadrat)
        //!!!przy wszystich zmiennych abstrakcyjnych trzeba pisac {get;} bo nie beda dzialac i wywali milion bledow
        public abstract int numer {get;} //numery kolejnych klockow, ida alfabetycznie I, J, L, O, S, T, Z
        public abstract Wspolrzedne startowe_wsp {get;} //starowe wspolrzedne klockow, miejsce spawnu w pierwszych dwoch rzedach, wszystkie maja takie same poza klockami O i I
        public abstract Wspolrzedne[][] klocek {get;} //tablica zawierajaca 4 tablice z 4 wartosciami mozliwych wypelnien 4 stanow obrocenia kazdego klocka

        public Klocki()
        {
            aktualne_wsp = new Wspolrzedne(startowe_wsp.rzad, startowe_wsp.kolumna);
        }

        public void Przesuwanie(int r, int k)
        {
            aktualne_wsp.rzad = aktualne_wsp.rzad + r;
            aktualne_wsp.kolumna = aktualne_wsp.kolumna + k;
        }

        public void Czyszczenie()
        {
            obrocenie = 0;
            aktualne_wsp.rzad = startowe_wsp.rzad;
            aktualne_wsp.kolumna = startowe_wsp.kolumna;
        }

        public IEnumerable<Wspolrzedne> Kolekcja_wsp() //kolekcja wspolrzednych
        {
            foreach (Wspolrzedne wpolrzedne in klocek[obrocenie]) //zwykla petla for nie dziala, dla kolekcji musi byc foreach
            {
                yield return new Wspolrzedne(wpolrzedne.rzad + aktualne_wsp.rzad, wpolrzedne.kolumna + aktualne_wsp.kolumna); //podawanie następnej wartości w iteracji
            }
        }
    }

    public class Klocek_I : Klocki //dla wszystkich klockow: tablice Wspolrzedne[] zawieraja wypelnienie kazdego stanu klocka np. new(1,0) oznacza 2 rzad i 1 kolumne
    {                              //new(1,1) 2 rzad i 2 kolumne itp. tworzac ostatecznie pozycje startowa klocka I czyli "polozone" I
        public override int numer => 1;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(-1, 3); //klocek I zaczyna jeden rzad nizej, bo jako jedyny zajmuje 1 rzad
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][]
        {
            new Wspolrzedne[] { new(1,0), new(1,1), new(1,2), new(1,3) },
            new Wspolrzedne[] { new(0,2), new(1,2), new(2,2), new(3,2) },
            new Wspolrzedne[] { new(2,0), new(2,1), new(2,2), new(2,3) },
            new Wspolrzedne[] { new(0,1), new(1,1), new(2,1), new(3,1) }
        };
    }

    public class Klocek_J : Klocki
    {
        public override int numer => 2;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 3);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][] 
        {
            new Wspolrzedne[] {new(0, 0), new(1, 0), new(1, 1), new(1, 2)},
            new Wspolrzedne[] {new(0, 1), new(0, 2), new(1, 1), new(2, 1)},
            new Wspolrzedne[] {new(1, 0), new(1, 1), new(1, 2), new(2, 2)},
            new Wspolrzedne[] {new(0, 1), new(1, 1), new(2, 1), new(2, 0)}
        };
    }

    public class Klocek_L : Klocki
    {
        public override int numer => 3;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 3);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][]
        {
            new Wspolrzedne[] {new(0,2), new(1,0), new(1,1), new(1,2)},
            new Wspolrzedne[] {new(0,1), new(1,1), new(2,1), new(2,2)},
            new Wspolrzedne[] {new(1,0), new(1,1), new(1,2), new(2,0)},
            new Wspolrzedne[] {new(0,0), new(0,1), new(1,1), new(2,1)}
        };
    }

    public class Klocek_O : Klocki
    {
        public override int numer => 4;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 4);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][]
        {
            new Wspolrzedne[] { new(0,0), new(0,1), new(1,0), new(1,1) }
        };
    }

    public class Klocek_S : Klocki
    {
        public override int numer => 5;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 3);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][] 
        {
            new Wspolrzedne[] { new(0,1), new(0,2), new(1,0), new(1,1) },
            new Wspolrzedne[] { new(0,1), new(1,1), new(1,2), new(2,2) },
            new Wspolrzedne[] { new(1,1), new(1,2), new(2,0), new(2,1) },
            new Wspolrzedne[] { new(0,0), new(1,0), new(1,1), new(2,1) }
        };
    }

    public class Klocek_T : Klocki
    {
        public override int numer => 6;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 3);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][] 
        {
            new Wspolrzedne[] {new(0,1), new(1,0), new(1,1), new(1,2)},
            new Wspolrzedne[] {new(0,1), new(1,1), new(1,2), new(2,1)},
            new Wspolrzedne[] {new(1,0), new(1,1), new(1,2), new(2,1)},
            new Wspolrzedne[] {new(0,1), new(1,0), new(1,1), new(2,1)}
        };
    }

    public class Klocek_Z : Klocki
    {
        public override int numer => 7;
        public override Wspolrzedne startowe_wsp => new Wspolrzedne(0, 3);
        public override Wspolrzedne[][] klocek => new Wspolrzedne[][] 
        {
            new Wspolrzedne[] {new(0,0), new(0,1), new(1,1), new(1,2)},
            new Wspolrzedne[] {new(0,2), new(1,1), new(1,2), new(2,1)},
            new Wspolrzedne[] {new(1,0), new(1,1), new(2,1), new(2,2)},
            new Wspolrzedne[] {new(0,1), new(1,0), new(1,1), new(2,0)}
        };
    }
}
