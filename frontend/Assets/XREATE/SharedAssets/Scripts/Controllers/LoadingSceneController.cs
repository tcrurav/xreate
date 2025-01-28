using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public string sceneToLoad;
    AsyncOperation loadingOperation;
    public Slider progressBar;
    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }
    void Update()
    {
        //progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }



    //public CanvasGroup canvasGroup;
    //void Start()
    //{
    //    StartCoroutine(FadeLoadingScreen(2));
    //}
    //IEnumerator FadeLoadingScreen(float duration)
    //{
    //    float startValue = canvasGroup.alpha;
    //    float time = 0;
    //    while (time < duration)
    //    {
    //        canvasGroup.alpha = Mathf.Lerp(startValue, 1, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    canvasGroup.alpha = 1;
    //}
}
