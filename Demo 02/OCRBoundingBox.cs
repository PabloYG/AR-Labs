using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public class OCRBoundingBox : MonoBehaviour {

	public GameObject ocrSpace1;
	public GameObject boundingBox;

	public RawImage rawImage;
	public Texture2D cajalStain;

	public Renderer m_Display;
	RectTransform rt;

	public bool grab = false;

	public Rect screenshotRect;

	public string data;

	Texture2D texture;

	// Use this for initialization
	void Start () {

		rt = boundingBox.GetComponent<RectTransform>();
		//texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

	}

	// Update is called once per frame
	void Update () {

		Rect visualRect = RendererBoundsInScreenSpace(ocrSpace1.GetComponentInChildren<Renderer>());

		rt.position = new Vector2(visualRect.xMin, visualRect.yMin);

		rt.sizeDelta = new Vector2( visualRect.width, visualRect.height );

		screenshotRect = visualRect;

	}

	/* 
	private void OnPostRender () {

		//texture = transform.GetComponent<CameraImageToMatExample>().outputTexture;

		texture.ReadPixels(rt.rect, 0, 0, false);
		texture.Apply();

		rawImage.texture = texture;
		m_Display.material.mainTexture = texture;

	}
	*/

	/*	
	public void LateUpdate() {
        StartCoroutine(RecordFrame());
    }

	IEnumerator RecordFrame()
    {

        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        // do something with texture
		//CropScreenshot(texture);
        // cleanup
        //UnityEngine.Object.Destroy(texture);
    }
	

	public void SaveScreenshot (Texture2D texture) {

		print (texture);

		texture.ReadPixels(screenshotRect, 0, 0, false);
		texture.Apply();

		byte[] bytes = texture.EncodeToJPG();

		string savedFile = Application.persistentDataPath + "SavedScreen.jpg";
			
		if (File.Exists (savedFile)){
			File.Delete(savedFile);
			System.IO.File.WriteAllBytes(Application.persistentDataPath + "SavedScreen.jpg", bytes);
		} else {
			System.IO.File.WriteAllBytes(Application.persistentDataPath + "SavedScreen.jpg", bytes);
		}

		rawImage.texture = texture;

	}
 	*/
	/*
	void OnPostRender () {

		if (screenshotRect != null) {
			tex.ReadPixels(screenshotRect, 0, 0);
			tex.Apply();

			rawImage.texture = tex;

			byte[] jpgBytes = tex.EncodeToJPG();

			string savedFile = Application.dataPath + "SavedScreen.jpg";
			
			if (File.Exists (savedFile)){
				File.Delete(savedFile);
				System.IO.File.WriteAllBytes(Application.dataPath + "SavedScreen.jpg", jpgBytes);
			} else {
				System.IO.File.WriteAllBytes(Application.dataPath + "SavedScreen.jpg", jpgBytes);
			}
		}

	}
 	

	public static Texture2D LoadPNG(string filePath) {
     
        Texture2D tex = null;
        byte[] fileData;
     
        if (File.Exists(filePath))     {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
     }
	 */

	static Vector3[] screenSpaceCorners;
	static Rect RendererBoundsInScreenSpace(Renderer r) {
		// This is the space occupied by the object's visuals
		// in WORLD space.
		Bounds bigBounds = r.bounds;

		if(screenSpaceCorners == null)
			screenSpaceCorners = new Vector3[8];

		Camera theCamera = Camera.main;

		// For each of the 8 corners of our renderer's world space bounding box,
		// convert those corners into screen space.
		
		screenSpaceCorners[0] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z ) );
		screenSpaceCorners[1] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z ) );
		screenSpaceCorners[2] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z ) );
		screenSpaceCorners[3] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z ) );
		screenSpaceCorners[4] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z ) );
		screenSpaceCorners[5] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z ) );
		screenSpaceCorners[6] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z ) );
		screenSpaceCorners[7] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z ) );
		
		/* 
		screenSpaceCorners[0] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[1] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[2] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[3] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[4] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[5] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[6] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		screenSpaceCorners[7] = theCamera.WorldToScreenPoint( new Vector3( bigBounds.size.x/2, bigBounds.size.y/2, bigBounds.size.z/2 ) );
		*/

		// Now find the min/max X & Y of these screen space corners.
		float min_x = screenSpaceCorners[0].x;
		float min_y = screenSpaceCorners[0].y;
		float max_x = screenSpaceCorners[0].x;
		float max_y = screenSpaceCorners[0].y;
		
		for (int i = 0; i < 7; i++) {
			if(screenSpaceCorners[i].x < min_x) {
				min_x = screenSpaceCorners[i].x;
			}
			if(screenSpaceCorners[i].y < min_y) {
				min_y = screenSpaceCorners[i].y;
			}
			if(screenSpaceCorners[i].x > max_x) {
				max_x = screenSpaceCorners[i].x;
			}
			if(screenSpaceCorners[i].y > max_y) {
				max_y = screenSpaceCorners[i].y;
			}
		}

		return Rect.MinMaxRect( min_x, min_y, max_x, max_y );

	}
}

