namespace GameObjectScript
{
    public class BishopController : FigureController
    {
        public override void FillMoveset()
        {
            CheckDiagonalMoves();
        }
    }
}