using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.Instance.teamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        //Bu satır, menü ekranı başlatıldığında MainManager'da (varsa) kaydedilen rengi önceden seçecektir.
        ColorPicker.SelectColor(MainManager.Instance.teamColor);

    }


    public void StartGame()
    {
         SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        // Bu satır, uygulamadan çıkıldığında kullanıcının son seçtiği rengi kaydedecektir.
        MainManager.Instance.SaveColor(); 

        #if UNITY_EDITOR
        
            EditorApplication.ExitPlaymode();

        #else

            Application.Quit();

        #endif
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.teamColor);

    }
}   
