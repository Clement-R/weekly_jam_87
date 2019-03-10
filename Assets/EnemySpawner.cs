using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyBehaviour> m_enemies = new List<EnemyBehaviour>();
    [SerializeField] private List<GameObject> m_enemiesPrefabs = new List<GameObject>();
    [SerializeField] private Transform m_defaultPosition;
    [SerializeField] private float m_spawnCooldown = 2f;
    [SerializeField] private float m_spawnSpacing = 256f;

    private GameObject m_targetedEnemy = null;
    private float m_nextSpawn = 0f;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector2 spawnPosition = Vector2.zero;
        Vector3 offset = new Vector3(m_spawnSpacing, 0f, 0f);

        if (m_enemies.Count == 0)
            spawnPosition = m_defaultPosition.position;
        else
            spawnPosition = m_enemies[m_enemies.Count - 1].transform.position + offset;

        GameObject enemy = Instantiate(m_enemiesPrefabs[0], spawnPosition, Quaternion.identity);
        m_enemies.Add(enemy.GetComponent<EnemyBehaviour>());

        if (GameOrchestrator.Instance.TargetedEnemy == null)
            GameOrchestrator.Instance.TargetedEnemy = enemy;
    }

    void Update()
    {
        
    }
}
