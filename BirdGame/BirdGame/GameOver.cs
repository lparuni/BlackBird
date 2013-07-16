using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;



namespace BubbleGame
{
    class GameOver : IScreen
    {
        HighScores hs;
        Dictionary<int, Texture2D> dictionary;
        FileStream stream;
        XmlSerializer serializer = new XmlSerializer(typeof(HighScores));
        string screen;

        // [DllImport("user32.dll", CharSet = CharSet.Auto)]
        // public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public GameOver(Dictionary<int, Texture2D> dict)
        {
            dictionary = dict;
            screen = "GameOver";
            int highScore = GamePlay.getScore();
            if (File.Exists("highscore1.xml"))
            {

                // Open the file
                stream = File.Open("highscore1.xml", FileMode.Open,
                        FileAccess.ReadWrite);

                // Stream reader = new FileStream("highscore1s.xml", FileMode.Open);
                hs = (HighScores)serializer.Deserialize(stream);
                stream.Close();
                File.Delete("highscore1.xml");
                stream = File.Open("highscore1.xml", FileMode.OpenOrCreate, FileAccess.Write);

                //   reader.Close();
            }
            else
            {
                stream = File.Open("highscore1.xml", FileMode.OpenOrCreate, FileAccess.Write);

                hs = new HighScores();
                for (int i = 0; i < 5; i++)
                {
                    hs.scores[i] = 0;
                }

            }

            save(highScore);

        }


        public void update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

            }
        }

        public void save(int newHighScore)
        {
            if (hs.scores[0] <= newHighScore)
            {

                hs.scores[0] = newHighScore;
            }

            Array.Sort(hs.scores);

            serializer.Serialize(stream, hs);

            // Close the file
            stream.Close();
        }

        /*

        public void save(int newHighScore, FileStream stream, XmlSerializer serializer)
        {

            hs.insert(newHighScore);

            serializer.Serialize(stream, hs);
            
            // Close the file
            stream.Close();


        }
        */
        public string getScreen()
        {
            return screen;
        }

        public void getNext()
        {
            Game1.ChangeScreen(new GamePlay(GamePlay.dictionary));
        }
        public void getLast()
        {
            Game1.ChangeScreen(new MainMenu());
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(dictionary[21], new Vector2(0, 0), Color.White);
            string printthis;
            int count = 0;


            spriteBatch.DrawString(Game1.spriteFont, "High Score Table", new Vector2(750, 175), Color.Black);
            count++;
            for (int i = 4; i >= 0; i--)
            {
                printthis = hs.scores[i].ToString();
                spriteBatch.DrawString(Game1.spriteFont, printthis, new Vector2(750, 175 + 50 * count), Color.Black);
                count++;
            }

        }


    }




}






