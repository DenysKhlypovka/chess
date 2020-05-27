using System.Collections.Generic;

namespace GameObjectScript
{
    public class KnightController : FigureController
    {
        public override void FillMoveset()
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