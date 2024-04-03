using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.InputManager
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance { get { return _instance; } }

        protected float horizontalInput;
        public float HorizontalInput { get { return horizontalInput; } }
        protected float verticalInput;
        public float VerticalInput { get { return verticalInput; } }
        protected float jumpInput;
        public float JumpInput { get { return jumpInput; } }
        protected bool shiftInput;
        public bool ShiftInput { get { return shiftInput; } }

        void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("There is more than one InputManager in the scene!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        public void GetInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            jumpInput = Input.GetAxis("Jump");
            shiftInput = Input.GetKey(KeyCode.LeftShift);
            PressKeyEsc();
        }
        public void PressKeyEsc()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Press key Esc");
                UiManager.Instance.TriggerMenu();
            }
        }
    }

}
