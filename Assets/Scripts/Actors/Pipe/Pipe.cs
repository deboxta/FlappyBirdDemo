using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(TranslateMover))]
    public class Pipe : MonoBehaviour
    {
        [Header("Position")][SerializeField] private float minY = -1f;
        [SerializeField] private float maxY = 1f;
        
        private GameController gameController;
        private ScoreEventChannel scoreEventChannel;
        private TranslateMover mover;
        private PlayerSensor playerSensor;

        private void Awake()
        {
            scoreEventChannel = Finder.ScoreEventChannel;
            gameController = Finder.GameController;
            mover = GetComponent<TranslateMover>();
            playerSensor = GetComponentInChildren<PlayerSensor>();
        }

        private void Start()
        {
            transform.Translate(Vector3.up * Random.Range(minY, maxY));
            
            //Destroy(gameObject, ...time); marche pour d/truire mais mieux dans timedDestroy
        }

        private void OnHit(GameObject other)
        {
            scoreEventChannel.NotifyScored();
        }

        private void OnEnable()
        {
            playerSensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            playerSensor.OnHit -= OnHit;
        }

        private void Update()
        {
            if (gameController.GameState == GameState.Playing)
            {
                mover.Move();
            }
        }
    }
}