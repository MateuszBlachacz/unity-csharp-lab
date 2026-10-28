// Wykład 1, slajd: Pierwszy skrypt: klasa dziedzicząca po MonoBehaviour
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float speed = 90f;   // stopnie na sekundę

    void Start()
    {
        Debug.Log("Start: " + gameObject.name);
    }

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
