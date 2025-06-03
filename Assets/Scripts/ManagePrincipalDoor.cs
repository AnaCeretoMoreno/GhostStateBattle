using UnityEngine;
using System.Collections;

public class ManagePrincipalDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform door1; // DoorBody_1
    public Transform door2; // DoorBody_2

    private Vector3 openPos1 = new Vector3(0.452f, -10.91f, -0.011f);
    private Vector3 openRot1 = new Vector3(0f, -68.14f, 0f);

    private Vector3 closedPos1 = new Vector3(0.525f, -10.91f, -0.011f);
    private Vector3 closedRot1 = new Vector3(0f, 0f, 0f);

    private Vector3 openPos2 = new Vector3(-0.361f, -10.91f, 0.044f);
    private Vector3 openRot2 = new Vector3(0f, 248.14f, 0f);

    private Vector3 closedPos2 = new Vector3(-0.469f, -10.91f, 0.044f);
    private Vector3 closedRot2 = new Vector3(0f, 180f, 0f);

    private AudioManager audioManager;
    public float animationDuration = 0.5f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void CloseDoor()
    {
        if (door1 != null && door2 != null)
        {
            StopAllCoroutines(); 
            StartCoroutine(AnimateDoor(door1, door1.localPosition, closedPos1, door1.localEulerAngles, closedRot1));
            StartCoroutine(AnimateDoor(door2, door2.localPosition, closedPos2, door2.localEulerAngles, closedRot2));

            audioManager.PlaySFX(audioManager.doorClose);
        }
    }

    public void OpenDoor()
    {
        if (door1 != null && door2 != null)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateDoor(door1, door1.localPosition, openPos1, door1.localEulerAngles, openRot1));
            StartCoroutine(AnimateDoor(door2, door2.localPosition, openPos2, door2.localEulerAngles, openRot2));

            audioManager.PlaySFX(audioManager.doorOpen);
        }
    }

    private IEnumerator AnimateDoor(Transform door, Vector3 startPos, Vector3 endPos, Vector3 startRot, Vector3 endRot)
    {
        float time = 0f;
        Quaternion startQuaternion = Quaternion.Euler(startRot);
        Quaternion endQuaternion = Quaternion.Euler(endRot);

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            door.localPosition = Vector3.Lerp(startPos, endPos, t);
            door.localRotation = Quaternion.Lerp(startQuaternion, endQuaternion, t);
            time += Time.deltaTime;
            yield return null;
        }

        door.localPosition = endPos;
        door.localRotation = endQuaternion;
    }

}
