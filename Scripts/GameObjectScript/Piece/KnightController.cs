using System.Collections.Generic;
using Model;

namespace GameObjectScript.Piece
{
    public class KnightController : PieceController
    {
        void Awake()
        {
            PieceType = Model.Piece.Knight;
        }
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return FilterInitialMoveset(new List<MoveProperties>
            {
                new MoveProperties(new Coordinate(Coordinate.X - 2, Coordinate.Y - 1)),
                new MoveProperties(new Coordinate(Coordinate.X - 2, Coordinate.Y + 1)),
                new MoveProperties(new Coordinate(Coordinate.X - 1, Coordinate.Y - 2)),
                new MoveProperties(new Coordinate(Coordinate.X - 1, Coordinate.Y + 2)),
                new MoveProperties(new Coordinate(Coordinate.X + 1, Coordinate.Y - 2)),
                new MoveProperties(new Coordinate(Coordinate.X + 1, Coordinate.Y + 2)),
                new MoveProperties(new Coordinate(Coordinate.X + 2, Coordinate.Y - 1)),
                new MoveProperties(new Coordinate(Coordinate.X + 2, Coordinate.Y + 1))
            });
        }
    }
}