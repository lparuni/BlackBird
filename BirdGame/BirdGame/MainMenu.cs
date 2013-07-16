using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubbleGame
{
    class MainMenu: IScreen
    {
        string screen;
        int framenumber;
        int lastGenerated;
        public MainMenu()
        {
            screen = "MainMenu";
            framenumber = 0;
            lastGenerated = 0;
        }
        public void update(GameTime gameTime)
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

        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GamePlay.dictionary[12], new Vector2(0, 0), Color.White);
            spriteBatch.Draw(GamePlay.dictionary[22 + framenumber], new Vector2(0, 150), Color.White);
        }

        public void getNext()
        {
            Game1.ChangeScreen(new GamePlay(GamePlay.dictionary));
        }

        public void getLast()
        {
            Game1.ChangeScreen(new Instructions());
        }

        public string getScreen()
        {
            return screen;
        }
    }
}
