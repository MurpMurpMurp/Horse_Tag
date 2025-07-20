using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Directions")]
    [SerializeField] private float m_mouseX;
    [SerializeField] private float m_mouseY;

    [Header("Mouse Variables")]
    [SerializeField] private float m_sensitivity = 100f;
    [SerializeField] private Slider m_sensitivitySlider;

    [Header("Player Body Reference")]
    [SerializeField] private Transform m_playerBodyTransform;

    private float m_xRotation = 0f;

    public bool m_hasGameEnded = true;

    private void Update()
    {
        if (m_hasGameEnded == false)
        {
            UpdateMouseDirection();
            RotateBody();
        }
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

    public void SetSensitivity()
    {
        m_sensitivity = m_sensitivitySlider.value;
    }
}
