using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Game
{
    //Pour dire qu<on a besoin du component flapmover pour marcher avec le component player dans unity ( evite des bugs) :
    [RequireComponent(typeof(FlapMover))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float targetMainMenuHeight = 0f;

        private GameController gameController;
        private PlayerDeathEventChannel playerDeathEventChannel;
        private FlapMover flapMover;
        private HazardSensor sensor;

        private void Awake()
        {
            //Appellé lorsque le composant est créé.
            gameController = Finder.GameController;
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            flapMover = GetComponent<FlapMover>();
            sensor = GetComponentInChildren<HazardSensor>();
        }

        private void Start()
        {
            //Appellé juste avant la première frame où ce composant est utilisé.
        }

        private void OnEnable()
        {
            //Appellé lorsque le composant est activé. Appellé avant le "start".
            sensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            //Appellé lorsque le composant est désactivé. N'est pas appellé si le
            //composant est initialement désactivé
            sensor.OnHit -= OnHit;
        }

        private void OnHit(GameObject other)
        {
            Die();
        }
        
        //contextemenu permet dappeller la fonction dans le menu (clic droit) de unity pour un gameobject
        [ContextMenu("Die")]
        private void Die()
        { 
            Destroy(gameObject);
            playerDeathEventChannel.NotifyPlayerDeath();
        }

        private void Update()
        {
            //Appelé à chaque frame. Le contenue devrait être légée puisque appellé pour tout les composants.
            
            //transform.Translate(Vector3.right * 5 * Time.deltaTime);

            var gameState = gameController.GameState;
            
            if (gameState == GameState.MainMenu)
            {
                if (transform.position.y < targetMainMenuHeight) 
                    flapMover.Flap();
            }
            else if (gameState == GameState.Playing)
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                    flapMover.Flap();
            }
        }

        private void FixedUpdate()
        {
            //Appellé à intervales réguliers, lorsque l'engin de physique se met à jour.
            //Utilisé pour le déplacement d'un joueur puisqu'au meme moment la physique se met à jour.
        }

        private void OnDestroy()
        {
            //Appellé lorsque le composant est détruit. Cela ne veut pas dire que le GameObject est détruit.
        }
    }
}