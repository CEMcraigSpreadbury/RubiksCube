using RubiksCube.Enums;

namespace RubiksCube.Models
{
    /// <summary>
    /// Stores the Colour and an Id of an individual Cubelet of a Face
    /// </summary>
    /// <remarks>
    /// Initialise the Cubelet
    /// </remarks>
    /// <param name="id">It's Id location on the Face</param>
    /// <param name="colour">The Colour of the Cubelet</param>
    public class Cubelet(int id, Colour colour)
    {
        public int Id { get; set; } = id;
        public Colour Colour { get; set; } = colour;
    }
}
