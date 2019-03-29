using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class ImageLoading : MonoBehaviour {

	public static ImageLoading instance;

	public string path = "";
	public string filename;

	public Image image;

	public string data;
	public float height;
	public float width;

	private void Awake() {

		if (instance) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
		
	}

	// Use this for initialization
	void Start () {

		image = GameObject.Find("Client Logo").gameObject.GetComponent<Image>();
		image.color = new Color32 (255, 255, 255, 0);
		LoadImageData();

	}

	void Update () {
		if (image.sprite == null){
			image.color = new Color32 (255, 255, 255, 0);
		} else {
			image.color = new Color32 (255, 255, 255, 175);
		}
	}
	
// ------------------------------------------------------ //

	public void SaveImage() {

		if (image != null){
			
			Sprite sprite = image.sprite;
			Texture2D textureToSave = sprite.texture;
			byte[] bytes = textureToSave.EncodeToPNG();
			data = Convert.ToBase64String(bytes);
			//height = sprite.textureRect.height;
			//width = sprite.textureRect.width;


			print ("SAVE IMAGE!");

			SaveImageData();

			/* 
			filename = fileName (Convert.ToInt32(textureToSave.width), Convert.ToInt32(textureToSave.height));
			//print(filename);
			//path = Application.persistentDataPath + "/UserAssets/" + filename;
			path = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
			//CreateFolder();
			System.IO.File.WriteAllBytes(path,bytes);
			//print(path);
			//print(bytes);
			*/
		}

	}

	public void SaveImageData(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveFile.dat");
		SaveFile savedData = new SaveFile ();
		
		savedData.data = data;
		//savedData.height = height;
		//savedData.width = width;

		bf.Serialize(file,savedData);
		file.Close();
		
		print ("SAVE DATA!");

	}

	public void LoadImageData () {

		if (File.Exists(Application.persistentDataPath + "/saveFile.dat"))
		{

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath+"/saveFile.dat", FileMode.Open);
			SaveFile savedData = (SaveFile)bf.Deserialize(file);
			file.Close();

			data = savedData.data;

			print("LOAD DATA!");

			LoadImage ();

		} else {
			print ("There is no Save File.");
		}
	}
	
	public void LoadImage () { 
		
		byte[] bytes = Convert.FromBase64String(data);
    	//byte[] bytes = System.IO.File.ReadAllBytes(path);
		Texture2D textureToLoad = new Texture2D(4, 4);
		textureToLoad.LoadImage(bytes);

		Sprite sprite = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f,0.5f));
		image.sprite = sprite;
		image.color = new Color32 (255, 255, 255, 175);

		SaveImage();

		print ("LOAD IMAGE!");

	}

	public void ClearImage() {
		image.sprite = null;
	}


// ------------------------------------------------------ //

	public void CreateFolder(){

		if(!Directory.Exists(path))
 		{    

    		 Directory.CreateDirectory(path);
 
 		}
	}

		string fileName(int width, int height){
    	return string.Format("client_logo.png",width, height);
  	}

}

// ------------------------------------------------------- //


[System.Serializable]
public class SaveFile
{
	public string data;
	//public float height;
	//public float width;
}


//Add to more fields to SaveFile: Height and Width
//Save them together, then Load them in different variables
//Apply Height and Width to Sprite's dimensions