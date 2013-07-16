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
    abstract class Drawable
    {
        protected float x, y;                 // position
        public Texture2D currimage;           // current image
        protected float vel;                  // the velocity
        public Boolean show;                         // should the image be shown
        protected float scale;

        // first bounding rectangle stuff
        protected int r1width, r1height;
        protected int r1x, r1y;

        // second bounding rectangle stuff
        public int r2width, r2height;
        public int r2x, r2y;

        public delegate void CollidedEvent();
        public event CollidedEvent collided;

        int[] boundingboxinfo;

        public Drawable(float xpos, float ypos, Texture2D image, float velocity, float scaleit)
        {
            x = xpos;
            y = ypos;
            vel = velocity;
            currimage = image;
            scale = scaleit;
            show = true;
            boundingboxinfo = new int[8];



        }

        public void defineBounds(int x1, int y1, int width1, int height1, int x2, int y2, int width2, int height2)
        {

            r1width = width1;
            r1height = height1;
            r1x = x1;
            r1y = y1;


            r2width = width2;
            r2height = height2;
            r2x = x2;
            r2y = y2;
        }


        public Rectangle boundingBox1
        {
            get
            {
                return new Rectangle((int)x + r1x, (int)y + r1y, r1width, r1height);
            }

        }

        public Rectangle boundingBox2
        {
            get
            {
                return new Rectangle((int)x + r2x, (int)y + r2y, r2width, r2height);
            }
        }


        public abstract Boolean update(GameTime gameTime);



        // return the x-position
        public float getX()
        {
            return x;
        }

        // return the y-position
        public float getY()
        {
            return y;
        }

        public void changeVel(float velo)
        {
            this.vel = velo;
        }

        public void noShow()
        {
            show = false;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (show)
                spriteBatch.Draw(currimage, new Vector2((int)x, (int)y), new Rectangle(0, 0, currimage.Width, currimage.Height), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r1x, (int)y + r1y, r1width, r1height), Color.Red);
            //spriteBatch.Draw(BubbleGame.GamePlay.character.big, new Rectangle((int)x + r2x, (int)y + r2y, r2width, r2height), Color.Red);
        }

        public Boolean equalsTo(Drawable draw)
        {
            if (this.currimage == draw.currimage)
            {
                return true;
            }
            else
                return false;
        }

        public void collide()
        {
            collided();
        }
        public Boolean getShow()
        {
            if (show)
                return true;
            else
                return false;
        }
        public float getVel()
        {
            return vel;
        }
    }
}

