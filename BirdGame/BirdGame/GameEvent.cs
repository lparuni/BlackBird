using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbleGame
{
    class GameEvent : EventArgs
    {
        Boolean isPopped;

        // Constructor.
        public GameEvent(bool isPopped) 
        {
          this.isPopped = isPopped;
        }


      // Properties.
        public bool BubblePopped 
        { 
            get 
            { 
                return isPopped; 
            } 
        } 

    }
}
