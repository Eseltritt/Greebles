using System.Collections;
using UnityEngine;

namespace NattyStuff
{
    public class CharacterMovementControllerAndTestBarrierLimiter : MonoBehaviour
    {

        [TextArea(3, 5)]
        public string testNote = "This class is a temporary placeholder for testing purposes. The actual cat controller will be implemented by Ryan in a separate class.";


        public float moveSpeed = 5f;
        public float turnSpeed = 180f;
        public float jumpHeight = 2f;
        public float jumpDuration = 0.5f;

        private Animator animator;
        private bool isJumping = false;

        //for checking if the barrier is there
        private Transform barrierTransform;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();


            // Find the barrier object with the "BarrierLayer" tag
            //barrierTransform = GameObject.FindWithTag("BarrierLayer").transform;

            //if (barrierTransform == null)
            //{
            //    Debug.LogError("Barrier object with tag 'BarrierLayer' not found in the scene.");
            //}

        }


        void Update()
        {
            bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

            // Set animation parameters
            animator.SetBool("IsMoving", isMoving);

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
            }

            // Movement and Rotation
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

            // Rotate the character towards the movement direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
         toRotation, turnSpeed * Time.deltaTime);

            }

            // Check for barrier collision
            RaycastHit hit;
            if (Physics.Raycast(transform.position, moveDirection, out hit, moveSpeed * Time.deltaTime))
            {
                if (hit.collider.tag == "BarrierLayer")
                {
                    moveDirection = Vector3.zero;
                }
            }

            // Move the character
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        public IEnumerator Jump()
        {
            isJumping = true;
            animator.SetTrigger("Jump");

            float startY = transform.position.y;
            float targetHeight = startY + jumpHeight;
            float jumpDuration = 0.5f; // Adjust as needed

            float stepHeight = (targetHeight - startY) / (jumpDuration / Time.deltaTime);

            Debug.Log("Start Y: " + startY);
            Debug.Log("Target Height: " + targetHeight);
            Debug.Log("Jump Duration: " + jumpDuration);
            Debug.Log("Step Height: " + stepHeight);

            for (float elapsedTime = 0f; elapsedTime < jumpDuration; elapsedTime += Time.deltaTime)
            {
                float currentHeight = startY + elapsedTime * stepHeight;
                Debug.Log("Elapsed Time: " + elapsedTime + ", Current Height: " + currentHeight);

                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
                yield return null;
            }

            isJumping = false;
        }
    }
}