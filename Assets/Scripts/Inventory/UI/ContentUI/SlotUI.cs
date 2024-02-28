using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    private int nb;
    private int id;

    private String description;

    public SlotUI(int id, int nb)
    {
        this.id = id;
        this.nb = nb;
        this.description = "";
    }

    public SlotUI(int id, int nb, String description)
    {
        this.id = id;
        this.nb = nb;
        this.description = description;
    }

    public int getNb()
    {
        return nb;
    }

    public int getId()
    {
        return id;
    }

    public void setNb(int nb)
    {
        this.nb = nb;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public String getDescription()
    {
        return description;
    }

    public void setDescription(String description)
    {
        this.description = description;
    }
}