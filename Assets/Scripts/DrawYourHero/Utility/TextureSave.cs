using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DrawYourHero.Utility
{
    public static class TextureSave
    {
        public static void AsPNG(Texture2D texture)
        {
            var fileName = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            
            // Ensure the output directory exists
            string folderPath = "Assets/Art/Output/";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Encode texture into PNG
            byte[] bytes = texture.EncodeToPNG();
            string fullPath = Path.Combine(folderPath, fileName + ".png");

            // Write the bytes to a file
            File.WriteAllBytes(fullPath, bytes);

            // Refresh the Unity Asset Database (only works in the editor)
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif

            Debug.Log("Saved texture as PNG: " + fullPath);
        }
    }
}