using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Controller
{
    public class LayoutTextManager
    {
        private Text layoutText;

        private static readonly String CHECK_TEXT = "Check";
        private static readonly String CHECK_MATE_TEXT = "Check Mate";

            public LayoutTextManager()
        {
            layoutText = ComponentsUtil.GetLayoutText();
        }

        private void DisplayColoredText(String text, Color color)
        {
            layoutText.text = text;
            layoutText.color = color;
        }

        public void DisplayCheckText(Color color)
        {
            DisplayColoredText(CHECK_TEXT, color);
        }

        public void DisplayCheckMateText(Color color)
        {
            DisplayColoredText(CHECK_MATE_TEXT, color);
        }

        public void DisplayNothing()
        {
            layoutText.text = "";
        }
    }
}