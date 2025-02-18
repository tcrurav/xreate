using System.Collections;
using UnityEngine;

public class TestActivityChallengeConfigItem : MonoBehaviour
{
    public ActivityChallengeConfigItemService service;

    private void Start()
    {
        StartCoroutine(TestGetAllById());
    }

    private IEnumerator TestGetAllById()
    {
        yield return StartCoroutine(service.GetAllById(3));

        if (service.activityChallengeConfigItems != null && service.activityChallengeConfigItems.Length > 0)
        {
            Debug.Log("Se recibieron datos correctamente:");
            foreach (var item in service.activityChallengeConfigItems)
            {
                Debug.Log($"ID: {item.id}, Item: {item.item}");
            }
        }
        else
        {
            Debug.Log("No se recibieron datos o el array está vacío.");
        }
    }
}
