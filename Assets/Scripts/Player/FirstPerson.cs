
using UnityEngine;

using KOS.Events;
using KOS.Weapons;

namespace KOS.Player
{
    public class FirstPerson : MonoBehaviour
    {
        [Header("Settings")]
        [Range(0.1f, 5)] public float mouseSensitivity;
        public bool invert = false;
        private float mouseX, mouseY;

        [Header("Options")]
        public bool lockOnPlay = true;

        [HideInInspector]
        public Transform player, view;
        private int IsInverted => (invert) ? -1 : 1;

        private bool cameraLocked;

        //> SET MOUSE SENSITIVITY
        private void SetMouseSensitivity(float newSensitivity) => mouseSensitivity = newSensitivity;

        //> SET FIELD OF VIEW
        private void SetFieldOfView(float fov) => view.GetComponent<Camera>().fieldOfView = fov;

        //> INITIALIZATION
        private void Awake()
        {
            view = transform;
            cameraLocked = lockOnPlay;
            if (lockOnPlay) Cursor.lockState = CursorLockMode.Locked;

            mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 1);
            view.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("fov", 90);

            // subscribe to events
            EventManager.Active.onWeaponFired += AddRecoil;
            EventManager.Active.onSetFieldOfView += SetFieldOfView;
            EventManager.Active.onToggleCursorLock += ToggleLockCursor;
            EventManager.Active.onSetMouseSensitivity += SetMouseSensitivity;
        }

        //> GET INPUT EVERY FRAME
        private void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Return)) ToggleLockCursor();
            #endif
            
            if (!cameraLocked) return; // Early stop if camera isn't locked

            HandleInput();
        }

        private void HandleInput()
        {
            // reduce recoil
            recoil *= 0.85f;
            
            // get mouse input
            mouseY = Input.GetAxis("Mouse X") * mouseSensitivity + transform.eulerAngles.y + recoil.x;
            mouseX += Input.GetAxis("Mouse Y") * mouseSensitivity * IsInverted + recoil.y;
    
            // clamp the vertical rotation
            mouseX = Mathf.Clamp(mouseX, -90.0f, 90.0f);
    
            // rotate the camera and player
            player.eulerAngles = new Vector3(0, mouseY, 0);
            view.eulerAngles = new Vector3(mouseX, mouseY, 0);
        }

        private Vector2 recoil; // hiding like a sneaky

        //> ADDITIVE RECOIL
        private void AddRecoil(WeaponData stats)
        {
            recoil.y += Random.Range(stats.verticalRecoil.x, stats.verticalRecoil.y);
            recoil.x += Random.Range(stats.horizontalRecoil.x, stats.horizontalRecoil.y);
        }
        
        //> LOCK CURSOR TO WINDOW
        private void LockCursor(bool truth)
        {
            if (truth == true) 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraLocked = true;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraLocked = false;
            }
        }

        //> TOGGLE LOCK CURSOR TO WINDOW
        private void ToggleLockCursor()
        {
            cameraLocked = !cameraLocked;

            if (Cursor.lockState == CursorLockMode.None) 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
