using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
  public class ButtonsController : MonoBehaviour
  {
    internal Button StartGameButton { get; set; }
    internal Button ExitGameButton { get; set; }
    internal Button ResumeButton { get; set; }
    internal Button GoToMainMenuButton { get; set; }
    internal Button PauseButton { get; set; }
    
    private List<Button> mainMenuButtons;
    private List<Button> pauseMenuButtons;

    private const float FADE_OUT_COROUTINE_STEPS = 70f;
    
    public void Init()
    {
      PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
      
      StartGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
      ExitGameButton = GameObject.Find("ExitGameButton").GetComponent<Button>();
      mainMenuButtons = new List<Button> {StartGameButton, ExitGameButton};
      
      ResumeButton = GameObject.Find("ResumeGameButton").GetComponent<Button>();
      GoToMainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
      pauseMenuButtons = new List<Button> {ResumeButton, GoToMainMenuButton};

      DisableHidePauseButton();
      ButtonsChangeEnabled(pauseMenuButtons, false);
      ButtonsChangeTextEnabled(pauseMenuButtons, false);
      pauseMenuButtons.ForEach(button =>
      {
        var buttonImage = button.GetComponent<Image>();
        var buttonImageColor = buttonImage.color;
        buttonImage.color = new Color(buttonImageColor.r, buttonImageColor.g, buttonImageColor.b, 0);
      });
    }

    public void EnableShowPauseButton()
    {
      var buttonImage = PauseButton.GetComponent<Image>();
      var buttonImageColor = buttonImage.color;
      buttonImage.color = new Color(buttonImageColor.r, buttonImageColor.g, buttonImageColor.b, 1);
      PauseButton.enabled = true;
    }

    public void DisableHidePauseButton()
    {
      var buttonImage = PauseButton.GetComponent<Image>();
      var buttonImageColor = buttonImage.color;
      buttonImage.color = new Color(buttonImageColor.r, buttonImageColor.g, buttonImageColor.b, 0);
      PauseButton.enabled = false;
    }

    private void ButtonsChangeEnabled(List<Button> buttons, bool isEnabled)
    {
      buttons.ForEach(button =>
      {
        button.enabled = isEnabled;
      });
    }

    private void ButtonsChangeTextEnabled(List<Button> buttons, bool isEnabled)
    {
      buttons.ForEach(button =>
      {
        button.transform.GetComponentInChildren<Text>().enabled = isEnabled;
      });
    }

    public void ToggleFadeOutMainMenuButtons()
    {
      StartCoroutine(Fade(mainMenuButtons, FadeType.FadeOut));
    }

    public void ToggleFadePauseMenuButtons(FadeType fadeType)
    {
      StartCoroutine(Fade(pauseMenuButtons, fadeType)); 
    }
    
    private IEnumerator Fade(List<Button> buttons, FadeType fadeType)
    {
      var isFadeIn = fadeType == FadeType.FadeIn;

      var opacityDelta = 1 / FADE_OUT_COROUTINE_STEPS * (!isFadeIn ? 1 : -1);
      
      ButtonsChangeEnabled(buttons, isFadeIn);
      for (var i = 0; i < FADE_OUT_COROUTINE_STEPS + 1; i++)
      {
        buttons.ForEach(button =>
        {
          var buttonImage = button.GetComponent<Image>();
          var buttonImageColor = buttonImage.color;
          buttonImage.color = new Color(buttonImageColor.r, buttonImageColor.g, buttonImageColor.b, buttonImageColor.a - opacityDelta);
        });
        yield return new WaitForEndOfFrame();
      }
      ButtonsChangeTextEnabled(buttons, isFadeIn);
    }
  }
}