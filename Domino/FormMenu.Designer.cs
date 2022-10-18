using System;
using System.Drawing;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.Windows.Forms;

namespace Domino
{
    sealed partial class FormMenu
    {
        private Thread thread;
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponentMenu()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "Домино";
            this.BackgroundImage = Image.FromFile("images domino/MenuBackground.jpg");
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            var label = new Label
            {
                Size = new Size(500,100),
                Location = new Point (ClientSize.Width / 2 - 250,  ClientSize.Height / 5 - 50),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.Transparent,
                Text = "Домино",
                Font = new Font("Times New Roman", 40),
                ForeColor = Color.GreenYellow
            };
            
            var buttonStart = new Button
            {
                Size = new Size(200,50),
                Location = new Point (ClientSize.Width / 2 - 100, 2 * ClientSize.Height / 5 - 25),
                Text = "Играть",
                Font = new Font("Arial", 20),
            };
            var buttonRules = new Button
            {
                Size = new Size(200,50),
                Location = new Point (ClientSize.Width / 2 - 100, 3 * ClientSize.Height / 5  - 25),
                Text = "Правила",
                Font = new Font("Arial", 17),
            };  
            var buttonExit = new Button
            {
                Size = new Size(200,50),
                Location = new Point (ClientSize.Width / 2 - 100, 4 * ClientSize.Height / 5  - 25),
                Text = "Выход",
                Font = new Font("Arial", 17),
            };  
            Controls.Add(buttonStart);
            Controls.Add(buttonRules);
            Controls.Add(buttonExit);
            Controls.Add(label);
            
            buttonStart.Click += (sender, args) =>
            {
                this.Close();
                thread = new Thread(Open);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            };
            buttonRules.Click += (sender, args) =>
            {
               new FormRules().Show();
            };
            buttonExit.Click += (sender, args) => Application.Exit();

            void Open(object obj)
            {
                Application.Run(new FormGame());
            }
        }
    }
}