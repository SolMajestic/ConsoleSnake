using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake.Tools
{
    public static class Output
    {
        /// <summary>
        /// Writes the sent char to console at the provided position.
        /// </summary>
        /// <param name="s"> The char to write. </param>
        /// <param name="x"> The column. </param>
        /// <param name="y"> The row. </param>
        public static void WriteChar(char c, (int, int) pos)
        {
            Console.SetCursorPosition(pos.Item1, pos.Item2);
            Console.Write(c);
        }

        /// <summary>
        /// Writes the sent string to console at the provided position.
        /// </summary>
        /// <param name="s"> The string to write. </param>
        /// <param name="x"> The column. </param>
        /// <param name="y"> The row. </param>
        public static void WriteString(string s, (int, int) pos)
        {
            Console.SetCursorPosition(pos.Item1, pos.Item2);
            Console.Write(s);
        }

        /// <summary>
        /// Clears console and redraws borders.
        /// </summary>
        public static void RedrawAll()
        {
            Console.Clear();
            Console.WriteLine(Border);
            Console.CursorVisible = false;
            Console.TreatControlCAsInput = true;
        }

        public static void ClearSnake( )
        {
            for(int i = 1 ; i <= 30 ; i++)
            {
                WriteString("                                                  ", (1, i));
            }
        }

        public static string Border => 
           // ╔═╦╗╠║╣╚╩═╝╬
           //0         1         2         3         4         5
           //0123456789012345678901234567890123456789012345678901
            "╔══════════════════════════════════════════════════╗\n" + // 00
            "║                                                  ║\n" + // 01
            "║                                                  ║\n" + // 02
            "║                                                  ║\n" + // 03
            "║                                                  ║\n" + // 04
            "║                                                  ║\n" + // 05
            "║                                                  ║\n" + // 06
            "║                                                  ║\n" + // 07
            "║                                                  ║\n" + // 08
            "║                                                  ║\n" + // 09
            "║                                                  ║\n" + // 10
            "║                                                  ║\n" + // 11
            "║                                                  ║\n" + // 12
            "║                                                  ║\n" + // 13
            "║                                                  ║\n" + // 14
            "║                                                  ║\n" + // 15
            "║                                                  ║\n" + // 16
            "║                                                  ║\n" + // 17
            "║                                                  ║\n" + // 18
            "║                                                  ║\n" + // 19
            "║                                                  ║\n" + // 20
            "║                                                  ║\n" + // 21
            "║                                                  ║\n" + // 22
            "║                                                  ║\n" + // 23
            "║                                                  ║\n" + // 24
            "║                                                  ║\n" + // 25
            "║                                                  ║\n" + // 26
            "║                                                  ║\n" + // 27
            "║                                                  ║\n" + // 28
            "║                                                  ║\n" + // 29
            "║                                                  ║\n" + // 30
            "╚══════════════════════════════════════════════════╝\n";  // 31
    }
}