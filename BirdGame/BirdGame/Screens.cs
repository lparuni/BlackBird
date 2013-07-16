using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbleGame
{
    class Screens
    {
        public delegate void ButtonEvent(String button);

        public event ButtonEvent buttonPressed;
    }
}
