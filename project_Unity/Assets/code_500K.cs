using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class code_500K : MonoBehaviour
{
     public Material material;
	 private int  PARTICLES = 500000;
	 private Color color = Color.white;

	 private float deltaTime;
	 private float fps;
	 
	 Particle_[] entity;

	 void Start()
	 {
		Application.targetFrameRate = 60;
		entity = new Particle_[PARTICLES];

        for(int i=0; i<PARTICLES; i++)
        {    
            Particle_ part = new Particle_();

            part.x = Random.Range(0,960);
            part.y = Random.Range(0,480);
            part.w = 10;
            part.h = 10; 


            part.origin_x = part.x;
            part.origin_y = part.y;
            part.angle = Random.Range(0,360);

            entity[i] = part;
        }
	 }
	 
	 
	 void OnGUI ()
	 {     
		for(int i=0; i<PARTICLES; i++)
        {    
            entity[i].angle += 1;
            entity[i].x = (int)(entity[i].origin_x + Mathf.Cos(entity[i].angle* Mathf.PI / 180)*100);
            entity[i].y = (int)(entity[i].origin_y + Mathf.Sin(entity[i].angle* Mathf.PI / 180)*100);
        }

		GUI.color = Color.black;
		GUI.Label(new Rect(10, 10, 100, 20), Mathf.Ceil (fps).ToString ());	      
	 }

	 void Update () {
         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         fps = 1.0f / deltaTime;
     }

		 
	 void DrawRectangle (int x, int y, int w, int h, Color color)
	 {    
		 material.SetPass(0);
		 GL.Color(color);
		 GL.Begin (GL.QUADS);
		 GL.Vertex3 (x, y, 0);
		 GL.Vertex3 (x + w, y, 0);
		 GL.Vertex3 (x + w, y + h, 0);
		 GL.Vertex3 (x, y + h, 0);
		 GL.End ();
	 }
	
}
