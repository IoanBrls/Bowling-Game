using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeOnClick : MonoBehaviour {

    public GameObject InGameMenu;

    public void ResumeGame()
    {
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
        Cursor.visible = false;
    }
}
