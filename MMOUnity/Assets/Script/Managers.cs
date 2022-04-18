using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("@GameManager");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@GameManager" };
                gameObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.GetComponent<Managers>();
        }
    }
}
