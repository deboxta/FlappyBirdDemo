using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class TimedDestroy : MonoBehaviour
    {
        [Header("Behaviour")][SerializeField] private float destroyDelayInSeconds = 20f;

        private void OnEnable()
        {
            StartCoroutine(DestroyRoot());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator DestroyRoot()
        {
            yield return new WaitForSeconds(destroyDelayInSeconds);
            Destroy(transform.gameObject);
        }
    }
}