using UnityEngine;

public class Camera : MonoBehaviour
{
    private static Camera _instance;
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
