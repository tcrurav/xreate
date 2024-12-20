using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthController : MonoBehaviour
{
    private AuthService authService;

    public TMP_InputField UsernameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField CodeInputField;

    public GameObject loadingCanvas;
    public GameObject errorCanvas;

    private void Start()
    {
        authService = gameObject.AddComponent<AuthService>();
    }

    public void Login()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(DoLogin());
    }

    IEnumerator DoLogin()
    {
        User user = new()
        {
            username = UsernameInputField.text,
            password = PasswordInputField.text,
            code = CodeInputField.text,
        };

        yield return authService.Login(user);

        Debug.Log("reponseCode");
        Debug.Log(authService.responseCode);

        if (authService.responseCode != 200)
        {
            loadingCanvas.SetActive(false);
            errorCanvas.SetActive(true);
            yield break;
        }

        SceneManager.LoadSceneAsync("SpaceshipScene");
    }

}
