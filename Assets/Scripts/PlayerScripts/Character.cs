using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MetroidvaniaTools
{
    public class Character : MonoBehaviour
    {
        protected Collider2D col;
        protected Rigidbody2D rb;
        void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
        }
    }
}

