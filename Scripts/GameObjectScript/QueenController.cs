namespace GameObjectScript
{
    public class QueenController : FigureController
    {
        public override void FillMoveset()
        {
            CheckHorizontalVerticalMoves();
            CheckDiagonalMoves();
        }
    }
}