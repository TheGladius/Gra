using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gra
{
    public partial class MainWindow : Window
    {
        Rozgrywka Rozgrywka = new Rozgrywka(); //nowa rozgrywka
        Image[,] wypelniona_plansza; //tablica dwuwymiarowa bedaca plansza ale wypelniona juz assetami
        int punktacja_koncowa; //koncowe pkt

        //kolory koniecznie w tej samej kolejnosci co ich klocki w klasie Klocki!!!
        ImageSource[] pola = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/square.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LightBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Orange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Yellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Purple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Red.png", UriKind.Relative))
        };
        //koniecznie w tej samej kolejnosci co klocki w klasie Klocki!!!
        ImageSource[] klocki = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Z.png", UriKind.Relative))
        };

        Image[,] Ustawienia_pol(Plansza grid)
        {
            Image[,] wypelniona_plansza = new Image[22, 10];
            //wymiary pojedynczych klockow to 64 piksele, wiec 32 dobrze sie skaluje
            for (int r = 0; r < 22; r++)
                for (int k = 0; k < 10; k++)
                {
                    Image pole = new Image
                    {
                        Width = 32,
                        Height = 32
                    };
                    Canvas.SetTop(pole, (r - 2) * 32);
                    Canvas.SetLeft(pole, k * 32);
                    Canvas.Children.Add(pole);
                    wypelniona_plansza[r, k] = pole;
                }
            return wypelniona_plansza;
        }

        void Rysowanie(Rozgrywka roz)
        {
            //rysowanie planszy
            for (int r = 0; r < 22; r++)
                for (int k = 0; k < 10; k++)
                {
                    int zmienna = roz.Plansza[r, k]; //roz.Plansza to Plansza z Rozgrywka, na poczotku jest pusta, ale potem jest aktualizowana przy ruchu i caly czas podawana do wypelniona_plansza, żeby plansza sie aktualizowala
                    wypelniona_plansza[r, k].Source = pola[zmienna];
                }
            //rysowanie klocka na planszy
            foreach (Wspolrzedne w in roz.Klocek.Kolekcja_wsp()) //wspolrzedne z kolekcji tylko tego konkretnego wylosowanego w klasie Rozgrywka klocka
            {
                wypelniona_plansza[w.rzad, w.kolumna].Source = pola[roz.Klocek.numer];
            }
            //dodawanie grafiki nastepnego klocka po prawej stronie
            {
                Klocki k = roz.MaszynaLosujaca.Nastepny; //to poprostu nastepny wylosowany klocek
                nastepny.Source = klocki[k.numer]; //przyporzodkowuje zdjecie klocka po jego numerze
            }
            Punkty.Text = $"Zdobyte Punkty: {roz.punkty}"; //aktualizuje punktacje
        }

        void Ruch(object sender, KeyEventArgs e)
        {
            if (Rozgrywka.koniec == true) //konczy mozliwosc ruchu
            {
                return; //w c# to break ale do if
            }

            switch (e.Key) //przypisanie klawiszy
            {
                //ruch
                case Key.Down:
                    Rozgrywka.Ruch_w_dol();
                    break;
                case Key.S:
                    Rozgrywka.Ruch_w_dol();
                    break;
                case Key.Right:
                    Rozgrywka.Ruch_w_prawo();
                    break;
                case Key.D:
                    Rozgrywka.Ruch_w_prawo();
                    break;
                case Key.Left:
                    Rozgrywka.Ruch_w_lewo();
                    break;
                case Key.A:
                    Rozgrywka.Ruch_w_lewo();
                    break;
                //obracanie
                case Key.Up:
                    Rozgrywka.Obracanie();
                    break;
                case Key.W:
                    Rozgrywka.Obracanie();
                    break;
                //upuszczenie
                case Key.Space:
                    Rozgrywka.Upuszczanie();
                    break;
                default:
                    return;
            }
            Rysowanie(Rozgrywka);
        }

        async Task Gramy() //musi byc async do czekania await
        {
            Rysowanie(Rozgrywka); //pierwsze narysowanie pustej planszy
            while (Rozgrywka.koniec == false)
            {
                await Task.Delay(750); //1.5 rzedu na sekunde (1000 = sekunda)
                Rozgrywka.Ruch_w_dol(); //samoczynne spadanie
                Rysowanie(Rozgrywka); //aktualizacja planszy
            }
            Game_Over.Visibility = Visibility.Visible; //pokazuje ekran GameOver
            Koncowe_punkty.Text = $"Punkty: {Rozgrywka.punkty}"; //punktacje koncowa wyciagnieta od razu z Rozgrywka
        }

        async void Plansza(object sender, RoutedEventArgs e)
        {
            await Gramy();
        }

        async void Zagraj_ponownie(object sender, RoutedEventArgs e)
        {
            Rozgrywka = new Rozgrywka();
            Game_Over.Visibility = Visibility.Hidden; //chowa ekran GameOver
            await Gramy();
        }

        void Koniec_gry(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0); //konczy dzialanie programu
        }

        public MainWindow()
        {
            InitializeComponent();
            wypelniona_plansza = Ustawienia_pol(Rozgrywka.Plansza);
        }
    }
}
