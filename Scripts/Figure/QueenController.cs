namespace Figure
{
    public class QueenController : BishopRookMoveset
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            CheckAvailableRookMoves();
            CheckAvailableBishopMoves();
        }
    }
}