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

    public TMP_InputField Name;

    public UnityEngine.UI.Button ConfirmButton;
    public UnityEngine.UI.Button QuickJoinButton;

    public GameObject loadingCanvas;
    public GameObject errorCanvas;

    private void Start()
    {
        authService = gameObject.AddComponent<AuthService>();
    }

    public void Login()
    {
        //loadingCanvas.SetActive(true);
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

        MainNavigationManager.EnableSceneContainer("MenuSceneContainer");

        // TODO - maybe remove this lines bellow
        // Don't use this option because it loads very slowly
        //SceneManager.LoadSceneAsync("MainScene");
        //SceneManager.LoadSceneAsync("RoomModuleBScene", LoadSceneMode.Additive);
    }

    public void NetworkQuickJoinLoginUsingUnity6TemplateMenus()
    {
        // TODO - Reduce Menu scale to near 0 so that it's virtually as hiding the menu windows
        Name.text = MainManager.GetUser().username;
        ConfirmButton.onClick.Invoke();
        QuickJoinButton.onClick.Invoke();
    }

}
