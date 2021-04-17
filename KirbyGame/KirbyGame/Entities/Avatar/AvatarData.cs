using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    class AvatarData
    {
        public const float AVATAR_FRICTION = .9F;
        public const float AVATAR_STOPPING_VELOCITY = .1F;

        public const int LARGE_CROUCH_ADJUST = 20;
        public const int JUMP_MAX_TIME = 350;

        public const float DEFAULT_RUNNING_ACCELERATION = .2F;
        public const float DEFAULT_FLOATING_ACCELERATION = .1F;
        public const float MAX_FLOAT_SPEED = 2;
        public const int INIT_JUMP_VELOCITY = -6;
        public const float INIT_RUN_VELOCITY = 1F;

        public const float GRAVITY = .3F;

        public const int INIT_NUM_LIVES = 3;
        public const int BOUNDING_BOX_COLLISION_TIMER = 180;
        public const int PIPE_TRANSITION_UP_ADJUST = 16;
        public const int POWER_TRANSITION_ADJUST = 32;
        public const int PIPE_TRANSITION_SIDE_ADJUST = 24;

        public const int ENEMY_BOUNCE_VELOCITY = -4;
    }
}
