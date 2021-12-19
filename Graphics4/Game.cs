using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics4
{
    public class Game
    {
        GameWindow window;
        int texture;
        
        double theta = 0.0;
        public Game (GameWindow window)
        {
            this.window = window;
           
            Start();
        }
        void Start()
        {
            window.Load += loaded;
            window.RenderFrame += renderF;
            window.Resize += resize;
            window.Run(1.0 / 60.0);

        }
        void resize(object ob, EventArgs e)
        {
           
            GL.Viewport(0,0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(1.0f, window.Width/window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }
       
        void renderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();

            GL.Translate(0.0, 0.0, -50.0);
            GL.Rotate(theta, 1.0, 0.0, 0.0);
            GL.Rotate(theta, 1.0, 0.0, 1.0);

            GL.Scale(0.7, 0.7, 0.7);
            draw_cube();

            GL.PopMatrix();

            window.SwapBuffers();

            theta += 1.0;
            if (theta > 360)
                theta -= 360;
        }
        void loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 0.2f);
            GL.Enable(EnableCap.DepthTest);

            float[] light_position = { 20, 20, 80 };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f };
            float[] light_ambient = { 0.5f, 0.5f, 0.5f };

            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0,LightParameter.Ambient, light_ambient);
            GL.Enable(EnableCap.Light0);


            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            System.Drawing.Imaging.BitmapData texData = loadTexture(@"C:\Users\moroz\source\repos\Graphics4\Graphics4\WoodTexture.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, texData.Width,texData.Height,0,PixelFormat.Bgr,
                PixelType.UnsignedByte,texData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        void draw_cube()
        {
            GL.Begin(BeginMode.Quads);
            GL.Color3(1.0, 1.0, 1.0);
            //front
            GL.Normal3(0.0, 0.0, 1.0);

            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, 10.0, 10.0);
            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, 10.0, -10.0);
            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10.0, 10.0, 10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, 10.0, 10.0);
            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, -10.0, 10.0);
            //right
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10.0, -10.0, 10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10.0, -10.0, -10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10.0, 10.0, -10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(10.0, 10.0, 10.0);

            //left
            GL.Normal3(-1.0, 0.0, -1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10.0, -10.0, 10.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10.0, -10.0, -10.0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-10.0, 10.0, -10.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10.0, 10.0, 10.0);
            GL.End();
        }

        System.Drawing.Imaging.BitmapData loadTexture(string filePath)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filePath);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0,0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, 
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmp.UnlockBits(bmpData);
            return bmpData;
        }
    }
}
