using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private int spawnDelay = 4;

        private GameController gameController;
        private PrefabFactory prefabFactory;

        private void Awake()
        {
            gameController = Finder.GameController;
            prefabFactory = Finder.PrefabFactory;
        }

        private void OnEnable()
        {
            //est dans onEnable afin de pouvoir activer et desactiver la couroutine
            StartCoroutine(SpawnPipeRoutine());
        }

        //Voici une couroutine :
        private IEnumerator SpawnPipeRoutine()
        {
            while (isActiveAndEnabled)
            {
                //Attend une seconde du moment que la couroutine est instancier et cree un pipe
                yield return new WaitForSeconds(spawnDelay);

                if (gameController.GameState != GameState.MainMenu && gameController.GameState != GameState.GameOver)
                    prefabFactory.CreatePipePair(transform.position, Quaternion.identity, null);
                //Instantiate(pipePrefab, transform.position, Quaternion.identity); // quaternion identity = 0 rotation // fait la meme chose que la ligne du dessus mais sans la factory
            }
        }
    }
}