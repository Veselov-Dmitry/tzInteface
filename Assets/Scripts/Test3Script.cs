using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Test3Script : MonoBehaviour {

    public Texture2D Test2_final;
    void Start ()
    {
        if (!Test2_final) { Debug.LogWarning("Main Camera=>Texture2D Test2_final is empty!"); return; }

        Color[] cols;
        int tWidth = Test2_final.width;
        int tHeight = Test2_final.height;

        cols = Test2_final.GetPixels(0,0, tWidth, tHeight);
        int len = cols.Length, ind;
        for (int i = 0; i < tWidth; i++)
        {

            for (int j = 0; j < tHeight; j++)
            {
                ind = i + j * tWidth;
                if (cols[ind].a >= 0.9f)//если не прозрачный
                {
                    
                    if (i != 0 || i != tWidth - 1||j != 0|| j != tHeight-1)//пропускаем если начало строки или конец или первый ряд или последний 
                    {
                        if(    cols[i + (j-1) * tWidth/*над текущим*/].a<0.9f
                            || cols[ind - 1/*перед текущим*/].a < 0.9f
                            || cols[ind + 1/*после текущего*/].a < 0.9f
                            || cols[i + (j + 1) * tWidth/*под текущим*/].a < 0.9f)//сверху снизу сзади или спереди прозрачный и сам текущий красный
                        {
                            cols[ind] = Color.blue;
                        }
                    }
                    else //если с любого края и сам текущий красный
                    {
                        cols[ind] = Color.blue;
                    }
                }
            }
        }

        Test2_final.wrapMode = TextureWrapMode.Clamp;
        Texture2D newText = new Texture2D(tWidth, tHeight, TextureFormat.RGBA32, false);
        newText.SetPixels(0, 0, tWidth, tHeight, cols);
        newText.alphaIsTransparency = true;
        newText.Apply();

        GameObject go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();

        Sprite sp = Sprite.Create(
            newText
            , new Rect(0, 0, newText.width, newText.height)
            , new Vector2(0.5f,0.5f)
            ,80
            );
        sr.sprite = sp;
        byte[] bytes = newText.EncodeToPNG();
        string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Test2_final)) + "/Test3_res.png";
        File.WriteAllBytes(path, bytes);
        print(string.Format("Result in \"{0}\"", path));
    }
}
