using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlayerCamera : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float ease = 1f;

    [SerializeField] private Camera playerHead;
    [SerializeField] private Transform playerTransform;

    public Transform targetTransform;

    void Update()
    {
        float angle = Vector3.Angle(playerHead.transform.forward, (targetTransform.position - playerHead.transform.position).normalized);

        float velocity;

        if (angle > speed)
        {
            velocity = (Interpolation.LinearToParabolic(angle, ease) - Interpolation.LinearToParabolic(angle - speed, ease)) * Time.deltaTime;
        }
        else
        {
            velocity = angle;
        }

        Vector3 cross = Quaternion.Inverse(playerHead.transform.rotation) *  Vector3.Cross(playerHead.transform.forward, (targetTransform.position - playerHead.transform.position).normalized);
        Vector2 rotationDifference = Quaternion.Euler(cross.normalized * velocity).eulerAngles;

        if (rotationDifference.x > 180f) { rotationDifference.x -= 360f; }
        if (rotationDifference.y > 180f) { rotationDifference.y -= 360f; }

        playerTransform.rotation *= Quaternion.Euler(Vector3.up * rotationDifference.y);
        playerHead.transform.localRotation *= Quaternion.Euler(Vector3.right * rotationDifference.x);
    }
}
