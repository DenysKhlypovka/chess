using UnityEngine;

namespace Controller
{
    public class MeshColliderController : MonoBehaviour
    {
        public static void ChangeMeshColliderEnabledProperty(GameObject gameObjectToChangeCollider, bool isEnabled)
        {
            gameObjectToChangeCollider.GetComponent<MeshCollider>().enabled = isEnabled;
        }
    }
}