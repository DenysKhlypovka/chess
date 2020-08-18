using System.Collections;
using UnityEngine;
using Util;
using Model;

namespace Controller
{
  public class CameraController : MonoBehaviour
  {
    private readonly Vector3 cameraStaticPosition = new Vector3(3.5f, 7.76f, -0.89f);
    private Quaternion cameraStaticQuaternion = Quaternion.Euler(new Vector3(60f, 0f, 360f));

    private Transform boardTransform;

    public ZoomType zoomType { get; set; } = ZoomType.ZoomOut;

    private static readonly Vector3 FixateCameraCoroutineTranslateStep = Vector3.forward * 0.04f;
    private const float FIXATE_CAMERA_COROUTINE_STEPS = 100f;

    private IEnumerator rotateCoroutine;

    void Start()
    {
      boardTransform = ComponentsUtil.GetBoardController().gameObject.transform;
      StartCoroutine(rotateCoroutine = Rotate());
      transform.Translate(-FixateCameraCoroutineTranslateStep * FIXATE_CAMERA_COROUTINE_STEPS);
    }

    public void ToggleZoom(ZoomType zoomType)
    {
      this.zoomType = zoomType;
      var zoomingVector = zoomType == ZoomType.ZoomOut ? -FixateCameraCoroutineTranslateStep : FixateCameraCoroutineTranslateStep;
      StartCoroutine(Translate(zoomingVector));
    }

    public void ToggleFixateCamera()
    {
      StopCoroutine(rotateCoroutine);
      StartCoroutine(ReturnToStaticPosition());
    }

    private IEnumerator Rotate()
    {
      while (true)
      {
        transform.RotateAround(boardTransform.position, boardTransform.up, 20 * Time.deltaTime);
        yield return new WaitForEndOfFrame();
      }
    }
    
    private IEnumerator ReturnToStaticPosition()
    {
      var transform = this.transform;

      var angle = cameraStaticQuaternion.eulerAngles.y - transform.rotation.eulerAngles.y;
      angle = Util.Util.CalculateAngle(angle);
      
      var angleDelta = angle / FIXATE_CAMERA_COROUTINE_STEPS;
      for (var i = 0; i < FIXATE_CAMERA_COROUTINE_STEPS; i++)
      {
        transform.RotateAround(boardTransform.position, boardTransform.up, angleDelta);
        yield return new WaitForEndOfFrame();
      }
    }

    private IEnumerator Translate(Vector3 translateStep)
    {
      for (var i = 0; i < FIXATE_CAMERA_COROUTINE_STEPS; i++)
      {
        transform.Translate(translateStep);
        yield return new WaitForEndOfFrame();
      }
    }
  }
}