using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDetection : MonoBehaviour
{
    static public ComboDetection Instance;

    private string m_combo = "aze";
    private int m_comboIndex = 0;
    private EnemyBehaviour m_enemy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        GameOrchestrator.Instance.OnNewTargetedEnemy += SetEnemy;
    }

    private void Update()
    {
        if (m_enemy != null && !m_enemy.IsVisible)
            return;

        if (Input.anyKeyDown && !(Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), m_combo[m_comboIndex].ToString().ToUpper())))
            {
                m_enemy.ValidateInput(m_comboIndex);

                Debug.Log("Right key detected");
                if (m_comboIndex + 1 < m_combo.Length)
                {
                    m_comboIndex++;
                    Debug.Log("Next : " + m_combo[m_comboIndex]);
                }
                else ComboDone();
            }
            else
            {
                m_enemy.WrongInput(m_comboIndex);
                Debug.Log("Wrong key detected");
            }
        }
    }

    public void SetEnemy()
    {
        m_enemy = GameOrchestrator.Instance.TargetedEnemy.GetComponent<EnemyBehaviour>();
        m_combo = m_enemy.Combo;
    }

    private void ComboDone()
    {
        GameOrchestrator.Instance.Player.ComboDone(m_enemy);
        m_enemy = null;
        Debug.Log("Combo done");
    }
}
