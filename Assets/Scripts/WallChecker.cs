using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("InsideWall"))
            Debug.Log("OnTriggerEnter for player " + gameObject.name + " with wall " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("InsideWall"))
            Debug.Log("OnTriggerExit for player " + gameObject.name + " with wall " + other.gameObject.name);
    }
}
