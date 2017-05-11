using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;                     // for Manny's GizmoGL
public class PiecePicker : MonoBehaviour {
    public float pieceHeight = 5f;
    public float rayDistance = 1000f;
    public LayerMask selectionIgnoreLayer;
    private Piece selectedPiece;
    private Checkerboard board;
	// Use this for initialization
	void Start () {
        board = FindObjectOfType<Checkerboard>();
        if (board == null) Debug.LogError("no board");
	}
	void CheckSelection()
    {
        //perform a ray cast from camera mouse position to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.color = Color.red;
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance, 0.1f, 0.3f);
        RaycastHit hit;
        //check mouse buttton
        if (Input.GetMouseButtonDown (0))
        {
            //cast ray to detect piece
            if(Physics.Raycast (ray, out hit, rayDistance ))
            {
                //Set the selected piece to be hit object
                selectedPiece = hit.collider.GetComponent<Piece>();
                if (selectedPiece == null) Debug.Log("Cannot pick up object:" + hit.collider.name);
            }
        }

    }
    void MoveSelection()
    {
        //chec if piece selected
        if (selectedPiece != null)
        {
            //make ray from camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance, 0.1f, 0.3f);

            RaycastHit hit;

            if (Physics .Raycast (ray,out hit,rayDistance, ~selectionIgnoreLayer ))    //tilde  bitwise
            {
                //Obtain the hit point
                GizmosGL.color = Color.blue;
                GizmosGL.AddSphere(hit.point, 0.5f);
                Vector3 piecePos = hit.point + Vector3.up * pieceHeight;
                selectedPiece.transform.position = piecePos;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        CheckSelection();
        MoveSelection();
       // print(Input.mousePosition);
	}
}
