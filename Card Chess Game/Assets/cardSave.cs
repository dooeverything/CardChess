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
    public static string[] pathMulligan = {"Assets/Prefab/chess_card_mulligan/attack/attack_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_Warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_king.prefab"};


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

    public static string[] piece = {"Assets/Prefab/test piece.prefab",
                                    "Assets/Prefab/chess_piece/Mage.prefab",
                                    "Assets/Prefab/chess_piece/Warrior.prefab",
                                    "Assets/Prefab/chess_piece/King.prefab"};
    public static int[,] positionMove = {
        {0, 1}, {1, 0}, {0, -1}, {-1, 0}
    };
    public static int[] cardList = new int[3];
    public static bool playFirst = true;
}
