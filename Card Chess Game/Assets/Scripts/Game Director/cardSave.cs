using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSave : MonoBehaviour
{

    public enum Piece : ushort
    {
        Archer = 0,
        Mage = 1,
        Warrior = 2,
        King = 3
    };

    public enum Card
    {
        AttackArcher,
        AttackMage,
        AttackWarrior,
        MoveArcher,
        MoveMage,
        MoveWarrior,
        MoveKing
    }
    public static string[] pathMulligan = { "Assets/Prefab/chess_card_mulligan/attack/attack_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_Warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_king.prefab"};
    public static string board_cell_path = "Assets/Prefab/board/cell.prefab";

    // public static string[] pathInGame = {"Assets/Prefab/chess_card_InGame/attack/attack_archer_InGame.prefab", 
    //                                "Assets/Prefab/chess_card_InGame/attack/attack_mage_InGame.prefab",
    //                                "Assets/Prefab/chess_card_InGame/attack/attack_warrior_InGame.prefab",
    //                                "Assets/Prefab/chess_card_InGame/move/move_archer_InGame.prefab",
    //                                "Assets/Prefab/chess_card_InGame/move/move_mage_InGame.prefab",
    //                                "Assets/Prefab/chess_card_InGame/move/move_Warrior_InGame.prefab",
    //                                "Assets/Prefab/chess_card_InGame/move/move_king_InGame.prefab"};
    public static string[] test = {"Assets/Prefab/cardTest.prefab",
                                   "Assets/Prefab/cardTest2.prefab",
                                   "Assets/Prefab/cardTest3.prefab",
                                   "Assets/Prefab/cardTest4.prefab",
                                   "Assets/Prefab/cardTest5.prefab",
                                   "Assets/Prefab/cardTest6.prefab",
                                   "Assets/Prefab/cardTest7.prefab"};

    public static float[] positionBoard = { -320f, -570f, 160 };

    public static string[] piece = {"Assets/Prefab/archerTest.prefab",
                                    "Assets/Prefab/mageTest.prefab",
                                    "Assets/Prefab//warriorTest.prefab",
                                    "Assets/Prefab/KingTest.prefab"};
    public static int[,] positionMove = {
        {0, 1}, {1, 0}, {0, -1}, {-1, 0}, {1, 1}, {-1, -1}, {-1, 1}, {1, -1}
    };
    public static int[] cardList = new int[3];
    public static bool playFirst = true;

    public static int absoluteX0 = -320; // (0,0) x위치 
    public static int absoluteX1 = 160;  // 1칸뛸때 더해주는 X거리
    public static int absolutey0 = -160; //(0,0) y위치 
    public static int absolutey2 = 160;  // 1칸뛸때 더해주는 Y거리

}
