using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("test stuff")]
    [SerializeField] private bool m_endGame = false;

    [Header("Script References")]
    [SerializeField] private HorseBrain m_horseBrain;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private MouseLook m_mouseLook;

    private void Update()
    {
        if (m_endGame)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        m_horseBrain.m_hasGameEnded = true;
        m_playerMovement.m_hasGameEnded = true;
        m_mouseLook.m_hasGameEnded = true;
    }
}
