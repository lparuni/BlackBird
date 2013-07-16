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
    class Particle
    {

        float x;                // x-position
        float y;                // y-position
        float xvel;             // x-velocity
        float yvel;             // y-velocity
        float rotation;
        int TTD;                // life span of particle
        Texture2D image;        // image of the particle
        Color c;

        // create a Particle with texture, at position (xp, yp)
        public Particle(Texture2D texture, float xp, float yp, int i, int n, Boolean yesno)
        {
            rotation = (float)Math.PI / (RandomGen(0, 16));

            x = xp;

            y = yp;
            if (yesno == true)
            {
                c = new Color(
              (byte)r.Next(0, 255),
              (byte)r.Next(0, 255),
              (byte)r.Next(0, 255));

                if ((i > 0) && (i < (n / 4)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * RandomGen(0, 10) * .1f;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * RandomGen(0, 10) * .1f;
                }

                else if ((i > (n / 4)) && (i < (n / 2)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * RandomGen(0, 10) * .1f;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * -1 * RandomGen(0, 10) * .1f;
                }

                else if ((i > (n / 2)) && (i < (n * 3 / 4)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * -1 * RandomGen(0, 10) * .1f;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * -1 * RandomGen(0, 10) * .1f;
                }

                else
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * -1 * RandomGen(0, 10) * .1f;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * RandomGen(0, 10) * .1f;
                }

            }

            else
            {
                if ((i > 0) && (i < (n / 4)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i);
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i);
                }

                else if ((i > (n / 4)) && (i < (n / 2)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i);
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * -1;
                }

                else if ((i > (n / 2)) && (i < (n * 3 / 4)))
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * -1;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i) * -1;
                }

                else
                {
                    xvel = (float)Math.Cos(2 * Math.PI / 50 * i) * -1;
                    yvel = (float)Math.Sin(2 * Math.PI / 50 * i);
                }
            }
            if (xvel == 0)
            {
                xvel = -.1f;
            }
            image = texture;

            TTD = 5;
        }


        // generate a random number
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        static Random r = new Random();
        static int RandomGen(int min1, int max1)
        {
            return r.Next(min1, max1);

        }


        public Boolean update(GameTime gameTime)
        {




            x += gameTime.ElapsedGameTime.Milliseconds * xvel * .5f;
            y += gameTime.ElapsedGameTime.Milliseconds * yvel * .5f;


            TTD -= gameTime.ElapsedGameTime.Seconds;
            if (TTD == 0)
            {
                return true;
            }

            return false;
        }

        public void draw(SpriteBatch spriteBatch)
        {

            //   spriteBatch.Draw(image, new Rectangle((int) x, (int) y, (int) image.Width, (int) image.Height), null, Color.Black, (float) (rotation * TTD), new Vector2(0, 0), SpriteEffects.None, 0);

                spriteBatch.Draw(image, new Vector2(x, y), Color.White);
            
        }
    }
}