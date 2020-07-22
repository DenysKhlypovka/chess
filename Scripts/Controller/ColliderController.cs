using UnityEngine;

namespace Controller
{
    public class ColliderController : MonoBehaviour
    {
        public static void ChangeMeshColliderEnabledProperty(GameObject gameObjectToChangeCollider, bool isEnabled)
        {
            gameObjectToChangeCollider.GetComponent<MeshCollider>().enabled = isEnabled;
        }
        public static void ChangeBoxColliderEnabledProperty(GameObject gameObjectToChangeCollider, bool isEnabled)
        {
            gameObjectToChangeCollider.GetComponent<BoxCollider>().enabled = isEnabled;
        }
    }
}