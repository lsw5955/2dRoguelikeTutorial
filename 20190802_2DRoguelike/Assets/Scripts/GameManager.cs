using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script
    public static GameManager instance = null;

    //Store a reference to our BoardManager which will set up the level
    private BoardManager boardScript;

    //Current level number, expressed in game as "Day 1"
    private int level = 3;

    //Awake is always called before any Start function
    private void Awake()
    {
        //Check if instance already exists
        if(instance == null)
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

        //Get a commponent reference to the attached BoardManager Script 
        boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level
        InitGame();

    }

    //Initializes the game for each level
    void InitGame()
    {
        //Call the setupScene function of the BoardManager script, pass it current level number
        boardScript.SetupScene(level);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
