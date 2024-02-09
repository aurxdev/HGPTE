using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Case> cases; // Liste des cases de l'inventaire

    // Start is called before the first frame update
    void Start()
    {
        cases = new List<Case>(); // Initialisation de la liste des cases
    }

    // Update is called once per frame
    void Update()
    {
        // Code de mise à jour de l'inventaire
    }

    // Ajoute une case à l'inventaire
    public void AddCase(int x, int y)
    {
        Case newCase = new Case(x, y);
        cases.Add(newCase);
    }

    // Change l'ID de l'objet d'une case spécifique
    public void ChangeCaseObject(int caseIndex, int id)
    {
        if (caseIndex >= 0 && caseIndex < cases.Count)
        {
            Case selectedCase = cases[caseIndex];
            selectedCase.ChangeIdObject(id);
        }
    }
}




public class Case
{
    private int idObject;
    private int coordX;
    private int coordY;

    public int CoordX { get { return coordX; } }
    public int CoordY { get { return coordY; } }
    public int IdObject { get { return idObject; } }

    public Case(int x, int y)
    {
        idObject = 0;
        coordX = x;
        coordY = y;
    }

    public void ChangeIdObject(int id)
    {
        idObject = id;
    }
}
