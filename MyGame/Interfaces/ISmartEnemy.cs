﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.interfaces;
    public interface ISmartEnemy : IEnemy
    {
        public bool IsDead { get; set; }

    }
