using RubiksCube.Models;
using RubiksCube.Repos;
using static System.Net.Mime.MediaTypeNames;

namespace RubiksCube
{
    public class CubeRunner
    {
        public Cube Cube;
        public List<string> Commands = ["F", "R", "U", "B", "L", "D", "F`", "R`", "U`", "B`", "L`", "D`"];
        private const int ScrambleCount = 20;

        public CubeRunner()
        {
            Cube = new Cube();

            Run();
        }

        public void Run()
        {
            Console.Clear();

            Cube = new Cube();

            WriteText("Enter a command to Rotate Faces of the Cube. Type Help for a list of commands.");
            Console.WriteLine();
            WriteText("Input rotation command: ");

            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine();

                if (input == null)
                    return;

                HandleInput(input.ToUpper());

                Console.WriteLine();
                Console.Write("Input rotation command: ");
            }
        }

        public void HandleInput(string input)
        {
            switch (input)
            {
                case "HELP":
                    DisplayHelp();
                    break;
                case "RESET":
                    Run();
                    break;
                case "EXIT":
                    System.Environment.Exit(0);
                    break;
                case "SCRAMBLE":
                    for (int i = 0; i < ScrambleCount + 1; i++)
                    {
                        HandleInput(GetRandomInput());
                        Console.WriteLine();
                    }
                    break;
                default:
                    var splitInputs = input.Split(" ");

                    // Perform all of the supplied rotations
                    foreach (var splitInput in splitInputs)
                    {
                        if (Commands.Contains(splitInput.ToUpper()))
                        {
                            var faceId = CubeHelper.GetFaceToRotate(splitInput);
                            bool clockwise = true;

                            if (splitInput.Contains('`'))
                                clockwise = false;

                            Cube.RotateFace(faceId, clockwise);
                        }
                    }

                    // Display the Cube once the rotations have completed
                    CubeHelper.DisplayCube([null, Cube.Faces[3], null, null]);
                    CubeHelper.DisplayCube([Cube.Faces[1], Cube.Faces[0], Cube.Faces[2], Cube.Faces[5]]);
                    CubeHelper.DisplayCube([null, Cube.Faces[4], null, null]);

                    break;
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Rotate Faces using F R U B L D");
            Console.WriteLine("Reset the Cube by typing Reset");
            Console.WriteLine("String rotations together by separating with a space e.g F U L");
            Console.WriteLine("Faces rotate Clockwise by default, add a ` BackTick to each Face rotation e.g F` R` U L to rotate that Face Counter Clockwise");
            Console.WriteLine("Scramble the Cube using 'scramble'");
        }

        /// <summary>
        /// This cool method writes each character of a string of text one by one, nice
        /// </summary>
        /// <param name="text">The text to display</param>
        private static void WriteText(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
        }

        /// <summary>
        /// Returns a random rotation Command
        /// </summary>
        /// <returns></returns>
        private string GetRandomInput()
        {
            var random = new Random();
            var randomCommandIndex = random.Next(0, Commands.Count);

            return Commands[randomCommandIndex];
        }
    }
}
