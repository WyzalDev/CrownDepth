using UnityEngine;

public class CameraDontDestroy : MonoBehaviour
{
    private static CameraDontDestroy _instance;
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
