using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Directions")]
    [SerializeField] private float m_mouseX;
    [SerializeField] private float m_mouseY;

    [Header("Mouse Variables")]
    [SerializeField] private float m_sensitivity = 100f;

    [Header("Player Body Reference")]
    [SerializeField] private Transform m_playerBodyTransform;

    private float m_xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        UpdateMouseDirection();

        RotateBody();
    }

    private void UpdateMouseDirection()
    {
        m_mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
        m_mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

        m_xRotation -= m_mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);
    }

    private void RotateBody()
    {
        m_playerBodyTransform.Rotate(Vector3.up * m_mouseX);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
    }
}
