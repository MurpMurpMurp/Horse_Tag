using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Movement Variables")]
    [SerializeField] private float m_moveSpeed = 12f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private float m_jumpHeight = 3f;
    [SerializeField] private float m_runningSpeed;

    [Header("Movement X and Y")]
    [SerializeField] private float m_x;
    [SerializeField] private float m_z;
    [SerializeField] private Vector3 m_movementVector;

    [Header("character controller reference")]
    [SerializeField] private CharacterController m_charController;

    [Header("Ground Check Reference")]
    [SerializeField] private Transform m_groundCheck;

    [Header("Ground Check Variables")]
    [SerializeField] private float m_groundDistance = 0.4f;
    [SerializeField] private LayerMask m_groundMask;
    [SerializeField] private bool m_isGrounded;

    [Header("Walking Sound Effects")]
    [SerializeField] private AudioClip m_walkingSound;
    [SerializeField] private AudioSource m_walkingAudioSource;
    [SerializeField] private float m_timeBetweenWalkingSounds;

    [HideInInspector] public bool m_hasGameEnded = true;
    [HideInInspector] private bool m_isRunning;
    [HideInInspector] private bool m_isMoving;

    private Vector3 m_velocity;
    private float m_timer;

    private void Update()
    {
        if (!m_hasGameEnded)
        {
            GetInputs();
            CheckIfThePlayerIsGrounded();
            PlaySoundsWhenPlayerIsGrounded();
        }
    }

    private void FixedUpdate()
    {
        if (m_hasGameEnded == false)
        {
            MoveCharacter();
            Jump();
            ApplyGravity();
        }
    }
    private void GetInputs()
    {
        m_x = Input.GetAxis("Horizontal");
        m_z = Input.GetAxis("Vertical");

        m_movementVector = transform.right * m_x + transform.forward * m_z;
    }

    private void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_charController.Move(m_movementVector * (m_moveSpeed + m_runningSpeed) * Time.deltaTime);
        }
        else
        {
            m_charController.Move(m_movementVector * m_moveSpeed * Time.deltaTime);
        }
        
    }

    private void ApplyGravity()
    {
        m_velocity.y += m_gravity * Time.deltaTime;
        m_charController.Move(m_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && m_isGrounded)
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
        }

        if (m_isGrounded && m_velocity.y <0)
        {
            m_velocity.y = -2f;
        }
    }

    private void CheckIfThePlayerIsGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
    }

    private void PlaySoundsWhenPlayerIsGrounded()
    {
        if (m_isGrounded)
        {
            CheckForInputs();
            m_timer += Time.deltaTime;
            if (m_timer >= m_timeBetweenWalkingSounds && m_isMoving)
            {
                m_walkingAudioSource.PlayOneShot(m_walkingSound);
                m_timer = 0;
            }
        }
    }

    private void CheckForInputs()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            m_isMoving = true;
        }
        else
        {
            m_isMoving = false;
        }
    }
}
