// Wykład 1, slajd: Typy Unity, których użyjesz od L1
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

Vector3 pos = new Vector3(1f, 0f, 2f);
Vector3 dir = Vector3.forward;       // (0, 0, 1)
float dist = Vector3.Distance(pos, Vector3.zero);
Vector3 step = dir * 5f * Time.deltaTime;

transform.position += step;
transform.rotation = Quaternion.Euler(0f, 45f, 0f);

Color c = Color.red;
GetComponent<Renderer>().material.color = c;

float hp = Mathf.Clamp(120f, 0f, 100f); // 100
int roll = Random.Range(1, 7);   // 1..6 dla int
