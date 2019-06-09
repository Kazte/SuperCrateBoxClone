using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public delegate void SimpleEventHandler<T>(T arg);

    public delegate void SimpleEventHandler<T0, T1>(T0 arg0, T1 arg1);

    public delegate void SimpleEventHandler<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2);
}
