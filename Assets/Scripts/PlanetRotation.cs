using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [Header("Настройка вращения")]
    public float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
