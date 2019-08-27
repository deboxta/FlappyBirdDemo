using UnityEngine;

namespace Game
{
    //sealed veux dire que la classe ne peut pas etre herite(pas tant important ici)
    public sealed class MainMenuController : MonoBehaviour
    {
        private GameController gameController;
        private Canvas canvas;
        
        private void Awake()
        {
            gameController = Finder.GameController;
            canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            UpdateVisibility(gameController.GameState);
        }
        //ceci aurait pu etre aussi fait dans le start et destroy
        //Il faut s<habonner et se desabonner au ongamestatechange :
        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
        }
        //Et se desabonenr egalement : 
        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.MainMenu;
        }
    }
}