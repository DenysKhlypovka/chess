using System.Collections.Generic;
using System.Linq;
using Model;

namespace GameObjectScript.Figure
{
    public class QueenController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckHorizontalVerticalMoves().Concat(CheckDiagonalMoves()).ToList();
        }
    }
}