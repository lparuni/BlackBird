using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BubbleGame
{
    class Instructions : IScreen
    {
        string screen;
        public Instructions()
        {
            screen = "Instructions";
        }

        public void update(GameTime gameTime)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GamePlay.dictionary[27], new Vector2(0, 0), Color.White);
        }

        public void getNext()
        {
            Game1.ChangeScreen(new MainMenu());
        }

        public void getLast()
        {
            Game1.ChangeScreen(new MainMenu());
        }

        public string getScreen()
        {
            return screen;
        }
    }
}
