using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {

        [SerializeField] private KeyCode startGameKey = KeyCode.Space;

        private PlayerDeathEventChannel playerDeathEventChannel;

        private ScoreEventChannel scoreEventChannel;

        private GameState gameState = Game.GameState.MainMenu;
        private int score;

        public int Score => score;
        //public GameState GameState { get; private set; } = GameState.MainMenu; ancien

        public GameState GameState
        {
            get => gameState;
            private set
            {
                if (gameState != value)
                {
                    gameState = value;
                    NotifyGameStateChanged();
                }
            }
        }


        private void NotifyGameStateChanged()
        {
            if (OnGameStateChanged != null) OnGameStateChanged(gameState);

        }

        public event GameStateChangedEventHandler OnGameStateChanged;
        public event GameScoreChangedEventHandler OnGameScoreChanged;

        private void Awake()
        {
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            scoreEventChannel = Finder.ScoreEventChannel;
            score = 0;
        }

        private void Start()
        {
            //Pour s<Assurer que la scene nest pas deja charg/e
            if (!SceneManager.GetSceneByName(Scenes.GAME).isLoaded)
                StartCoroutine(LoadGame());
            else
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
        }

        IEnumerator LoadGame()
        {
            //TODO : Show loading screen
            yield return SceneManager.LoadSceneAsync(Scenes.GAME, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.GAME));
            //TODO : Hide loading screen
        }
        
        IEnumerator ReloadGame()
        {
            //TODO : Show loading screen
            yield return UnloadGame();
            yield return LoadGame();
            //TODO : Hide loading screen
        }

        private IEnumerator UnloadGame()
        {
            yield return SceneManager.UnloadSceneAsync(Scenes.GAME);
        }

        private void OnEnable()
        {
            scoreEventChannel.OnScored += IncrementScore;
            playerDeathEventChannel.OnPlayerDeath += EndGame;
        }

        private void OnDisable()
        {
            scoreEventChannel.OnScored -= IncrementScore;
            playerDeathEventChannel.OnPlayerDeath -= EndGame;
        }

        private void IncrementScore()
        {
            score++;
            if (OnGameScoreChanged != null)
            {
                OnGameScoreChanged(score);
            }
            //Debug.Log("allo");
        }

        private void EndGame()
        {
            GameState = GameState.GameOver;
        }

        private void RestartGame()
        {
            GameState = GameState.MainMenu;
            score = 0;

            StartCoroutine(ReloadGame());
        }

        private void Update()
        {
            if (GameState == GameState.MainMenu && Input.GetKeyDown(startGameKey))
            {
                GameState = GameState.Playing;
            }
            if (GameState == GameState.GameOver && Input.GetKeyDown(startGameKey))
                RestartGame();
        }
        
        private void NotifyTimerChanged()
        {
            if (OnGameStateChanged != null) OnGameStateChanged(GameState);
        }
        
    }

    public delegate void GameStateChangedEventHandler(GameState newGameState);
    public delegate void GameScoreChangedEventHandler(int newScore);
    
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
}