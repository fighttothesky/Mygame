﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.interfaces
{
    public interface Iobserver
    {
        void Update(Isubject subject);
    }
}
