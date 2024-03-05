using RubiksCube.Enums;
using RubiksCube.Models;

namespace RubiksCube.Repos
{
    /// <summary>
    /// Contains methods a Cube will need to help perform functions
    /// </summary>
    public static class CubeHelper
    {
        /// <summary>
        /// Display each of the Faces provided to the Console
        /// </summary>
        /// <param name="faces"></param>
        public static void DisplayCube(List<Face?> faces)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var face = faces[j] as Face;

                    for (int z = 0; z < 3; z++)
                    {
                        if (face != null)
                        {
                            // Get and set the Foreground Colour to the Colour of the Cubelet
                            Console.ForegroundColor = GetColor(face.Cubelets[i, z].Colour);
                            Console.Write($"[{face.Cubelets[i, z].Id}]", Console.ForegroundColor);
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                }

                // Break to the next row of Cubelets
                Console.WriteLine();
            }

            // Reset the Console colour to white
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Map colors to Console Colours
        /// </summary>
        /// <param name="colour">The Colour of the Cubelet to map from</param>
        /// <returns></returns>
        private static ConsoleColor GetColor(Colour colour)
        {
            switch (colour)
            {
                case Colour.Red:
                    return ConsoleColor.Red;
                case Colour.Green:
                    return ConsoleColor.Green;
                case Colour.Blue:
                    return ConsoleColor.Blue;
                case Colour.Yellow:
                    return ConsoleColor.Yellow;
                case Colour.Orange:
                    return ConsoleColor.DarkYellow;
                case Colour.White:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.Gray;
            }
        }

        /// <summary>
        /// Returns the Id of the Face being rotated
        /// </summary>
        /// <param name="face">Shorthand for the Face to Rotate e.g F </param>
        /// <returns>Face Id</returns>
        public static int GetFaceToRotate(string face)
        {
            switch (face)
            {
                case "F":
                case "F`":
                    return 0;
                case "L":
                case "L`":
                    return 1;
                case "R":
                case "R`":
                    return 2;
                case "U":
                case "U`":
                    return 3;
                case "D":
                case "D`":
                    return 4;
                case "B":
                case "B`":
                    return 5;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Retrieve the Cubelets on the connected side of the Face we're rotating
        /// </summary>
        /// <param name="faceToRotate"></param>
        /// <param name="direction"></param>
        public static List<Cubelet> GetConnectedCubelets(Face faceToRotate, Face connectingFace)
        {
            List<Cubelet> cubeletsToMove = new List<Cubelet>();

            // If the face being rotated is the Northern face of the Connecting Face, give me the Cubelets on the top row of that Connecting Face:
            // 0,0  0,1  0,2 <------
            // 1,0  1,1  1,2
            // 2,0  2,1  2,2

            if (faceToRotate.Id == connectingFace.NorthId)
            {
                cubeletsToMove.Add(connectingFace.Cubelets[0, 0]);
                cubeletsToMove.Add(connectingFace.Cubelets[0, 1]);
                cubeletsToMove.Add(connectingFace.Cubelets[0, 2]);
            }
            else if (faceToRotate.Id == connectingFace.EastId)
            {
                cubeletsToMove.Add(connectingFace.Cubelets[0, 2]);
                cubeletsToMove.Add(connectingFace.Cubelets[1, 2]);
                cubeletsToMove.Add(connectingFace.Cubelets[2, 2]);
            }
            else if (faceToRotate.Id == connectingFace.SouthId)
            {
                cubeletsToMove.Add(connectingFace.Cubelets[2, 0]);
                cubeletsToMove.Add(connectingFace.Cubelets[2, 1]);
                cubeletsToMove.Add(connectingFace.Cubelets[2, 2]);
            }
            else if (faceToRotate.Id == connectingFace.WestId)
            {
                cubeletsToMove.Add(connectingFace.Cubelets[0, 0]);
                cubeletsToMove.Add(connectingFace.Cubelets[1, 0]);
                cubeletsToMove.Add(connectingFace.Cubelets[2, 0]);
            }

            return cubeletsToMove;
        }
    }
}