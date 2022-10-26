using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gra
{
    public class Plansza
    {
        int[,] plansza; //dwuwymiarowa tablica = plansza

        public int this[int r, int k]
        {
            get => plansza[r, k];
            set => plansza[r, k] = value;
        }

        public Plansza()
        {
            plansza = new int[22, 10];
        }

        public bool Puste_pole(int r, int k) //sprawdza czy pole jest puste i czy nie wychodzi za krawedzie
        {
            if(r >= 0 && r < 22 && k >= 0 && k < 10 && plansza[r, k] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Pelny_rzad(int r) //sprawdza czy dany rzad jest zapelniony
        {
            int licznik = 0;
            for (int k = 0; k < 10; k++)
            {
                if (plansza[r, k] == 0)
                {
                    licznik = 1;
                    break;
                }
            }
            if(licznik == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int Czyszczenie_i_przesuwanie() //obsluga pelnych rzedow
        {
            int licznik = 0; //ilosc pelnych rzedow
            for (int r = 22 - 1; r >= 0; r--)
            {
                if (Pelny_rzad(r) == true)
                {
                    for (int k = 0; k < 10; k++) //czyszczenie pelnego rzedu
                    {
                        plansza[r, k] = 0;
                    }
                    licznik++;
                }
                else if (licznik > 0)
                {
                    for (int k1 = 0; k1 < 10; k1++) //przesuwanie rzedu w dol
                    {
                        plansza[r + licznik, k1] = plansza[r, k1]; //r + licznik to przesuniecie o tyle rzedow ile sie zapelnilo, bo moga sie zapelnic np. dwa jednoczesnie
                        plansza[r, k1] = 0;
                    }
                }
            }
            return licznik; //potrzebne do przyszlego nabijania punktow
        }

        public bool Pusty_rzad(int r) //sprawdza czy dany rzad jest pusty
        {
            int licznik = 0;
            for (int k = 0; k < 10; k++)
            {
                if (plansza[r, k] != 0)
                {
                    licznik = 1;
                    break;
                }
            }
            if (licznik == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
