using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardEffect
{
    public static void Knights_Move(GameObject piece, GameObject card) {
        
        int [,] move_list_core = {{1, 2}, {2, 1}}; 
        List<int[]> move_list = new List<int[]>();
        List<GameObject> dots = new List<GameObject>();

        for(int i = 0; i < 2; i++) {
            for(int j = 0; j < 2; j++) {
                for(int k = 0; k < move_list_core.GetLength(0); k++) {
                    int diffX = move_list_core[k, 0];
                    int diffY = move_list_core[k, 1];
                    if(i == 1) {
                        diffX *= -1; 
                    }
                    if(j == 1) {
                        diffY *= -1; 
                    }
                    move_list.Add(new int[]{diffX, diffY}); 
                }
            }
        }
        piece.GetComponent<ChessPiece>().createDots(move_list, card); 
    }
}
