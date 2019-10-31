using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerGame;

    private Optimizer m_optimizer;

    private List<GameObject> players;

    private void Awake()
    {
        if (!isPlayerGame)
        {
            m_optimizer = GameObject.FindGameObjectWithTag("Evaluator").GetComponent<Optimizer>();
        }
        players = new List<GameObject>();
    }

    private void Start()
    {
        //if(!isPlayerGame)
        //{
        //    m_optimizer.StartEA();
        //}
    }

    public void AddPlayer(GameObject t_player)
    {
        if (players == null)
        {
            players = new List<GameObject>();
        }

        players.Add(t_player);
    }

    // <return> Returns the amount of players left
    public int RemovePlayer(GameObject t_player)
    {
        players.Remove(t_player);

        return players.Count;
    }

    public void LoadScene(int t_sceneIndex)
    {
        SceneManager.LoadScene(/*SceneManager.GetActiveScene().buildIndex*/t_sceneIndex);
    }
}
