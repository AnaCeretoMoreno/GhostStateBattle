using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float h = 0f;
        float v = 0f;

        // Movimiento con flechas
        if (Input.GetKey(KeyCode.LeftArrow))
            h = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            h = 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            v = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            v = -1f;

        Vector3 movement = new Vector3(h, 0, v).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Solo para debug: detectar cuando sueltas las teclas
        if (Input.GetKeyUp(KeyCode.LeftArrow)) Debug.Log("Left Arrow released");
        if (Input.GetKeyUp(KeyCode.RightArrow)) Debug.Log("Right Arrow released");
        if (Input.GetKeyUp(KeyCode.UpArrow)) Debug.Log("Up Arrow released");
        if (Input.GetKeyUp(KeyCode.DownArrow)) Debug.Log("Down Arrow released");
    }
}
