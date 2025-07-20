using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUponTagging : MonoBehaviour
{
    [Header("Reference To Game Manager")]
    [SerializeField] private GameManager m_gameManager;

    private void OnTriggerEnter(Collider other)
    {
#pragma warning disable CS0642 // Possible mistaken empty statement
        if (other.CompareTag("tag")) ;
        {
            Transform temp = other.gameObject.transform.Find("where to look at");
            m_gameManager.EndGame(temp);
        }
    }
}
