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
        [HideInInspector]
        public bool isGrounded;
        [HideInInspector]
        public bool isCrouching;
        [HideInInspector]
        public bool isDashing;
        [HideInInspector]
        public bool isWallSliding;
        
        protected Collider2D col;
        protected Rigidbody2D rb;
        protected Animator anim;
        protected HorizontalMovement movement;
        protected Jump jump;

        private Vector2 facingLeft;
        void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movement = GetComponent<HorizontalMovement>();
            jump = GetComponent<Jump>();
            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        protected virtual void Flip()
        {
            if (isFacingLeft || (!isFacingLeft && isWallSliding))
            {
                transform.localScale = facingLeft;
            }

            if (!isFacingLeft || (isFacingLeft && isWallSliding))
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        protected virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        protected virtual void FallSpeed(float speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * speed));
        }
        
    }
}

