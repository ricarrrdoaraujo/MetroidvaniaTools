using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] protected KeyCode crouchHeld;
        [SerializeField] protected KeyCode dashPressed;
        [SerializeField] protected KeyCode sprintingHeld;
        [SerializeField] protected KeyCode jump;
        void Update()
        {
            CrouchHeld();
            DashPressed();
            SprintingHeld();
            JumpPressed();
            JumpHeld();
        }
        
        public virtual bool CrouchHeld()
        {
            if (Input.GetKey(crouchHeld))
            {
                return true;
            }
            return false;
        }
        
        public virtual bool DashPressed()
        {
            if (Input.GetKeyDown(dashPressed))
            {
                return true;
            }
            else
                return false;
        }
        
        public virtual bool SprintingHeld()
        {
            if (Input.GetKey(sprintingHeld))
            {
                return true;
            }
            else
                return false;
        }
        
        public virtual bool JumpHeld()
        {
            if (Input.GetKey(jump))
            {
                return true;
            }
            else
                return false;
        }
        
        public virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(jump))
            {
                return true;
            }
            else
                return false;
        }
    }
}

