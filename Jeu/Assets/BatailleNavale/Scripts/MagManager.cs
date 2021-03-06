﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagManager
{
    private GameObject MagHolder;
    private CanvasGenerator Cvs;
    private int Magasinpos;//var qui dit si le magasin est ouvert ou fermé
    private Vector3 pos;
    private ShipManager SM;
    private VisualManager VM;
    private Camera cam;
    private int value;

    public MagManager(string nom, Vector3 posvm, ShipManager SMx, Camera camx)
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        value = 1;
        MagHolder = new GameObject(nom);
        pos = new Vector3(posvm.x, posvm.y, posvm.z);
        this.SM = SMx;
        this.cam = camx;
        Magasinpos = 1;//ouvre le magasin au début de la scène
        Cvs = new CanvasGenerator("CanvasMagasin", new Vector3(pos.x, pos.y, pos.z), new Vector2(1, 1), RenderMode.WorldSpace, cam, 10, "PanelLayer", MagHolder);
        Cvs.addPanel("PanelBateaux", new Vector3(pos.x + 12f, pos.y + 5f, pos.z + 0), new Vector2(7.25f, 12), new Color32(255, 255, 255, 160));
        Cvs.addPanel("PanelMag" + pos.x, new Vector3(pos.x + 8.875f, pos.y + 5, pos.z + 0), new Vector2(1, 12), new Color32(22, 25, 77, 175));
        Cvs.addText(Cvs.getPanel(1), "MagasinText", Cvs.getPanel(1).transform.position, new Vector2(12, 2), 1,0.5f, "Magasin", Color.black, TextAnchor.MiddleCenter);
        Cvs.rotateText(0);
        Cvs.addPanel("¨Panelplacement", new Vector3(pos.x - 3, pos.y + 10.1f, pos.z), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs.addText(Cvs.getPanel(2), "TextPlacement", Cvs.getPanel(2).transform.position, new Vector2(11, 4), 1, 0.4f, "Confirmer position\n(Press F1)", Color.black, TextAnchor.MiddleCenter);
        Cvs.addPanel("¨PanelInstrut", new Vector3(pos.x +12.43f, pos.y + 10.30f, pos.z), new Vector2(6.1f, 1), new Color32(192, 72, 73, 180));
        Cvs.addText(Cvs.getPanel(3), "TextInstru", Cvs.getPanel(3).transform.position, new Vector2(11, 4), 1, 0.4f, "Rotation\n(Press R)", Color.black, TextAnchor.MiddleCenter);
    }

    public void MoveMagasin()//fonction qui répond à l'action du bouton du panel2
    {
        if (Magasinpos == 0)//si les panneaux sont rangés, déplace les deux panneaux pour l'ouverture
        {
            this.setOuvrir();
            SM.moveShip(-4.5f, 0, 0);
        }
        else
        {
            if (Magasinpos == 1)//si les panneaux sont ouverts, déplace les deux panneaux pour la fermeture
            {
                this.setFermer();
                SM.moveShip(4.5f, 0, 0);
            }
        }
    }

    public void setFermer()//Fonction que ferme le magasin (decallage sur la droite)
    {
        Cvs.MovePanel(0, 6, 0, 0);
        Cvs.MovePanel(1, 6, 0, 0);
        Magasinpos = 0;
        SM.moveShip(4.5f, 0, 0);
    }

    public void setOuvrir()//Fonction qui ouvre le magasin (decallage sur la gauche)
    {
        Cvs.MovePanel(0, -6, 0, 0);
        Cvs.MovePanel(1, -6, 0, 0);
        Magasinpos = 1;
        SM.moveShip(-4.5f, 0, 0);
    }

    public int getMagasinpos()//Retourne un booleen si le magasin est ouvert (true) ou fermé (false)
    {
        return Magasinpos;
    }

    public bool FinPlacement()//(Confirme le placement des bateaux et ferme (et delete) définitivement le magasin
    {
        if (Cvs.getCanvas().GetComponent<Canvas>().enabled == true)
        {
            if (cam.enabled == false)
            {
                return false;
            }

            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                if (SM.getShip(i).GetComponent<Draggable>().getMag() == false)
                {
                    count++;
                }
            }
            if (count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    SM.getShip(i).GetComponent<BoxCollider>().enabled = false;
                }
                VM.switchPlayer();
                Cvs.disable();
                VM.EnableCvs(cam.gameObject.name[cam.gameObject.name.Length-1]-48);
                if ((cam.name[cam.name.Length - 1]-48) == 1)
                {
                    GameObject.Find("MarkerTab2").transform.position = new Vector3(27, 10.1f, 0);
                }
                else
                {
                    GameObject.Find("MarkerTab1").transform.position = new Vector3(-3, 10.1f, 0);             
                }
                value = 0;
                return true;
            }
        }
        return false;
    }
    public int getvalue()
    {
        return value;
    }
}