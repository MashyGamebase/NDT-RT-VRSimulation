using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NDTSourceGetImage : Singleton<NDTSourceGetImage>
{
    [Header("Image Output Prefab")]
    [SerializeField] private GameObject xrayImageOutputPF;
    [SerializeField] private Transform spawnPosition;

    [Header("Radiation fill indicator")]
    [SerializeField] private Image radiationFill;

    [Header("Capture Properties")]
    public string FileName;
    public RenderTexture RT;
    public GameObject renderCamera;
    public GameObject subject;

    public XRGrabInteractable parentObject;
    public InputActionReference triggerAction;

    [SerializeField] private ItemObject cameraInstance;

    public bool isPowerConnected = false, isCameraConnected = false;

    const float crankTarget = 10f;
    public float crankPower = 0f;
    

    public MeshRenderer powerIndicator, cameraIndicator;

    private void Start()
    {
        powerIndicator.material.color = Color.red;
        cameraIndicator.material.color = Color.red;

        triggerAction.action.performed += OnTriggerActionPerformed;
    }

    private void OnDisable()
    {
        triggerAction.action.performed -= OnTriggerActionPerformed;
    }

    #region INPUT_CALLBACK
    private void OnTriggerActionPerformed(InputAction.CallbackContext context)
    {
        if (parentObject.isSelected)
        {
            if(isPowerConnected && isCameraConnected && crankPower >= crankTarget)
                GetSetImageButton();
        }

    }
    #endregion

    #region CONNECTION_CALLBACK
    public void TogglePower(bool toggle)
    {
        isPowerConnected = toggle;

        powerIndicator.material.color = toggle ? Color.green : Color.red;
    }

    public void ToggleCamera(bool toggle)
    {
        isCameraConnected = toggle;

        cameraIndicator.material.color = toggle ? Color.green : Color.red;
    }
    #endregion

    public void AddCrankPower(float power)
    {
        if (!isPowerConnected) return;

        if (crankPower < crankTarget)
        {
            crankPower += power;
            crankPower = Mathf.Clamp(crankPower, 0, crankTarget); // Ensure crankPower doesn't exceed crankTarget
            UpdateRadiationFill();
        }
    }

    void UpdateRadiationFill()
    {
        float fillAmount = crankPower / crankTarget;
        radiationFill.fillAmount = fillAmount; // Update the UI fill
    }

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

        GameObject obj = Instantiate(xrayImageOutputPF, spawnPosition.position, Quaternion.Euler(new Vector3(90f, 0, 0)));
        obj.GetComponent<NDTOutputHolder>().outputImage.gameObject.SetActive(true);
        obj.GetComponent<NDTOutputHolder>().outputImage.texture = texture2D;
        obj.GetComponent<NDTOutputHolder>().canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    
    IEnumerator RenderProcess()
    {
        renderCamera.SetActive(true);
        crankPower = 0;
        UpdateRadiationFill();
        yield return new WaitForSeconds(0.001f);
        GetImage();
        yield return new WaitForSeconds(0.1f);
        //--
        yield return new WaitForSeconds(0.2f);
        SetImage();
        yield return new WaitForSeconds(0.3f);
        renderCamera.SetActive(false);
    }

    [ContextMenu("GetSetImage")]
    public void GetSetImageButton()
    {
        StartCoroutine(RenderProcess());
    }
    
}