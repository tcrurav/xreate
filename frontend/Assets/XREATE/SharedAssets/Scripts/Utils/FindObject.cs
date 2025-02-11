using TMPro;
using Unity.Netcode;
using UnityEngine;

public class FindObject
{
    // FindObject: finds an inactive game object, and an active as well (but for that I use FindGameObjectWithTag).
    public static GameObject FindInsideParentByName(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject FindInsideNetworkObjectParentByName(NetworkObject parent, string name)
    {
        TMP_Text[] trs = parent.GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}

// From: https://discussions.unity.com/t/how-to-find-an-inactive-game-object/129521
