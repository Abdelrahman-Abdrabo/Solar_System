
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar_system
{
    enum Planets
    { Mercury, Venus, Earth, Mars, Jupiter, Saturn, Neptune, Uranus, Pluton }


    public struct Position
    {
        public float x;
        public float y;
        public float z;

        public Position(int x,int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class SolarSystem
    {
        Camara camara = new Camara();
        Star estrella = new Star();
        Sun sol = new Sun();
        List<Planet> planetas = new List<Planet>();
       

        public void CreateScene()
        {
            planetas.Add(new Planet(0.5f, Planets.Mercury, new Position(5, 0, 0), "mercurio.jpg",false, 1.0f));
            planetas.Add(new Planet(0.7f, Planets.Venus, new Position(11, 0, 0), "venus.jpg", false, 0.08f));
            planetas.Add(new Planet(1, Planets.Earth, new Position(15, 0, 0), "tierra.jpg", true, 0.07f));
            planetas.Add(new Planet(1, Planets.Mars, new Position(22, 0, 0), "marte.jpg", false, 0.06f));
            planetas.Add(new Planet(1.5f, Planets.Jupiter, new Position(28, 0, 0), "jupiter.jpg", false, 0.05f));
            planetas.Add(new Planet(1.2f, Planets.Saturn, new Position(35, 0, 0), "saturno.bmp", false, 0.04f));
            planetas.Add(new Planet(1.1f, Planets.Uranus, new Position(41, 0, 0), "urano.jpg", false, 0.03f));
            planetas.Add(new Planet(1.05f, Planets.Neptune, new Position(51, 0, 0), "neptune.jpg", false, 0.02f));
            planetas.Add(new Planet(0.9f, Planets.Pluton, new Position(60, 0, 0), "pluton.jpg", false, 0.01f));
            estrella.CreateStars(500);
            sol.Create();
            foreach (var item in planetas)
            {
                item.Create();  
            }
        }

        public Camara Camara
        {
            get { return camara; }
        }

        public void DrawScene()
        {  
            //draw the stars
            estrella.Draw();
            sol.Paint();
            foreach (var item in planetas)
            {
                item.Paint(); 
            }
        }
    }
}
