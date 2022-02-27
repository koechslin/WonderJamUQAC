using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTitleMenu : MonoBehaviour
{
    [SerializeField] private Text m_titleText;
    [SerializeField] private string m_title;

    private Coroutine titleCoroutine;

    private void OnEnable()
    {
        titleCoroutine = StartCoroutine(Write(m_title));
    }

    private void OnDisable()
    {
        StopCoroutine(titleCoroutine);
        AudioManager.instance.Stop("Typing");
    }

    IEnumerator Write(string sentence)
    {
        AudioManager.instance.Play("Typing");
        m_titleText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            m_titleText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        AudioManager.instance.Stop("Typing");
    }
}
