using UnityEngine;

public class MobileAssetsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mobileAssets;
    [SerializeField]
    private GameObject[] desktopAssets;
    private void Awake()
    {
       if(Application.isEditor)
        {
            SetActiveAssets(desktopAssets, true);
        }
        else if (Application.isMobilePlatform)
        {
            SetActiveAssets(mobileAssets, true);
        }
        else
        {
            SetActiveAssets(desktopAssets, true);
        }
    }
    private void SetActiveAssets(GameObject[] assets, bool isActive)
    {
        foreach (var asset in assets)
        {
            asset.SetActive(isActive);
        }
    }
}
