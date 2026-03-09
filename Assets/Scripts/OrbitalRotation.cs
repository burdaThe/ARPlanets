using UnityEngine;

public class OrbitalRotation : MonoBehaviour
{
    [Header("Целевой объект")]
    public Transform targetObject;
    public float orbitSpeed = 30f;


    private void Update()
    {
        if (targetObject != null)
        {
            transform.RotateAround(targetObject.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
