using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class ForceAttentionController : MonoBehaviour
{
    public Toggle ForceAttentionToggle;
    public GameObject Jail;
    public GameObject Ground;

    private TeleportationArea teleportationArea;

    private void Start()
    {
        teleportationArea = Ground.GetComponent<TeleportationArea>();

        if (MainManager.GetUser().role != "TEACHER")
        {
            //Only teachers can force attention
            Debug.Log("ForceAttentionController - Only a Teacher can enable next Room");
            ForceAttentionToggle.gameObject.GetComponent<Toggle>().enabled = false;
        }
    }
    public void OnToggleValueChanged()
    {
        if (ForceAttentionToggle.isOn)
        {
            // Jail is 3 x 3 x 3, (3 cube meters).
            Vector3 newPosition = new(Jail.transform.position.x, 0, Jail.transform.position.z + 1.5f); 
            MainNavigationManager.ChangePosition(newPosition);
            Jail.SetActive(true);
            teleportationArea.enabled = false;

            GetComponent<SlideShowManager>().ChangeForceAttentionServerRpc(true);

            return;
        }

        Jail.SetActive(false);
        teleportationArea.enabled = true;

        GetComponent<SlideShowManager>().ChangeForceAttentionServerRpc(false);
    }

    public void SetToggleValue(bool isOn)
    {
        ForceAttentionToggle.isOn = isOn;
    }

}
