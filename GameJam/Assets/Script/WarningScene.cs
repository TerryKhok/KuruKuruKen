using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WarningScene : MonoBehaviour
{
    public Animator transition;
    public float sceneDuration;
    public float transitionTime;

    void Start()
    {
        if (SceneManager.GetActiveScene().name=="WarningScene"){
            StartCoroutine(Warning());
        }
    }

    private IEnumerator Warning()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
