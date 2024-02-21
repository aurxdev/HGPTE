using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    private int nb;
    private int id;

    public SlotUI(int id, int nb)
    {
        this.id = id;
        this.nb = nb;
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
}