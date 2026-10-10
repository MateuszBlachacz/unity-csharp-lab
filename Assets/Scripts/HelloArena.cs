using UnityEngine;

public class HelloArena : MonoBehaviour
{
    [SerializeField] private string playerName = "Ola";
    [SerializeField] private int lives = 3;
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private bool easyMode = true;

    private void Start()
    {
        Debug.Log($"Witaj, {playerName}! Masz {lives} życia, prędkość {speed}, tryb łatwy: {easyMode}");
    }
}
