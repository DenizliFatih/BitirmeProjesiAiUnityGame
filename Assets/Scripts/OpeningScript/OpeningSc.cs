using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class OpeningSc : MonoBehaviour
{
    public List<TMP_Text> openingTextList;
    int i = 0;

    private void Start()
    {
        TextDelays();
    }

    private void TextDelays()
    {

        openingTextList[i].DOFade(1, 1.5f).OnComplete(() =>
        {
            openingTextList[i].DOFade(0, 1.5f).OnComplete(() =>
            {
                openingTextList[i].gameObject.SetActive(false);
                i++;
                if (i < openingTextList.Count)
                {
                    openingTextList[i].gameObject.SetActive(true);

                    TextDelays();
                }
                else
                {
                    Debug.Log("bitti");
                    SceneManager.LoadScene("03NewGame");
                }
            });
        });
    }
}

