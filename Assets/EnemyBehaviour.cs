using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using DG.Tweening;

public class EnemyBehaviour : MonoBehaviour
{
    public bool IsVisible { get { return m_isVisible;  } }
    public string Combo;

    [SerializeField] private GameObject m_comboUIContainer;
    [SerializeField] private GameObject m_comboUIElement;
    [SerializeField] private bool m_isVisible = false;
    [SerializeField] private float m_oneStepDuration = 0.12f;
    [SerializeField] private float m_moveOffset = 5f;

    private List<GameObject> m_comboElements = new List<GameObject>();
    private RectTransform m_containerRt;
    private bool m_isPlaying = false;

    void Start()
    {
        List<Vector2> positions = new List<Vector2>();

        for (int i = 0; i < Combo.Length; i++)
        {
            GameObject comboElement = Instantiate(m_comboUIElement, m_comboUIContainer.transform);
            comboElement.GetComponentInChildren<TMP_Text>().SetText(Combo[i].ToString());
            m_comboElements.Add(comboElement);
        }

        m_containerRt = m_comboUIContainer.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_containerRt);

        for (int i = 0; i < Combo.Length; i++)
        {
            positions.Add(m_comboElements[i].GetComponent<RectTransform>().position);
        }
        
        m_containerRt.GetComponent<HorizontalOrVerticalLayoutGroup>().enabled = false;
        m_containerRt.GetComponent<ContentSizeFitter>().enabled = false;

        for (int i = 0; i < m_comboElements.Count; i++)
        {
            m_comboElements[i].GetComponent<RectTransform>().position = positions[i];
        }
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x - (Time.deltaTime * GameOrchestrator.Instance.ScrollSpeed)), transform.position.y);

        m_isVisible = transform.position.x - (m_containerRt.rect.width / 2f) <= 128f;
    }

    public void ValidateInput(int p_index)
    {
        GameObject comboElement = m_comboElements[p_index];
        Image img = comboElement.GetComponent<Image>();
        img.color = Color.green;
    }

    public void WrongInput(int p_index)
    {
        GameObject comboElement = m_comboElements[p_index];


        if(!m_isPlaying)
        {
            Image img = comboElement.GetComponent<Image>();
            img.color = Color.red;

            m_isPlaying = true;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(comboElement.transform.DOMoveY(comboElement.transform.position.y + m_moveOffset, m_oneStepDuration))
              .Append(comboElement.transform.DOMoveY(comboElement.transform.position.y, m_oneStepDuration))
              .Append(comboElement.transform.DOMoveY(comboElement.transform.position.y - m_moveOffset, m_oneStepDuration))
              .Append(comboElement.transform.DOMoveY(comboElement.transform.position.y, m_oneStepDuration)
              .OnComplete(() => { img.color = Color.white; m_isPlaying = false; }));

            mySequence.Play();
        }
    }
}
