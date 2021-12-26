using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics; 

public class Game_Manager : MonoBehaviour
{
    // public static int a = 1; 
        public static List<GameObject> archerOnBoard_player1 = new List<GameObject>();
        public static List<ChessPiece> archerConstructors_player1 = new List<ChessPiece>();

        public static List<GameObject> mageOnBoard_player1 = new List<GameObject>();
        public static List<ChessPiece> mageConstructors_player1 = new List<ChessPiece>();

        public static List<GameObject> warriorOnBoard_player1 = new List<GameObject>();
        public static List<ChessPiece> warriorConstructors_player1 = new List<ChessPiece>();

        public static List<GameObject> kingOnBoard_player1 = new List<GameObject>();
        public static List<ChessPiece> kingConstructors_player1 = new List<ChessPiece>();

        public static List<GameObject> dots = new List<GameObject>();


}
