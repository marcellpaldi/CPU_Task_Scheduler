using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpu_optimalizalo_BUQ6LY
{
    public delegate void FeladatBeutemezoFigyelo(IFeladat feladat);
    class CPUFeladatUtemezo
    {
        
        private Lista lista = new Lista();
        
        public void BeszurasHelyre(IFeladat beszurando)
        {
            lista.HozzaAd(beszurando);
        }

        public void Torol(IFeladat feladat)
        {
            lista.Torol(feladat);
        }

        public bool Kesz()
        {
            return lista.Ures();
        }

        public void Bejaras()
        {
            ListaElem p = lista.fej;
            while (p != null)
            {
                p.Tartalom.HanyKorOtaEl++;
                if (p.Tartalom.HanyKorOtaEl>0)
                {
                    throw new ArgumentException($"Túl sok kör óta él. {p.Tartalom}");
                }
                p = p.Kovetkezo;
            }
        }

        public Allapot keres(int maxIdo)
        {
            return keres(new Allapot(0, new IFeladat[0], 0), maxIdo, this.lista.fej);
        }

        public Allapot keres(Allapot elozo, int maxIdo, ListaElem fej)
        {
            if (elozo == null) return null;
            if (elozo.Ido > maxIdo) return null;
            if (elozo.Ido == maxIdo) return elozo;
            if (fej == null) return elozo;

            IFeladat feladat = fej.Tartalom;
            feladat.hany_szimulacios += () => feladat.HanyKorOtaEl++;
            Allapot vele = new Allapot(elozo.Ido + feladat.Idoigeny, new IFeladat[0], elozo.Prioritas + feladat.Prioritas);

            vele.feladatok = new IFeladat[elozo.feladatok.Length + 1];

            for (int i = 0; i < elozo.feladatok.Length; i++)
            {
                vele.feladatok[i] = elozo.feladatok[i];
            }
            vele.feladatok[elozo.feladatok.Length] = feladat;
            
            
            Allapot veleA = keres(vele, maxIdo, fej.Kovetkezo);
            Allapot nelkuleA = keres(elozo, maxIdo, fej.Kovetkezo);

            if (veleA != null && (nelkuleA == null || (veleA.Ido > nelkuleA.Ido || veleA.Prioritas >= nelkuleA.Prioritas)))
            {
                
                return veleA;
            }
            else
            {
                
                return nelkuleA;
                
            }
            
        }
        
    }

    class Allapot
    {
        public Allapot(int ido, IFeladat[] feladatok, int prioritas)
        {
            Ido = ido;
            this.feladatok = feladatok;
            Prioritas = prioritas;
        }

        public int Ido { get; set; }
        public IFeladat[] feladatok { get; set; }
        public int Prioritas { get; set; }
    }

    class ListaElem
    {
        public IFeladat Tartalom { get; set; }
        public ListaElem Kovetkezo { get; set; }

        public ListaElem(IFeladat fel) { Tartalom = fel; }

    }

    class Lista
    {
        public event FeladatBeutemezoFigyelo FeladatBeutemezve;
        public ListaElem fej { get; set; }
        public Lista()
        {
            FeladatBeutemezve += Kiiro;
        }
        public bool Ures()
        {
            return fej == null;
        }
        public void Kiiro(IFeladat feladat)
        {
            Console.WriteLine($"{feladat} - {feladat.Idoigeny} - {feladat.Prioritas}");
        }
        public void Torol(IFeladat elem)
        {
            
            if (fej == null) return;

            if (fej.Tartalom == elem)
            {
                
                FeladatBeutemezve?.Invoke(elem);

                Console.WriteLine($"{elem} törölve.");
                fej = fej.Kovetkezo;
                return;
            }

            ListaElem current = fej;
            while (current != null)
            {
                if (current.Kovetkezo?.Tartalom == elem)
                {
                   
                    FeladatBeutemezve?.Invoke(elem);

                    Console.WriteLine($"{elem} törölve.");
                    current.Kovetkezo = current.Kovetkezo.Kovetkezo;
                    return;
                }

                current = current.Kovetkezo;
            }
        }

        public void HozzaAd(IFeladat elem)
        {
            ListaElem ujElem = new ListaElem(elem);

            if (fej == null)
            {
                fej = ujElem;
            }
            else
            {
                if (fej.Tartalom.Prioritas > elem.Prioritas)
                {
                    ujElem.Kovetkezo = fej;
                    fej = ujElem;
                }
                else
                {
                    ListaElem current = fej;

                    while (current.Kovetkezo != null && (current.Kovetkezo.Tartalom.Prioritas < elem.Prioritas))
                    {
                        current = current.Kovetkezo;
                    }

                    if (current.Kovetkezo != null)
                    {
                        ujElem.Kovetkezo = current.Kovetkezo;
                        current.Kovetkezo = ujElem;
                    }
                    else
                    {
                        current.Kovetkezo = ujElem;
                        ujElem.Kovetkezo = null;
                    }

                }
            }
        }
    }
}
