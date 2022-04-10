using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Configuration File For Everything Related To Pieces and Board
*/
namespace Config {
    public enum Piece : ushort
    {
        Archer,
        Mage,
        Warrior,
        King
    };

    public static class PieceConfiguration
    {
        public static int[,] move_list_surround = { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        public static int[,] move_list_knight = { { 2, 1 }, { 1, 2 }, { 2, -1 }, { 1, -2 }, { -2, 1 }, { -1, 2 }, { -2, -1 }, { -1, -2 } };
        public static int numPieceTypes = 4; 
        public static GameObject[,] cells = new GameObject[5, 8];
    }
}
