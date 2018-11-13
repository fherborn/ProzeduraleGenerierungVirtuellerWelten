using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tread : MonoBehaviour {

    public Vector3[] points;
    public HouseData houseData;

    public Tread(HouseData houseData, Vector3 m)
    {
        this.houseData = houseData;
        Create(m);
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
    public void Create(Vector3 m)
    {
//        points = CalculatePoints(new Vector3[8], m);
        //Berechnung der triangles, UVs, Vertices

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
//        points = CalculatePoints(points, m);

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
//    public Vector3[] CalculatePoints(Vector3[] points, Vector3 m)
//    {
//        HouseConfig config = houseData.houseConfig;
//        points[0] = m + Vector3.left * config.treadWidth / 2;
//        points[1] = points[0] + Vector3.forward * config.treadLength;
//        points[2] = points[0] + Vector3.up * config.treadHeight;
//        points[3] = points[1] + Vector3.up * config.treadHeight;
//        points[4] = m + Vector3.right * config.treadWidth / 2;
//        points[5] = points[4] + Vector3.up * config.treadHeight;
//        points[6] = points[4] + Vector3.forward * config.treadLength;
//        points[7] = points[6] + Vector3.up * config.treadHeight;
//        return points;
//    }
}
