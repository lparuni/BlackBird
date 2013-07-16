using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BubbleGame
{
    class GamePaused : IScreen
    {
        string screen;

        public GamePaused()
        {
            screen = "Pause";
        }
        public void update(GameTime gameTime)
        {

        }
        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(GamePlay.dictionary[4], new Vector2(0, 0), Color.White);
        }
        public void getLast()
        {
            Game1.ChangeScreen(new MainMenu());
        }

        public void getNext()
        {
            Game1.numbertrack = 0;
            Game1.ChangeScreen(Game1.lastScreen);
        }

        public string getScreen()
        {
            return screen;
        }
    }
}
