
using UnityEngine;

using KOS.Events;

namespace KOS.Weapons
{
    public class WeaponSway : MonoBehaviour
    {
        // public Transform aimPoint;
        [SerializeField]
        private Transform weapon;

        [Header("Movement")]
        public float moveSwayAmount;
        public float maxMoveSway;
        private Vector3 defaultPosition, previousPosition, deltaPosition, moveInput;
        
        [Header("Mouse")]
        public float mouseSwayAmount;
        public float maxMouseSway;
        private Vector2 mouseInput;

        [Header("Recoil")]
        public float recoilSwayAmount;
        public float maxRecoilSway;
        private float recoilX, recoilY;
        private Vector2 recoil;

        [Header("")]
        public float totalMaxSway;
        public float smoothing;

        private void Awake()
        {
            weapon = this.transform;
            defaultPosition = weapon.localPosition;    
        }

        private void OnEnable()
        {
            recoilX = 0f;
            recoilY = 0f;
            weapon.localPosition = defaultPosition;
            // transform.LookAt(aimPoint.position);
            
            EventManager.Active.onWeaponFired += AddRecoil;
        }

        private void onDisable() => weapon.localPosition = defaultPosition;

        private void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            mouseInput = new Vector2(mouseX, mouseY) * mouseSwayAmount;
            mouseInput = Vector2.ClampMagnitude(mouseInput, maxMouseSway);

            recoilX *= 0.4f;
            recoilY *= 0.4f;
            recoil = new Vector2(recoilX, recoilY) * recoilSwayAmount;
            recoil = Vector2.ClampMagnitude(recoil, maxRecoilSway);

            AddSway();
        }

        private void FixedUpdate()
        {
            deltaPosition = transform.position - previousPosition;
            previousPosition = transform.position;
            float moveX = Vector3.Dot(deltaPosition, transform.right);
            float moveY = Vector3.Dot(deltaPosition, transform.up);
            float moveZ = Vector3.Dot(deltaPosition, transform.forward);
            moveInput = new Vector3(moveX, moveY, moveZ) * moveSwayAmount;
            moveInput = Vector3.ClampMagnitude(moveInput, maxMoveSway);
        }

        private void AddSway()
        {
            Vector3 mouseSway = new Vector3(-mouseInput.x, -mouseInput.y, 0f);
            Vector3 recoilSway = new Vector3(recoil.x, -recoil.y/2, recoil.y);
            Vector3 movementSway = new Vector3(-moveInput.x, -moveInput.y, -moveInput.z);
            Vector3 combinedSway = recoilSway + mouseSway + movementSway;
            combinedSway = Vector3.ClampMagnitude(combinedSway, totalMaxSway);

            weapon.localPosition = Vector3.Lerp(weapon.localPosition, defaultPosition + combinedSway, Time.deltaTime * smoothing);
        }

        private void AddRecoil(WeaponData stats)
        {
            recoilY += Random.Range(stats.verticalRecoil.x, stats.verticalRecoil.y);
            recoilX += Random.Range(stats.horizontalRecoil.x, stats.horizontalRecoil.y);
        }
    }
}
