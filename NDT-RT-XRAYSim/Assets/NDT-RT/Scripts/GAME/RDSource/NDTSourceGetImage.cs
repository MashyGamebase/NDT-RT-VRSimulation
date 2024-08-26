using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NDTSourceGetImage : MonoBehaviour
{
    [Header("Capture Properties")]
    public string FileName;
    public RenderTexture RT;
    public GameObject renderCamera;
    public List<RawImage> rawImage;
    public GameObject subject;

    [SerializeField] private Interactable cameraInstance;

    #region INPUT_CALLBACK
    public void OnCapture(InputAction.CallbackContext context)
    {
        if (context.performed && cameraInstance.isInteracted)
        {
            GetSetImageButton();
        }
    }
    #endregion

    public void GetImage()
    {
        Texture2D texture2D = new Texture2D(RT.width, RT.height, TextureFormat.ARGB32, false);
        RenderTexture.active = RT;
        texture2D.ReadPixels(new Rect(0, 0, RT.width, RT.height), 0, 0);
        texture2D.Apply();
        // Get subject's screen position
        Vector3 subjectScreenPos = renderCamera.GetComponent<Camera>().WorldToScreenPoint(subject.transform.position);

        // Convert the image to black and white, invert colors, and make the subject completely black
        Color subjectColor = Color.black;
        for (int y = 0; y < texture2D.height; y++)
        {
            for (int x = 0; x < texture2D.width; x++)
            {
                Color color = texture2D.GetPixel(x, y);

                // Check if the current pixel corresponds to the subject's position
                if (IsSubjectPixel(x, y, subjectScreenPos))
                {
                    texture2D.SetPixel(x, y, subjectColor); // Make subject black
                }
                else
                {
                    // Convert to grayscale and invert
                    float gray = (color.r + color.g + color.b) / 3f;
                    gray = 1f - gray;
                    texture2D.SetPixel(x, y, new Color(gray, gray, gray, color.a));
                }
            }
        }

        string Path = Application.persistentDataPath + "/" + FileName + ".png";
        byte[] bytes = texture2D.EncodeToPNG();

        File.WriteAllBytes(Path, bytes);
    }

    private bool IsSubjectPixel(int x, int y, Vector3 subjectScreenPos)
    {
        // Convert the pixel coordinates to screen space
        float screenX = (x / (float)RT.width) * Screen.width;
        float screenY = (y / (float)RT.height) * Screen.height;

        // Define a tolerance to cover the subject's area in pixels (this depends on the size of the subject in the screen)
        float tolerance = 10f; // Adjust this value based on the size of your subject on screen

        // Check if the current pixel is within the tolerance range of the subject's screen position
        return (Mathf.Abs(screenX - subjectScreenPos.x) <= tolerance && Mathf.Abs(screenY - subjectScreenPos.y) <= tolerance);
    }

    public void SetImage()
    {
        Texture2D texture2D = new Texture2D(RT.width, RT.height);
        string path = Application.persistentDataPath + "/" + FileName + ".png";
        byte[] bytes = File.ReadAllBytes(path);

        texture2D.LoadImage(bytes);
        texture2D.Apply();
        foreach(var image in rawImage)
            image.texture = texture2D;
    }

    
    IEnumerator RenderProcess()
    {
        renderCamera.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        GetImage();
        yield return new WaitForSeconds(0.1f);
        foreach (var image in rawImage)
            image.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SetImage();
        yield return new WaitForSeconds(0.3f);
        renderCamera.SetActive(false);
    }

    public void GetSetImageButton()
    {
        StartCoroutine(RenderProcess());
    }
    
}