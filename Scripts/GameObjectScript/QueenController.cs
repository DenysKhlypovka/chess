using System.Collections.Generic;
using System.Linq;

namespace GameObjectScript
{
    public class QueenController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckHorizontalVerticalMoves().Concat(CheckDiagonalMoves()).ToList();
        }
    }
}