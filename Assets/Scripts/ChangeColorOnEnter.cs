using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColorOnEnter : MonoBehaviour
{
    public MeshRenderer model;
    public Color normalColor = Color.white;
    public Color activeColor = new Color(1f, 0.9f, 0.6f);
    public GameObject player1;
    public GameObject player2;


    void Start()
    {
        // Solo activar si NO estamos en la escena "Game"
        if (SceneManager.GetActiveScene().name == "Game")
        {
            enabled = false;
            return;
        }

        if (model != null)
        {
            model.material.color = normalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1 && model != null)
        {
            model.material.color = activeColor;
        }
        else if (other.gameObject == player2 && model != null)
        {
            model.material.color = activeColor;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1 && model != null)
        {
            model.material.color = normalColor;
        }
        else if (other.gameObject == player2 && model != null)
        {
            model.material.color = normalColor;
        }
    }
}
