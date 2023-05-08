using LifeGame.Simulation.Ground;
using LifeGame.Simulation.Logic;
using System.Text;

namespace LifeGame.Graphics.Scenes
{
    public class SceneSimulation : Scene
    {
        private readonly Ground _ground;

        private readonly int _screenWidth, _screenHeight;
        private int _xOffset, _yOffset;
        private readonly int _xCenter, _yCenter;

        public SceneSimulation(IContext context, Ground ground) : base(context)
        {
            _ground = ground;

            _screenWidth = Console.WindowWidth;
            _screenHeight = Console.WindowHeight - 1;

            _xCenter = (_screenWidth / 2) + (_screenWidth % 2);
            _yCenter = (_screenHeight / 2) + (_screenHeight % 2);

            // _xOffset = -(_screenWidth / 2);
            // _yOffset = -(_screenHeight / 2);

            _xOffset = -(_screenWidth / 2) + 1 - (_screenWidth % 2);
            _yOffset = -(_screenHeight / 2) + 1 - (_screenHeight % 2);
        }

        private protected override string Draw()
        {
            StringBuilder result = new StringBuilder();

            for (int y = 0; y < _screenHeight - 1; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    if (y == 0 || y == _screenHeight - 2)
                    {
                        _ = result.Append('-');
                    }
                    else if (x == 0 || x == _screenWidth - 1)
                    {
                        _ = result.Append('|');
                    }
                    else
                    {
                        int bareX = x + _xOffset - 1;
                        int bareY = y + _yOffset - 1;

                        if (bareX % 2 == 1 || bareX % 2 == -1 || bareY % 2 == 1 || bareY % 2 == -1)
                        {
                            if (y == _yCenter)
                            {
                                if (x == _xCenter - 1)
                                {
                                    _ = result.Append('>');
                                    continue;
                                }
                                else if (x == _xCenter + 1)
                                {
                                    _ = result.Append('<');
                                    continue;
                                }
                            }

                            _ = result.Append(' ');
                        }
                        else
                        {
                            ECell? cell = _ground.GetCell(bareX / 2, bareY / 2);

                            if (cell == null)
                            {
                                _ = result.Append('~');
                            }
                            else
                            {
                                if (cell == ECell.Alive)
                                {
                                    _ = result.Append('X');
                                }
                                else
                                {
                                    _ = result.Append('\'');
                                }
                            }
                        }
                    }
                }

                _ = result.Append('\n');
            }

            _ = result.Append($"Press 's' to stop simulation and enter the command {_xOffset / 2} {_yOffset / 2}");

            return result.ToString();
        }

        private protected override void UpdateControl()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.S:
                    Console.WriteLine("You paused simulation");
                    _ = Console.ReadKey();
                    _ground.Update();
                    break;
                case ConsoleKey.UpArrow:
                    _yOffset -= 2;
                    break;
                case ConsoleKey.DownArrow:
                    _yOffset += 2;
                    break;
                case ConsoleKey.LeftArrow:
                    _xOffset -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    _xOffset += 2;
                    break;
                case ConsoleKey.Enter:
                    int x = (_xCenter + _xOffset) / 2;
                    int y = (_yCenter + _yOffset) / 2;

                    _ground.SetCell(x, y);
                    break;
                case ConsoleKey.Backspace:
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.Clear:
                    break;
                case ConsoleKey.Pause:
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Spacebar:
                    break;
                case ConsoleKey.PageUp:
                    break;
                case ConsoleKey.PageDown:
                    break;
                case ConsoleKey.End:
                    break;
                case ConsoleKey.Home:
                    break;
                case ConsoleKey.Select:
                    break;
                case ConsoleKey.Print:
                    break;
                case ConsoleKey.Execute:
                    break;
                case ConsoleKey.PrintScreen:
                    break;
                case ConsoleKey.Insert:
                    break;
                case ConsoleKey.Delete:
                    break;
                case ConsoleKey.Help:
                    break;
                case ConsoleKey.D0:
                    break;
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.D3:
                    break;
                case ConsoleKey.D4:
                    break;
                case ConsoleKey.D5:
                    break;
                case ConsoleKey.D6:
                    break;
                case ConsoleKey.D7:
                    break;
                case ConsoleKey.D8:
                    break;
                case ConsoleKey.D9:
                    break;
                case ConsoleKey.A:
                    break;
                case ConsoleKey.B:
                    break;
                case ConsoleKey.C:
                    break;
                case ConsoleKey.D:
                    break;
                case ConsoleKey.E:
                    break;
                case ConsoleKey.F:
                    break;
                case ConsoleKey.G:
                    break;
                case ConsoleKey.H:
                    break;
                case ConsoleKey.I:
                    break;
                case ConsoleKey.J:
                    break;
                case ConsoleKey.K:
                    break;
                case ConsoleKey.L:
                    break;
                case ConsoleKey.M:
                    break;
                case ConsoleKey.N:
                    break;
                case ConsoleKey.O:
                    break;
                case ConsoleKey.P:
                    break;
                case ConsoleKey.Q:
                    break;
                case ConsoleKey.R:
                    break;
                case ConsoleKey.T:
                    break;
                case ConsoleKey.U:
                    break;
                case ConsoleKey.V:
                    break;
                case ConsoleKey.W:
                    break;
                case ConsoleKey.X:
                    break;
                case ConsoleKey.Y:
                    break;
                case ConsoleKey.Z:
                    break;
                case ConsoleKey.LeftWindows:
                    break;
                case ConsoleKey.RightWindows:
                    break;
                case ConsoleKey.Applications:
                    break;
                case ConsoleKey.Sleep:
                    break;
                case ConsoleKey.NumPad0:
                    break;
                case ConsoleKey.NumPad1:
                    break;
                case ConsoleKey.NumPad2:
                    break;
                case ConsoleKey.NumPad3:
                    break;
                case ConsoleKey.NumPad4:
                    break;
                case ConsoleKey.NumPad5:
                    break;
                case ConsoleKey.NumPad6:
                    break;
                case ConsoleKey.NumPad7:
                    break;
                case ConsoleKey.NumPad8:
                    break;
                case ConsoleKey.NumPad9:
                    break;
                case ConsoleKey.Multiply:
                    break;
                case ConsoleKey.Add:
                    break;
                case ConsoleKey.Separator:
                    break;
                case ConsoleKey.Subtract:
                    break;
                case ConsoleKey.Decimal:
                    break;
                case ConsoleKey.Divide:
                    break;
                case ConsoleKey.F1:
                    break;
                case ConsoleKey.F2:
                    break;
                case ConsoleKey.F3:
                    break;
                case ConsoleKey.F4:
                    break;
                case ConsoleKey.F5:
                    break;
                case ConsoleKey.F6:
                    break;
                case ConsoleKey.F7:
                    break;
                case ConsoleKey.F8:
                    break;
                case ConsoleKey.F9:
                    break;
                case ConsoleKey.F10:
                    break;
                case ConsoleKey.F11:
                    break;
                case ConsoleKey.F12:
                    break;
                case ConsoleKey.F13:
                    break;
                case ConsoleKey.F14:
                    break;
                case ConsoleKey.F15:
                    break;
                case ConsoleKey.F16:
                    break;
                case ConsoleKey.F17:
                    break;
                case ConsoleKey.F18:
                    break;
                case ConsoleKey.F19:
                    break;
                case ConsoleKey.F20:
                    break;
                case ConsoleKey.F21:
                    break;
                case ConsoleKey.F22:
                    break;
                case ConsoleKey.F23:
                    break;
                case ConsoleKey.F24:
                    break;
                case ConsoleKey.BrowserBack:
                    break;
                case ConsoleKey.BrowserForward:
                    break;
                case ConsoleKey.BrowserRefresh:
                    break;
                case ConsoleKey.BrowserStop:
                    break;
                case ConsoleKey.BrowserSearch:
                    break;
                case ConsoleKey.BrowserFavorites:
                    break;
                case ConsoleKey.BrowserHome:
                    break;
                case ConsoleKey.VolumeMute:
                    break;
                case ConsoleKey.VolumeDown:
                    break;
                case ConsoleKey.VolumeUp:
                    break;
                case ConsoleKey.MediaNext:
                    break;
                case ConsoleKey.MediaPrevious:
                    break;
                case ConsoleKey.MediaStop:
                    break;
                case ConsoleKey.MediaPlay:
                    break;
                case ConsoleKey.LaunchMail:
                    break;
                case ConsoleKey.LaunchMediaSelect:
                    break;
                case ConsoleKey.LaunchApp1:
                    break;
                case ConsoleKey.LaunchApp2:
                    break;
                case ConsoleKey.Oem1:
                    break;
                case ConsoleKey.OemPlus:
                    break;
                case ConsoleKey.OemComma:
                    break;
                case ConsoleKey.OemMinus:
                    break;
                case ConsoleKey.OemPeriod:
                    break;
                case ConsoleKey.Oem2:
                    break;
                case ConsoleKey.Oem3:
                    break;
                case ConsoleKey.Oem4:
                    break;
                case ConsoleKey.Oem5:
                    break;
                case ConsoleKey.Oem6:
                    break;
                case ConsoleKey.Oem7:
                    break;
                case ConsoleKey.Oem8:
                    break;
                case ConsoleKey.Oem102:
                    break;
                case ConsoleKey.Process:
                    break;
                case ConsoleKey.Packet:
                    break;
                case ConsoleKey.Attention:
                    break;
                case ConsoleKey.CrSel:
                    break;
                case ConsoleKey.ExSel:
                    break;
                case ConsoleKey.EraseEndOfFile:
                    break;
                case ConsoleKey.Play:
                    break;
                case ConsoleKey.Zoom:
                    break;
                case ConsoleKey.NoName:
                    break;
                case ConsoleKey.Pa1:
                    break;
                case ConsoleKey.OemClear:
                    break;
                default:
                    break;
            }

        }
    }
}