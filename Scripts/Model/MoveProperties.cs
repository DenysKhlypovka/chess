using System.Collections.Generic;

namespace Model
{
    public class MoveProperties
    {
        public Coordinate Coordinate { get; }
        public MoveType MoveType { get; set; }

        public MoveProperties(Coordinate coordinate, MoveType moveType)
        {
            Coordinate = coordinate;
            MoveType = moveType;
        }
    
        public MoveProperties(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public static MoveProperties GetUnavailableMoveProperties()
        {
            return new MoveProperties(new Coordinate(-1, -1), MoveType.Unavailable);
        }

        public static void AddToMoveset(List<MoveProperties> moveset, MoveProperties move)
        {
            if (move.MoveType != MoveType.Unavailable)
            {
                moveset.Add(move);
            }
        }

        public override string ToString()
        {
            return $"{nameof(Coordinate.X)}: {Coordinate.X}, {nameof(Coordinate.Y)}: {Coordinate.Y}, {nameof(MoveType)}: {MoveType}";
        }
    }
}