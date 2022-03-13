using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSave : MonoBehaviour
{

    public static int numPieceTypes = 4; 
    public static GameObject[,] cells = new GameObject[5, 8];

    public static Piece[,] pieces = {
        {Piece.Archer, Piece.Mage, Piece.King, Piece.Mage, Piece.Archer},
        {Piece.Warrior, Piece.Warrior, Piece.Warrior, Piece.Warrior, Piece.Warrior}
    };
    public enum Piece : ushort
    {
        Archer,
        Mage,
        Warrior,
        King
    };

    // public enum Card
    // {
    //     Knights_Move
    // }

    public string [] Card_List = {"Knights_Move"}; 

    // Path for Prefab cards for *Mulligan*
    public static string[] pathMulligan = { "Assets/Prefab/chess_card_mulligan/attack/attack_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/attack/attack_warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_archer.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_mage.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_Warrior.prefab",
                                            "Assets/Prefab/chess_card_mulligan/move/move_king.prefab"};
    
    // Path for Prefab Board
    public static string board_cell_path = "Assets/Prefab/board/cell.prefab";


    public static string[] test = {"Assets/Prefab/cardTest.prefab",
                                   "Assets/Prefab/cardTest2.prefab",
                                   "Assets/Prefab/cardTest3.prefab",
                                   "Assets/Prefab/cardTest4.prefab",
                                   "Assets/Prefab/cardTest5.prefab",
                                   "Assets/Prefab/cardTest6.prefab",
                                   "Assets/Prefab/cardTest7.prefab"};

    public static string[,] test2 = {
        {"archer", "attack"}, 
        {"mage", "attack"},
        {"warrior", "attack"}, 
        {"archer", "move"}, 
        {"mage", "move"}, 
        {"warrior", "move"}, 
        {"king", "move"}, 
    };

    public static float[] positionBoard = { -320f, -570f, 160 };

    public static string[] piece = {"Assets/Prefab/archerTest.prefab",
                                    "Assets/Prefab/mageTest.prefab",
                                    "Assets/Prefab//warriorTest.prefab",
                                    "Assets/Prefab/KingTest.prefab"};
    public static int[,] position = {
        {0, 1}, {1, 0}, {0, -1}, {-1, 0}, {1, 1}, {1, -1}, {-1, 1}, {-1, -1}
    };
    
    public static int[] cardList = new int[3];
    public static bool playFirst = true;

    
}
