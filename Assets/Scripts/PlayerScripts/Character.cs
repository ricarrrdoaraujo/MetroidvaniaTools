using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MetroidvaniaTools
{
    public class Character : MonoBehaviour
    {
        [HideInInspector]
        public bool isFacingLeft;
        
        protected Collider2D col;
        protected Rigidbody2D rb;

        private Vector2 facingLeft;
        void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        protected virtual void Flip()
        {
            if (isFacingLeft)
            {
                transform.localScale = facingLeft;
            }
            else
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}

