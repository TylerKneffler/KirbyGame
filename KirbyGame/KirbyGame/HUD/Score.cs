using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame.HUD
{
    public class Score
    {
        private int score;

        public Score()
        {
            score = 0;
        }

        public int GetScore()
        {
            return score;
        }

        //public void OnAddScore(object source, Collision collision)
        //{
        //    if (collision.A is IPointable)
        //        score += collision.A.Points();
        //    if (collision.B is IPointable)
        //        score += collision.B.Points();
        //}

        public void OnAddScore(object source, Collision collision)
        {
            IPointable temp;
            if (collision.A is IPointable)
            {
                temp = (IPointable)collision.A;
                score += temp.Points();
            }
            if (collision.B is IPointable)
            {
                temp = (IPointable)collision.B;
                score += temp.Points();
            }
        }
    }
}
