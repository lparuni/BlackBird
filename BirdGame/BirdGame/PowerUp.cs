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
    class PowerUp : Drawable
    {
        


        public PowerUp(float x, float y, float vel, Texture2D img)
            : base(x, y, img, vel, 1f)
        {
            this.x = x;
            this.y = y;
            currimage = img;
            this.vel = vel;
        }

        public override bool update(GameTime gameTime)
        {
            x += vel * gameTime.ElapsedGameTime.Milliseconds;
            if (x < (0 - (currimage.Width / 2)))
            {
                return false;
            }
            return true;
        }

  
    }
}
