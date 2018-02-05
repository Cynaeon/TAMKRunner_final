using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float m_fMovementSpeed;
    public float m_fGravity;
    public float m_fKillY;
    public float m_fJumpSpeed;


    public Vector3 m_vStartPos;

    private CharacterController m_gcCharacterController;
    private float m_fJumpVelocity;
    private Vector3 m_vTrajectory;
    private Transform m_tTransform;
    private bool m_bIsJumping = false;

    private GameGlobals m_gcGameGlobals;

	// Use this for initialization
	void Start () {
        m_gcCharacterController = GetComponent<CharacterController>();
        if (null == m_gcCharacterController)
        {
            Debug.LogError("Player Character does not have a character controller assigned!");
            return;
        }

        m_tTransform = GetComponent<Transform>();
        m_tTransform.position = m_vStartPos;

        m_gcGameGlobals = GameGlobals.Instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_gcGameGlobals.m_bPlayerIsAlive)
            return;

        // Check to see if the player has fallen below the Y threshold
        if (m_tTransform.position.y < m_fKillY || m_tTransform.position.z < m_vStartPos.z - 0.25f)
        {
            KillPlayer();
        }

        // Clear any previous movement
        m_vTrajectory = Vector3.zero;

        // Clear the jump flag if we're touching the ground
        // We can also do this by just checking the current y position
        if (m_bIsJumping && (
          m_gcCharacterController.collisionFlags == CollisionFlags.Below ||
          m_gcCharacterController.collisionFlags == CollisionFlags.CollidedBelow ||
          m_gcCharacterController.isGrounded))
        {
            m_bIsJumping = false;
            m_fJumpVelocity = 0.0f;
        }

        // Detect a jump
        if (Input.GetButton("Jump") && !m_bIsJumping)
        {
            m_bIsJumping = true;
            m_fJumpVelocity = m_fJumpSpeed;
        }

        // Roll off the current jump velocity so the upward movement slows over time
        {
            if (m_fJumpVelocity > 0.0f)
                m_fJumpVelocity -= (m_fJumpSpeed * 2) * Time.deltaTime;
            else
                m_fJumpVelocity = 0.0f;
        }

        // Apply the vertical and horizontal movement to the trajectory
        {
            m_vTrajectory.y = m_fJumpVelocity;
            m_vTrajectory.y -= m_fGravity * Time.deltaTime;
            m_vTrajectory.x = (Mathf.Clamp(Input.GetAxis("Horizontal"), -1.0f, 1.0f) * m_fMovementSpeed) * Time.deltaTime;
        }

        // Give the trajectory to the Character Controller's Move()
        m_gcCharacterController.Move(m_vTrajectory);
    }

    public void KillPlayer()
    {
        // Spawn another particle effect
        m_gcGameGlobals.m_bPlayerIsAlive = false;
        m_tTransform.position = new Vector3(m_vStartPos.x, 8.0f, m_vStartPos.z);
        m_bIsJumping = false;
        m_fJumpVelocity = 0.0f;
    }
}
