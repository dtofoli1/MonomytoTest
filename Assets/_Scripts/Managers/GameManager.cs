using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    public GameObject level;
    public GameObject boxPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public List<GameObject> weapons = new List<GameObject>();
    public Transform[] patrolPoints;
    public Player player;
    public int points;
    private float offset = 0.5f;

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled;
        DestructableBox.OnBoxDestruct += HandleBoxDestructed;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
        DestructableBox.OnBoxDestruct -= HandleBoxDestructed;
    }

    public void GameStart()
    {
        uiManager.HideMenu();
        SpawnPlayer();
        SpawnBox();
        StartCoroutine(SpawnTimer(() => SpawnBox(), 10));
        SpawnEnemy();
        StartCoroutine(SpawnTimer(() => SpawnEnemy(), 15));
    }
    private void SpawnBox()
    {
        Vector3 position = GetSpawnPoint();
        if (ValidateSpawnPoint(position))
        {
            Instantiate(boxPrefab, position, Quaternion.identity, level.transform);
        }
        else
        {
            SpawnBox();
        }
    }

    private Vector3 GetSpawnPoint()
    {
        float x = Random.Range(-11, 11) + offset;
        float z = Random.Range(-11, 11) + offset;
        Vector3 spawnPosition = new Vector3(x, offset, z);
        return spawnPosition;
    }

    private bool ValidateSpawnPoint(Vector3 target)
    {
        Ray ray = Camera.main.ScreenPointToRay(target);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<InteractableObject>())
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnPlayer()
    {
        Vector3 position = GetSpawnPoint();
        position.y *= 2;
        if (ValidateSpawnPoint(position))
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
            player = spawnedPlayer.GetComponent<Player>();
        }
        else
        {
            SpawnPlayer();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = GetSpawnPoint();
        position.y *= 2;
        if (ValidateSpawnPoint(position))
        {
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        else
        {
            SpawnEnemy();
        }
    }

    public void HandleEnemyKilled(Enemy enemy)
    {
        points += 5;
        uiManager.UpdateText(uiManager.points, points.ToString());
        StartCoroutine(SpawnTimer(() => SpawnEnemy(), 10));
    }

    public void HandleBoxDestructed(DestructableBox box)
    {
        points += 3;
        uiManager.UpdateText(uiManager.points, points.ToString());
        Instantiate(weapons[Random.Range(0, weapons.Count)], box.transform.position, Quaternion.identity);
        StartCoroutine(SpawnTimer(() => SpawnBox(), 5));
    }

    private IEnumerator SpawnTimer(System.Action callback, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while(percent < duration)
        {
            percent += Time.deltaTime;
            yield return update;
        }

        callback();
    }

    public void GameOver()
    {
        if (player != null)
        {
            uiManager.menuPoints.SetActive(true);
            uiManager.UpdateText(uiManager.menuPointsText, points.ToString());
            uiManager.startButton.onClick.RemoveAllListeners();
            uiManager.startButton.onClick.AddListener(() => RestartGame());
            Destroy(player.gameObject);
            player = null;
            uiManager.ShowMenu();
            uiManager.UpdateText(uiManager.info, "GAME OVER");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
