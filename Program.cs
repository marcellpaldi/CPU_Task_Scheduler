using System;
using System.Security.Cryptography.X509Certificates;

namespace cpu_optimalizalo_BUQ6LY
{
    internal class Program
    {

        static void Main(string[] args)
        {
            SorosPortIO sp1 = new SorosPortIO(3, 5);
            MerevlemezIO me1 = new MerevlemezIO(2, 4);
            IOFeladat io1 = new IOFeladat(10, 6);
            SzamitasiFeladat sz1 = new SzamitasiFeladat(1, 8);

            CPUFeladatUtemezo cpu = new CPUFeladatUtemezo();
            cpu.BeszurasHelyre(sp1);
            cpu.BeszurasHelyre(me1);
            cpu.BeszurasHelyre(io1);
            cpu.BeszurasHelyre(sz1);
            //cpu.Bejaras();
            try
            {
                Futtat(cpu);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        static void Futtat(CPUFeladatUtemezo cpu)
        {
            while (!cpu.Kesz())
            {
                Allapot a = cpu.keres(20);

                if (a != null)
                {
                    foreach (var feladat in a.feladatok)
                    {
                        cpu.Torol(feladat);
                    }
                    cpu.Bejaras();
                    //foreach (var feladat in a.feladatok)
                    //{

                    //    feladat.Novelo();
                    //    if (feladat.HanyKorOtaEl > 1)
                    //    {
                    //        throw new ArgumentException($"Túl sok kör óta él. {feladat}");
                    //    }
                    //}
                }
                Console.WriteLine("---");
            }
        }
    }
}
