using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BubbleGame
{
    class Block {
    
        Queue<Obstacle> obstacles;
        Queue<Animatable> animatables;
        Queue<double> timesteps;
        //Queue<double> timesteps2;//this might be used for the animatables
        double time = 0;
        public static CreateBlock[] blocks = new CreateBlock[15];
        //Block.blocks[0] = new BlockDone(easyBlock);
        public static Drawable.CollidedEvent[] onCollide = new Drawable.CollidedEvent[6];

        public delegate Block BlockDone();

        public delegate Block CreateBlock();

        public event BlockDone newBlock;

       

        public Block()
        {
            obstacles = new Queue<Obstacle>();
            animatables = new Queue<Animatable>();
            timesteps = new Queue<double>();
            time = 0;
 
        }

        public void add(Obstacle obs, double time)
        {
            obstacles.Enqueue(obs);
            timesteps.Enqueue(time/GamePlay.velRatio);
        }

        public void add(Animatable obs, double time)
        {
            animatables.Enqueue(obs);
            timesteps.Enqueue(time / GamePlay.velRatio);
        }

        //2800-- approx time to cover the screen's length at a vel of 0.4 f

        public static Block easyBlock()
        {
            Block curr = new Block();
            curr.add(Block.invincible(150), 2800);
            curr.add(Block.bee(250), 2800);
            curr.add(Block.tallTree(), 2800);
            curr.add(kite(250), 0);
            curr.add(Block.airplane(GamePlay.RandomGen(0, 200)), 1400);
            curr.add(Block.ovalTree(), 1400);
            curr.add(gravity(100), 0);
            return curr;
        }

        public static Block easyBlock2() //set up a gravity change //trap
        {
            Block curr = new Block();
            curr.add(fly(150), 1000);
            curr.add(gravity(400), 400);
            return curr;
        }

        public static Block easyBlock3()
        {
            Block curr = new Block();
            curr.add(Block.bee(150), 2800);
            curr.add(fly(150), 1100);
            curr.add(ovalTree(), 1600);
            return curr;
        }

        //time in timesteps is time elapsed since last obstacle was added
        public static Block mediumBlock()
        {
            Block curr = new Block();
            curr.add(Block.fly(150), 2800);
            curr.add(Block.tallTree(), 1800);
            curr.add(Block.airplane(GamePlay.RandomGen(0,51)), 0);
            curr.add(Block.airplane(GamePlay.RandomGen(50, 251)), 2100);
            curr.add(Block.tallTree(), 0);
            return curr;
        }

        public static Block mediumBlock2()
        {
            Block curr = new Block();
            curr.add(Block.kite(150), 2800);
            curr.add(Block.airplane(150), 1800);
            curr.add(Block.airplane(230), 1800);
            curr.add(Block.tallTree(), 100);
            curr.add(Block.airplane(50), 300);
            return curr;
        }

        public static Block mediumBlock3() //seemingly impossible//gottA get that powerup
        {
            Block curr = new Block();
            curr.add(tallTree(), 1000);
            curr.add(airplane(230), 100);
            curr.add(hangingTree(), 1000);
            curr.add(destroyer(300), 50);
            curr.add(tallTree(), 1000);
            return curr;
        }

        public static Block hardBlock()
        {
            Block curr = new Block();
            curr.add(hangingTree(), 1300);
            curr.add(airplane(350), 400);
            curr.add(airplane(500), 400);
            curr.add(tallTree(), 500);
            curr.add(hangingTree(), 1200);
            return curr;
        }

        
        public static Block hardBlock1()
        {
            Block curr = new Block();
           // curr.add(airplane(350), 1300);
            curr.add(airplane(250), 400);
            curr.add(tallTree(), 500);
            curr.add(hangingTree(), 1200);
            curr.add(hangingTree(), 800);
            curr.add(hangingTree(), 800);
            curr.add(ovalTree(), 0);
            /*curr.add(hangingTree(), 800);
            curr.add(ovaltree(), 0);
            curr.add(hangingTree(), 800);
            curr.add(ovaltree(), 0);
            curr.add(hangingTree(), 800);
            curr.add(ovaltree(), 0);
            curr.add(hangingTree(), 800);
            curr.add(ovaltree(), 0);*/
            //curr.add(gravity(400), 0);
            return curr;
        }
        public static Block hangingBlock()
        {
            Block a = new Block();
            a.add(Block.ovalTree(), 1000);
            a.add(Block.gravity(50), 0);
            a.add(Block.tallTree(), 1000);
            a.add(Block.hangingTree(), 1000);
           // a.add(Block.destroyer(vel, onCollide), 1200);
            a.add(Block.tallTree(), 1300);
            return a;
        }

        public static Block RandomGen()
        {
            int n = GamePlay.RandomGen(0,9);
            Block generated = Block.blocks[9]();
            return generated;
        }
        public static Block testBlock()
        {
            Block curr = new Block();
            curr.add(ovalTree(), 300);
            curr.add(gravity(300), 1000);
            curr.add(kite(150), 2800);
            curr.add(slowMo(200), 1000);
            return curr;
        }

        // DONE
        public static Obstacle ovalTree()
        {
            Obstacle ovaltree = new Obstacle(GamePlay.xpos, 640 - GamePlay.dictionary[15].Height + 95, GamePlay.dictionary[15], GamePlay.vel, 1f); //420
            ovaltree.defineBounds(60, 30, 80, 190, 95, 160, 10, 375);
            //115
            if (Bubble.human)
            {
                ovaltree.collided += GamePlay.destroy;
                ovaltree.collided += GamePlay.die;
            }
            else
            {
                ovaltree.collided += ovaltree.noShow;
            }
            return ovaltree;
        }

        // DONE
        public static Obstacle tallTree()
        {
            Obstacle talltree = new Obstacle(GamePlay.xpos, 640 - GamePlay.dictionary[13].Height + 90, GamePlay.dictionary[13], GamePlay.vel, 1f); //300
            talltree.defineBounds(80, 50, 145, 150, 140, 15, 40, 375);
            //110
            talltree.collided += GamePlay.destroy;
            talltree.collided += GamePlay.die;
            return talltree;
        }

        // DONE
        public static Obstacle hangingTree()
        {
            Obstacle a = new Obstacle(GamePlay.xpos, 640 - GamePlay.dictionary[14].Height + 35, GamePlay.dictionary[14], GamePlay.vel, 1f); //0
            a.defineBounds(55, 20, 130, 110, 115, 130, 10, 375);
            a.collided += GamePlay.destroy;
            a.collided += GamePlay.die;
            return a;
        }


        // DONE
        public static Obstacle airplane(float ypos)
        {
            Obstacle airplane = new Obstacle(GamePlay.xpos, ypos, GamePlay.dictionary[2], GamePlay.vel * 1.5f, .5f); //30
            airplane.defineBounds(15, 60, 100, 15, 60, 30, 70, 40);
            airplane.collided += GamePlay.destroy;
            airplane.collided += GamePlay.die;
            return airplane;
        }

        public static Animatable kite(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[7], GamePlay.vel * 1.5f, .75f);
            a.defineBounds(15, 50, 75, 45, 35, 35, 45, 75);
            a.collided += GamePlay.die;
            a.collided += a.noShow;
            return a;
        }



        public static Animatable bee(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[3], GamePlay.vel * 1.5f, 1f);
            a.defineBounds(13, 35, 30, 30, 40, 45, 30, 25);
            a.collided += GamePlay.die;
            return a;
        }

        public static Animatable fly(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[5], GamePlay.vel * 1.5f, 1f);
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += GamePlay.die;
            a.collided += a.noShow;
            return a;
        }

        public static Animatable slowMo(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[10], GamePlay.vel * 1.5f, 1f);
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += GamePlay.slowMo;
            a.collided += a.noShow;
            return a;
        }
        // END USABLE IMAGES

        public static Animatable gravity(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[8], GamePlay.vel*1.5f, 1f);
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += onCollide[3]; //changecontrols
            a.collided += a.noShow; //noShow
            return a;
        }

  /*      public static Animatable normalGravity(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[10], GamePlay.vel * 1.5f, 1f); //200
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += onCollide[5];
            a.collided += a.noShow; //noShow
            return a;
        }
   */
  
        public static Animatable destroyer(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[9], GamePlay.vel * 1.5f, 1f); //500
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += onCollide[2]; //destroy
            a.collided += a.noShow; //noShow
            return a;
        }

        public static Animatable invincible(float ypos)
        {
            Animatable a = new Animatable(GamePlay.xpos, ypos, GamePlay.dictionary[11], GamePlay.vel * 1.5f, 1f);
            a.defineBounds(23, 38, 28, 27, 0, 0, 0, 0);
            a.collided += Bubble.changeUpdate1;
            a.collided += a.noShow;
            return a;
        }
        public void update(GameTime gameTime, Queue<Drawable> drawables, Queue<Animatable> animat)
        {
            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            
            while (time >= timesteps.Peek())
            {
                Drawable a;
                Animatable b;
                if (obstacles.Count != 0)
                {
                    a = obstacles.Dequeue();
                    time -= timesteps.Dequeue();
                    drawables.Enqueue(a);
                }
                //time -= timesteps.Dequeue();
                if (animatables.Count != 0)
                {
                    b = animatables.Dequeue();
                    time -= timesteps.Dequeue();
                    animat.Enqueue(b);
                }

                
                //time -= timesteps.Dequeue();
                if ((obstacles.Count == 0) && (animatables.Count == 0))
                {
                    //time = 0;//unnecessary, redundant if code functions properly
                    newBlock();
                    break;

                }
            }


        }

    }
}
