using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;

    public float turnDelay = .1f;

    //Static instance of GameManager which allows it to be accessed by any other script
    public static GameManager instance = null;

    //Store a reference to our BoardManager which will set up the level
    private BoardManager boardScript;

    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;
    //Current level number, expressed in game as "Day 1"
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;

    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    public void GameOver()
    {
        levelText.text = "After " + level + "days, you starved";
        levelImage.SetActive(true);
        enabled = false;
    }

    //Awake is always called before any Start function
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not , set instance to this 
            instance = this;
        }
        //if instance already exists and it's not this;
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern , meaning there can only ever be one of a GameManager
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();
        //Get a commponent reference to the attached BoardManager Script 
        boardScript = GetComponent<BoardManager>();
        
        //Call the InitGame function to initialize the first level
        InitGame();

        //SceneManager.sceneLoaded += OnLoaded;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene s1, LoadSceneMode ls1)
    {
        instance.level++;
        instance.InitGame();
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    instance.level++;
    //    instance.InitGame();
    //}

    //Initializes the game for each level
    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        levelText.text = "DAY " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();

        //Call the setupScene function of the BoardManager script, pass it current level number
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(turnDelay);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
}
