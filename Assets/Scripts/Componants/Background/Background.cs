using System;
using UnityEngine;

namespace Game
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Vector2 TranslationForce = Vector2.left * 5;
        private SpriteRenderer spriteRenderer;
        private float offset = 2;

        private float backgroundLength;
        
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            backgroundLength = spriteRenderer.size.x;
        }
        
        private void Update()
        {
            if (transform.position.x <= -(backgroundLength+offset))
            {
                var position = transform.position;
                position.x += (backgroundLength) * 4 - 1;
                transform.position = position;
            }
            else
            {
                transform.Translate(TranslationForce);
            }
        }

    }
}