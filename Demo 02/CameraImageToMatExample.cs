using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Vuforia;
using Image = Vuforia.Image;
using OpenCVForUnity;

/// <summary>
/// Camera image to mat sample.
/// https://library.vuforia.com/content/vuforia-library/en/articles/Solution/Working-with-the-Camera.html#How-To-Access-the-Camera-Image-in-Unity
/// </summary>
public class CameraImageToMatExample : MonoBehaviour
{

    public static CameraImageToMatExample instance;

    private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.UNKNOWN_FORMAT;

    private bool mAccessCameraImage = true;
    private bool mFormatRegistered = false;

    public GameObject quad;
    public bool capturing = false;
    public Camera mainCamera;
    Mat inputMat;
    Texture2D outputTexture;

    UnityEngine.Rect relevantRect;

    private string imageText="";

	private void Awake() {

		instance = this;
		
	}

    void Start ()
    {

        #if UNITY_EDITOR
        mPixelFormat = Image.PIXEL_FORMAT.GRAYSCALE; // Need Grayscale for Editor
        #else
        mPixelFormat = Image.PIXEL_FORMAT.RGB888; // Use RGB888 for mobile
        #endif

        // Register Vuforia life-cycle callbacks:
        VuforiaARController.Instance.RegisterVuforiaStartedCallback (OnVuforiaStarted);
        VuforiaARController.Instance.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
        VuforiaARController.Instance.RegisterOnPauseCallback (OnPause);

    }
        
    void OnVuforiaStarted ()
    {

        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat (mPixelFormat, true)) {
            Debug.Log ("Successfully registered pixel format " + mPixelFormat.ToString ());

            mFormatRegistered = true;
        } else {
            Debug.LogError (
                "\nFailed to register pixel format: " + mPixelFormat.ToString () +
                "\nThe format may be unsupported by your device." +
                "\nConsider using a different pixel format.\n");

            mFormatRegistered = false;
        }

    }
/*
    public IEnumerator CaptureToTexture () {

        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture

        outputTexture = texture;
        quad.GetComponent<Renderer> ().material.mainTexture = outputTexture;
        StartCoroutine(getTextFromImage(EncodeImageBase64(outputTexture)));

        // cleanup
        Object.Destroy(texture);
    }
 */
    public IEnumerator CaptureTime () {

        capturing = true;
        
        yield return new WaitForSeconds(0.05f);

        if (capturing) {
            capturing = false;
        }
        
    }

    /// <summary>
    /// Called each time the Vuforia state is updated
    /// </summary>
    void OnTrackablesUpdated ()
    {

        if (capturing) {
            if (mFormatRegistered) {
                if (mAccessCameraImage) {
                    Vuforia.Image image = CameraDevice.Instance.GetCameraImage (mPixelFormat);

                    if (image != null) {
    //                    Debug.Log (
    //                        "\nImage Format: " + image.PixelFormat +
    //                        "\nImage Size:   " + image.Width + "x" + image.Height +
    //                        "\nBuffer Size:  " + image.BufferWidth + "x" + image.BufferHeight +
    //                        "\nImage Stride: " + image.Stride + "\n"
    //                    );

    //                    byte[] pixels = image.Pixels;
    //
    //                    if (pixels != null && pixels.Length > 0)
    //                    {
    //                        Debug.Log(
    //                            "\nImage pixels: " +
    //                            pixels[0] + ", " +
    //                            pixels[1] + ", " +
    //                            pixels[2] + ", ...\n"
    //                        );
    //                    }

                        
                        if (mPixelFormat == Image.PIXEL_FORMAT.GRAYSCALE) {
                            inputMat = new Mat (image.Height, image.Width, CvType.CV_8UC1);
                        } else if (mPixelFormat == Image.PIXEL_FORMAT.RGB888) {
                            inputMat = new Mat (image.Height, image.Width, CvType.CV_8UC3);
                        }
                        //Debug.Log ("inputMat dst ToString " + inputMat.ToString ());
                        
                        
                        
                        inputMat.put (0, 0, image.Pixels);

                        Imgproc.cvtColor(inputMat, inputMat, Imgproc.COLOR_BGR2GRAY);
                        Imgproc.threshold(inputMat, inputMat, 0, 255, Imgproc.THRESH_OTSU);
                        //Imgproc.equalizeHist (inputMat, inputMat);
                        
                        //Imgproc.putText (inputMat, "CameraImageToMatSample " + inputMat.cols () + "x" + inputMat.rows (), new Point (5, inputMat.rows () - 5), Core.FONT_HERSHEY_PLAIN, 1.0, new Scalar (255, 0, 0, 255));
                        

                        if (outputTexture == null) {
                            outputTexture = new Texture2D (inputMat.cols (), inputMat.rows (), TextureFormat.RGB24, false);
                        }


                        //outputTexture = new Texture2D (inputMat.cols (), inputMat.rows (), TextureFormat.RGBA32, false);
                                        
                        Utils.matToTexture2D (inputMat, outputTexture);

    //                  relevantRect = transform.GetComponent<OCRBoundingBox>().screenshotRect;

    //		            outputTexture.ReadPixels(relevantRect, 0, 0, false);
    //		            outputTexture.Apply();

    //                  int x = Mathf.FloorToInt(relevantRect.x);
    //                  int y = Mathf.FloorToInt(relevantRect.y);
    //                  int width = Mathf.FloorToInt(relevantRect.width);
    //                  int height = Mathf.FloorToInt(relevantRect.height);

    //                  outputTexture.Resize(Screen.width, Screen.height, TextureFormat.RGBA32, false);

                        
    //                  Color[] pix = outputTexture.GetPixels(x, y, width, height);

    //                  Texture2D displayTexture = new Texture2D(width, height);
    //                  displayTexture.SetPixels(pix);
    //                  displayTexture.Apply();

    //                  quad.transform.localScale = new Vector3 ((float)image.Width, (float)image.Height, 1.0f);
                        //quad.GetComponent<Renderer> ().material.mainTexture = outputTexture;
                        
                        mainCamera.orthographicSize = image.Height / 2;

                        TesseractDemoScript.instance.SendToTesseract(TesseractDemoScript.instance.rotateTexture(outputTexture, true));
                        //TesseractDemoScript.instance.SendToTesseract(outputTexture);
                        //StartCoroutine(getTextFromImage(EncodeImageBase64(outputTexture)));

                        capturing = false;
                        
                    }
                }
            }
        }
        
    }

    private string EncodeImageBase64(Texture2D outputTexture){

		string base64Img = "";
		base64Img = System.Convert.ToBase64String (outputTexture.EncodeToJPG(20));

	    return base64Img;
    }

    private string ocrPostURL = "http://api.ocr.space/parse/image";//URL of OCR API post call

	IEnumerator getTextFromImage(string _encodedImage) {

		WWWForm form = new WWWForm();//call form
		form.AddField( "apikey", "a1db1bf7cf88957" ); //OCR API Key goes here
		form.AddField( "language", "eng" );
		form.AddField( "isOverlayRequired", "true" );
		form.AddField( "base64Image", "data:image/png;base64,"+ _encodedImage );
		
    	WWW www = new WWW(ocrPostURL, form);
		yield return StartCoroutine(WaitForRequest(www));//launch async coroutine and wait until it ends

		imageText = www.text;//save response in global variable

        print (imageText);

    }

	IEnumerator WaitForRequest(WWW _www)
	{
		yield return _www;
		if (_www.error == null) {
			Debug.Log("WWW Ok!: " + _www.text);
		} else {
			Debug.Log("WWW Error: " + _www.error);
		}
    }



    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    void OnPause (bool paused)
    {
        if (paused) {
            Debug.Log ("App was paused");
            UnregisterFormat ();
        } else {
            Debug.Log ("App was resumed");
            RegisterFormat ();
        }
    }

    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    void RegisterFormat ()
    {
        if (CameraDevice.Instance.SetFrameFormat (mPixelFormat, true)) {
            Debug.Log ("Successfully registered camera pixel format " + mPixelFormat.ToString ());
            mFormatRegistered = true;
        } else {
            Debug.LogError ("Failed to register camera pixel format " + mPixelFormat.ToString ());
            mFormatRegistered = false;
        }
    }

    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    void UnregisterFormat ()
    {
        Debug.Log ("Unregistering camera pixel format " + mPixelFormat.ToString ());
        CameraDevice.Instance.SetFrameFormat (mPixelFormat, false);
        mFormatRegistered = false;
    }
        
}
