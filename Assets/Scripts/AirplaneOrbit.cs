using UnityEngine;

public class AirplaneOrbit : MonoBehaviour
{
    public float horizontalRadius = 10f; // The horizontal radius of the ellipse
    public float verticalRadius = 5f; // The vertical radius of the ellipse
    public float altitudeChange = 0f; // The maximum altitude change
    public float speed = 5f; // The speed of the airplane
    public Vector3 center; // The center point of the ellipse

    private float angle;
    private float altitudeAngle;

    private void Start()
    {
        // Initialize the airplane position based on the horizontal radius, vertical radius, and center point
        transform.position = new Vector3(center.x + horizontalRadius, transform.position.y, center.z);
    }

    private void Update()
    {
        // Calculate the new angle based on the speed and time
        angle += speed * Time.deltaTime;

        // Calculate the new altitude angle based on a slower speed than the main angle
        altitudeAngle += speed * Time.deltaTime * 0.5f;

        // Calculate the new position based on the angle, horizontal radius, and vertical radius
        float x = center.x + Mathf.Cos(angle) * horizontalRadius;
        float z = center.z + Mathf.Sin(angle) * verticalRadius;

        // Calculate the new altitude based on the altitude angle and altitude change
        float y = center.y + Mathf.Sin(altitudeAngle) * altitudeChange;

        // Update the airplane position
        transform.position = new Vector3(x, y, z);

        // Calculate the forward direction
        Vector3 forwardDirection = Vector3.Normalize(new Vector3(Mathf.Cos(angle + Mathf.PI / 2), Mathf.Sin(altitudeAngle), Mathf.Sin(angle + Mathf.PI / 2)));

        // Calculate the new rotation while limiting the rotation on the X-axis
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
        Vector3 targetEulerAngles = targetRotation.eulerAngles;
        targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, -4f, 4f); // Adjust the limits as needed
        Quaternion limitedRotation = Quaternion.Euler(targetEulerAngles);

        transform.rotation = limitedRotation;
    }
}
