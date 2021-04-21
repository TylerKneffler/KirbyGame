using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

namespace KirbyGame
{
    public class ControllerKeyboard : IController
    {
        KeyboardState previousKeyboardState;
        KeyboardState currentKeyboardState;

        private Dictionary<Keys, ICommand> KeyPressMap;
        private Dictionary<Keys, ICommand> KeyReleaseMap;

        public ControllerKeyboard()
        {
            previousKeyboardState = Keyboard.GetState();
            KeyPressMap = new Dictionary<Keys, ICommand>();
            KeyReleaseMap = new Dictionary<Keys, ICommand>();
        }

        public void UpdateInput()
        {
            // Get the current gamepad state.
             currentKeyboardState = Keyboard.GetState();

            foreach (KeyValuePair<Keys, ICommand> pair in KeyPressMap)
            {
                if (currentKeyboardState.IsKeyDown(pair.Key) && !previousKeyboardState.IsKeyDown(pair.Key))
                {
                    if (pair.Value != null)
                        pair.Value.Execute();
                }
            }

            foreach (KeyValuePair<Keys, ICommand> pair in KeyReleaseMap)
            {
                if (!currentKeyboardState.IsKeyDown(pair.Key) && previousKeyboardState.IsKeyDown(pair.Key))
                {
                    if (pair.Value != null)
                        pair.Value.Execute();
                }
            }
            previousKeyboardState = currentKeyboardState;
        }

        public void addPressCommand(Keys key, ICommand value)
        {
            KeyPressMap.Add(key, value);
        }

        public void removePressCommand(Keys key)
        {
            KeyPressMap.Remove(key);
        }

        public void addReleaseCommand(Keys key, ICommand value)
        {
            KeyReleaseMap.Add(key, value);
        }

        public void removeReleaseCommand(Keys key)
        {
            KeyReleaseMap.Remove(key);
        }

        public void clearCommands()
        {
            KeyPressMap.Clear();
            KeyReleaseMap.Clear();
        }

        public void LoadDefaultCommands(Avatar mario, Game1 game)
        {
            addPressCommand(Keys.X, new MarioPressJump(mario));
            addPressCommand(Keys.Left, new MarioPressLeft(mario));
            addPressCommand(Keys.Down, new MarioPressDown(mario));
            addPressCommand(Keys.Right, new MarioPressRight(mario));
            addPressCommand(Keys.Up, new MarioPressFloat(mario));
            addReleaseCommand(Keys.X, new MarioReleaseJump(mario));
            addReleaseCommand(Keys.Left, new MarioReleaseLeft(mario));
            addReleaseCommand(Keys.Down, new MarioReleaseDown(mario));
            addReleaseCommand(Keys.Right, new MarioReleaseRight(mario));
            addReleaseCommand(Keys.Up, new MarioReleaseFloat(mario));
            addPressCommand(Keys.C, new BoundingBoxToggle(game));
            addPressCommand(Keys.Z, new AvatarTrigger(mario));
            addReleaseCommand(Keys.Z, new AvatarReleaseTrigger(mario));
            addPressCommand(Keys.M, new ToggleMute(game));
            addPressCommand(Keys.P, new TogglePause(game));
            addPressCommand(Keys.LeftShift, new AvatarClearPower(mario));
            addPressCommand(Keys.Q, new ExitCommand(game));
            addPressCommand(Keys.R, new ResetCommand(game));
            addPressCommand(Keys.Space, new MarioFireBall(mario));
        }
    }
}