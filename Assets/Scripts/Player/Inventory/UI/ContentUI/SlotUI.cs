using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    private int nb;
    private int id;

    private String description;

    // constructeur
    public SlotUI(int id, int nb)
    {
        this.id = id;
        this.nb = nb;
        this.description = "";
    } // SlotUI(int, int)

    // constructeur
    public SlotUI(int id, int nb, String description)
    {
        this.id = id;
        this.nb = nb;
        this.description = description;
    } // SlotUI(int, int, String)

    // ------ GETTERS ------
    public int getNb()
    {
        return nb;
    }

    public int getId()
    {
        return id;
    }

    public String getDescription()
    {
        return description;
    }
    
    // ------ SETTERS ------
    public void setNb(int nb)
    {
        this.nb = nb;
    }

    public void setId(int id)
    {
        this.id = id;
    }
    public void setDescription(String description)
    {
        this.description = description;
    }
}