using System.Collections.Generic;

namespace GameObjectScript
{
    public class BishopController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckDiagonalMoves();
        }
    }
}