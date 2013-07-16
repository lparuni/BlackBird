using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BubbleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static IScreen currscreen;                         // stores the current screen of the game
        Dictionary<int, Texture2D> dictionary;      // dictionary of Texture2Ds that is used throughout the game
        public static SpriteFont spriteFont;
        public Texture2D big;
        public static IScreen lastScreen;
        public delegate void ButtonEvent();
        double timetrack;
        public static int numbertrack;
        public event ButtonEvent buttonPressed;

        public event ButtonEvent button2Pressed;
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public Game1()
        {
            this.buttonPressed += gameScreen;
            this.button2Pressed += gameScreen2;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 640;
            graphics.PreferredBackBufferWidth = 1136;
            Content.RootDirectory = "Content";
            timetrack = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Courier New");
            // TODO: use this.Content to load your game content here

            // create the dictionary
            dictionary = new Dictionary<int, Texture2D>();

            // fill in dictionary with loaded images
            // 1 is the bird image
            dictionary.Add(1, Content.Load<Texture2D>("bird"));

            // obstacles
            dictionary.Add(2, Content.Load<Texture2D>("airplane"));
            big = Content.Load<Texture2D>("test");
            // animatables
            dictionary.Add(3, Content.Load<Texture2D>("bee"));
            dictionary.Add(4, Content.Load<Texture2D>("pausescreen"));
            dictionary.Add(5, Content.Load<Texture2D>("fly"));

            dictionary.Add(6, Content.Load<Texture2D>("test"));
            dictionary.Add(7, Content.Load<Texture2D>("kite"));
            dictionary.Add(8, Content.Load<Texture2D>("swirlfly"));
            dictionary.Add(9, Content.Load<Texture2D>("shockfly"));
            dictionary.Add(10, Content.Load<Texture2D>("superfly"));
            dictionary.Add(11, Content.Load<Texture2D>("starfly"));

            dictionary.Add(12, Content.Load<Texture2D>("menuscreen"));
            dictionary.Add(13, Content.Load<Texture2D>("tree2"));
            dictionary.Add(14, Content.Load<Texture2D>("tree3"));
            dictionary.Add(15, Content.Load<Texture2D>("tree1"));
            // 16 is the dog image
            dictionary.Add(16, Content.Load<Texture2D>("dog"));
            // 17 through 19 are the different cloud images
            dictionary.Add(17, Content.Load<Texture2D>("clouds1"));
            dictionary.Add(18, Content.Load<Texture2D>("clouds2"));
            dictionary.Add(19, Content.Load<Texture2D>("clouds3"));
            // 20 is the background sun
            dictionary.Add(20, Content.Load<Texture2D>("sun"));
            dictionary.Add(21, Content.Load<Texture2D>("gameoverscreen"));
            dictionary.Add(22, Content.Load<Texture2D>("menubird1"));
            dictionary.Add(23, Content.Load<Texture2D>("menubird2"));
            dictionary.Add(24, Content.Load<Texture2D>("menubird3"));
            dictionary.Add(25, Content.Load<Texture2D>("blood"));
            dictionary.Add(26, Content.Load<Texture2D>("finalstar"));
            dictionary.Add(27, Content.Load<Texture2D>("instructions"));
            Obstacle.CollidedEvent gameOver = () =>
            {
                GamePlay.destroy(); ChangeScreen(new GameOver(dictionary));
            };

            //add blocks to array
            Block.blocks[0] = new Block.CreateBlock(Block.easyBlock);
            Block.blocks[1] = new Block.CreateBlock(Block.easyBlock2);
            Block.blocks[2] = new Block.CreateBlock(Block.easyBlock3);
            Block.blocks[3] = new Block.CreateBlock(Block.mediumBlock);
            Block.blocks[4] = new Block.CreateBlock(Block.mediumBlock2);
            Block.blocks[5] = new Block.CreateBlock(Block.mediumBlock3);
            Block.blocks[6] = new Block.CreateBlock(Block.hardBlock);
            Block.blocks[7] = new Block.CreateBlock(Block.hangingBlock);
            Block.blocks[8] = new Block.CreateBlock(Block.hardBlock1);
            Block.blocks[9] = new Block.CreateBlock(Block.testBlock);



            // add collision results to array
            Block.onCollide[0] = new Drawable.CollidedEvent(gameOver);
            Block.onCollide[1] = new Drawable.CollidedEvent(GamePlay.slowMo);
            Block.onCollide[2] = new Drawable.CollidedEvent(GamePlay.destroy);
            Block.onCollide[3] = new Drawable.CollidedEvent(Bubble.gravityChange);
            Block.onCollide[4] = new Drawable.CollidedEvent(GamePlay.die);
     //       Block.onCollide[5] = new Drawable.CollidedEvent(Bubble.normalGravity);

            // set current screen to GamePlay
            new GamePlay(dictionary);
            currscreen = new MainMenu();


        }

        public static void gameScreen()
        {
            string now = currscreen.getScreen();
            Dictionary<int, Texture2D> a = GamePlay.dictionary;
            /* if (now == "Pause")
             {
                 ChangeScreen(lastScreen);
             }
             else if (now == "MainMenu")
             {
                 ChangeScreen(new GamePlay(GamePlay.dictionary)); 
             }
             else if (now == "GameOver" && Keyboard.GetState().IsKeyDown(Keys.Delete))
             {
                 ChangeScreen(new MainMenu());
             }
             else if (now == "GameOver")
             {
                 ChangeScreen(new GamePlay(GamePlay.dictionary)); 
             }

             else if (now == "GamePlay")
             {
                 lastScreen = currscreen;
                 ChangeScreen(new GamePaused());
             }*/
            currscreen.getNext();
        }

        public static void gameScreen2()
        {
            currscreen.getLast();
        }

        public static void ChangeScreen(IScreen iScreen)
        {
            currscreen = iScreen;
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (currscreen.getScreen() == "GameOver")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))// && currscreen.getScreen() != "GamePlay")
            {
                buttonPressed();
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                timetrack += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (numbertrack < 1)
                {
                    button2Pressed();
                    numbertrack++;
                    timetrack = 0;

                }
            }


            currscreen.update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            currscreen.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

