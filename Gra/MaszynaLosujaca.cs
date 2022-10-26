using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    public class MaszynaLosujaca
    {
        public Klocki Nastepny;
        Random rand = new Random();

        private Klocki[] klocki = new Klocki[]
        {
            new Klocek_I(),
            new Klocek_J(),
            new Klocek_L(),
            new Klocek_O(),
            new Klocek_S(),
            new Klocek_T(),
            new Klocek_Z()
        };

        public MaszynaLosujaca() //funkcja Random losuje jakis klocek z 7 mozliwych
        {
            Nastepny = klocki[rand.Next(klocki.Length)];
        }

        public Klocki Losowanie()
        {
            Klocki k = Nastepny;              //losuje tak dlugo dopoki nie wylosuje klocka innego niz poprzedni
            while (k.numer == Nastepny.numer) //bez tego z niewiadomych przyczyn na poczatku wylosuje jakis klocek i potem caly czas losuje sie to samo
            {
                Nastepny = klocki[rand.Next(klocki.Length)];
            }
            return k;
        }
    }
}
