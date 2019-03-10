using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour
{
    public static GameOrchestrator Instance;

    public Action OnNewTargetedEnemy;

    public PlayerBehaviour Player;
    public int ScrollSpeed { get { return m_scrollSpeed; } }
    public GameObject TargetedEnemy
    {
        get { return m_targetedEnemy; }
        set
        {
            m_targetedEnemy = value;
            if (OnNewTargetedEnemy != null) OnNewTargetedEnemy.Invoke();
        }
    }

    [SerializeField] private int m_scrollSpeed = 240;

    private GameObject m_targetedEnemy = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
