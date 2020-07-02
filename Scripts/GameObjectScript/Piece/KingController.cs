using System.Collections.Generic;
using Model;

namespace GameObjectScript.Piece
{
    public class KingController : PieceController
    {
        void Awake()
        {
            piece = Model.Piece.King;
        }
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return FilterInitialMoveset(new List<MoveProperties>
            {
                new MoveProperties(new Coordinate(Coordinate.X - 1, Coordinate.Y - 1)),
                new MoveProperties(new Coordinate(Coordinate.X - 1, Coordinate.Y)),
                new MoveProperties(new Coordinate(Coordinate.X - 1, Coordinate.Y + 1)),
                new MoveProperties(new Coordinate(Coordinate.X, Coordinate.Y - 1)),
                new MoveProperties(new Coordinate(Coordinate.X, Coordinate.Y + 1)),
                new MoveProperties(new Coordinate(Coordinate.X + 1, Coordinate.Y - 1)),
                new MoveProperties(new Coordinate(Coordinate.X + 1, Coordinate.Y)),
                new MoveProperties(new Coordinate(Coordinate.X + 1, Coordinate.Y + 1))
            });
        }
    }
}