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
    class Animatable : Drawable
    {
        int framenumber;

       // Boolean show;
        int lastGenerated = 0;
        public Animatable(float xpos, float ypos, Texture2D image, float velocity, float scale) : base(xpos, ypos, image, velocity, scale)
        {
            framenumber = 0;
           // show = true;
        }
        /*public new void noShow()
        {
            show = false;
        }*/
        public override Boolean update(GameTime gameTime)
        {
            int numbah = 300;
            // every set amount of gameTime, generate new obstacle in random position and enqueue
            if ((gameTime.TotalGameTime.TotalMilliseconds - lastGenerated) >= numbah)
            {

                if (framenumber == 2)
                {
                    framenumber = 0;
                }

                else framenumber++;

                lastGenerated += numbah;
            }
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

        public new void draw(SpriteBatch spriteBatch)
        {

            if (show)
            {
             spriteBatch.Draw(currimage, new Vector2((int)x, (int)y), new Rectangle(0 + (currimage.Width / 3) * framenumber, 0, currimage.Width / 3, currimage.Height), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            
             //spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r1x, (int)y + r1y, r1width, r1height), Color.Red);
             //spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r2x, (int)y + r2y, r2width, r2height), Color.Red);


                
            }
        }
    }
}
