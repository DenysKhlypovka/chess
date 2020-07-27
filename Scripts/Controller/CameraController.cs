using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Util;

namespace Controller
{
  public class CameraController : MonoBehaviour
  {
    private bool isRotating = true;
    Vector3 cameraStaticPosition = new Vector3(3.5f, 7.76f, -0.89f);
    Quaternion cameraStaticQuaternion = Quaternion.Euler(new Vector3(60f, 0f, 360f));
    
    float timeLeft = 6;

    private Transform boardTransform;

    private float fixateCameraCoroutineSteps = 40f;
    private float fixateCameraCoroutineStepDuration = 0.015f;

    void Start()
    {
      boardTransform = ComponentsUtil.GetBoardController().gameObject.transform;
    }

    void Update()
    {
      if (isRotating)
      {
        transform.RotateAround(boardTransform.position, boardTransform.up, 20 * Time.deltaTime);
      }
      
      timeLeft -= Time.deltaTime;
      if ( timeLeft < 0 )
      {
        timeLeft = 5000;
        ToggleFixateCamera();
      }
    }

    void ToggleFixateCamera()
    {
      GetComponent<PostProcessLayer>().enabled = false;
      isRotating = false;
      StartCoroutine(ReturnToStaticPosition());
    }
    
    private IEnumerator ReturnToStaticPosition()
    {
      var transform = this.transform;
      // transform.position = cameraStaticPosition;
      // transform.rotation = cameraStaticQuaternion;

      var deltaPosition = (cameraStaticPosition - transform.position) / fixateCameraCoroutineSteps;
      var deltaRotation = (cameraStaticQuaternion.eulerAngles - transform.rotation.eulerAngles) / fixateCameraCoroutineSteps;
      for (var i = 0; i < fixateCameraCoroutineSteps; i++)
      {
        transform.position += deltaPosition;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + deltaRotation);
        yield return new WaitForSeconds(fixateCameraCoroutineStepDuration);
      }
    }
  }
}