using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Events;

namespace KOS.Player
{
    public class Movement : MonoBehaviour
    {
        public float defaultSpeed = 10f, sprintSpeed = 15f, acceleration = 25f, airAcceleration = 8f, jumpHeight = 1.5f;
        public float maxGroundAngle = 25f, maxStairAngle = 50f, groundSnappingThreshold = 12f;
        public int  maxAirJumps = 1;
        
        private float minGroundDP, minStairDP;
        private int  jumpPhase, groundContacts, steepContacts;
        private int  stepsSinceGrounded, stepsSinceJumped;

        [SerializeField, Min(0)]
        private float groundedDistance = 1.25f;

        [SerializeField]
        private LayerMask groundMask = -1, stairMask = -1;

        private HashSet<GameObject> collisions; // can't add duplicates

        private Rigidbody player;
        private Vector3 newVelocity, maximumVelocity;
        private Vector3 contactNormal, steepNormal;
        private Vector2 movementInput;
        private bool jumped, sprinting;

        private PlayerStats playerStats; // for getting speed boost

        private bool OnSteep  => steepContacts  > 0; // are you on a steep surface
        private bool OnGround => groundContacts > 0; // are you and a ground surface

        //> INITIALIZATION
        private void Awake()
        {
            player = GetComponent<Rigidbody>();
            collisions = new HashSet<GameObject>();
            playerStats = GetComponent<PlayerStats>();
            
            OnValidate();
        }

        //> UPDATE WITH INSPECTOR CHANGES
        private void OnValidate()
        {
            minGroundDP = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            minStairDP = Mathf.Cos(maxStairAngle * Mathf.Deg2Rad);
        }

        //> GET INPUT EVERY FRAME
        private void Update() 
        {
            movementInput.x = Input.GetAxis("Horizontal");
            movementInput.y = Input.GetAxis("Vertical");
            movementInput = Vector2.ClampMagnitude(movementInput, 1);

            if (movementInput.magnitude <= 0 || movementInput.y < 0.5)
            {
                sprinting = false;
                EventManager.Active.Sprinting(false);
            }


            jumped |= Input.GetKeyDown(KeyCode.Space); // stays true once set, until manually reverted
            sprinting |= Input.GetKeyDown(KeyCode.LeftShift); // stays true once set, until manually reverted



            if (movementInput.magnitude <= 0 || movementInput.y < 0.5)
            {
                EventManager.Active.Sprinting(false);
                sprinting = false;
            }

            
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                EventManager.Active.Sprinting(false);
                movementInput = Vector2.zero;
                sprinting = false;
                jumped = false;
            }

            float maxSpeed = (sprinting) ? sprintSpeed : defaultSpeed;
            maxSpeed *= playerStats.GetSpeedMultiplier();
            maximumVelocity = transform.TransformDirection(movementInput.x, 0f, movementInput.y) * maxSpeed;

            if (sprinting) EventManager.Active.Sprinting(true);

            // Debug.DrawRay(player.position, Vector3.down * groundedDistance, Color.green);
            // Debug.DrawRay(player.position, newVelocity, Color.blue);
        }

        //> APPLY MOVEMENT LOGIC
        private void FixedUpdate()
        {
            UpdateState();
            CalculateVelocity();
            if (jumped) Jump();

            player.velocity = newVelocity;

            ClearState();
        }

        //> UPDATE THE TRACKER VARIABLES
        private void UpdateState()
        {
            stepsSinceGrounded += 1;
            stepsSinceJumped += 1;
            newVelocity = player.velocity;

            if (OnGround || SnapToGround() || MultipleSteepContacts())
            {
                stepsSinceGrounded = 0;

                if (stepsSinceJumped > 1) jumpPhase = 0;
                if (groundContacts > 1) contactNormal.Normalize();
            }
            else contactNormal = Vector3.up;
        }

        //> RESET THE TRACKERS TO ZERO
        private void ClearState()
        {
            groundContacts = steepContacts = 0;
            contactNormal = steepNormal = Vector3.zero;
        }

        //> CALCULATE THE INTERMEDIARY VELOCITY EACH PHYSICS STEP
        private void CalculateVelocity()
        {
            // don't try and understand this, I don't...

            //@ refactor with use of Vector3.ProjectOnPlane()
            // Vector3 projectedVelocity = Vector3.ProjectOnPlane(newVelocity, contactNormal);
            // float acceleration = OnGround ? acceleration : airAcceleration;
            // float maxSpeedDelta = acceleration * Time.deltaTime;
            // newVelocity += Vector3.MoveTowards(newVelocity, projectedVelocity, maxSpeedDelta);

            Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            float currentX = Vector3.Dot(newVelocity, xAxis);
            float currentZ = Vector3.Dot(newVelocity, zAxis);

            float currentAcceleration = OnGround ? acceleration : airAcceleration;
            float maxSpeedDelta = currentAcceleration * Time.deltaTime;
            
            float newX = Mathf.MoveTowards(currentX, maximumVelocity.x, maxSpeedDelta);
            float newZ = Mathf.MoveTowards(currentZ, maximumVelocity.z, maxSpeedDelta);

            newVelocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }

        //> HELPER FOR CALCULATE VELOCITY
        private Vector3 ProjectOnContactPlane(Vector3 v) => v - contactNormal * Vector3.Dot(v, contactNormal);

        //> DETERMING IF PLAYER IS ABLE TO JUMP
        private void Jump()
        {
            jumped = false;
            Vector3 jumpDirection;
            EventManager.Active.Jumped();

            if (OnGround)
            {
                jumpDirection = contactNormal;
            }
            else if (OnSteep)
            {
                jumpDirection = steepNormal;
                jumpPhase = 0;
            }
            else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
            {
                if (jumpPhase == 0) jumpPhase = 1; // skip first jump phase if jumping
                jumpDirection = contactNormal;
            }
            else return;

            stepsSinceJumped = 0;
            jumpPhase += 1;

            // calculate speed required to reach desired height
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            // add upwards bias to jump direction
            jumpDirection = (jumpDirection + Vector3.up).normalized;
            // project the jump speed onto the jump direction
            float alignedSpeed = Vector3.Dot(newVelocity, jumpDirection);
            // limit the players vertical speed to avoid spam exploitation
            if (alignedSpeed > 0f) jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);

            newVelocity += jumpDirection * jumpSpeed;
        }

        //> DETERMINE IF WE SHOULD SNAP THE PLAYER TO THE GROUND
        private bool SnapToGround()
        {
            // cancel if not on ground or recently jumped
            if (stepsSinceGrounded > 1 || stepsSinceJumped <= 2) return false;

            // cancel if moving faster than snapping threshold
            if (newVelocity.magnitude > groundSnappingThreshold) return false;

            // cancel if grounding check fails
            if (!Physics.Raycast(player.position, Vector3.down, out RaycastHit hit, groundedDistance, groundMask)) return false;

            // cancel if grounding check successful but surface angle is too steep
            if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer)) return false;

            // IF ALL CONDITIONS NOT TRUE THEN WE CAN SNAP TO THE GROUND

            groundContacts = 1; // only care about surface we hit
            contactNormal = hit.normal;
            float dot = Vector3.Dot(newVelocity, hit.normal);
            // project our speed onto the surface normal

            //@ could be restructured with Vector3.ProjectOnPlane()

            // if our new speed is greater than 0, then our new velocity is our previous speed
            // projected onto the surface normal
            if (dot > 0) newVelocity = (newVelocity - hit.normal * dot).normalized * newVelocity.magnitude;
            return true; // snap was successful
        }

        //> BULLSHIT I DON'T UNDERSTAND: layer masks are stored in binary...
        private float GetMinDot(int layer) => (stairMask & (1 << layer)) == 0 ? minGroundDP : minStairDP;

        //> IF THIS IS TRUE WE MAY BE STUCK IN A CREVASE
        private bool MultipleSteepContacts()
        {
            if (steepContacts <= 1) return false; // not on multiple steep surfaces
            
            steepNormal.Normalize(); // the average normal

            // if the average normal is greater than the minimum ground normal
            if (steepNormal.y >= minGroundDP)
            {
                // pretend we are on the ground so we can escape
                steepContacts = 0;
                groundContacts = 1;
                contactNormal = steepNormal;
                return true;
            }
            return false; // not on multiple steep surfaces
        }


        //> MANAGE SURFACE CONTACTS
        private void OnCollisionExit(Collision collision) => EvaluateCollision(collision);
        private void OnCollisionStay(Collision collision) => EvaluateCollision(collision);

        private void EvaluateCollision(Collision collision)
        {
            collisions.Clear(); // maybe optional

            float minDot = GetMinDot(collision.gameObject.layer);

            // check every surface player is currently touching
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (!collisions.Add(collision.gameObject)) continue; // ignore repeats

                Vector3 normal = collision.GetContact(i).normal;

                // if a standable surface add it to the surface normal
                if (normal.y >= minDot)
                {
                    groundContacts += 1;
                    contactNormal += normal;
                }
                // this is a wall
                else if (normal.y > -0.01)
                {
                    steepContacts += 1;
                    steepNormal += normal;
                }
            }
        }
    }
}

