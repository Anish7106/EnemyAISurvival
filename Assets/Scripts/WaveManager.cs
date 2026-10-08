using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player;

    public int startingZombies = 3;

    public TMP_Text waveText;
    public TMP_Text countdownText;

    private int currentWave = 0;
    private int zombiesAlive = 0;
    private bool startingWave = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    public void ZombieKilled()
    {
        zombiesAlive--;

        if (zombiesAlive <= 0 && !startingWave)
        {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        startingWave = true;

        currentWave++;

        int zombieCount = startingZombies + (currentWave - 1) * 2;

        waveText.text = "WAVE: " + currentWave;

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = "Starting in " + i + "...";
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";

        zombiesAlive = zombieCount;

        for (int i = 0; i < zombieCount; i++)
        {
            SpawnZombie();
        }

        yield return new WaitForSeconds(1f);

        countdownText.text = "";

        startingWave = false;
    }

    void SpawnZombie()
    {
        Vector3 screenPosition = new Vector3(
            Random.Range(0.15f, 0.85f),
            Random.Range(0.15f, 0.85f),
            10f
        );

        Vector3 worldPosition =
            Camera.main.ViewportToWorldPoint(screenPosition);

        GameObject zombie = Instantiate(
            zombiePrefab,
            worldPosition,
            Quaternion.identity
        );

        EnemyAI enemyAI = zombie.GetComponent<EnemyAI>();

        if (enemyAI != null)
        {
            enemyAI.player = player;
        }
    }
}