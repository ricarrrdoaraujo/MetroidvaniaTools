using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected bool limitAirJumps;
        [SerializeField] protected int maxJumps;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float holdForce;
        [SerializeField] protected float buttonHoldTime;
        [SerializeField] protected float distanceToCollider;
        [SerializeField] protected float horizontalWallJumpForce;
        [SerializeField] protected float verticalWallJumpForce;
        [SerializeField] protected float maxJumpSpeed;
        [SerializeField] protected float maxFallSpeed;
        [SerializeField] protected float acceptedFallSpeed;
        [SerializeField] protected float glideTime;
        [SerializeField] [Range(-2, 2)] protected float gravity;
        [SerializeField] protected float wallJumpTime;
        [SerializeField] public LayerMask collisionLayer;

        private bool isJumping;
        private bool isWallJumping;
        private bool flipped;
        private float jumpCountDown;
        private float fallCountDown;
        private int numberOfJumpsLeft;

        protected override void Initialization()
        {
            base.Initialization();
            numberOfJumpsLeft = maxJumps;
            jumpCountDown = buttonHoldTime;
            fallCountDown = glideTime;
        }

        protected virtual void Update()
        {
            CheckForJump();
        }

        protected virtual bool CheckForJump()
        {
            if (input.JumpPressed())
            {
                if (!character.isGrounded && numberOfJumpsLeft == maxJumps)
                {
                    isJumping = false;
                    return false;
                }

                if (limitAirJumps && Falling(acceptedFallSpeed))
                {
                    isJumping = false;
                    return false;
                }

                if (character.isWallSliding)
                {
                    isWallJumping = true;
                    return false;
                }
                numberOfJumpsLeft--;
                if (numberOfJumpsLeft >= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    jumpCountDown = buttonHoldTime;
                    isJumping = true;
                    fallCountDown = glideTime;
                }
                return true;
            }
            else
                return false;
        }

        protected void FixedUpdate()
        {
            IsJumping();
            Gliding();
            GroundCheck();
            WallSliding();
            WallJumping();
        }

        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                anim.SetBool("Grounded", true);
                character.isGrounded = true;
                numberOfJumpsLeft = maxJumps;
                fallCountDown = glideTime;
            }
            else
            {
                anim.SetBool("Grounded", false);
                character.isGrounded = false;
                if (Falling(0) && rb.velocity.y < maxFallSpeed)
                { 
                    rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
                }
            }
            anim.SetFloat("VerticalSpeed", rb.velocity.y);
        }

        protected virtual bool WallCheck()
        {
            if ((!character.isFacingLeft && CollisionCheck(Vector2.right, distanceToCollider, collisionLayer)
                 || character.isFacingLeft && CollisionCheck(Vector2.left, distanceToCollider, collisionLayer))
                && movement.MovementPressed() && !character.isGrounded)
            {
                return true;
            }
            return false;
        }

        protected virtual bool WallSliding()
        {
            if (WallCheck())
            {
                if (!flipped)
                {
                    Flip();
                    flipped = true;
                }
                FallSpeed(gravity);
                character.isWallSliding = true;
                anim.SetBool("WallSliding", true);
                return true;
            }
            else
            {
                character.isWallSliding = false;
                anim.SetBool("WallSliding", false);
                if (flipped && !isWallJumping)
                {
                    Flip();
                    flipped = false;
                }
                return false;
            }
        }

        protected virtual void WallJumping()
        {
            if (isWallJumping)
            {
                rb.AddForce(Vector2.up * verticalWallJumpForce);
                if (!character.isFacingLeft)
                {
                    rb.AddForce(Vector2.left * horizontalWallJumpForce);
                }
                if (character.isFacingLeft)
                {
                    rb.AddForce(Vector2.right * horizontalWallJumpForce);
                }
                StartCoroutine(WallJumped());
            }
        }

        protected virtual IEnumerator WallJumped()
        {
            movement.enabled = false;
            yield return new WaitForSeconds(wallJumpTime);
            movement.enabled = true;
            isWallJumping = false;
        }

        protected virtual bool Falling(float velocity)
        {
            if (!isGrounded && rb.velocity.y < velocity)
            {
                return true;
            }
            else
                return false;
        }

        protected virtual void IsJumping()
        {
            if (isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce);

                AdditionalAir();
            }

            if (rb.velocity.y > maxJumpSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            }
        }

        protected virtual void Gliding()
        {
            if (Falling(0) && input.JumpHeld())
            {
                fallCountDown -= Time.deltaTime;
                if (fallCountDown > 0 && rb.velocity.y > acceptedFallSpeed)
                {
                    anim.SetBool("Gliding", true);
                    FallSpeed(gravity);
                    return;
                }
            }
            anim.SetBool("Gliding", false);
        }

        protected virtual void AdditionalAir()
        {
            if (input.JumpHeld())
            {
                jumpCountDown -= Time.deltaTime;
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    isJumping = false;
                }
                else
                    rb.AddForce(Vector2.up * holdForce);
            }
            else
                isJumping = false;
        }
    }
}

