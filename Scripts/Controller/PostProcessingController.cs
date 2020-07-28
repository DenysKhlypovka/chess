using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Controller
{
  public static class PostProcessingController
  {
    public static void EnablePostProcessLayer(GameObject gameObject)
    {
      ChangePostProcessLayerEnabled(gameObject, true);
    }
    
    public static void DisablePostProcessLayer(GameObject gameObject)
    {
      ChangePostProcessLayerEnabled(gameObject, false);
    }
    
    private static void ChangePostProcessLayerEnabled(GameObject gameObject, bool enabled)
    {
      gameObject.GetComponent<PostProcessLayer>().enabled = enabled;
    }
  }
}