using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] public uint m_score;
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private GameObject m_scoreBackGround;

    [Header("End Screen Stuff")]
    [SerializeField] private Animator m_imageAnimator;
    [SerializeField] private AudioSource m_endAudioSource;
    [SerializeField] private bool m_endGame = false;

    [Header("Music AudioSource Reference")]
    [SerializeField] private AudioSource m_mainMusic;

    [Header("Script References")]
    [SerializeField] private HorseBrain m_horseBrain;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private MouseLook m_mouseLook;
    [SerializeField] private CharacterLookAtHorse m_characterLookAtHorse;
    [SerializeField] private HorseSpawner m_horseSpawner;

    private float m_FloatToUIntScore;
    private Scene m_scene;
    private float m_timer = 0;

    private void Start()
    {
        EndGame(this.transform);
        m_characterLookAtHorse.enabled = false;
        m_imageAnimator.SetTrigger("reset");

        m_scene = SceneManager.GetActiveScene();
        m_scoreText.enabled = false;
        m_scoreBackGround.SetActive(false);
    }

    private void Update()
    {
        UpdateScore();

        if (m_endGame)
        {
            PlayEndSounds();
        }
    }

    private void PlayEndSounds()
    {
        if (m_timer >= 0.7f && m_timer <= 2f)
        {
            m_endAudioSource.PlayOneShot(m_endAudioSource.clip);
            m_timer = 4f;
        }
        else
        {
            m_timer += Time.deltaTime;
        }
    }

    private void UpdateScore()
    {
        if (!m_endGame)
        {
            m_FloatToUIntScore += Time.deltaTime * 5f;
            m_score = (uint)m_FloatToUIntScore;
            m_scoreText.text = new string("Score: " + m_score.ToString());
        }
    }

    public void EndGame(Transform horse)
    {
        m_imageAnimator.SetTrigger("close");
        m_characterLookAtHorse.SetHorseToLookAt(horse);
        m_characterLookAtHorse.enabled = true;
        m_playerMovement.m_hasGameEnded = true;
        m_mouseLook.m_hasGameEnded = true;
        m_endGame = true;
        m_horseSpawner.m_hasGameEnded = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        m_horseBrain.m_startGame = true;
        m_playerMovement.m_hasGameEnded = false;
        m_mouseLook.m_hasGameEnded = false;
        m_endGame = false;
        m_horseSpawner.m_hasGameEnded = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_score = 0;
        m_scoreText.enabled = true;
        m_scoreBackGround.SetActive(true);
    }

    public void ResetGame()
    {
        m_mainMusic.Stop();
        SceneManager.LoadScene(m_scene.name, LoadSceneMode.Single);
    }

}
