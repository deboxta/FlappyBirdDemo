﻿using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Game
{
    public class TranslateMover : MonoBehaviour
    {
        [SerializeField] private Vector3 velocity = Vector3.left;

        public void Move()
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}