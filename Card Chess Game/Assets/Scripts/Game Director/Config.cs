using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Config {
    enum Prefab {
        Dot_Move, 
        Thunder_Bolt, 
        Cell, 
    }

    enum Names {
        P1_First = 1, 
        P2_First = 2,
        Mulligan, 
    }


    // Class For Path Strings
    public static class Path {
        public static string prefab_base_path = "Assets/Prefab"; 
        public static string sprite_base_path = "Sprites"; 
        public static class Prefab {
            public static string dot_move = "Dot_Move";
            public static string thunder_bolt = "Thunder_Bolt"; 
            public static string board = "Board/Cell"; 
        }
    }

    // Class For Helper/Utility Functions
    public static class Helper {
        // Creates And Returns GameObject Based On Prefab Name As String
        public static GameObject prefabNameToGameObject(string prefab_name){
            string path = (typeof(Path.Prefab).GetProperty(prefab_name.ToLower()).GetValue(null) as string).ToUpper();
            Object prefab = AssetDatabase.LoadAssetAtPath($"{Path.prefab_base_path}/{path}", typeof(GameObject));
            return GameObject.Instantiate(prefab) as GameObject;
        }

        public static Sprite generateSprite(params string[] path_list) {
            string fileLocation = $"{Config.Path.sprite_base_path}/{string.Join("/", path_list)}";
            return Resources.Load<Sprite>(fileLocation);
        }
    }
}

