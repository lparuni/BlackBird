using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BubbleGame
{

    using System;
    using System.Collections;

    //  public delegate void MyDelegate(); 

    public interface IScreen
    {


        /*public void pressed(String button)
        {
            buttonPressed(button);
        }*/
        // update
        void update(GameTime gametime);

        // draw
        void draw(SpriteBatch spriteBatch);

        string getScreen();

        void getNext();

        void getLast();
    }
}

