
using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using ShadowEngine;

namespace Solar_system
{
    class Planet
    {
        Planets planetName; 
        Position p;
        float anguloRotacion;
        float anguloOrbita;
        float radius;
        int list;
        static Random r = new Random();
        float OrpitalSpeed;
        string texture;
        Moon moon;
        bool hasMoon;
 
        public Planet(float radius, Planets planetName, Position posision, string texture,bool hasMoon, float OrpitalSpeed)
        {
            this.radius = radius;
            this.planetName = planetName;
            p = posision;
            anguloOrbita = r.Next(360);
            this.OrpitalSpeed = OrpitalSpeed;
            this.texture = texture;
            this.hasMoon = hasMoon;
            if (hasMoon)
            {
                moon = new Moon(0.5f, planetName, posision , "luna.jpg", (float)(OrpitalSpeed/5)); 
            }   
        }

        public void Create()
        {
            Glu.GLUquadric quadratic = Glu.gluNewQuadric();  //create the quadratic object 
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);
            list = Gl.glGenLists(1);  //create the list
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glPushMatrix();
            Gl.glRotated(270, 1, 0, 0);
            Glu.gluSphere(quadratic, radius, 100, 100); //create the sphere
            Gl.glPopMatrix();
            Gl.glEndList();
            if (hasMoon)
            {
                moon.Create();
            }
        }

        public void DrawOrbit()
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (int i = 0; i <= 360; i++)
            {
                Gl.glVertex3f(p.x * (float)Math.Sin(i * Math.PI / 180), 0, p.x * (float)Math.Cos(i * Math.PI / 180));
            }
            Gl.glEnd(); 
        }

        public void Paint()
        {
            if (MainForm.ShowOrbit)
            {
                DrawOrbit();
            }
            if (hasMoon)
            {
                moon.Paint(p, anguloOrbita);  
            }
            // Enables the use of 2D textures in OpenGL
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName(texture));
            // Pushes the current modelview matrix onto a stack, preserving its state
            Gl.glPushMatrix();
            anguloOrbita += OrpitalSpeed;
            anguloRotacion += 0.6f;
            Gl.glRotatef(anguloOrbita, 0, 1, 0);
            Gl.glTranslatef(-p.x, -p.y, -p.z);

            Gl.glRotatef(anguloRotacion, 0, 1, 0);
           
            Gl.glCallList(list);
          
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        } 
    }
}
