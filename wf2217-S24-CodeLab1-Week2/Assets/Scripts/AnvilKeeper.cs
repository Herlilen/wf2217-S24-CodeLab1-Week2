using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AnvilKeeper : MonoBehaviour
{
    public static AnvilKeeper Instance;
    public GameManager _gameManager;
    public GameObject particlePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_gameManager.hammerDown)
        {
            //spawn particle on anvil
            Instantiate(particlePrefab, new Vector3(0, 0, 0), quaternion.identity);
        }
    }
}
