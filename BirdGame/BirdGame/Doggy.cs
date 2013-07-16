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
    class Doggy: Animatable
    {
        float xvel;
        float yvel;
        float ya;
        int framenumber;
        int lastGenerated;
        Boolean off = false;

        public Doggy(Texture2D image)
            : base(500, 640, image, 0.5f, .8f)
        {
            currimage = image;
            xvel = -1f;
            yvel = 1.5f;
            ya = .085f;
            framenumber = 0;
            lastGenerated = 0;
        }





        public override Boolean update(GameTime gameTime)
        {
            int numbah = 750;
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


            x += xvel * gameTime.ElapsedGameTime.Milliseconds * 1f;


           //     yvel += ya * gameTime.ElapsedGameTime.Milliseconds;

            if (x > 125)
            {
                yvel += ya * gameTime.ElapsedGameTime.Milliseconds;
                y -= yvel * gameTime.ElapsedGameTime.Milliseconds * .05f;
            }
            else
            {
                yvel += ya * gameTime.ElapsedGameTime.Milliseconds * -1f;
                y += yvel * gameTime.ElapsedGameTime.Milliseconds * .05f;
            }

            if (y < (0 - (currimage.Width / 2)))
            {
                off = true;
                return false;

            }

            return true;
        }

        public Boolean offScreen()
        {
            return off;
        }

        public new void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(currimage, new Vector2((int)x, (int)y), new Rectangle(0 + currimage.Width/3 * framenumber, 0, currimage.Width/3, currimage.Height), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
       //     spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r1x, (int)y + r1y, r1width, r1height), Color.Red);
       //     spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r2x, (int)y + r2y, r2width, r2height), Color.Red);
            
        }

    }
}
