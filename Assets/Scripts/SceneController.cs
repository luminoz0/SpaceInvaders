using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Animator fadeAnimator;
    [SerializeField]
    private UnityEvent onSceneStarted;
    private void Start()
    {
        onSceneStarted?.Invoke();
    }
    public void GoToSceneWithFade(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }
    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        fadeAnimator.Play("FadeOut");
        yield return fadeAnimator.WaitForCurrentAnimation();
        SceneManager.LoadScene(sceneName);
    }
}