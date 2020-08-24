using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
  public class LayoutManager
  {
    private readonly Dictionary<Color, Image> checkIconByColor;

    public LayoutManager()
    {
      checkIconByColor = new Dictionary<Color, Image>
      {
        {Color.white, GameObject.Find("white_king_icon").GetComponent<Image>()},
        {Color.black, GameObject.Find("black_king_icon").GetComponent<Image>()}
      };
      DisplayNothing();
    }

    public void DisplayCheck(Color color)
    {
      checkIconByColor[color].enabled = true;
    }

    public void DisplayCheckMate(Color color)
    {
      //check mate
    }

    public void DisplayNothing()
    {
      checkIconByColor.Values.ToList().ForEach(checkImage => checkImage.enabled = false);
    }
  }
}