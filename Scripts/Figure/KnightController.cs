using System.Collections.Generic;

namespace Figure
{
    public class KnightController : FigureWithDefinedMoveset
    {
        private void Start()
        {
            moveset = new List<MoveOffset>
            {
                new MoveOffset(-2, -1),
                new MoveOffset(-2, 1),
                new MoveOffset(-1, -2),
                new MoveOffset(-1, 2),
                new MoveOffset(1, -2),
                new MoveOffset(1, 2),
                new MoveOffset(2, -1),
                new MoveOffset(2, 1)
            };
        }
    }
}