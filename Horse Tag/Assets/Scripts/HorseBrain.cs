using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseBrain : MonoBehaviour
{
    [Header("Horse Related Reference")]
    [SerializeField] private NavMeshAgent m_horseAgent;
    [SerializeField] private SpriteRenderer m_horseSpriteRenderer;

    [Header("Nav Agent Variables")]
    [SerializeField] private float m_horseSpeed;
    [SerializeField] private float m_maxHorseSpeed;

    [Header("Timer Stuff")]
    [SerializeField] private float m_timer;
    [SerializeField] private float m_timeToReach;

    [Header("Player Transform Reference")]
    [SerializeField] private Transform m_playerLocation;

    [Header("Slowly Increase Horse Speed Stuff")]
    [SerializeField] private float m_changeSpeed;

    [Header("Horse Clop Sounds")]
    [SerializeField] private AudioClip[] m_horseClops;
    [SerializeField] private AudioSource m_horseAudioSource;

    private float m_defaultHorseSpeed;
    private float m_timeForChange;

    [HideInInspector] public bool m_hasGameEnded = false;

    private void Start()
    {
        m_defaultHorseSpeed = m_horseSpeed;
        m_horseAgent.speed = m_horseSpeed;
    }
    private void Update()
    {
        if (!m_hasGameEnded)
        {
            FlipHorseSpriteSoThatHeLooksLikeHesRunning();
            IncreaseHorseSpeedOverTimer();

            MakeHorseChasePlayer();
            m_horseAgent.isStopped = false;
        }
        else
        {
            m_horseAgent.isStopped = true;
            m_horseAgent.acceleration = 999999999;
        }
    }

    private void MakeHorseChasePlayer()
    {
        m_horseAgent.destination = m_playerLocation.position;
    }

    private void FlipHorseSpriteSoThatHeLooksLikeHesRunning()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_timeToReach)
        {
            int temp = Random.Range(0, m_horseClops.Length);
            m_horseAudioSource.PlayOneShot(m_horseClops[temp]);
            m_horseSpriteRenderer.flipX = !m_horseSpriteRenderer.flipX;
            m_timer = 0f;
        }
    }

    private void IncreaseHorseSpeedOverTimer()
    {
        m_horseSpeed = Mathf.Lerp(m_defaultHorseSpeed, m_maxHorseSpeed, m_timeForChange);
        m_timeForChange += m_changeSpeed * Time.deltaTime;
    }
}
