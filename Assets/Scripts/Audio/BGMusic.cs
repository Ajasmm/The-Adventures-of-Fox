using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private BGMusic Instance;

    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
}
