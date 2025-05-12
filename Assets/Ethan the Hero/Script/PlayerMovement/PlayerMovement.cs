using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EthanTheHero
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELD

        [SerializeField] private PlayerMovementData data;
        [SerializeField] private float lastOnGroundTime;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private Transform WallCheckLeft;
        [SerializeField] private Transform WallCheckRight;
        // Super Speed Power-Up
        [SerializeField] private float superSpeedMultiplier = 2f;
        [SerializeField] private float superSpeedDuration = 10f;
        private bool isSuperSpeedActive = false;
        private float superSpeedEndTime = 0f;

        // High Jump Power-up
        [SerializeField] private float highJumpMulitplier = 1.5f;
        [SerializeField] private float highJumpDuration = 10f;
        private bool isHighJumpActive = false;
        private float highJumpEndTime = 0f;

        [HideInInspector] public Vector2 move;

        private Rigidbody2D myBody;
        private Animator myAnim;

        //Dash
        [HideInInspector] public bool isDashing;
        private bool canDash = true;
        private bool dashButtonPressed;

        //Jump
        [HideInInspector] public bool grounded;
        [HideInInspector] public bool isJumping;
        private bool jumpButtonPressed;

        //Wall Sliding and Wall Jump
        [HideInInspector] public bool wallJump;
        [HideInInspector] public bool wallSliding;
        private RaycastHit2D wall;
        private float jumpTime;

        #endregion

        #region MONOBEHAVIOUR
        void Awake()

		{
			myBody = GetComponent<Rigidbody2D>();
			myAnim = GetComponent<Animator>();
		}
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Transform newGroundCheckPoint = transform.Find("GroundCheckPoint");

            if (newGroundCheckPoint != null)
            {
                groundCheckPoint = newGroundCheckPoint;
				Debug.Log("GroundCheck found in the scene.");
            }
            else
            {
                Debug.LogWarning("GroundCheck not found in the scene.");
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void Update()
        {
            if (isDashing || wallJump || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack03"))
                return;

            lastOnGroundTime -= Time.deltaTime;

            // Input Handler
            move.x = Input.GetAxisRaw("Horizontal");
            dashButtonPressed = Input.GetKeyDown(KeyCode.W);
            jumpButtonPressed = Input.GetButtonDown("Jump");

            jump();

            if (move.x != 0)
                CheckDirectionToFace(move.x > 0);

            if (dashButtonPressed && canDash && !wallSliding)
                StartCoroutine(dash());

            if (wallSliding && jumpButtonPressed)
                StartCoroutine(wallJumpMechanic());

            // Check if super speed duration is over
            if (isSuperSpeedActive && Time.time >= superSpeedEndTime)
            {
                isSuperSpeedActive = false;
            }

            if (isHighJumpActive && Time.time >= highJumpEndTime)
            {
                isHighJumpActive = false;
            }
        }
      

		void FixedUpdate()
		{
			if (isDashing || wallJump || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack03"))
				return;

			if (!wallSliding)
				run(1);
			//checks if set box overlaps with ground
			if (groundCheckPoint != null && Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer))
			{
				lastOnGroundTime = 0.1f;
				grounded = true;
			}
			else
				grounded = false;


			WallSlidingMechanic();
		}

        #endregion

        #region RUN
        private void run(float lerpAmount)
        {
            float speed = data.runMaxSpeed;
            if (isSuperSpeedActive)
                speed *= superSpeedMultiplier;

            float targetSpeed = move.x * speed;

            float accelRate;

            targetSpeed = Mathf.Lerp(myBody.linearVelocity.x, targetSpeed, lerpAmount);

            // Calculate Acceleration and Deceleration
            if (lastOnGroundTime > 0)
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAccelAmount : data.runDeccelAmount;
            else
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.runAccelAmount * data.accelInAir : data.runDeccelAmount * data.deccelInAir;

            if (data.doConserveMomentum && Mathf.Abs(myBody.linearVelocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(myBody.linearVelocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastOnGroundTime < 0)
                accelRate = 0;

            float speedDif = targetSpeed - myBody.linearVelocity.x;
            float movement = speedDif * accelRate;

            // Implementing run
            myBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
        #endregion

        #region DASH
        private IEnumerator dash()
        {
            canDash = false;
            isDashing = true;
            float oriGrav = myBody.gravityScale;
            myBody.gravityScale = 0f;

            myBody.linearVelocity = new Vector2(transform.localScale.x * data.dashPower, 0f);
            yield return new WaitForSeconds(data.dashingTime);
            if (move.x > 0)
            {
                myBody.linearVelocity = new Vector2(data.runMaxSpeed, myBody.linearVelocity.y);
            }
            else if (move.x < 0)
            {
                myBody.linearVelocity = new Vector2(-data.runMaxSpeed, myBody.linearVelocity.y);
            }
            myBody.gravityScale = oriGrav;

            isDashing = false;
            yield return new WaitForSeconds(data.dashingCoolDown);
            canDash = true;
        }
        #endregion

        #region JUMP
        private void jump()
        {
            if (grounded)
                isJumping = false;

            float jumpForce = data.jumpHeight;

            if (isHighJumpActive)
            {
                jumpForce *= highJumpMulitplier;
            }
                

            if (jumpButtonPressed && grounded)
            {
                isJumping = true;
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, data.jumpHeight);
            }
        }
        #endregion

        #region Wall Sliding and Wall Jump
        private void WallSlidingMechanic()
        {
            wall = Physics2D.Raycast(WallCheckRight.position, Vector2.right, data.wallDistance, wallLayer);
            Debug.DrawRay(WallCheckRight.position, Vector2.right * data.wallDistance, Color.red);

            RaycastHit2D wallLeft = Physics2D.Raycast(WallCheckLeft.position, Vector2.left, data.wallDistance, wallLayer);
            Debug.DrawRay(WallCheckLeft.position, Vector2.left * data.wallDistance, Color.red);

            if (!grounded && (wall || wallLeft))
            {
                wallSliding = true;
                jumpTime = Time.time + data.wallJumpTime;
            }
            else if (jumpTime < Time.time)
            {
                wallSliding = false;
            }
            else
            {
                wallSliding = false;
            }

            if (wallSliding)
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, Mathf.Clamp(myBody.linearVelocity.y, -data.wallSlideSpeed, float.MaxValue));
        }

        private IEnumerator wallJumpMechanic()
        {
            wallJump = true;
            if (transform.localScale.x == -1f)
                myBody.linearVelocity = new Vector2(data.wallJumpingXPower, data.wallJumpingYPower);
            else
                myBody.linearVelocity = new Vector2(-data.wallJumpingXPower, data.wallJumpingYPower);
            yield return new WaitForSeconds(data.WallJumpTimeInSecond);
            wallJump = false;
        }
        #endregion

        #region OTHER
        private void CheckDirectionToFace(bool isMovingRight)
        {
            Vector3 tem = transform.localScale;
            if (!isMovingRight)
                tem.x = -1f;
            else
                tem.x = 1f;
            transform.localScale = tem;
        }
        #endregion

        #region SUPER SPEED
        public void ActivateSuperSpeed()
        {
            isSuperSpeedActive = true;
            superSpeedEndTime = Time.time + superSpeedDuration;
        }

        public void SetSuperSpeed(bool isActive)
        {
            isSuperSpeedActive = isActive;
            if (isActive)
            {
                superSpeedEndTime = Time.time + superSpeedDuration;
            }
        }
        #endregion

        #region HIGH JUMP
        public void ActivateHighJump()
        {
            isHighJumpActive = true;
            highJumpEndTime = Time.time + highJumpDuration;
        }

        public void SetHighJump(bool isActive)
        {
            isHighJumpActive = isActive;
            if (!isActive)
            {
                highJumpEndTime = Time.time + highJumpDuration;
            }
        }
        #endregion
    }
}