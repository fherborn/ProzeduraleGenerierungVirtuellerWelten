using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVZ : MonoBehaviour {


	public static Texture2D Visualize(Texture2D oldtexture ,HouseConfig houseConfig, FloorPlan floorData, UnitType unitTypeToPaint, Color color){
		Texture2D texture;
		int width = floorData.area.GetLength (0) * (int)houseConfig.blockWidth;
		int length = floorData.area.GetLength (1) * (int)houseConfig.blockLength;

		if (oldtexture==null) {
			texture = new Texture2D (width, length);
		} else {
			texture = oldtexture;
		}

		for (int x = 0; x < texture.width; x++) {
			for (int z = 0; z < texture.height; z++) {
				if (floorData.area [(int)(x / houseConfig.blockWidth), (int)(z /houseConfig.blockLength)] == unitTypeToPaint) {
					//Paint in Color
					texture.SetPixel(x,texture.height-z, color);
				}
			}
		}

		texture.Apply ();
		return texture;
	}

}
