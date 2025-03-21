using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedUsersController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    //void OnEnable()
    //{
    //    Refresh();
    //}

    public void Refresh()
    {
        ClearContent();

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            NetworkObject playerObject = client.PlayerObject;
            int studentId = playerObject.GetComponent<PlayerSync>().PlayerId.Value;

            string studentName = playerObject.GetComponent<PlayerSync>().PlayerName.Value.ToString();

            int clientTeamId = CurrentActivityManager.GetTeamIdByStudentId(studentId);

            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(buttonContainer.transform, false);

            string rejoin = "refresh";
            if (studentId == MainManager.GetUser().id) rejoin = "rejoin";

            GameObject textObject = FindObject.FindInsideParentByName(newButton, "Text (TMP)");
            textObject.GetComponent<TMP_Text>().SetText(
                "StudentId: " + studentId.ToString() + "<br>" +
                "StudentName: " + studentName + "<br>" +
                "TeamId: " + clientTeamId.ToString() +
                " - " + rejoin);

            Button tempButton = newButton.GetComponent<Button>();
            tempButton.onClick.AddListener(() =>
            {
                if (rejoin == "rejoin")
                {
                    MainNetworkManager.NetworkQuickJoinLoginUsingUnity6TemplateMenus();
                    return;
                }
                //Refresh();
            });

        }
    }

    public void ClearContent()
    {
        int childrenCount = buttonContainer.transform.childCount;

        for (int i = childrenCount - 1; i >= 0; i--)
        {
            DestroyImmediate(buttonContainer.transform.GetChild(i).gameObject);
        }
    }

}
