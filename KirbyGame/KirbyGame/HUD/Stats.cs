﻿using System;
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

        public Stats(int lifeTotal, int healthTotal, int startingScore)
        {
            _lives = lifeTotal;
            _score = startingScore;
            _health = healthTotal;
            _power = ePower.NORMAL;
        }

        public void mario_CollisionEvent(object sender, Collision collision)
        {
            //Debug.WriteLine("Collision Event!");
            IPointable temp;
            if (collision.A is IPointable)
            {
                temp = (IPointable)collision.A;
                _score+= temp.Points();
            }
            if (collision.B is IPointable)
            {
                temp = (IPointable)collision.B;
                _score += temp.Points();
            }

            if (collision.A is EnemyTest && collision.B is Avatar)
            {
                Debug.WriteLine("Collision enemy!");
                Debug.WriteLine("Health Pre: "+_health);
                _health--;
                Debug.WriteLine("Health Post: " + _health);
            }
            if (collision.B is EnemyTest && collision.A is Avatar)
            {
                Debug.WriteLine("Collision enemy!");
                Debug.WriteLine("Health Pre: " + _health);
                _health--;
                Debug.WriteLine("Health Post: " + _health);
            }

            if (_health <= 0)
            {
                OnLifeLost(EventArgs.Empty);
            }
        }
        public void mario_PowerUpChange(object sender, ePower power)
        {
            //do something
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

        protected virtual void OnLifeLost(EventArgs e)
        {
            //Insert reset game event or call here
            _lives--;
            _health = 6;

            if (_lives <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }

        }
        protected virtual void OnZeroLives(EventArgs e)
        {
            ZeroLives?.Invoke(this, e);
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