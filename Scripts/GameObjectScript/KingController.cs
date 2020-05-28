using System.Collections.Generic;
using System.Linq;

namespace GameObjectScript
{
    public class KingController : FigureController
    {
        private List<MoveProperties> UnavailableMoves { get; set; }

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

        public void AddToUnavailableMoves(MoveProperties moveProperties)
        {
            UnavailableMoves.Add(moveProperties);
        }
        
        public override void ClearAvailableMovesList()
        {
            base.ClearAvailableMovesList();
            UnavailableMoves.Clear();
        }

        void Awake()
        {
            UnavailableMoves = new List<MoveProperties>();
        }
        
        protected override void CheckAvailableMoves()
        {
            moveset.ForEach(moveLocationOffset =>
            {
                var moveProperties = GetMoveProperties(LocationX + moveLocationOffset.OffsetX,
                    LocationY + moveLocationOffset.OffsetY);
                if (moveProperties.MoveType != MoveType.Unavailable && !UnavailableMoves.Any(unavailableMove => unavailableMove.LocX == moveProperties.LocX && unavailableMove.LocY == moveProperties.LocY))
                {
                    availableMoves.Add(moveProperties);
                }
            });
        }
    }
}