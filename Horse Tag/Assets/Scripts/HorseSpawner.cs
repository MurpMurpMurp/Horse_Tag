using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class HorseSpawner : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float m_timer;
    [SerializeField] private float m_timeToReach;

    [Header("SpawnPoint")]
    [SerializeField] private Transform m_spawnPoint;

    [Header("Horse Prefab Reference")]
    [SerializeField] private GameObject m_horsePrefab;

    [Header("Player Location Reference")]
    [SerializeField] public Transform m_playerLocation;

    [Header("Horse Neigh Sound Effect")]
    [SerializeField] private AudioSource m_audioSource;

    [HideInInspector] public bool m_hasGameEnded = true;

    private NavMeshHit m_hit;

    private void Update()
    {
        if (m_hasGameEnded == false)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_timeToReach)
            {
                NavMesh.SamplePosition(m_spawnPoint.position, out m_hit, 10f, NavMesh.AllAreas);
                GameObject tempGO = Instantiate(m_horsePrefab, m_hit.position, m_spawnPoint.rotation);
                HorseBrain tempHorseBrain = tempGO.GetComponent<HorseBrain>();
                tempHorseBrain.m_horseAgent.Warp(m_hit.position);
                tempHorseBrain.enabled = true;
                tempHorseBrain.m_playerLocation = m_playerLocation;
                tempHorseBrain.m_startGame = true;
                m_audioSource.PlayOneShot(m_audioSource.clip);
                m_timer = 0;
            }
        }
    }
}
