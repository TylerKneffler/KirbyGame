using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KirbyGame
{
    public class Stats
    {
        private Lives _lives;
        private Score _score;
        private Health _health;

        public event EventHandler ZeroLives;

        public Stats(int lifeTotal, int healthTotal, int startingScore)
        {
            _lives = new Lives(lifeTotal);
            _score = new Score(startingScore);
            _health = new Health(healthTotal);
        }

        public Stats()
        {

        }

        public void cl_CollisionEvent(object sender, Collision collision)
        {
            IPointable temp;
            if (collision.A is IPointable)
            {
                temp = (IPointable)collision.A;
                _score.Add(temp.Points());
            }
            if (collision.B is IPointable)
            {
                temp = (IPointable)collision.B;
                _score.Add(temp.Points());
            }

            if (collision.A is EnemyTest && collision.B is Avatar)
            {
                _health.RemoveOne();
            }
            if (collision.B is EnemyTest && collision.A is Avatar)
            {
                _health.RemoveOne();
            }

            if (_health.GetHealth() <= 0)
            {
                OnLifeLost(EventArgs.Empty);
            }
        }

        public Lives GetLives()
        {
            return _lives;
        }
        public Score GetScore()
        {
            return _score;
        }
        public Health GetHealth()
        {
            return _health;
        }

        protected virtual void OnLifeLost(EventArgs e)
        {
            _lives.RemoveOne();

            if (_lives.GetLives() <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }

        }
        protected virtual void OnZeroLives(EventArgs e)
        {
            ZeroLives?.Invoke(this, e);
        }

        public class Lives
        {
            private int lifeTotal;

            public Lives(int lives)
            {
                lifeTotal = lives;
            }

            public int GetLives()
            {
                return lifeTotal;
            }
            public void RemoveOne()
            {
                lifeTotal--;
            }
        }
        public class Score
        {
            private int score;
            public Score()
            {
                score = 0;
            }
            public Score(int startingScore)
            {
                score = startingScore;
            }
            public int GetScore()
            {
                return score;
            }
            public void Add(int x)
            {
                score += x;
            }
        }
        public class Health
        {
            private int healthBars;


            public Health()
            {
                healthBars = 6;
            }

            public Health(int healthTotal)
            {
                healthBars = healthTotal;
            }

            public int GetHealth()
            {
                return healthBars;
            }
            
            public void RemoveOne()
            {
                healthBars--;
            }
        }
    }
}
