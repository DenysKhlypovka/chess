namespace GameObjectScript
{
    public class RookController : FigureController
    {
        //TODO: fix, refactor castling
        public override void FillMoveset()
        {
            CheckHorizontalVerticalMoves();
            CheckCastling();
        }

        private void CheckCastling()
        {
            if (gameController.IsCastlingAvailable(this))
            {
                highlightManager.HighlightCastlingCell(this);
            }
        }
    }
}