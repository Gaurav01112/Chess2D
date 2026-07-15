using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject gameOverUI;
    private void Awake()
    {
        Instance = this;
    }
}
