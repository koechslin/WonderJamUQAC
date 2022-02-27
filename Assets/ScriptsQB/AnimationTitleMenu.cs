using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTitleMenu : MonoBehaviour
{
    [SerializeField] private Text m_titleText;
    [SerializeField] private string m_title;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Write(m_title));
    }

    // Update is called once per frame
    void Update()
    {
        
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
