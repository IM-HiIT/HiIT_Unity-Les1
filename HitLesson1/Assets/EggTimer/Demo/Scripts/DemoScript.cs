using Cikoria.EggTimer;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    void Start()
    {
        // Print "Hello World" to the console
        // for three seconds
        EggTimer.Instance.Execute(() =>
        {
            Debug.Log("Hello World");
        })
        .ForDuration(3f);
    }
}