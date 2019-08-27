using System;
using UnityEngine;

namespace Game
{
    public class MovingBackground : MonoBehaviour
    {
        [Header("Visuals")] [SerializeField] private Sprite sprite = null;
        [Range(0, 10)] [SerializeField] private uint nbTiles = 3;
        [SerializeField] private string sortingLayerName = "Default";
        [Header("Behaviour")] [Range(0,100)] [SerializeField] private float speed = 1f;

        private Vector2 tileSize;
        private Vector3 initiallPosition;
        private float offset;
        private GameController gameController;

        private void Start()
        {
            gameController = Finder.GameController;
            tileSize = sprite.bounds.size;
            for (uint i = 0; i < nbTiles; i++)
            {
                var tile = new GameObject(i.ToString());
                tile.transform.parent = transform;
                tile.transform.localPosition = tileSize.x * i * Vector3.right;
                
                var spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingLayerName = sortingLayerName;
            }
            initiallPosition = transform.position;
            offset = 0f;
        }

        private void Update()
        {
            if (gameController.GameState == GameState.Playing)
            {
                offset = (offset + (speed * Time.deltaTime)) % tileSize.x;

                transform.position = initiallPosition + Vector3.left * offset;
                
            }
        }
        
        //Pour afficher les carres des planches
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var size = sprite  == null? Vector3.one : sprite.bounds.size;
            var center = transform.position;
            
            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}