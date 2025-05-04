// By Adam Nixdorf

using UnityEngine;
using System;


internal class JsonHelper
{
    // Helper class to deserialize JSON arrays
    public static T[] FromJson<T>(string json)
    {
        // Wrap the JSON array in an object
        string newJson = "{\"Items\": " + json + "}";
        return JsonUtility.FromJson<Question<T>>(newJson).Items;
        
    }
    [System.Serializable]
    private class Question<T>
    {
        public T[] Items;
    }
}