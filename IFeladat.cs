using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpu_optimalizalo_BUQ6LY
{
    public delegate int HanySzimulaciosKorOtaEl();
    public delegate int FeladatBeutemezve();
    public interface IFeladat
    {
        public int Prioritas { get;  }
        public int Idoigeny { get;  }
        public int HanyKorOtaEl { get; set; }
        public event FeladatBeutemezve feladat;
        public event HanySzimulaciosKorOtaEl hany_szimulacios;

        public void Novelo();
        
    }
}
