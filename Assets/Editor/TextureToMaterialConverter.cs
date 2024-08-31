using UnityEditor;
using UnityEngine;
using System.IO;

public class TextureToMaterial : MonoBehaviour
{
    [MenuItem("Tools/Convert Textures to Materials")]
    public static void ConvertTexturesToMaterials()
    {
        string textureFolderPath = "Assets/Textures/Living Room/textures";
        string materialFolderPath = "Assets/Materials/Living Room/living_1";

        if (!Directory.Exists(textureFolderPath))
        {
            Debug.LogError("Texture folder path does not exist: " + textureFolderPath);
            return;
        }

        if (!Directory.Exists(materialFolderPath))
        {
            Directory.CreateDirectory(materialFolderPath);
        }

        string[] textureFiles = Directory.GetFiles(textureFolderPath, "*.*", SearchOption.AllDirectories);
        foreach (string textureFile in textureFiles)
        {
            if (textureFile.EndsWith(".jpg") || textureFile.EndsWith(".jpeg") || textureFile.EndsWith(".png"))
            {
                string relativeTexturePath = textureFile.Replace(Application.dataPath, "Assets");
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(relativeTexturePath);

                if (texture != null)
                {
                    Material material = new Material(Shader.Find("Standard"));
                    material.mainTexture = texture;

                    string materialPath = Path.Combine(materialFolderPath, texture.name + ".mat");
                    materialPath = materialPath.Replace("\\", "/");

                    AssetDatabase.CreateAsset(material, materialPath);
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Conversion complete. Materials saved to: " + materialFolderPath);
    }
}
