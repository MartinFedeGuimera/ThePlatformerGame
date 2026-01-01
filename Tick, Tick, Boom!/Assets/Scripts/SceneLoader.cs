using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private AudioClip loadSound;

    [SerializeField] private SceneReference nextLevel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.PlayOneShot(loadSound);
    }

    private void ResetLevel()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadScene(activeSceneName));
    }

    private void LoadNextLevel()
    {
        string nextLevelName = nextLevel.SceneName;
        StartCoroutine(LoadScene(nextLevelName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        animator.SetBool("LoadingScene", true);
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        GameEventsController.PlayerDied += ResetLevel;
        GameEventsController.TimeFinished += ResetLevel;

        GameEventsController.LevelFinished += LoadNextLevel;
    }
    private void OnDisable()
    {
        GameEventsController.PlayerDied -= ResetLevel;
        GameEventsController.TimeFinished -= ResetLevel;

        GameEventsController.LevelFinished -= LoadNextLevel;
    }
}
