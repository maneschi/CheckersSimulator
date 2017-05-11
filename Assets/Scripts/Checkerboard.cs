using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkerboard : MonoBehaviour {
    public GameObject blackPiece, whitePiece;
    public int boardX = 8, boardZ = 8;
    public float pieceRadius = 0.5f;
    public Piece[,] pieces;
    private int halfBoardX, halfBoardZ;
    private float pieceDiameter;
    private Vector3 bottomLeft;

	// Use this for initialization
	void Start () {
        halfBoardX = boardX / 2;
        halfBoardZ = boardZ / 2;
        pieceDiameter = pieceRadius * 2f;
        bottomLeft = transform.position - Vector3.right * halfBoardX - Vector3.forward * halfBoardZ;
        CreateGrid();
	}

    void CreateGrid()
    {
        //init 2d array
        pieces = new Piece[boardX, boardZ];
        #region Generate White Pieces
        // loop thru
        for(int x=0; x < boardX; x += 2)
        {
            for (int z = 0; z < 3; z++)
            {
                bool evenRow = z % 2 == 0;               //modulus 2
                int gridX = evenRow ? x : x + 1;   //ternary
                int gridZ = z;

                //gen piece
                GeneratePiece(whitePiece, gridX, gridZ);
            }
        }

        #endregion
        #region Generate Black Pieces
        for (int x = 0; x < boardX; x += 2)
        {
            for (int z = boardZ-3; z < boardZ; z++)
            {
                bool evenRow = z % 2 == 0;               //modulus 2
                int gridX = evenRow ? x : x + 1;   //ternary
                int gridZ = z;

                //gen piece
                GeneratePiece(blackPiece, gridX, gridZ);
            }
        }
        #endregion
    }

    void GeneratePiece(GameObject piecePrefab, int x, int z)                             //GO tab > GameObject
    {
        GameObject clone = Instantiate(piecePrefab);
        clone.transform.SetParent(transform);
        Piece piece = clone.GetComponent<Piece>();
        PlacePiece(piece, x, z);

    }

    void PlacePiece(Piece piece,int x, int z)
    {
        //calc offset
        float xOffset = x * pieceDiameter + pieceRadius;
        float zOffset = z * pieceDiameter + pieceRadius;

        //Set pieces new grid coordinates
        piece.gridX = x;
        piece.gridZ = z;

        //Move piece to board coordinate
        piece.transform.position = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;

        //Set piece in array slot
        pieces[x, z] = piece;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
