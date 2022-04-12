using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Config { 
    enum Sprite {
        Dot_Move, 
        Thunder_Bolt, 
    }

    // Class For Path Strings
    public static class Path {
        public static string prefab_base_path = "Assets/Prefab"; 
        
    }

    // Class For Helper/Utility Functions
    public static class Helper {
        public static GameObject prefabToGameObject(string prefab_name){
            string path = typeof(Path).GetProperty(prefab_name).GetValue(null) as string;
            Object prefab = AssetDatabase.LoadAssetAtPath($"{Path.prefab_base_path}/{path}", typeof(GameObject));
            return GameObject.Instantiate(prefab) as GameObject;
        }
    }
}

