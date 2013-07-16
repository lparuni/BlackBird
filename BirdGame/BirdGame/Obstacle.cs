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
    class Obstacle : Drawable
    {
        //float v;                    // obstacle's velocity
       // Texture2D image;            // image of obstacle
       // static Boolean show = true;
      
        public Obstacle(float xpos, float ypos, Texture2D obimage, float vel, float scale) : base(xpos, ypos, obimage, vel, scale)
        {
            x = xpos;
            y = ypos;
            this.vel = vel;
            currimage = obimage;
            
         //   show = true;
        }

      /*  public static void noShow()
        {
           show = false;
        }*/

        private void updateSpeed()
        {
            this.vel = this.vel * (float)GamePlay.velRatio ;
        }
        // update the obstacle
        public override Boolean update(GameTime gameTime)
        {
            updateSpeed();
            //vel = GamePlay.vel;   //to make slow work//the obstacles velocities are always synced to the GamePlay's.
            {
                // moves the obstacle
                x -= vel * gameTime.ElapsedGameTime.Milliseconds;
            }
            
           
                // check if it's still on the screen
                if (x < (0 - (currimage.Width / 2)))
                {
                    return false;
                }

                else return true;
            
        }

        /*public new void draw(SpriteBatch spriteBatch)
        {
            if (show)
                spriteBatch.Draw(currimage, new Vector2(x, y), Color.White);
        }*/


        

    }


}
