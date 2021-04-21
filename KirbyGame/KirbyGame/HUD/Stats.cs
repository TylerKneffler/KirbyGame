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
        private int _lives;
        private int _score;
        private int _health;
        private ePower _power;

       
        public enum ePower
        {
            NORMAL,
            BEAM,
            CUTTER
        }
        public event EventHandler ZeroLives;
        public event EventHandler TakeDamage;

        public Stats(int lifeTotal, int healthTotal, int startingScore)
        {
            _lives = lifeTotal;
            _score = startingScore;
            _health = healthTotal;
            _power = ePower.NORMAL;
        }

        public void mario_CollisionEvent(object sender, Collision collision)
        {
            
            IPointable temp;
            if (collision.A is IPointable)
            {
                Debug.WriteLine("Collision Event!");
                temp = (IPointable)collision.A;
                _score+= temp.Points();
            }
            if (collision.B is IPointable)
            {
                Debug.WriteLine("Collision Event!");
                temp = (IPointable)collision.B;
                _score += temp.Points();
            }
        }
        public void mario_TakeDamage(object sender, EventArgs e)
        {
            _health--;
            if (_health <= 0)
            {
                OnLifeLost(EventArgs.Empty);
            }
        }
        public void mario_PowerUpChange(object sender, ePower power)
        {
            _power = power;
        }

        public int GetLives()
        {
            return _lives;
        }
        public int GetScore()
        {
            return _score;
        }
        public int GetHealth()
        {
            return _health;
        }
        public ePower GetPower()
        {
            return _power;
        }
        protected virtual void OnLifeLost(EventArgs e)
        {
            //Insert reset game event or call here
            _lives--;

            if (_lives <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }

        }

        public void ResetHealth()
        {
            _health = 6;
        }
        protected virtual void OnZeroLives(EventArgs e)
        {
            ZeroLives?.Invoke(this, e);
            ResetHealth();

        }

        //public class Lives
        //{
        //    private int lifeTotal;

        //    public Lives(int lives)
        //    {
        //        lifeTotal = lives;
        //    }

        //    public int GetLives()
        //    {
        //        return lifeTotal;
        //    }
        //    public void RemoveOne()
        //    {
        //        lifeTotal--;
        //    }
        //}
        //public class Score
        //{
        //    private int score;
        //    public Score()
        //    {
        //        score = 0;
        //    }
        //    public Score(int startingScore)
        //    {
        //        score = startingScore;
        //    }
        //    public int GetScore()
        //    {
        //        return score;
        //    }
        //    public void Add(int x)
        //    {
        //        score += x;
        //    }
        //}
        //public class Health
        //{
        //    private int healthBars;


        //    public Health()
        //    {
        //        healthBars = 6;
        //    }

        //    public Health(int healthTotal)
        //    {
        //        healthBars = healthTotal;
        //    }

        //    public int GetHealth()
        //    {
        //        return healthBars;
        //    }
            
        //    public void RemoveOne()
        //    {
        //        healthBars--;
        //    }
        //}
    }
}
