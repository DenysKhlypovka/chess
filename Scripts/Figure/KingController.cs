using System.Collections.Generic;

namespace Figure
{
    public class KingController : FigureWithDefinedMoveset
    {
        private void Start()
        {
            moveset = new List<MoveOffset>
            {
                new MoveOffset(-1, -1),
                new MoveOffset(-1, 0),
                new MoveOffset(-1, 1),
                new MoveOffset(0, -1),
                new MoveOffset(0, 1),
                new MoveOffset(1, -1),
                new MoveOffset(1, 0),
                new MoveOffset(1, 1)
            };
        }
    }
}