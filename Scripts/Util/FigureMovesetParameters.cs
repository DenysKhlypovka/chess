using System.Collections.Generic;
using Model;

namespace Util
{
    public static class FigureMovesetParameters
    {
        public static List<MovesetParameters> GetDiagonalMovesetParametersList(Coordinate coordinate)
        {
            return new List<MovesetParameters>
            {
                new MovesetParameters(coordinate.X + 1, coordinate.Y + 1, x => x < 8, y => y < 8, 1, 1),
                new MovesetParameters(coordinate.X + 1, coordinate.Y - 1, x => x < 8, y => y > -1, 1, -1),
                new MovesetParameters(coordinate.X - 1, coordinate.Y + 1, x => x > -1, y => y < 8, -1, 1),
                new MovesetParameters(coordinate.X - 1, coordinate.Y - 1, x => x > -1, y => y > -1, -1, -1)
            };
        }

        public static List<MovesetParameters> GetHorizontalVerticalMovesetParametersList(Coordinate coordinate)
        {
            return new List<MovesetParameters>
            {
                new MovesetParameters(coordinate.X + 1, coordinate.Y, x => x < 8, y => true, 1, 0),
                new MovesetParameters(coordinate.X - 1, coordinate.Y, x => x > -1, y => true, -1, 0),
                new MovesetParameters(coordinate.X, coordinate.Y + 1, x => true, y => y < 8, 0, 1),
                new MovesetParameters(coordinate.X, coordinate.Y - 1, x => true, y => y > -1, 0, -1)
            };
        }
    }
}