using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Domino
{
    partial class FormPlayerWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponentPlayerWin()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Text = "";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Aqua;
            
            var buttonOk = new Button()
            {
                Size = new Size(120, 40),
                Location = new Point(ClientSize.Width / 2 - 60, 13 * ClientSize.Height / 14 - 20),
                Text = "ОК",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.GhostWhite,
                FlatAppearance =
                {
                    BorderSize = 1,
                    MouseDownBackColor = Color.GhostWhite,
                    MouseOverBackColor = Color.GhostWhite,
                }
            };
            buttonOk.Click += (sender, args) => { Application.Exit(); };

            var pictureBoxGif = new PictureBox()
            {
                Size = new Size(ClientSize.Width, ClientSize.Height),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("images domino/win.gif"),
                BackColor = Color.Transparent
            };

            var labelWin = new Label
            {
                Size = new Size(400,82),
                Location = new Point (0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Text = "Вы выиграли!",
                Font = new Font("Comic Sans", 32),
                ForeColor = Color.Red
            };
            
            Controls.Add(buttonOk);
            Controls.Add(pictureBoxGif);
            Controls.Add(labelWin);
            
            labelWin.Parent = pictureBoxGif;

        }
    }
}