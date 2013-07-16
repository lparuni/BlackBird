using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    [Serializable]
    public class HighScores
    {
        public int[] scores;          // array to store top 5 scores
    //    public int min;                            // stores index of min score/name    
           
            
            public HighScores()
            {
                scores = new int[5];
    //            min = 0;
            }

        /*
            public void insert(int score)
            {
                Boolean track = false;
                for (int i = 0; i < 5; i++)
                {
                    if ((scores[i] == 0) && (track == false))
                    {
                        scores[i] = score;
                        track = true;
                    }
                }

                Array.Sort(scores);

                
            }

        */
        
    }

}
