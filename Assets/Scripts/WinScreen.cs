using UnityEngine;
using UnityEngine.Events;
 
public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onShowWinScreen;
    [SerializeField]
    private UnityEvent onShowLoseScreen;
    [SerializeField]
    private TextMesh[] textMeshes;
    [SerializeField]
    private GameObject nextLevelButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject[] screenAssets;
    private bool isWinScreenShown = false;
    private void Awake ()
    {
        ShowScreenAssets(false);
    }
    public void ShowWinScreen()
    {
        if (isWinScreenShown) return;
        isWinScreenShown = true;
        ShowScreenAssets(true);
        onShowWinScreen?.Invoke();
        ChangeTextMeshes("You\nWin!");
        levelManager.NextLevel();
        nextLevelButton.SetActive(!levelManager.IsPastLastLevel);
        quitButton.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        if (isWinScreenShown) return;
        isWinScreenShown = false;
        ShowScreenAssets(true);
        onShowLoseScreen?.Invoke();
        ChangeTextMeshes("You\nLose!");
        nextLevelButton.SetActive(false);
        quitButton.SetActive(true);
    }
    private void ChangeTextMeshes(string text)
    {
        foreach (TextMesh textMesh in textMeshes)
        {
            textMesh.text = text;
        }
    }
    private void ShowScreenAssets(bool show)
    {
        foreach (GameObject asset in screenAssets)
        {
            asset.SetActive(show);
        }
    }
}