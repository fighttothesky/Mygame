using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.interfaces
{
    public interface Isubject
    {

        void Attach(Iobserver observer);

        void Notify();
    }
}
