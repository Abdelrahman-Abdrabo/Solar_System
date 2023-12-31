
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ShadowEngine;
using ShadowEngine.OpenGL; 
using Tao.OpenGl; 

namespace Solar_system
{
    public partial class MainForm : Form
    {
        //viewport handle
        uint hdc;
        SolarSystem sistema = new SolarSystem();
        static bool showOrbit = true;
        static Vector2 formPos;
        int moviendo;

        public static Vector2 FormPos
        {
            get { return MainForm.formPos; }
            set { MainForm.formPos = value; }
        }

        public static bool ShowOrbit
        {
            get { return MainForm.showOrbit; }
            set { MainForm.showOrbit = value; }
        } 

        public MainForm()
        {
            InitializeComponent();
            hdc = (uint)pnlViewPort.Handle;
            string error = "";

            //Viewport initialization command
            OpenGLControl.OpenGLInit(ref hdc, pnlViewPort.Width, pnlViewPort.Height, ref error);

            if (error != "")
            {
                MessageBox.Show(error);   
            }

            //starts the position of the camera as well as defines the perspective angle, etc.
            sistema.Camara.InitCamara(); 
            
            //Enable the lights
            float[] materialAmbient = { 0.5F, 0.5F, 0.5F, 1.0F };
            float[] materialDiffuse = { 1f, 1f, 1f, 1.0f };
            float[] materialSpecular = { 1.0F, 1.0F, 1.0F, 1.0F };
            float[] ambientLightPosition = { 0F, 0F, 0F, 1.0F }; // position
            float[] lightAmbient = { 0.5F, 0.5F, 0.5F, 0.0F }; // light intensity
            
            Lighting.MaterialAmbient = materialAmbient;
            Lighting.MaterialDiffuse = materialDiffuse;
            Lighting.MaterialSpecular = materialSpecular;
            Lighting.AmbientLightPosition = ambientLightPosition;
            Lighting.LightAmbient = lightAmbient;

            Lighting.SetupLighting(); 
            

            //load textures
            ContentManager.SetTextureList("texturas\\");
            ContentManager.LoadTextures(); 
            sistema.CreateScene();
            Camara.CenterMouse(); 
            //Background color
            Gl.glClearColor(0, 0, 0, 1);   //red green blue alpha 
        }

        // Render frames and drawing 
        private void tmrPaint_Tick(object sender, EventArgs e)
        {
            
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            sistema.Camara.Update(moviendo);
            
            sistema.DrawScene();
            
            Winapi.SwapBuffers(hdc);
         
            Gl.glFlush(); 
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            formPos = new Vector2(this.Left, this.Top); 
        }

        private void pnlViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moviendo = 1;
            }
            else
            {
                moviendo = -1;
            }
           
        }

        private void pnlViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            moviendo = 0;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode  == Keys.Escape)
            {
                this.Close(); 
            }
            if (e.KeyCode == Keys.O)
            {
                showOrbit = !showOrbit; 
            }
        }
    }
}
