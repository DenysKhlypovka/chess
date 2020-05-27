using UnityEngine;

namespace Controller
{
    public class MeshColliderController : MonoBehaviour
    {
        public static void EnableMeshCollider(GameObject gameObjectToEnableCollider)
        {
            ChangeMeshColliderEnabledProperty(gameObjectToEnableCollider, true);
        }

        public static void DisableMeshCollider(GameObject gameObjectToDisableCollider)
        {
            ChangeMeshColliderEnabledProperty(gameObjectToDisableCollider, false);
        }

        private static void ChangeMeshColliderEnabledProperty(GameObject gameObjectToChangeCollider, bool isEnabled)
        {
            gameObjectToChangeCollider.GetComponent<MeshCollider>().enabled = isEnabled;
        }
    }
}