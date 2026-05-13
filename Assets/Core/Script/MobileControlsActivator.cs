using UnityEngine;

public class MobileControlsActivator : MonoBehaviour
{
    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        gameObject.SetActive(false);   // на ПК скрываем весь Canvas
#else
        gameObject.SetActive(true);    // на мобильных – показываем
#endif
    }
}