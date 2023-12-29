
using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using System.Drawing;

namespace Solar_system
{
    public class Camara
    {
        #region Camera constants
        
        const double div1 = Math.PI / 180;
        const double div2 = 180 / Math.PI;
        
        #endregion 
        
        #region Private atributes

        static float eyex, eyey, eyez;
        static float centerx, centery, centerz;
        static float forwardSpeed = 0.3f;
        // Yaw: Rotation around Y axis (left/right)
        // Pitch: Rotation around X axis (up/down)
        static float yaw, pitch;
        static float rotationSpeed = 0.25f;
        static double i, j, k;

        #endregion

        public static float Pitch
        {
            get { return Camara.pitch; }
            set { Camara.pitch = value; }
        }

        public static float Yaw
        {
            get { return Camara.yaw; }
            set { Camara.yaw = value; }
        }

        public void InitCamara()
        {
            // camera locition 
            eyex = 0f;
            eyey = 2f;
            eyez = 30f;
            // camera look at 
            centerx = 0;
            centery = 2;
            centerz = 0; 
            Look();
        }

        public void Look()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(eyex, eyey, eyez, centerx, centery, centerz, 0, 1, 0);
        }

        static public float AnguloARadian(double pAngle)
        {
            return (float)(pAngle * div1);
        }

        static public float RadianAAngulo(double pAngle)
        {
            return (float)(pAngle * div2);
        }

        public void UpdateDirVector()
        {
            i = -Math.Sin(AnguloARadian((double)yaw)); //x axis
            j = Math.Sin(AnguloARadian((double)pitch)); //y axis
            k = Math.Cos(AnguloARadian((double)yaw)); //z axis
                 
            centerz = eyez - (float)k; // calculate where the camera is looking
            centerx = eyex - (float)i;
            centery = eyey - (float)j;
        }

        public static void CenterMouse()
        {
            Winapi.SetCursorPos(MainForm.FormPos.X + 512, MainForm.FormPos.Y + 384);   
        }

        public void Update(int pressedButton)
        {
            #region Aim camera

                Pointer position = new Pointer();
                Winapi.GetCursorPos(ref position);   

                int difX = MainForm.FormPos.X + 512 - position.x;
                int difY = MainForm.FormPos.Y + 384 - position.y;

                if (position.y < 384)
                {
                    pitch -= rotationSpeed * difY;
                }
                else
                    if (position.y > 384)
                    {
                        pitch += rotationSpeed * -difY;
                    }
                if (position.x < 512)
                {
                    yaw += rotationSpeed * -difX;
                }
                else
                    if (position.x > 512)
                    {
                        yaw -= rotationSpeed * difX;
                    }
                UpdateDirVector();
                CenterMouse();

                // pressedButton takes value (-1, 0 , 1)
                // 0 no button pressed, 1 left click and -1 right click 
                eyex -= (float)i * forwardSpeed * pressedButton;
                eyey -= (float)j * forwardSpeed * pressedButton;
                eyez -= (float)k * forwardSpeed * pressedButton;      

            #endregion

            Look();  
        }
    }
}
