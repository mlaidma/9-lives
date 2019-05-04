using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StoreObjectText")]
public class StoreObjectText : ScriptableObject
{
    [TextArea(5, 6)] [SerializeField] string objectName;
    [TextArea(5, 6)] [SerializeField] string objectDescription;


    public string GetObjectName()
    {
        return objectName;
    }

    public string GetObjectDescription()
    {
        return objectDescription;
    }
}
