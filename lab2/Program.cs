using System;
using System.Net;

namespace lab2
{
    class Program
    {
        interface IPaczka {
            void Przygotuj();
        }

        interface IFabrykaPaczek {
            IPaczka UtworzPaczke();
        }

        class MalaPaczka : IPaczka {
            public void Przygotuj() {
                Console.WriteLine("Przygotowano małą paczkę.");
            }
        }

        class DuzaPaczka : IPaczka {
            public void Przygotuj() {
                Console.WriteLine("Przygotowano dużą paczkę");
            }
        }

        class FabrykaMalychPaczek : IFabrykaPaczek {
            public IPaczka UtworzPaczke() {
                return new MalaPaczka();
            }
        }

        class FabrykaDuzychPaczek : IFabrykaPaczek {
            public IPaczka UtworzPaczke() {
                return new DuzaPaczka();
            }
        }

        class ZarzadzanieProdukcja {
            private IFabrykaPaczek fabrykaPaczek;
            private static ZarzadzanieProdukcja _instance;
            private ZarzadzanieProdukcja() { }
            public static ZarzadzanieProdukcja getInstance() {
                if (_instance == null) {
                    _instance = new ZarzadzanieProdukcja();
                }
                return _instance;
            }

            public void produkujMalePaczki() {
                fabrykaPaczek = new FabrykaMalychPaczek();
            }
            public void produkujDuzePaczki() {
                fabrykaPaczek = new FabrykaDuzychPaczek();
            }
            public void wyprodukujPaczke() {
                fabrykaPaczek.UtworzPaczke().Przygotuj();
            }
        }

        static void Main(string[] args)
        {
            ZarzadzanieProdukcja produkcja = ZarzadzanieProdukcja.getInstance();
            ZarzadzanieProdukcja produkcja2 = ZarzadzanieProdukcja.getInstance();
            if (produkcja != produkcja2) {
                Console.WriteLine("singleton nie dziala");
                return;
            }
            produkcja.produkujMalePaczki();
            produkcja.wyprodukujPaczke();
        }
    }
}
