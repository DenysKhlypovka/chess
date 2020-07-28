using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
  public class ButtonsController : MonoBehaviour
  {
    public Button StartGameButton { get; set; }
    public Button ExitGameButton { get; set; }

    private List<Button> allButtons;

    private const float FADE_OUT_COROUTINE_STEPS = 70f;
    
    public void Init()
    {
      StartGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
      ExitGameButton = GameObject.Find("ExitGameButton").GetComponent<Button>();
      allButtons = new List<Button> {StartGameButton, ExitGameButton};
    }

    public void ToggleFadeOutButtons()
    {
      StartCoroutine(FadeOut()); 
    }
    
    private IEnumerator FadeOut()
    {
      allButtons.ForEach(button => button.enabled = false);
      const float opacityDelta = 1 / FADE_OUT_COROUTINE_STEPS;
      for (var i = 0; i < FADE_OUT_COROUTINE_STEPS + 1; i++)
      {
        allButtons.ForEach(button =>
        {
          var buttonImage = button.GetComponent<Image>();
          var buttonImageColor = buttonImage.color;
          buttonImage.color = new Color(buttonImageColor.r, buttonImageColor.g, buttonImageColor.b, buttonImageColor.a - opacityDelta);
        });
        yield return new WaitForEndOfFrame();
      }
      allButtons.ForEach(button =>
      {
        button.transform.GetComponentInChildren<Text>().enabled = false;
      });
    }
  }
}