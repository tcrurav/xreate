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

    private void Start()
    {
        authService = gameObject.AddComponent<AuthService>();
    }

    public void Login()
    {
        User user = new()
        {
            name = "",
            username = UsernameInputField.text,
            password = PasswordInputField.text
        };

        authService.Login(user);
    }

    public void Register()
    {
        User user = new()
        {
            name = NameInputField.text,
            username = UsernameInputField.text,
            password = PasswordInputField.text
        };

        authService.Register(user);
    }

}
