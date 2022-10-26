using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    internal class Rozgrywka
    {
        public Plansza Plansza;
        public MaszynaLosujaca MaszynaLosujaca;
        Klocki nastepny_klocek;

        public Klocki Klocek //musi być konstrukcja get, set bo inaczej nie da sie zabrac klocka tak zeby program nie wywalil
        {
            get => nastepny_klocek;
            set
            {
                nastepny_klocek = value;
                nastepny_klocek.Czyszczenie(); //koniecznie tutaj czyszczenie, przy czyszczeniu na koniec ruchu klocki nie zaczna w miejscu konca ruchu
            }
        }

        public Rozgrywka()
        {
            Plansza = new Plansza();
            MaszynaLosujaca = new MaszynaLosujaca();
            Klocek = MaszynaLosujaca.Losowanie(); //losuje klocek
        }

        void Wypelnij()
        {
            foreach (Wspolrzedne w in Klocek.Kolekcja_wsp()) //wpisuje do talicy plansza numery klockow zeby bylo wiadomo jak pokolorowac pola
            {
                Plansza[w.rzad, w.kolumna] = Klocek.numer;
            }
        }

        bool Klocek_sie_miesci()
        {
            foreach (Wspolrzedne w in Klocek.Kolekcja_wsp())
            {
                if (Plansza.Puste_pole(w.rzad, w.kolumna)==false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool koniec; //zmienna konca gry
        public int punkty; //przechowuje punkty danej rozgrywki
        //zawsze musi byc najpierw przesuniecie, a potem warunek z cofnieciem, bo inaczej wspolrzedne wyjda poza tablice
        public void Ruch_w_dol()
        {
            Klocek.Przesuwanie(1, 0); //1 rzad w dol
            if (Klocek_sie_miesci() == false)
            {
                Klocek.Przesuwanie(-1, 0); // cofa w gore
                Wypelnij();
                punkty = punkty + 10 * Plansza.Czyszczenie_i_przesuwanie(); //nabija punkty, mnozy 10 * ilosc zapelnionych rzedow za jednym razem

                if (Plansza.Pusty_rzad(0) == false || Plansza.Pusty_rzad(1) == false) //sprawdza czy gra sie nie skonczyla
                {
                    koniec = true;
                }
                else
                {
                    Klocek = MaszynaLosujaca.Losowanie(); //losuje nastepny klocek
                }
            }
        }

        public void Ruch_w_prawo()
        {
            Klocek.Przesuwanie(0, 1); //1 kolumna w prawo
            if (Klocek_sie_miesci() == false)
            {
                Klocek.Przesuwanie(0, -1); //cofa
            }
        }

        public void Ruch_w_lewo()
        {
            Klocek.Przesuwanie(0, -1); //1 kolumna w lewo
            if (Klocek_sie_miesci() == false)
            {
                Klocek.Przesuwanie(0, 1); //cofa
            }
        }

        public void Obracanie()
        {
            Klocek.obrocenie = (Klocek.obrocenie + 1) % Klocek.klocek.Length; //zeby pasowalo i do tych co maja 4 stany obrocenia i do kwadratu
            if (Klocek_sie_miesci()==false) //jak sie nie miesci to cofamy obrocenie
            {
                if (Klocek.obrocenie == 0)
                {
                    Klocek.obrocenie = Klocek.klocek.Length - 1; //jesli np. klocek ma 4 stany obrocenia a jest nr 0 to idzie do stanu nr 3 (czyli czwartego stanu)
                }
                else
                {
                    Klocek.obrocenie--; //a jak jest juz obrocony to cofamy do poprzedniego stanu tylko
                }
            }
        }

        public void Upuszczanie()
        {
            int rzedy = 22;
            foreach (Wspolrzedne w in Klocek.Kolekcja_wsp()) //funkcja sprawdza kazde z 4 pol klocka
            {
                int zmienna = 0;
                while (Plansza.Puste_pole(w.rzad + 1 + zmienna, w.kolumna)) //sprawdza o ile rzedow mozna upuscic klocka o konkretnym ksztalcie (ile rzedow jest pustych)
                {
                    zmienna++;
                }
                rzedy = Math.Min(rzedy, zmienna); //najmniejsza liczba z rzedow, czesto dolne pola nie pasuja, gdy pasuja gorne itp wiec bierzemy dystans dla klocka dla ktorego pasuje najmniej
            }

            Klocek.Przesuwanie(rzedy, 0);
            Wypelnij();
            punkty = punkty + 10 * Plansza.Czyszczenie_i_przesuwanie();

            if (Plansza.Pusty_rzad(0) == false || Plansza.Pusty_rzad(1) == false)
            {
                koniec = true;
            }
            else
            {
                Klocek = MaszynaLosujaca.Losowanie();
            }
        }
    }
}
