using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public FireControl _fireControl;
    public float hammerPower;
    public float hammerPowerMax;
    public float HammerPowerIncreaceRate;
    public bool hammerDown;

    public GameObject hammerui;

    //score to move the level
    public int swordScore;
    public int targetScore;
    public int currentScene;
    public TextMeshProUGUI swordbuildText;
    public TextMeshProUGUI swordTarget;
    
    //spawn swords
    public GameObject swordPrefab;

    private void Awake()
    {
        if (instance == null) //if the instance var has not been set
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //if there is already a singleton of this type
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hammerDown = false;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //increasing hammer power
        if (hammerPower < hammerPowerMax)
        {
            hammerPower += HammerPowerIncreaceRate * Time.deltaTime;
        }

        if (hammerPower >= hammerPowerMax)
        {
            hammerPower = hammerPowerMax;
        }
        
        //able to forge after reaching enough heat
        if (_fireControl.particleAlpha >= 0.9f)
        {
            if (hammerPower == hammerPowerMax)
            {
                //initialize ui
                hammerui.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.L))
                {
                    //forge the sword
                    hammerDown = true;
                    //reset the power
                    hammerPower = 0;
                    //spawn sword
                    Instantiate(swordPrefab, new Vector3(-1.28f, 2.77f, -.18f), quaternion.identity);
                    swordScore++;
                }
                else
                {
                    hammerDown = false;
                }
            }
            else
            {
                hammerui.SetActive(false);
                hammerDown = false;
            }
        }
        else
        {
            hammerDown = false;
            hammerui.SetActive(false);
        }
        
        swordbuildText.text = "Sword Built: " + swordScore;
        swordTarget.text = "Target: " + targetScore;

        //to next level
        if (swordScore == targetScore)
        {
            if (currentScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentScene++);
            }
            targetScore = Mathf.RoundToInt(targetScore + targetScore * 1.5f);
        }
    }
}
