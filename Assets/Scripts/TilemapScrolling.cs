using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScrolling : MonoBehaviour
{
    [SerializeField]  private GameObject m_tilemapPrefab;

    private GameObject[] m_tilemaps = new GameObject[3];

    private const int TILEMAP_WIDTH = 256;

    void Start()
    {
        for (int i = 0; i < m_tilemaps.Length; i++)
            m_tilemaps[i] = Instantiate(m_tilemapPrefab, new Vector2(i * TILEMAP_WIDTH, 0f), Quaternion.identity, transform);
    }

    void Update()
    {
        for (int i = 0; i < m_tilemaps.Length; i++)
        {
            GameObject tilemap = m_tilemaps[i];
            if (tilemap.transform.position.x <= -TILEMAP_WIDTH)
            {
                int idx = i - 1 >= 0 ? i -1 : m_tilemaps.Length - 1;
                tilemap.transform.position = new Vector2(m_tilemaps[idx].transform.position.x + TILEMAP_WIDTH, 0f);
            }

            tilemap.transform.position = new Vector2(Mathf.Round(tilemap.transform.position.x - (Time.deltaTime * GameOrchestrator.Instance.ScrollSpeed)), 0f);
        }
    }
}
