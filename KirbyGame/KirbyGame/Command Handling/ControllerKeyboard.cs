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
    }
}