using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gamepoint
{
    public partial class Form1 : Form
    {
        private int speed = 5;
        private string direction = "Down";
        private bool lose = false;
        private List<PictureBox> pictureBoxList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            CreatePictureBox();
            KeyPreview = true;
        }

        private void CreatePictureBox()
        {
            for (int i = 0; i < 33; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(10 + i * 25, 10); // Устанавливаем координаты расположения
                pictureBox.Size = new Size(20, 20); // Устанавливаем размер
                pictureBox.BackColor = Color.FromArgb(255, 128, 0);
                pictureBoxList.Add(pictureBox);
                Controls.Add(pictureBox); // Добавляем PictureBox на форму
            }
        }

        private void BoolBoundPictureBoxwithPlayer()
        {
            for (int i = 0; i < pictureBoxList.Count; i++)
            {
                if (pictureBoxList[i].Bounds.IntersectsWith(boxPlayer.Bounds))
                {
                    pictureBoxList[i].Dispose();
                    if (direction == "UpLeft")
                    {
                        direction = "DownLeft";
                    } else if (direction == "UpRight")
                    {
                        direction = "DownRight";
                    }
                }
            }
        }

        private void MoveDirection()
        {
            switch (direction) 
            {
                case "UpLeft":
                    boxPlayer.Left -= speed;
                    boxPlayer.Top -= speed;
                    break;
                case "UpRight":
                    boxPlayer.Left += speed;
                    boxPlayer.Top -= speed;
                    break;
                case "DownLeft":
                    boxPlayer.Left -= speed;
                    boxPlayer.Top += speed;
                    break;
                case "DownRight":
                    boxPlayer.Left += speed;
                    boxPlayer.Top += speed;
                    break;
                case "Down":
                    boxPlayer.Top += speed;
                    break;
            }
        }

        private void BoolBoundsBoxPlayer()
        {
            if ((boxPlayer.Bounds.Right >= 840) && direction == "UpRight") 
            {
                direction = "UpLeft";
            }
            if ((boxPlayer.Bounds.Right >= 840) && direction == "DownRight")
            {
                direction = "DownLeft";
            }
            if ((boxPlayer.Bounds.Top <= 0) && direction == "UpRight")
            {
                direction = "DownRight";
            }
            if ((boxPlayer.Bounds.Top <= 0) && direction == "UpLeft")
            {
                direction = "DownLeft";
            }
            if ((boxPlayer.Bounds.Left <= 0) && direction == "UpLeft")
            {
                direction = "UpRight";
            }
            if ((boxPlayer.Bounds.Left <= 0) && direction == "DownLeft")
            {
                direction = "DownRight";
            }
            if (boxPlayer.Bounds.Bottom >= 650)
            {
                timer.Enabled = false;
                btn.Visible = true;
                lose = true;
            }
        }

        private void BoolBoundsBoxPlayerWithPlayer()
        {
            switch (boxPlayer.Bounds.IntersectsWith(player.Bounds))
            {
                case true:
                    if (direction == "Down")
                    {
                        Random rand = new Random();
                        int nav = rand.Next(-5, 6);
                        if (nav >= 0)
                        {
                            direction = "UpRight";
                        }
                        else if (nav < 0)
                        {
                            direction = "UpLeft";
                        }
                    }
                    if (direction == "DownLeft")
                    {
                        direction = "UpLeft";
                    }
                    else
                    {
                        direction = "UpRight";
                    }
                    break;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveDirection();
            BoolBoundsBoxPlayer();
            BoolBoundsBoxPlayerWithPlayer();
            BoolBoundPictureBoxwithPlayer();


        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose)
            {
                return;
            }
            int speed = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left >= 0)
            {
                player.Left -= speed;
            } else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Right <= 840) 
            {
                player.Left += speed;
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            direction = "Down";
            boxPlayer.Left = 388;
            boxPlayer.Top = 418;
            player.Left = 320;
            timer.Enabled = true;
            btn.Visible = false;
            lose = false;
        }



        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) { Close(); }
        }

        
    }
}
