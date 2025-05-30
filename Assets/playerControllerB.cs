using UnityEngine;

public class playerControllerB : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float h = 0f;
        float v = 0f;

        // Movimiento con flechas
        if (Input.GetKey(KeyCode.A))
            h = -1f;
        else if (Input.GetKey(KeyCode.D))
            h = 1f;

        if (Input.GetKey(KeyCode.W))
            v = 1f;
        else if (Input.GetKey(KeyCode.S))
            v = -1f;

        Vector3 movement = new Vector3(h, 0, v).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

    }
}
