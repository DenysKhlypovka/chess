using System.Collections.Generic;

namespace GameObjectScript
{
    public class KingController : FigureController
    {
        public override void FillMoveset()
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