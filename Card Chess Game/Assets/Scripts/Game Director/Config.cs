using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Config { 
    enum Sprite {
        Dot_Move, 
        Thunder_Bolt, 
    }
    // Configurations And Helper Functions 
    public static class Config
    {
        public static string prefab_path = "Assets/Prefab/"; 

        public static string prefabPathGenerator(string prefab_name) {
            return $"{prefab_path}{prefab_name}.prefab"; 
        }
        public static GameObject prefabToGameObject(string prefab_name){
            Object prefab = AssetDatabase.LoadAssetAtPath(Config.prefabPathGenerator(prefab_name), typeof(GameObject));
            return GameObject.Instantiate(prefab) as GameObject;
        }
    }
}

