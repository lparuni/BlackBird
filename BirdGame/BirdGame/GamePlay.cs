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
    class GamePlay : IScreen
    {
        public static Dictionary<int, Texture2D> dictionary;   // stores the Texture2Ds used
        static Queue<Drawable> drawables;                      // store the drawables
        static Queue<Animatable> animatables;                  // store the animatables
        public static Bubble character;                        // the bird character
        static int score;                                             // the player's score
        double timer;                                             //keeps track of time to increase speed 
        double timer2;
        public static double slowTimer;
        public static Boolean done;   //checks if the pause has already been used once 
        double secondTimer;
        public string screen;
        // cloud background positions 1-3
        float backgroundx1;
        float backgroundx2;
        float backgroundx3;

        Doggy fido;                                             // the Dog
        public static Boolean isDog;                            // is the Dog on the screen?



        // i don't know yet
        int lastGenerated50 = 0;
        int lastGenerated = 0;
        int lastGenerated2 = 0;
        int lastGenerated3, lastGenerated4 = 0;
        int spacer = 0;
        float xp = 1136;
        Block currBlock;
        int track;
        public static float vel = 0.4f;
        Drawable.CollidedEvent gameOver;
        public static int xpos = 1136;
        public static double velRatio = GamePlay.vel / 0.4;
        public Texture2D big;

        public GamePlay(Dictionary<int, Texture2D> dict)
        {
            dictionary = dict;
            drawables = new Queue<Drawable>();
            animatables = new Queue<Animatable>();
            screen = "GamePlay";

            character = new Bubble(75, 270, dict[1], 0.6f, dict[6]);
            character.defineBounds(90, 25, 35, 28, 63, 57, 39, 35);
            backgroundx1 = 0;
            backgroundx2 = 1136;
            backgroundx3 = -1136;
            isDog = false;
            score = 0;



            // stuff after this idk
            gameOver = Block.onCollide[0];



            // character.collided += destroy;
            // character.collided += Bubble.die;
            character.died += gameOver;
            currBlock = newRandomBlock();
            // currBlock.newBlock += basicBlock;

            // PowerUp.collided += gravity;


        }
        //GameScreen methods
        public static void change(String button)
        {
            IScreen a = new GameOver(GamePlay.dictionary);
            if (button.Equals("Space Bar"))
            {
                 a = new GamePlay(dictionary);
                // a.buttonPressed += change;
            }
            
      
            Game1.ChangeScreen(a);
        }

        //
        public static void updateVel()
        {
            GamePlay.velRatio = 0.4 / GamePlay.vel;
        }
        
        public static void die()
        {
            //character.xv = 0.1f;
            Bubble.alive = false;
        }

        //generate block
        public Block newRandomBlock()
        {
            currBlock = Block.RandomGen();
            currBlock.newBlock += newRandomBlock;
            return currBlock;
        }

        // generate a random number
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);

        }

        public string getScreen()
        {
            return screen;
        }
        public static int getScore()
        {
            return score;
        }

        static Random r = new Random();
        public static int RandomGen(int min1, int max1)
        {
            return r.Next(min1, max1);

        }

        public void updateBackground(GameTime gameTime)
        {
            // update cloud background 1
            if (backgroundx1 >= -1136 * 2)
            {
                backgroundx1 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                backgroundx1 = 1136;
                backgroundx1 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }

            // update cloud background 2
            if (backgroundx2 >= -1136 * 2)
            {
                backgroundx2 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                backgroundx2 = 1136;
                backgroundx2 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }

            // update cloud background 3
            if (backgroundx3 >= -1136 * 2)
            {
                backgroundx3 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                backgroundx3 = 1136;
                backgroundx3 -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }

        }

        // changed to static methods to allow access from Game1 while loading into array of events
        //check later to see if better way exists to do this.
        public static void slowMo()
        {
            //slowTimer = 1;
            vel = .2f;
           // updateVel();
            int n = drawables.Count;
            int b = animatables.Count;
            while (n != 0)
            {
                Drawable a = drawables.Dequeue();
                a.changeVel(vel);
                drawables.Enqueue(a);
                n--;
            }

            while (b != 0)
            {
                Animatable a = animatables.Dequeue();
                a.changeVel(vel);
                animatables.Enqueue(a);
                b--;
            }
        }

        public static void destroy()
        {
            while (drawables.Count != 0)
            {
                drawables.Dequeue();
            }
        }

        public void getNext()
        {
            
        }

        public void getLast()
        {
            Game1.lastScreen = Game1.currscreen;
            Game1.ChangeScreen(new GamePaused());
        }
     
        public void update(GameTime gameTime)
        {
            /*timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= 5)
            {
                GamePlay.vel += 0.1f;
                timer = 0;
            }*/

            //updateVel();
            
           /* foreach (Animatable c in animatables)
            {
                character.checkCollisions(c);
                c.update(gameTime);
            }*/


            if (vel != .4f)
            {
                slowTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (slowTimer >= 5000)
                {
                    vel = 0.4f;
                   // updateVel();
                }
            }
            if (Bubble.flipped && timer2 < 1 && !done )
            {
                timer2 += gameTime.ElapsedGameTime.TotalSeconds;

            }
           /* else if (Bubble.flipped && timer2 < 1 && done)
            {
                done = false;
            } */

            else if (!Bubble.flipped ^ (Bubble.flipped && timer2 > 1) ^ (Bubble.flipped && done) ^ (secondTimer >= 20 && done))
            {
                if (Bubble.alive)
                {

                    updateBackground(gameTime);
                    currBlock.update(gameTime, drawables, animatables);

                }
                character.update(gameTime);

                if (timer2 >= 1)
                {
                    done = true;
                    Bubble.flipped = false;
                    timer2 = 0;
                }
                if (Bubble.flipped && done)
                {
                    secondTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    
                }

                if (secondTimer >= 20)
                    secondTimer = 0;




                if (Bubble.alive)
                score += 1;
                int n = drawables.Count;
                Boolean test = false;
                while (n != 0)
                {
                    Drawable a = drawables.Dequeue();

                    character.checkCollisions(a);

                    test = a.update(gameTime);

                    if (test)
                    {
                        drawables.Enqueue(a);
                    }

                    n--;
                }

                int i = animatables.Count;
                Boolean test2 = false;
                while (i != 0)
                {
                    Animatable a = animatables.Dequeue();

                    character.checkCollisions(a);

                    test2 = a.update(gameTime);

                    if (test2)
                    {
                        animatables.Enqueue(a);
                    }
                    i--;
                }
            }

            //      int numb = 100;
            //          if ((gameTime.TotalGameTime.TotalMilliseconds - lastGenerated50) >= numb)
            //          {


            //    lastGenerated50 += numb;
            //         }

            // if the bird's position falls within range, and the Dog isn't already called, call the doggy
            if ((character.getY() > 500) && (isDog == false))
            {


                fido = new Doggy(dictionary[16]);
                fido.collided += GamePlay.die;
              //  animatables.Enqueue(fido);
                fido.defineBounds(23, 38, 75, 60, 30, 115, 150, 40);

                isDog = true;
            }

            // if the Dog is called, update its position and check to see if it moved off the screen
            if (isDog)
            {
                character.checkCollisions(fido);
                fido.update(gameTime);
                Boolean t = fido.offScreen();
                if (t == true)
                {
                    isDog = false;
                }

            } 






        }

        public void draw(SpriteBatch spriteBatch)
        {

            // draw the sun background
            spriteBatch.Draw(dictionary[20], new Vector2(0, 0), Color.White);

            // draw the cloud backgrounds
            spriteBatch.Draw(dictionary[17], new Vector2(backgroundx1, 0), Color.White);
            spriteBatch.Draw(dictionary[18], new Vector2(backgroundx2, 0), Color.White);
            spriteBatch.Draw(dictionary[19], new Vector2(backgroundx3, 0), Color.White);

            // draw the bird
            character.draw(spriteBatch);

            // draw the dog (if it is on the screen)
            if (isDog)
            {
                fido.draw(spriteBatch);
            }

            // Draw all the drawables
            foreach (Drawable a in drawables)
            {
                a.draw(spriteBatch);
            }

            // Draw all the animatables
            foreach (Animatable a in animatables)
            {
                a.draw(spriteBatch);
            }

            // print the scores on the screen
            string scores = score.ToString();
            spriteBatch.DrawString(Game1.spriteFont, scores, new Vector2(1000, 0), Color.Black);
            string GETREADY = "PREPARE FOR CONTROL FLIP";
            
            if (Bubble.flipped == true)
            {
//                spriteBatch.DrawString(Game1.spriteFont, GETREADY, new Vector2(380, 250), Color.Black);
                spriteBatch.DrawString(Game1.spriteFont, GETREADY, new Vector2(330, 250), Color.Black, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            }

        }

    }
}


