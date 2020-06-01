using System;

namespace Model
{
    public readonly struct MovesetParameters
    {
        
        public int InitialX { get; }
        public int InitialY { get; }
        public Func<int, bool> ConditionX { get; }
        public Func<int, bool> ConditionY { get; }
        public int IteratorDiffX { get; }
        public int IteratorDiffY { get; }

        public MovesetParameters(int initialX, int initialY, Func<int, bool> conditionX, Func<int, bool> conditionY,
            int iteratorDiffX, int iteratorDiffY)
        {
            InitialX = initialX;
            InitialY = initialY;
            ConditionX = conditionX;
            ConditionY = conditionY;
            IteratorDiffX = iteratorDiffX;
            IteratorDiffY = iteratorDiffY;
        }
    }
}