using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLife
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        GameEngine gameEngine;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = gameEngine.ValidateMousePosition(x, y);
                
                if (validationPassed)
                    gameEngine.ValidationPassed(x, y, true);
                UpdateField();
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var validationPassed = gameEngine.ValidateMousePosition(x, y);
                if (validationPassed)
                    gameEngine.ValidationPassed(x, y, false);
                UpdateField();
            }
        }
        private void FillCell(int x, int y)
        {
            Rectangle rectangle = new Rectangle(x * resolution, y * resolution, resolution, resolution);
            graphics.FillRectangle(Brushes.Crimson, rectangle);
            
        }

        private void DrawBoard(int x, int y)
        {
            Rectangle rectangle = new Rectangle(x * resolution, y * resolution, resolution, resolution);
            ControlPaint.DrawBorder(graphics, rectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }
        private void UpdateField()
        {
            graphics.Clear(Color.Black);
            for(int x = 0; x < gameEngine.cols; x++)
            {
                for (int y = 0; y < gameEngine.rows; y++)
                {
                    if(gameEngine.field[x,y])
                    {
                        FillCell(x, y);
                        if (radioButton2.Checked)
                            DrawBoard(x, y);
                    }
                    if (radioButton1.Checked)
                        DrawBoard(x, y);
                }
            }
            pictureBox1.Refresh();
        }
        private void StartGame()
        {
            if(gameEngine == null)
            {
                resolution = (int)numResolution.Value;
                int rows = pictureBox1.Height / resolution;
                int cols = pictureBox1.Width / resolution;
                gameEngine = new GameEngine(rows, cols, (int)numDestiny.Value);
            }
            if(graphics == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                graphics = Graphics.FromImage(pictureBox1.Image);
            }

            resolution = (int)numResolution.Value; 

            timer1.Start();
        }
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();

            numResolution.Enabled = true;
            numDestiny.Enabled = true;
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gameEngine.NextGeneration();
            
            Text = $"Generation {++gameEngine.currentGeneration}";
            UpdateField();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
