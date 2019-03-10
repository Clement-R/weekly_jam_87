using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private bool m_canAttack = false;
    private EnemyBehaviour m_enemy = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_canAttack)
            Attack();
    }

    public void Attack()
    {
        m_canAttack = false;
        m_animator.SetBool("Attack", true);
        StartCoroutine(_Attack());
    }

    public void ComboDone(EnemyBehaviour p_enemy)
    {
        m_canAttack = true;
        m_enemy = p_enemy;
    }

    private IEnumerator _Attack()
    {
        yield return null;
        m_animator.SetBool("Attack", false);
    }
}
