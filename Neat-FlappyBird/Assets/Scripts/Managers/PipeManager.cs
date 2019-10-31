using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject[] currentPipes;

    [SerializeField]
    private float m_spawnRate = 2f;
    private float m_lastTimeSpawned;

    [SerializeField]
    private GameObject m_pipePrefab = null;
    [SerializeField]
    private GameObject m_scoreTrigger = null;

    private GameManager m_gm;
    private ScoreManager m_sm;

    private List<GameObject> m_pipes;

    private void Awake()
    {
        m_gm = GetComponent<GameManager>();
        m_sm = GetComponent<ScoreManager>();

        m_pipes = new List<GameObject>();

        currentPipes = new GameObject[2];
    }

    private void Start()
    {
        SpawnPipes();

        currentPipes[0] = m_pipes[0];
        currentPipes[1] = m_pipes[1];
    }

    private void Update()
    {
        if(Time.time >= m_lastTimeSpawned + m_spawnRate)
        {
            SpawnPipes();
        }
    }

    private void SpawnPipes()
    {
        Vector3 spawnPos = new Vector3(2, 0, 0);
        float yCenter = UnityEngine.Random.Range(-.45f, .45f);
        float holeHeight = UnityEngine.Random.Range(.2f, .25f);
        spawnPos.y = yCenter - holeHeight;
        GameObject p1 = Instantiate(m_pipePrefab, spawnPos, Quaternion.identity);

        spawnPos.y = yCenter + holeHeight;
        GameObject p2 = Instantiate(m_pipePrefab, spawnPos, Quaternion.Euler(0, 0, 180));

        m_pipes.Add(p1);
        m_pipes.Add(p2);

        int indexOfSpawnedPipes = m_pipes.IndexOf(p1);

        try
        {
            currentPipes[0] = m_pipes[indexOfSpawnedPipes - 2];
            currentPipes[1] = m_pipes[indexOfSpawnedPipes - 1];
        }
        catch(Exception e) { print("Initial pipes"); }

        // We are in the menu
        if (m_scoreTrigger != null)
        {
            GameObject st = Instantiate(m_scoreTrigger, spawnPos, Quaternion.identity, p1.transform);
            st.GetComponent<ScoreTrigger>().m_sm = m_sm;
        }


        m_lastTimeSpawned = Time.time;
    }
}
