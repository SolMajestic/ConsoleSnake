using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ConsoleSnake.Tools;

namespace ConsoleSnake
{
    public class Snake
    {
        private readonly List<(int, int)> positions = new();
        private readonly List<(int, int)> coins = new();
        private bool alive = true;
        private string lastDir = string.Empty;
        
        private double tickTime;
        private readonly double maxTick;
        private double length;
        private double score;
        private static int CoinCount { get; set; } = 0;

        private readonly char snakeHeadChar = '@';
        private readonly char snakeBodyChar = '*';
        private readonly char snakeTailChar = '~';
        private readonly char coinChar = '$';

        public int Score => (int)score;

        public Snake(double length = 10, double tickTime = 300)
        {
            positions.Clear(); 
            this.length = length;
            this.tickTime = tickTime;
            this.maxTick = tickTime;
        }

        /// <summary>
        /// Starts a new game of snake and continues while the snake is alive.
        /// </summary>
        public void StartSnake()
        {
            Output.RedrawAll( );
            positions.Add((25, 15));
            this.GetKeypresses();

            while (alive)
            {
                this.DrawSnake( );
                Task.Delay((int)tickTime).Wait();
                this.MoveSnake( );

                /// Checks length
                while(positions.Count > length)
                {
                    Output.WriteChar(' ', positions.First( ));
                    _ = positions.Remove(positions.First( ));
                }

                // Only runs if game has been started by user input.
                if (lastDir != string.Empty)
                {
                    if(Random.Shared.Next(1, 5 + (coins.Count * 5)) == 1)
                    { 
                        this.AddCoin( ); 
                    }

                    this.TickScore( );
                    tickTime -= (tickTime - 50) * .01;
                }
            }
        }

        /// <summary>
        /// Moves the snake if a direction has been pressed.
        /// </summary>
        private void MoveSnake( )
        {
            (int x, int y) headPos = positions.Last();

            switch(lastDir)
            {
                case "left":
                    headPos.x--;
                    break;
                case "right":
                    headPos.x++;
                    break;
                case "up":
                    headPos.y--;
                    break;
                case "down":
                    headPos.y++;
                    break;
            }

            // Check colisions.
            if (lastDir != string.Empty 
                && (positions.Contains(headPos)
                   ||
                   headPos.x < 1 || headPos.x > 50
                   ||
                   headPos.y < 1 || headPos.y > 30)
                )
            {
                this.alive = false;
            }
            else if(lastDir != string.Empty && coins.Contains(headPos))
            {
                this.CollectCoin(headPos);
            }
            
            positions.Add(headPos);
        }

        /// <summary>
        /// Draws the current position of the snake to the console.
        /// </summary>
        private void DrawSnake()
        {
            foreach ((int, int) pos in positions)
            {
                if (positions.Last() == pos)
                {
                    Output.WriteChar(snakeHeadChar, pos);
                }
                else if(positions.First( ) == pos)
                {
                    Output.WriteChar(snakeTailChar, pos);
                }
                else
                {
                    Output.WriteChar(snakeBodyChar, pos);
                }

                Output.WriteString("Level:1", (0, 32));
                Output.WriteString("Speed:" + ((int)(maxTick - tickTime)).ToString( ) + "    ", (20, 32));
                Output.WriteString("Length:" + ((int)length).ToString( ) + "    ", (40, 32));
                Output.WriteString("$" + CoinCount + "    ", (0, 33));
                Output.WriteString("Score:" + this.Score + "     ", (20, 33));
                Output.WriteString("Lives:1", (40, 33));
            }
        }

        /// <summary>
        /// Adds to the score based on current values.
        /// </summary>
        private void TickScore() => score += maxTick - tickTime + (length * 10);

        /// <summary>
        /// Adds an coin to the coin locations.
        /// </summary>
        private void AddCoin()
        {
            Random rnd = new();

            (int x, int y) coinPos = new ();
            while(coinPos == (0, 0) || positions.Contains(coinPos))
            {
                coinPos.x = rnd.Next(1,50);
                coinPos.y = rnd.Next(1,30);
            }

            this.coins.Add(coinPos);
            Output.WriteChar(coinChar, coinPos);
        }

        /// <summary>
        /// Eats an apple, extending the length of the snake.
        /// </summary>
        /// <param name="headPos"> The position of the head of the snake. </param>
        private void CollectCoin((int, int) headPos)
        {
            score += 1000;
            CoinCount++;
            _ = coins.Remove(headPos);
            length += Random.Shared.Next(1,3);
        }

        /// <summary>
        /// Watches for keypresses and sets the last direction based on them.
        /// </summary>
        private void GetKeypresses( )
        {
            Task.Run(( ) =>
            {
                while(alive)
                {
                    Console.CursorVisible = false;
                    Console.TreatControlCAsInput = true;
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch(keyInfo.Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.J:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.NumPad4:
                        {
                            lastDir = "left";
                            break;
                        }

                        case ConsoleKey.D:
                        case ConsoleKey.L:
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.NumPad6:
                        {
                            lastDir = "right";
                            break;
                        }

                        case ConsoleKey.W:
                        case ConsoleKey.I:
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.NumPad8:
                        {
                            lastDir = "up";
                            break;
                        }

                        case ConsoleKey.S:
                        case ConsoleKey.K:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.NumPad2:
                        {
                            lastDir = "down";
                            break;
                        }
                    }
                }
            });
        }
    }
}
