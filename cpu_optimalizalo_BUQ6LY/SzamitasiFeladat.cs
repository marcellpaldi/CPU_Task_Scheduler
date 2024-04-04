using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpu_optimalizalo_BUQ6LY
{
    class SzamitasiFeladat : IFeladat
    {
        public SzamitasiFeladat(int prio, int ido)
        {
            Prioritas= prio;
            Idoigeny= ido;
        }
        public int Prioritas { get; }
        public int Idoigeny { get; }
        public int HanyKorOtaEl { get; set; }

        public event FeladatBeutemezve feladat;
        public event HanySzimulaciosKorOtaEl hany_szimulacios;

        public void Novelo()
        {
            hany_szimulacios?.Invoke();
        }
    }
}
