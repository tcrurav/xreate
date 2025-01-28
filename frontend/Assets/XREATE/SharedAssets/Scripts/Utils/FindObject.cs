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

}

// From: https://discussions.unity.com/t/how-to-find-an-inactive-game-object/129521
