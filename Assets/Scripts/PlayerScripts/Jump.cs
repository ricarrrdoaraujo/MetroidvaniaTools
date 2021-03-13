using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected int maxJumps;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float distanceToCollider;
        [SerializeField] private LayerMask collisionLayer;

        private bool isJumping;
        private int numberOfJumpsLeft;

        protected override void Initialization()
        {
            base.Initialization();
            numberOfJumpsLeft = maxJumps;
        }

        protected virtual void Update()
        {
            JumpPressed();
        }

        protected virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!character.isGrounded && numberOfJumpsLeft == maxJumps)
                {
                    isJumping = false;
                    return false;
                }
                numberOfJumpsLeft--;
                if (numberOfJumpsLeft >= 0)
                {
                    isJumping = true;
                }
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
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                character.isGrounded = true;
                numberOfJumpsLeft = maxJumps;
            }
            else
            {
                character.isGrounded = false;
                isJumping = false;
            }
                
        }

        protected virtual void IsJumping()
        {
            if (isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }
}

