using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MetroidvaniaTools
{
    [RequireComponent (typeof (CapsuleCollider2D))]
    public class Dash : Abilities
    {
        [SerializeField] protected float dashForce;
        [SerializeField] protected float dashCoolDownTime;
        [SerializeField] protected float dashAmountTime;
        [SerializeField] protected LayerMask dashingLayers;

        private bool canDash;
        private float dashCountDown;
        private CapsuleCollider2D capsuleCollider2D;
        private Vector2 originalCollider;
        private Vector2 dashCollider;

        protected override void Initialization()
        {
            base.Initialization();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            originalCollider = capsuleCollider2D.size;
            // invert collider on dash
            dashCollider = new Vector2(capsuleCollider2D.size.y, capsuleCollider2D.size.x);
        }

        protected virtual void Update()
        {
            Dashing();
        }



        protected virtual void Dashing()
        {
            if (input.DashPressed() && canDash)
            {
                anim.SetBool("Dashing", true);
                dashCountDown = dashCoolDownTime;
                character.isDashing = true;
                capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
                capsuleCollider2D.size = dashCollider;
                StartCoroutine(FinishedDashing());
            }
        }

        protected virtual void FixedUpdate()
        {
            DashMode();
            ResetDashCounter();
        }

        protected virtual void DashMode()
        {
            if (character.isDashing)
            {
                FallSpeed(0);
                movement.enabled = false;
                if (!character.isFacingLeft)
                {
                    DashCollision(Vector2.right, .5f, dashingLayers);
                    rb.AddForce(Vector2.right * dashForce);
                }
                else
                {
                    DashCollision(Vector2.left, .5f, dashingLayers);
                    rb.AddForce(Vector2.left * dashForce);
                }
                    
            }
        }
        
        protected virtual void DashCollision(Vector2 direction, float distance, LayerMask collision)
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                    hits[i].collider.enabled = false;
                    StartCoroutine(TurnColliderBackOn(hits[i].collider.gameObject));
                }
            }
        }

        protected virtual void ResetDashCounter()
        {
            if (dashCountDown > 0)
            {
                canDash = false;
                dashCountDown -= Time.deltaTime;
            }
            else
            {
                canDash = true;
            }
        }

        protected virtual IEnumerator FinishedDashing()
        {
            yield return new WaitForSeconds(dashAmountTime);
            character.isDashing = false;
            FallSpeed(1);
            movement.enabled = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
            capsuleCollider2D.size = originalCollider;
            anim.SetBool("Dashing", false);
        }

        protected virtual IEnumerator TurnColliderBackOn(GameObject obj)
        {
            yield return new WaitForSeconds(dashAmountTime);
            obj.GetComponent<Collider2D>().enabled = true;
        }

    }
}

