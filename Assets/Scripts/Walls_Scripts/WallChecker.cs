using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Debug.Log("Jugador entró al muro: " + other.name);

            WallPenaltyManager penalty = other.GetComponent<WallPenaltyManager>();

            if (penalty != null)
            {
                penalty.StartPenalty();
            }
            else
            {
                Debug.LogWarning("El jugador no tiene WallPenaltyManager.");
            }
        }
    }
}



