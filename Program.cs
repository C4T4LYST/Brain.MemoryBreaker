using System;
using System.Collections.Generic;
using System.Threading;

namespace MemoryBreaker
{
    class Program
    {
        private static List<int> nums = new List<int>();
        private static double w = 0;
        private static bool paused = true;
        private static int Stopper = 0;
        private static int LastTime = 0;
        private static int LastAvarge = 0;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WriteLine("Push a button to start!");
            Console.ReadKey();
            SetColor(ConsoleColor.Black, ConsoleColor.White);
            Console.Clear();
            Reset();


            new Thread(() =>
            {
                while (true)
                {
                    if (w > 0)
                    {
                        w--;
                        Console.Title = $"MemoryBreaker    Completed: {nums.Count - 1}"+ $"     Timer: {Stopper / 1000}" + $"    Clock: " + ((w / 1000).ToString() + "000").Substring(0, 3);
                    }
                    else
                    {
                        Console.Title = $"MemoryBreaker    Completed: {nums.Count - 1}" + $"     Timer: {Stopper / 1000}";
                        w = 0;
                    }
                    Thread.Sleep(1);
                }
            }).Start();

            while (true)
            {
                bool k = true;
                while (k)
                {
                    if (!WriteDown()) k = false;
                    else
                    {
                        Console.Title = $"Completed levels: {nums.Count}";
                        Console.Clear();
                        Console.WriteLine($"(Press enter to continue)\n\n Completed levels: {nums.Count} \n Time: {MilisecToSec((float) getWaitInMilisec())}s -> {MilisecToSec((float) getWaitInMilisec(1))}s \n\n AllTime: {MilisecToSec(Stopper)}s \n PartTime: {MilisecToSec(LastAvarge)}s -> {MilisecToSec(Stopper-LastTime)}s");
                        addNum();
                        LastAvarge = Stopper - LastTime;
                        LastTime = Stopper;
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                paused = true;
                Console.WriteLine("(Press a button to continue)\n\n Grat teso, {0} Szintet teljesítettél {1} másodperc alatt", nums.Count - 1, MilisecToSec(Stopper));
                Thread.Sleep(1500);
                Console.ReadKey(true);
                //Stopper = 0;
                Reset();
            }
        }
        private static double getWait()
        {
            return nums.Count  * 1.2;
        }
        private static double getWaitInMilisec()
        {
            return (nums.Count * 1.2) * 1000;
        }
        private static double getWaitInMilisec(int Addlvl)
        {
            return (nums.Count + Addlvl) * 1000 * 1.2;
        }

        public static void addNum()
        {
            Random r = new Random();
            nums.Add(r.Next(10, 100));
        }

        private static string MilisecToSec(int milisec)
        {
            string asd = (milisec / 1000).ToString().Substring(0, 1) + "." + ((milisec / 1000).ToString() + "0000").Substring(2);
            return asd.Substring(0, 3);
        }
        private static string MilisecToSec(float milisec)
        {
            string asd = (milisec / 1000).ToString().Substring(0, 1) + "." + ((milisec / 1000).ToString() + "0000").Substring(2);
            return asd.Substring(0, 3);
        }

        public static bool WriteDown()
        {
            paused = false;
            Console.Clear();
            Console.Write("(Press a button to skip)\n");
            foreach (int num in nums.ToArray())
            {
                Console.Write("\n > {0}", num.ToString());
            }
            double wait = getWait();
            w = wait * 1000;
            Thread.Sleep(500);
            while(w > 0)
            {
                Thread.Sleep(1);
                if (Console.KeyAvailable) { Console.ReadKey(); w = 0; } 
            }
            w = 0;

            Console.Clear();
            Console.CursorVisible = true;
            Console.Write("(Write the nums)\n\n > ");

            for (int i = 0; i < nums.Count; i++) {
                if (nums[i] != int.Parse(Console.ReadLine())) 
                { 
                    Console.CursorVisible = false; 
                    return false; 
                }
                Console.Write(" > ");
            }

            Console.CursorVisible = false;
            return true;
        }

        public static void Reset()
        {
            nums.Clear();
            addNum();
        }

        public static void SetColor(ConsoleColor forgeGround, ConsoleColor backGround)
        {
            Console.BackgroundColor = backGround;
            Console.ForegroundColor = forgeGround;
        }
    }
}
