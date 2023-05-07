using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 10;
    [SerializeField] private float horizontalSpacing = 1.5f;
    [SerializeField] private float verticalSpacing = 1.5f;
    [SerializeField] private Vector2 startOffset;

    private EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 spawnPosition = new Vector3(j * horizontalSpacing, -i * verticalSpacing, 0) + (Vector3)startOffset;
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
                enemyManager.RegisterEnemy(newEnemy);
            }
        }
    }
}
