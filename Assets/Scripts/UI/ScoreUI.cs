using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ScoreUI : MonoBehaviour
    {
        private const string Format = "00";

        private GameController gameController;
        private Text text;

        private void Awake()
        {
            gameController = Finder.GameController;

            text = GetComponent<Text>();
        }

        private void Update()
        {
            //text.text = String.Format((Format, gameController.Score));
        }
    }
}