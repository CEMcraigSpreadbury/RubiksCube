using RubiksCube.Enums;

namespace RubiksCube.Models
{
    public class Face
    {
        public int Id { get; set; }

        public Cubelet[,] Cubelets = new Cubelet[3, 3];

        public int NorthId {  get; set; }
        public int EastId {  get; set; }
        public int SouthId {  get; set; }
        public int WestId {  get; set; }

        /// <summary>
        /// Initialise the Face with an Id and a Colour
        /// Also initialise each of the 9 Cubelets it contains with the provided Colour
        /// </summary>
        /// <param name="id"></param>
        /// <param name="colour"></param>
        public Face(int id, Colour colour)
        {
            Id = id;

            int cubeletId = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubelets[i, j] = new Cubelet(cubeletId, colour);
                    cubeletId++;
                }
            }
        }

        /// <summary>
        /// Set the North, East, South and West Face Ids connected to this Face
        /// </summary>
        /// <param name="northId">The Face directly North</param>
        /// <param name="eastId">The Face directly East</param>
        /// <param name="southId">The Face directly South</param>
        /// <param name="westId">The Face directly West</param>
        public void SetCardinalFaceIds(int northId, int eastId, int southId, int westId)
        {
            NorthId = northId;
            EastId = eastId;
            SouthId = southId;
            WestId = westId;
        }
    }
}
