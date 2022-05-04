using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PersistentGameObject : MonoBehaviour
{
    [SerializeField] public string id = string.Empty;

    public string Id => id;

    [ContextMenu("Generate GUID")]
    private void GenerateGUID() => id = Guid.NewGuid().ToString();


    public object CaptureState()
    {
        var data = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            data[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        return data;
    }

    public void RestoreState(object data)
    {
        var dataToRestore = (Dictionary<string, object>)data;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (dataToRestore.TryGetValue(typeName, out object val))
            {
                saveable.RestoreState(val);
            }

        }
    }
}
