using ConsoleSnake;
using ConsoleSnake.Tools;

bool play = true;
Console.CursorVisible = false;

try
{
#pragma warning disable CA1416 // Validate platform compatibility
    Console.SetWindowSize(52,34);
#pragma warning restore CA1416 // Validate platform compatibility
}
catch
{
    // Do nothing, console size not supported on platform.
}

while(play)
{
    Snake snake = new();
    snake.StartSnake( );

    Output.WriteString(" YOU DIED ", (20, 18));
    Output.WriteString(" Score: " + snake.Score, (18, 19));
    Output.WriteString(" Try Again? Y/N", (17, 20));

    ConsoleKey inputKey = new();
    while (inputKey is not ConsoleKey.Y and not ConsoleKey.N)
    {
        inputKey = Console.ReadKey( ).Key;
    }   
    
    if(Console.ReadKey( ).Key == ConsoleKey.N)
    {
        play = false;
    }
}
