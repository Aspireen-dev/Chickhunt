using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject chickenPrefab;
    [SerializeField]
    private List<Transform> navPaths;

    private int nbTotalChicken;
    private int nbChickenToSpawn;
    private int nbChickenOnField = 0;
    private int nbChickenKilled = 0;

    private GameManager gameManager;
    private UI ui;

    private static ChickenSpawner _instance;
    public static ChickenSpawner Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Tutorial":
                nbTotalChicken = 5;
                break;
            case "Game":
                nbTotalChicken = 20;
                break;
            default:
                break;
        }
        nbChickenToSpawn = nbTotalChicken;
        gameManager = GameManager.Instance;
        ui = UI.Instance;
        ui.SetNbChickenKilled(0, nbTotalChicken);
        StartCoroutine(SpawnChickens());
    }

    private IEnumerator SpawnChickens()
    {
        while (nbChickenToSpawn > 0)
        {
            if (nbChickenOnField < 10)
            {
                SpawnChicken();
            }
            yield return new WaitForSeconds(5);
        }
    }

    private void SpawnChicken()
    {
        Transform randomNavPath = navPaths[Random.Range(0, navPaths.Count)];
        Transform randomNavStep = randomNavPath.GetChild(Random.Range(0, randomNavPath.childCount));

        List<Transform> navSteps = new List<Transform>();
        foreach (Transform navStep in randomNavPath)
        {
            navSteps.Add(navStep);
        }

        GameObject chickenInstance = Instantiate(chickenPrefab, randomNavStep.position, Quaternion.identity);
        ChickenAI chickenAI = chickenInstance.GetComponent<ChickenAI>();
        chickenAI.navSteps = navSteps;
        chickenAI.enabled = true;

        nbChickenToSpawn--;
        nbChickenOnField++;
    }

    public void ChickenKilled(int value)
    {
        nbChickenOnField--;
        nbChickenKilled++;
        ui.SetNbChickenKilled(nbChickenKilled, nbTotalChicken);
        // <= for security
        gameManager.ChickenKilled(value);
        if (nbChickenToSpawn <= 0 && nbChickenOnField <= 0)
        {
            gameManager.EndGame(true);
        }
    }

}
