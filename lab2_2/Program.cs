using System;
using System.ComponentModel;

namespace lab2_2
{
    class Program
    {
        public enum Rozmiar {
            mala, 
            duza
        }
        public enum Kurier {
            DHL,
            UPS
        }
        interface IPaczka {
            public void Spakuj();
        }

        interface IKurier {
            public void Dostarcz();
        }

        interface IFabrykaLogistyki {
            IPaczka utworzPaczke(Rozmiar rozmiar);
            IKurier utworzKuriera(Kurier kurier);
        }

        class MalaPaczka : IPaczka {
            public void Spakuj() {
                Console.WriteLine("Spakowano małą paczkę");
            }
        }

        class DuzaPaczka : IPaczka {
            public void Spakuj() {
                Console.WriteLine("Spakowano dużą paczkę");
            }
        }

        class DHLKurier : IKurier {
            public void Dostarcz() {
                Console.WriteLine("Dostarczono przez kuriera DHL");
            }
        }

        class UPSKurier : IKurier {
            public void Dostarcz() {
                Console.WriteLine("Dostarczono przez kuriera UPS");
            }
        }

        class FabrykaLogistykiPolska : IFabrykaLogistyki
        {
            public IKurier utworzKuriera(Kurier kurier)
            {
                switch (kurier) {
                    case Kurier.DHL:
                        return new DHLKurier();
                    case Kurier.UPS:
                        return new UPSKurier();
                    default:
                        return null;
                }
            }

            public IPaczka utworzPaczke(Rozmiar rozmiar)
            {
                switch (rozmiar) {
                    case Rozmiar.mala:
                        return new MalaPaczka();
                    case Rozmiar.duza:
                        return new DuzaPaczka();
                    default:
                        return null;
                }
            }
        }

            class FabrykaLogistykiUSA : IFabrykaLogistyki
            {
                public IKurier utworzKuriera(Kurier kurier)
                {
                    switch (kurier) {
                        case Kurier.DHL:
                            return new DHLKurier();
                        case Kurier.UPS:
                            return new UPSKurier();
                        default:
                            return null;
                    }
                }

            public IPaczka utworzPaczke(Rozmiar rozmiar)
                {
                    switch (rozmiar) {
                        case Rozmiar.mala:
                            return new MalaPaczka();
                        case Rozmiar.duza:
                            return new DuzaPaczka();
                        default:
                            return null;
                    }
                }
            }

            class ZarzadzaniePrzesylkami {
            private IFabrykaLogistyki fabrykaLogistyki;
            private static ZarzadzaniePrzesylkami _instance;
            private ZarzadzaniePrzesylkami() {}
            public static ZarzadzaniePrzesylkami Instance {
                get {
                    if (_instance == null) {
                        _instance = new ZarzadzaniePrzesylkami();
                    }
                    return _instance;
                }
            }
            public enum Lokalizacja {
                Polska,
                USA
            }

            public void PrzyjmijZamowienie(Lokalizacja lokalizacja, Rozmiar rozmiar, Kurier kurier) {
                switch (lokalizacja) {
                    case Lokalizacja.Polska:
                        fabrykaLogistyki = new FabrykaLogistykiPolska();
                        break;
                    case Lokalizacja.USA:
                        fabrykaLogistyki = new FabrykaLogistykiUSA();
                        break;
                    default:
                        throw new InvalidEnumArgumentException("Nieobsługiwana lokalizacja");
                }

                IPaczka p = fabrykaLogistyki.utworzPaczke(rozmiar);
                IKurier k = fabrykaLogistyki.utworzKuriera(kurier);
                p.Spakuj();
                k.Dostarcz();
            }
        }

        static void Main(string[] args)
        {
            ZarzadzaniePrzesylkami.Instance.PrzyjmijZamowienie(ZarzadzaniePrzesylkami.Lokalizacja.Polska, Rozmiar.duza, Kurier.DHL);
            ZarzadzaniePrzesylkami.Instance.PrzyjmijZamowienie(ZarzadzaniePrzesylkami.Lokalizacja.USA, Rozmiar.mala, Kurier.UPS);
        }
    }
}
