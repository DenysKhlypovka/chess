using System.Collections.Generic;

namespace GameObjectScript
{
    public class KingController : FigureController
    {
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