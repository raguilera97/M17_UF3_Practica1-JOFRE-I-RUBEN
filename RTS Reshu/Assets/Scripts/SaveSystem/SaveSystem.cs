using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // <GUID, Dictionary<String, object>>
    private string SavePath => $"{Application.persistentDataPath}/saverts.txt";

    [ContextMenu("Save")]
    public void Save()
    {
        // Serializa data
        var data = LoadFile();
        CaptureState(data);
        SaveFile(data);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        // Deserialize data
        var data = LoadFile();
        RestoreState(data);

    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            return data;
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            Dictionary<string, object> data = formatter.Deserialize(stream) as Dictionary<string, object>;
            return data;
        }
    }

    private void SaveFile(object data)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    private void CaptureState(Dictionary<string, object> data)
    {
        foreach (var objectToSave in FindObjectsOfType<PersistentGameObject>())
        {
            data[objectToSave.Id] = objectToSave.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> data)
    {

        foreach (var objectToLoad in FindObjectsOfType<PersistentGameObject>())
        {
            if (data.TryGetValue(objectToLoad.Id, out object val))
            {
                objectToLoad.RestoreState(val);
            }
        }
    }

}