using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    //managers
    public GameManager gameManager;
    public PieceFactory pieceFactory;
    public DestGridManager destGridManager;
    public CaptureManager captureManager;
    public UINariManager uiNariManager;
    public UIBoxManager uiBoxManager;
    public UISettleManager uiSettleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide_AllUI();

        uiBoxManager.Show_BoxUI();
        Switching_Box();
    }


    public void Hide_AllUI()
    {
        uiNariManager.Hide_NariUI();
        uiSettleManager.Hide_SettledUI();
    }


    public void Settled(bool is1PTurn)
    {
        Hide_AllUI();
        uiSettleManager.Show_SettledUI(is1PTurn);
    }

    public void Switching_Box()
    {
        uiBoxManager.Switching_BoxButton();
    }

    public void Hide_NariSelect()
    {
        uiNariManager.Hide_NariUI();
    }

    public void Show_NariSelect()
    {
        Hide_AllUI();
        uiNariManager.Show_NariUI();
    }
    public void Show_Box()
    {
        uiBoxManager.Show_BoxUI();
    }
}