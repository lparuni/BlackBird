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
    class Bubble : Drawable
    {
        // float yv;                        // velocity, only in the y-direction
        float a;                         // acceleration
        public static Boolean flipped;          // are the controls for the bird currently flipped?
        public static Boolean flipped2;
        Boolean keyHelds;                // is the key that controls the bird being held?
        public static Boolean alive;     // is the bird alive? true until it hits an obstacle
        public static Boolean human;     // is the "invicible" powerup activated?
        int framenumber;
        int lastGenerated;
        double timer = 0;
        public Texture2D big;
        ParticleBot super;

        ParticleBot circulation;
        Boolean isbleeding;
        public event CollidedEvent died;
        public delegate void UpdateMethod();
        static UpdateMethod[] updates = new UpdateMethod[10];

        UpdateMethod currUpdate;

        public Bubble(float xpos, float ypos, Texture2D unpopped, float scale, Texture2D bah)
            : base(xpos, ypos, unpopped, 0f, scale)
        {
            big = bah;
            a = 0f;                                 // initial acceleration is 0;
            flipped = false;                        // initial controls are NOT flipped    
            alive = true;
            keyHelds = false;
            lastGenerated = 0;
            human = true;
            currUpdate = updates[0];
            circulation = new ParticleBot(GamePlay.dictionary[25], true);
            isbleeding = false;
            super = new ParticleBot(GamePlay.dictionary[26], true);
            flipped2 = false;
        }




        // check to see if the bird has collided with a Drawable
        public void checkCollisions(Drawable obs)
        {
            if ((this.boundingBox1.Intersects(obs.boundingBox1) || this.boundingBox1.Intersects(obs.boundingBox2))
                || (this.boundingBox2.Intersects(obs.boundingBox1) || this.boundingBox2.Intersects(obs.boundingBox2)))
            {
                //this.pop(obs);
                // call on the obstacle instead

                System.Diagnostics.Debug.WriteLine(obs.currimage);
                if (Bubble.human && obs.getShow())
                    obs.collide();
                //could have another method here to call a say smashing effect
                // death = obs;
            }
        }

        // flip the controls
        public static void gravityChange()
        {
            /* if (!flipped)
                flipped = true;
            else
                flipped = false; */

             flipped = true;
             GamePlay.done = false;
            flipped2 = !flipped2;
        }

        // flip controls back to normal
        public static void normalGravity()
        {
                flipped = false;
        }

        // what is this..?
        public static void changeUpdate1()
        {
            human = false;
            //GamePlay.character.currUpdate = updates[1];//this is one way
            //GamePlay.vel *= 2;
        }

        // update position if key is held
        public void keyHeld(GameTime gameTime)
        {

            keyHelds = true;
            if (a >= -0.0001f)
            {
                a += -.00004f * gameTime.ElapsedGameTime.Milliseconds;
            }
            vel += a * gameTime.ElapsedGameTime.Milliseconds;
            y += gameTime.ElapsedGameTime.Milliseconds * vel;


        }


        // update position if key is not held
        public void keyNotHeld(GameTime gameTime)
        {

            keyHelds = false;

            if (a <= 0.0001f)
            {
                a += 0.00004f * gameTime.ElapsedGameTime.Milliseconds;
            }
            vel += a * gameTime.ElapsedGameTime.Milliseconds;
            y += gameTime.ElapsedGameTime.Milliseconds * vel;


        }

        // update the Bubble entirely
        public override Boolean update(GameTime gameTime)
        {

            super.update(gameTime);
            if (!human)
            {
                super.generate(4, (int)x + currimage.Width / 9, (int)y + currimage.Height / 5);
                

                timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 5)
                {
                    human = true;
                    timer = 0;
                }
            }

            int numbah = 100;
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

            //Boolean state = Keyboard.GetState().IsKeyDown(Keys.Space);
            if (!alive)
            {
                if (isbleeding == false)
                {
                    circulation.generate(100, (int)x + currimage.Width / 8, (int)y + currimage.Height / 3);
                    isbleeding = true;
                }
                circulation.update(gameTime);
                x += gameTime.ElapsedGameTime.Milliseconds * 0.2f;
                y += gameTime.ElapsedGameTime.Milliseconds * 0.2f;
                if (y >= 500)
                    died();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) ^ (flipped2))
            {
                keyHeld(gameTime);
            }
            else keyNotHeld(gameTime);

            // check if bubble has hit boundary and needs to pop
            if (y <= 0)
            {

                y = 0;

                vel *= -0.1f;



                y += gameTime.ElapsedGameTime.Milliseconds * vel;


            }


            if (y >= 640 - currimage.Height / 2)
            {

                y = 640 - currimage.Height/2;
                vel *= -0.1f;
                y += gameTime.ElapsedGameTime.Milliseconds * vel;

            }

            return true;

        }

        /*  public void GravityChange(Drawable draw)
          {
              if (draw.currimage == dictionary[10])
              {
                  draw.a *= -1;
                  draw.v *= -1;
              }
          }*/
        // change the image of bubble to popped image
        /* public void pop(Drawable draw)
         {
             // RAISE EVENT to go to GameOver screen
             if (bubbleCollided != null)
             {
                 bubbleCollided(draw);
             }
         }*/

        public new void draw(SpriteBatch spriteBatch)
        {

                super.draw(spriteBatch);
            
            circulation.draw(spriteBatch);
            if (keyHelds)
            {
                spriteBatch.Draw(currimage, new Vector2((int)x, (int)y), new Rectangle(0 + currimage.Width / 3 * framenumber, 0, currimage.Width / 3, currimage.Height), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0f);

            }
            else
            {
                spriteBatch.Draw(currimage, new Vector2((int)x, (int)y), new Rectangle(0 + currimage.Width / 3 * 2, 0, currimage.Width / 3, currimage.Height), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            }

   //             spriteBatch.Draw(big, new Rectangle((int)x + r1x, (int)y + r1y, r1width, r1height), Color.White);
    //            spriteBatch.Draw(big, new Rectangle((int)x + r2x, (int)y + r2y, r2width, r2height), Color.White);
        }
    }
}


