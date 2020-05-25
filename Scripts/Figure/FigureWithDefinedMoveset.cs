using System.Collections.Generic;

namespace Figure
{
    public abstract class FigureWithDefinedMoveset : FigureController
    {
        protected List<MoveOffset> moveset;

        private void CheckMoves()
        {
            moveset.ForEach(moveLocationOffset => HighlightCells(LocationX + moveLocationOffset.OffsetX, LocationY + moveLocationOffset.OffsetY));
        }
        
        void OnMouseDown()
        {
            Activate();
            CheckMoves();
        }
    }
}