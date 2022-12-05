using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    [Header("Points Settings")]
    public Transform[] points;

    [Header("Speed Settings")]
    public float speed;

    [Header("Spawn Settings")]
    public GameObject blockPrefab;
    public GameObject blocksParent;
    bool canSpawn;

    [Header("Timer Settings")]
    public float endGameTimer;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI endGameTimeText;
    public TextMeshProUGUI returnText;
    public Button endGameButton;
    public Image backgroundCanvas;
    float timer;

    void Start()
    {
        Instantiate(blockPrefab, transform.position, transform.rotation, blocksParent.transform);
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimer();

        transform.position = Vector3.Lerp(points[0].position, points[1].position, Mathf.PingPong(Time.time * speed, 1.0f));

        if (canSpawn)
        {
            Instantiate(blockPrefab, transform.position, transform.rotation, blocksParent.transform);
            canSpawn = false;
        }

        if (GameObject.Find("BlocksInGround").transform.childCount >= 10 || timer >= endGameTimer)
        {
            endGameTimeText.text = "Time: " + timer.ToString("F0");

            timer = 0;
            UpdateTimer();
            timerText.gameObject.SetActive(false);

            endGameButton.gameObject.SetActive(true);
            backgroundCanvas.gameObject.SetActive(true);
            returnText.gameObject.SetActive(true);

            for (int i = 0; i < blocksParent.transform.childCount; i++)
            {
                Destroy(blocksParent.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < GameObject.Find("BlocksInGround").transform.childCount; i++)
            {
                Destroy(GameObject.Find("BlocksInGround").transform.GetChild(i).gameObject);
            }

            canSpawn = true;

            gameObject.SetActive(false);
        }
    }

    public void SetCanSpawn(bool _canSpawn)
    {
        canSpawn = _canSpawn;
    }

    void UpdateTimer()
    {
        timerText.text = "Timer: " + timer.ToString("F2");
    }
}
