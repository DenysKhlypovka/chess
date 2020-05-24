namespace Figure
{
    public class BishopController : BishopRookMoveset
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            CheckAvailableBishopMoves();
        }
    }
}