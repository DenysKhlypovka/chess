namespace Figure
{
    public class RookController : BishopRookMoveset
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            CheckAvailableRookMoves();
            CheckCastling();
        }

        void CheckCastling()
        {
            if (gameController.IsCastlingAvailable(gameObject))
            {
                gameController.HighlightCastlingCube(boardController.GetCube(GetCastlingX(), LocationY));
            }
        }
        
        int GetCastlingX()
        {
            return LocationX == 0 ? 3 : 5;
        }
    }
}