using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Game
{
    public class HUDController : MonoBehaviour
    {
        private GameController gameController;
        private Canvas canvas;
        private Text text;
        
        private void Awake()
        {
            gameController = Finder.GameController;
            canvas = GetComponent<Canvas>();
            text = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            UpdateVisibility(gameController.GameState);
            UpdateScore(gameController.Score);
        }
        //ceci aurait pu etre aussi fait dans le start et destroy
        //Il faut s<habonner et se desabonner au ongamestatechange :
        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
            gameController.OnGameScoreChanged += UpdateScore;
        }
        //Et se desabonenr egalement : 
        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
            gameController.OnGameScoreChanged -= UpdateScore;
        }
        
        private void UpdateScore(int score)
        {
            text.text = score.ToString("00");
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.Playing || gameState == GameState.GameOver;
        }
    }
}