using System.Collections.Generic;
using Model;

namespace GameObjectScript.Figure
{
    public class BishopController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckDiagonalMoves();
        }
    }
}