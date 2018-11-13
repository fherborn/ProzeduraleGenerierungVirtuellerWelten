using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tread  {

    public Vector3[] points;
    public HouseData houseData;

	public Vector3[] vertices = new Vector3[24];
	public int[] allTriangles = new int[36];
	public Vector2[] uvs = new Vector2[24];


	public Tread(HouseData houseData, Vector3 m, int i)
    {
        this.houseData = houseData;
		Create(m, i);
    }

    /**
     * Erstellung eienr Stufe
     * Vector3 m = mittelpunkt auf der Unterkante einer Stufe     
     * 
     * 
     *    /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/
     *   /________________/ |
     *   |                | / 
     *   |                |/ | 
     *  /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/  /
     * /________________/ |/
     * |                | / 
     * |                |/
     *  ‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾
     *  
     *  
     **/
	public void Create(Vector3 m, int treadCount)
    {
        points = CalculatePoints(new Vector3[8], m);
        
		//Berechnung der triangles, UVs, Vertices
		#region front
		vertices[0] = points[0];
		vertices[1] = points[2];
		vertices[2] = points[4];
		vertices[3] = points[5];

		allTriangles[0] = 0 +treadCount*24;   
		allTriangles[1] = 1 +treadCount*24;   
		allTriangles[2] = 2 +treadCount*24;

		allTriangles[3] = 2+treadCount*24;
		allTriangles[4] = 1+treadCount*24;
		allTriangles[5] = 3+treadCount*24;

		uvs[0] = new Vector2(0f,0f);
     	uvs[1] = new Vector2(0f,1f);
     	uvs[2] = new Vector2(1f,0f);
     	uvs[3] = new Vector2(1f,1f); 
		#endregion

		#region back
		vertices[4] = points[6];
		vertices[5] = points[7];
		vertices[6] = points[1];
		vertices[7] = points[3];

		allTriangles[6] = 4+treadCount*24;   
		allTriangles[7] = 5+treadCount*24;   
		allTriangles[8] = 6+treadCount*24;

		allTriangles[9] = 6+treadCount*24;
		allTriangles[10] = 5+treadCount*24;
		allTriangles[11] = 7+treadCount*24;

		uvs[4] = new Vector2(0f,0f);
		uvs[5] = new Vector2(0f,1f);
		uvs[6] = new Vector2(1f,0f);
		uvs[7] = new Vector2(1f,1f); 
		#endregion

		#region top
		vertices[8] = points[2];
		vertices[9] = points[3];
		vertices[10] = points[5];
		vertices[11] = points[7];

		allTriangles[12] = 8+treadCount*24;   
		allTriangles[13] = 9+treadCount*24;   
		allTriangles[14] = 10+treadCount*24;

		allTriangles[15] = 10+treadCount*24;
		allTriangles[16] = 9+treadCount*24;
		allTriangles[17] = 11+treadCount*24;

		uvs[8] = new Vector2(0f,0f);
		uvs[9] = new Vector2(0f,1f);
		uvs[10] = new Vector2(1f,0f);
		uvs[11] = new Vector2(1f,1f); 
		#endregion 

		#region bottom
		vertices[12] = points[4];
		vertices[13] = points[6];
		vertices[14] = points[0];
		vertices[15] = points[1];

		allTriangles[18] = 12+treadCount*24;   
		allTriangles[19] = 13+treadCount*24;   
		allTriangles[20] = 14+treadCount*24;

		allTriangles[21] = 14+treadCount*24;
		allTriangles[22] = 13+treadCount*24;
		allTriangles[23] = 15+treadCount*24;	

		uvs[12] = new Vector2(0f,0f);
		uvs[13] = new Vector2(0f,1f);
		uvs[14] = new Vector2(1f,0f);
		uvs[15] = new Vector2(1f,1f); 
		#endregion 

		#region left side
		vertices[16] = points[1];
		vertices[17] = points[3];
		vertices[18] = points[0];
		vertices[19] = points[2];

		allTriangles[24] = 16+treadCount*24;   
		allTriangles[25] = 17+treadCount*24;   
		allTriangles[26] = 18+treadCount*24;

		allTriangles[27] = 18+treadCount*24;
		allTriangles[28] = 17+treadCount*24;
		allTriangles[29] = 19+treadCount*24;	

		uvs[16] = new Vector2(0f,0f);
		uvs[17] = new Vector2(0f,1f);
		uvs[18] = new Vector2(1f,0f);
		uvs[19] = new Vector2(1f,1f); 
		#endregion 

		#region right side
		vertices[20] = points[4];
		vertices[21] = points[5];
		vertices[22] = points[6];
		vertices[23] = points[7];

		allTriangles[30] = 20+treadCount*24;   
		allTriangles[31] = 21+treadCount*24;   
		allTriangles[32] = 22+treadCount*24;

		allTriangles[33] = 22+treadCount*24;
		allTriangles[34] = 21+treadCount*24;
		allTriangles[35] = 23+treadCount*24 ;	

		uvs[20] = new Vector2(0f,0f);
		uvs[21] = new Vector2(0f,1f);
		uvs[22] = new Vector2(1f,0f);
		uvs[23] = new Vector2(1f,1f); 
		#endregion 

    }


    /**
     *Für ein Update der Punkte bei einer Höhenveränderung oder Configveränderung 
     * 
     * 
     *    /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/
     *   /________________/ |
     *   |                | / 
     *   |                |/ | 
     *  /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/  /
     * /________________/ |/
     * |                | / 
     * |                |/
     *  ‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾
     **/
    public void UpdatePoints(Vector3 m)
    {
        points = CalculatePoints(points, m);

    }

    /**
     * 
     * Berechnung der Punkte, die für die Stufe benätigt werden
     * Vector3[] points = Ausgangspunkt (Leeres Array oder schon bestehende Punkte)
     * Vector3 m = mittelpunkt auf der Unterkante einer Stufe     
     * 
     *     /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/
     *    /________________/ |
     *    |                | / 
     *    |p3            p7|/| 
     *   /‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾/  /
     *  /p2____________p5/ |/
     *  |  p1            | /p6
     *  |p0              |/p4
     *   ‾‾‾‾‾‾‾m‾‾‾‾‾‾‾‾
     **/
    public Vector3[] CalculatePoints(Vector3[] points, Vector3 m)
    {
        HouseConfig config = houseData.houseConfig;
        points[0] = m + Vector3.left * config.treadWidth / 2;
        points[1] = points[0] + Vector3.forward * config.treadLength;
        points[2] = points[0] + Vector3.up * config.treadHeight;
        points[3] = points[1] + Vector3.up * config.treadHeight;
        points[4] = m + Vector3.right * config.treadWidth / 2;
        points[5] = points[4] + Vector3.up * config.treadHeight;
        points[6] = points[4] + Vector3.forward * config.treadLength;
        points[7] = points[6] + Vector3.up * config.treadHeight;
        return points;
    }
		
}
