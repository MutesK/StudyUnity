using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    Debug.Log("싱글턴 " + typeof(T) + "이 씬에 존재하지않음");
                }
            }
            return _instance;
        }
        set => _instance = value;
    }
}
