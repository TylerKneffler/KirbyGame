using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

namespace KirbyGame
{
    public class ControllerGamepad : IController
    {

        GamePadState currentGamepadState;
        GamePadState previousGamePadState;

        private Dictionary<Buttons, ICommand> ButtonPressMap;
        private Dictionary<Buttons, ICommand> ButtonReleaseMap;

        public ControllerGamepad()
        {
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            ButtonPressMap = new Dictionary<Buttons, ICommand>();
            ButtonReleaseMap = new Dictionary<Buttons, ICommand>();
        }
        public void UpdateInput()
        {
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            foreach (KeyValuePair<Buttons, ICommand> pair in ButtonPressMap)
            {
                if (currentGamepadState.IsButtonDown(pair.Key) && !previousGamePadState.IsButtonDown(pair.Key))
                {
                    if (pair.Value != null)
                        pair.Value.Execute();
                }
            }
            foreach (KeyValuePair<Buttons, ICommand> pair in ButtonPressMap)
            {
                if (!currentGamepadState.IsButtonDown(pair.Key) && previousGamePadState.IsButtonDown(pair.Key))
                {
                    if (pair.Value != null)
                        pair.Value.Execute();
                }
            }
            previousGamePadState = currentGamepadState;
        }

        public void addPressCommand(Buttons key, ICommand value)
        {
            ButtonPressMap.Add(key, value);
        }

        public void removePressCommand(Buttons key)
        {
            ButtonPressMap.Remove(key);
        }

        public void addReleaseCommand(Buttons key, ICommand value)
        {
            ButtonReleaseMap.Add(key, value);
        }

        public void removeReleaseCommand(Buttons key)
        {
            ButtonReleaseMap.Remove(key);
        }

        public void clearCommands()
        {
            ButtonPressMap.Clear();
            ButtonReleaseMap.Clear();
        }
    }

}