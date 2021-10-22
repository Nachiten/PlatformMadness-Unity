using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static bool flag = true;

    /* -------------------------------------------------------------------------------- */

    void Awake()
    {
        if (flag)
        {
            DontDestroyOnLoad(gameObject);
            flag = false;
        }
        else
        {
            Destroy(gameObject);
        } 
    }
}
