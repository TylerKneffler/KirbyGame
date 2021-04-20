using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    public class Score
    {
        Collision cl;
        private int score;

        public Score()
        {
            cl = new Collision();
            cl.CollisionEvent += cl_CollisionEvent;
            score = 0;
        }

        public Score(int startingScore)
        {
            cl = new Collision();
            cl.CollisionEvent += cl_CollisionEvent;
            score = startingScore;
        }

        public int GetScore()
        {
            return score;
        }

        public void cl_CollisionEvent(object sender, Collision collision)
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
