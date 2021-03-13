using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float distanceToCollider;
        [SerializeField] private LayerMask collisionLayer;

        private bool isJumping;
        
        protected virtual void Update()
        {
            JumpPressed();
        }

        protected virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                return true;
            }
            else
                return false;
        }

        protected void FixedUpdate()
        {
            IsJumping();
            GroundCheck();
        }

        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer))
            {
                character.isGrounded = true;
            }
            else
                character.isGrounded = false;
        }

        protected virtual void IsJumping()
        {
            if (!character.isGrounded)
            {
                isJumping = false;
                return;
            }
            if (isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }
}

