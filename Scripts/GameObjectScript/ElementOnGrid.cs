using Controller;
using UnityEngine;

namespace GameObjectScript
{
    public class ElementOnGrid : MonoBehaviour
    {
        protected GameController gameController;
        public Coordinate Coordinate { get; set; }
        public Color Color { get; set; }
        public bool IsActivated { get; set; }
    }
}