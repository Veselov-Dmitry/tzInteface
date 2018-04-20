using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Test2Script : MonoBehaviour {

    public Texture2D Test2;
    void Start () {
        if (!Test2) { Debug.LogWarning("Main Camera=>Texture2D Test2 is empty!"); return; }
        
        Color[] cols;
        int tWidth = Test2.width;
        int tHeight = Test2.height;

        cols = Test2.GetPixels(0,0, tWidth, tHeight);
        int len = cols.Length;
        for (int k = 0; k < len; k++)
        {
            if (cols[k].a >= 0.9f)
            {
                cols[k] = (cols[k].r >= 0.9f) ? Color.red : Color.white;
            }
        }
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
        string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Test2)) + "/Test2_final.png";
        File.WriteAllBytes(path, bytes);
        print(string.Format("Result in \"{0}\"", path));
    }
}
