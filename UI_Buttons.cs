using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoSingleton<UI_Buttons>
{
    [SerializeField] public GameObject nextButton;
    [SerializeField] private GameObject retryButton;
    [SerializeField] public List<GameObject> endPoints = new List<GameObject>();
    [SerializeField] private GameObject confettiParticle;
    public void NextLevelButtonActivate()
    {
        if(endPoints.Count == 2)
        {
            if(endPoints[0].GetComponent<End>().isComplated == true && endPoints[1].GetComponent<End>().isComplated == true )
            {
            Instantiate(confettiParticle,endPoints[0].transform.position,confettiParticle.transform.rotation);
            Instantiate(confettiParticle,endPoints[1].transform.position,confettiParticle.transform.rotation);
            nextButton.SetActive(true);
            }
        }
        else if (endPoints.Count ==1)
        {
            if(endPoints[0].GetComponent<End>().isComplated == true)
            {
            Instantiate(confettiParticle,endPoints[0].transform.position,confettiParticle.transform.rotation);
            nextButton.SetActive(true);
            }
        }
    }
    public void LevelComplete()
    {
        NextLevelButtonActivate();
    }
    

    public void NextLevelButtonClick()
    {
        GameControl.Instance.level++;
        SceneManager.LoadScene("Level " + GameControl.Instance.level);
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

