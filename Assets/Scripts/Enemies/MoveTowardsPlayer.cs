using UnityEngine;


/// <summary>
/// filthy poo poo class thats a placeholder for showcasing a demo
/// </summary>
public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 5f; // You can adjust this speed to suit your game

    private Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.Log("Player object not found");
        }
    }

    void Update()
    {
        // Make sure the player was found
        if (player != null)
        {
            // Move our object towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(10);
        }
    }
}
