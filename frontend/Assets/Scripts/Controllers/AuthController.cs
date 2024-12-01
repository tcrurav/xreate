using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AuthController : MonoBehaviour
{
    private AuthService authService;

    public TMP_InputField NameInputField;
    public TMP_InputField UsernameInputField;
    public TMP_InputField PasswordInputField;
    public GameObject loadingSceneCanvas;

    private void Start()
    {
        authService = gameObject.AddComponent<AuthService>();
    }

    public void Login()
    {
        StartCoroutine(DoLogin());
    }

    IEnumerator DoLogin()
    {
        User user = new()
        {
            name = "",
            username = UsernameInputField.text,
            password = PasswordInputField.text
        };

        yield return authService.Login(user);

        if(authService.responseCode == 200) loadingSceneCanvas.SetActive(true);

        //TODO: Error handling
    }

    public void Register()
    {
        StartCoroutine(DoRegister());
    }

    IEnumerator DoRegister()
    {
        User user = new()
        {
            name = "",
            username = UsernameInputField.text,
            password = PasswordInputField.text
        };

        yield return authService.Register(user);

        if (authService.responseCode == 200) loadingSceneCanvas.SetActive(true);

        //TODO: Error handling
    }

}
