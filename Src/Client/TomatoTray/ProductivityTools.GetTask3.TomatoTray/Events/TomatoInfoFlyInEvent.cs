﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class TomatoInfoFlyInEvent : BaseEvent
    {
        public TomatoContract.Tomato Tomato { get; set; }
    }
}
