using RubiksCube.Enums;
using RubiksCube.Repos;

namespace RubiksCube.Models
{
    public class Cube
    {
        public List<Face> Faces { get; set; }

        /// <summary>
        /// A dictionary of key value pairs to define where each Cubelet could move from and to. 
        /// Cubelet located at 0,0 will rotate (clockwise) to coordinate 0,2 etc.
        /// Rotating a face anticlockwise reverses the move (0,2 moves to 0,0)
        /// Note: coordinate 1,1 is abscent as that is the central Cubelet of a Face that can never change position
        /// </summary>
        private readonly Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> Moves = new Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>>
        {
                { new KeyValuePair<int, int>(0, 0), new KeyValuePair<int, int>(0, 2) },
                { new KeyValuePair<int, int>(0, 1), new KeyValuePair<int, int>(1, 2) },
                { new KeyValuePair<int, int>(0, 2), new KeyValuePair<int, int>(2, 2) },
                { new KeyValuePair<int, int>(1, 0), new KeyValuePair<int, int>(0, 1) },
                { new KeyValuePair<int, int>(1, 2), new KeyValuePair<int, int>(2, 1) },
                { new KeyValuePair<int, int>(2, 2), new KeyValuePair<int, int>(2, 0) },
                { new KeyValuePair<int, int>(2, 1), new KeyValuePair<int, int>(1, 0) },
                { new KeyValuePair<int, int>(2, 0), new KeyValuePair<int, int>(0, 0) }
        };

        /// <summary>
        /// A Cube instantiated in a solved orientation
        /// </summary>
        public Cube()
        {
            // Instantiate the Faces
            var frontFace = new Face(0, Colour.Green);
            var leftFace = new Face(1, Colour.Orange);
            var rightFace = new Face(2, Colour.Red);
            var topFace = new Face(3, Colour.White);
            var bottomFace = new Face(4, Colour.Yellow);
            var backFace = new Face(5, Colour.Blue);

            // Set all of the Cardinal Face Ids for each connected Face
            frontFace.SetCardinalFaceIds(topFace.Id, rightFace.Id, bottomFace.Id, leftFace.Id);
            leftFace.SetCardinalFaceIds(topFace.Id, frontFace.Id, bottomFace.Id, backFace.Id);
            rightFace.SetCardinalFaceIds(topFace.Id, backFace.Id, bottomFace.Id, frontFace.Id);
            topFace.SetCardinalFaceIds(backFace.Id, rightFace.Id, frontFace.Id, leftFace.Id);
            bottomFace.SetCardinalFaceIds(frontFace.Id, rightFace.Id, backFace.Id, leftFace.Id);
            backFace.SetCardinalFaceIds(topFace.Id, leftFace.Id, bottomFace.Id, rightFace.Id);

            Faces = [frontFace, leftFace, rightFace, topFace, bottomFace, backFace];

            // Display the Faces of the Cube in a flattened formation
            CubeHelper.DisplayCube([null, Faces[3], null, null]);
            CubeHelper.DisplayCube([Faces[1], Faces[0], Faces[2], Faces[5]]);
            CubeHelper.DisplayCube([null, Faces[4], null, null]);
        }

        /// <summary>
        /// Rotate the given face in the given direction
        /// </summary>
        /// <param name="faceId">The Face to rotate</param>
        /// <param name="clockwise">The direction to rotate, defaults to True</param>
        public void RotateFace(int faceId, bool clockwise = true)
        {
            var face = Faces[faceId];

            // Take a copy of the original Cubelet positions
            var originalCubeletPositions = (Cubelet[,])face.Cubelets.Clone();

            // Loop through each of the moves that need to be performed on this face by reassigning the coordinates of each Cubelet
            foreach (var move in Moves)
            {
                var originalLocation = move.Key;
                var newLocation = move.Value;

                if (!clockwise)
                {
                    originalLocation = move.Value;
                    newLocation = move.Key;
                }

                face.Cubelets[newLocation.Key, newLocation.Value] = originalCubeletPositions[originalLocation.Key, originalLocation.Value];
            }

            RotateCardinalFaces(face, clockwise);
        }

        public void RotateCardinalFaces(Face face, bool clockwise = true)
        {
            // Get each of the Cardinal Faces Cubelets, and create a copy of them to reference their original state
            var northCubelets = CubeHelper.GetConnectedCubelets(face, Faces[face.NorthId]);
            var tempNorthCubelets = northCubelets.Select(v => new Cubelet(v.Id, v.Colour)).ToList();

            var eastCubelets = CubeHelper.GetConnectedCubelets(face, Faces[face.EastId]);
            var tempEastCubelets = eastCubelets.Select(v => new Cubelet(v.Id, v.Colour)).ToList();

            var southCubelets = CubeHelper.GetConnectedCubelets(face, Faces[face.SouthId]);
            var tempSouthCubelets = southCubelets.Select(v => new Cubelet(v.Id, v.Colour)).ToList();

            var westCubelets = CubeHelper.GetConnectedCubelets(face, Faces[face.WestId]);
            var tempWestCubelets = westCubelets.Select(v => new Cubelet(v.Id, v.Colour)).ToList();

            // Set the Colour and Id of each row of each connecting Face, adhering to whether it is a Clockwise Rotation or not
            for (int i = 0; i < 3; i++)
            {
                if (!clockwise)
                {
                    westCubelets[i].Colour = tempNorthCubelets[i].Colour;
                    westCubelets[i].Id = tempNorthCubelets[i].Id;

                    southCubelets[i].Colour = tempWestCubelets[i].Colour;
                    southCubelets[i].Id = tempWestCubelets[i].Id;

                    eastCubelets[i].Colour = tempSouthCubelets[i].Colour;
                    eastCubelets[i].Id = tempSouthCubelets[i].Id;

                    northCubelets[i].Colour = tempEastCubelets[i].Colour;
                    northCubelets[i].Id = tempEastCubelets[i].Id;
                }
                else
                {
                    eastCubelets[i].Colour = tempNorthCubelets[i].Colour;
                    eastCubelets[i].Id = tempNorthCubelets[i].Id;

                    southCubelets[i].Colour = tempEastCubelets[i].Colour;
                    southCubelets[i].Id = tempEastCubelets[i].Id;

                    westCubelets[i].Colour = tempSouthCubelets[i].Colour;
                    westCubelets[i].Id = tempSouthCubelets[i].Id;

                    northCubelets[i].Colour = tempWestCubelets[i].Colour;
                    northCubelets[i].Id = tempWestCubelets[i].Id;
                }
            }
        }
    }
}
