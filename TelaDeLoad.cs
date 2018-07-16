using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelaDeLoad: MonoBehaviour
{

    [SerializeField] private string sceneName;
    //nome da cena
    [SerializeField] private GameObject loadingUI;
    //o gameobject é um fundo X
    [SerializeField] private Slider loadingSlider;
    //o slider vai carregar de 0 a 100%

    public void ChangeScene(string sceneName) //vai no botão start
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            loadingSlider.value += operation.progress; //conta de 0 a 1
            yield return null;
        }
    }
}
