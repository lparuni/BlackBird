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
    class ParticleBot
    {
        Texture2D particle;            // dictionary of images
        Queue<Particle> collect = new Queue<Particle>();
        Boolean random;

        // create a ParticleBot
        public ParticleBot(Texture2D image, Boolean yesno)
        {
            particle = image;
            random = yesno;
        }

        // Generate n particles that emit from (xp, yp) 
        public void generate(int n, int xp, int yp)
        {
            for (int i = 0; i < n; i++)
            {
                collect.Enqueue(new Particle(particle, xp, yp, i, n, random));
            }
        }

        // update all the Particles currently managed by ParticleBot
        public void update(GameTime gameTime)
        {
            Boolean track = false;
            int n = collect.Count;
            while (n != 0)
            {
                Particle a = (Particle)collect.Dequeue();

                track = a.update(gameTime);

                if (track == false)
                {
                    collect.Enqueue(a);
                }

                n--;
            }
        }

        // draw all the particles
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Particle a in collect)
            {
                a.draw(spriteBatch);
            }
        }
    }
}



