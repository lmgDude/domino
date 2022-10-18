using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Domino
{
    partial class FormGame
    {
        private Logic logic;
        private IContainer components = null;
        List<Button> placesDominoLeft = new List<Button>(7);
        List<Button> placesDominoRight = new List<Button>(7);
        private Button buttonMarket;
        private Label scoreYou;
        private Label scoreBot;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void MakeScoreLabel()
        {
            scoreYou = new Label()
            {
                Size = new Size(330,120),
                Location = new Point (ClientSize.Width / 2 - 330, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.Transparent,
                Text = "ВЫ: " +  0,
                Font = new Font("Times New Roman", 42),
                ForeColor = Color.GreenYellow
            };
            
            scoreBot = new Label()
            {
                Size = new Size(330,120),
                Location = new Point (ClientSize.Width / 2, 0),
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = System.Drawing.Color.Transparent,
                Text = 0 + " :БОТ",
                Font = new Font("Times New Roman", 42),
                ForeColor = Color.GreenYellow
            };
            
            Controls.Add(scoreYou);
            Controls.Add(scoreBot);
        }

        public void ChangeScore(int uScor, int botScore)
        {
            scoreYou.Text = "ВЫ: " + uScor;
            scoreBot.Text = botScore + " :БОТ";
        }

        private void MakeMarketButton()
        {
            buttonMarket = new Button
            {
                Size = new Size(150,150),
                Location = new Point (11 * ClientSize.Width / 13 - (ClientSize.Width / 13 - 80) / 2,
                    19 * ClientSize.Height / 24 + 5),
                BackgroundImage = Image.FromFile("images domino/bazar.png"),
                BackColor = System.Drawing.Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance =
                {
                    BorderSize = 0,
                    MouseDownBackColor = Color.Transparent,
                    MouseOverBackColor = Color.Transparent,
                }
            };

            buttonMarket.Click += (sender, args) =>
            {
                logic.TakeBazar(logic.playerDomino);
            };
            
            Controls.Add(buttonMarket);
        }

        private void MakeDominoButtons()
        {
            for (var i = 0; i <= 6; i++)
            {
                placesDominoLeft.Add(new Button
                    {
                        Size = new Size(41, 160),
                        Location = new Point((3 + i) * ClientSize.Width / 13 + (ClientSize.Width / 13 - 80) / 2,
                            19 * ClientSize.Height / 24),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        FlatAppearance =
                        {
                            BorderSize = 0,
                            CheckedBackColor = Color.Transparent,
                            MouseDownBackColor = Color.Transparent,
                            MouseOverBackColor = Color.Transparent,
                        }
                    }
                );
                placesDominoRight.Add(new Button
                    {
                        Size = new Size(40, 160),
                        Location = new Point((3 + i) * ClientSize.Width / 13 + (ClientSize.Width / 13) / 2,
                            19 * ClientSize.Height / 24),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        FlatAppearance =
                        {
                            BorderSize = 0,
                            CheckedBackColor = Color.Transparent,
                            MouseDownBackColor = Color.Transparent,
                            MouseOverBackColor = Color.Transparent,
                        }
                    }
                );
            }

            foreach (var place in placesDominoLeft)
            {
                place.Click += (sender, args) =>
                {
                    
                    logic.MakeMoveLeft(placesDominoLeft.IndexOf(place));
                };
                Controls.Add(place);
            }
            
            foreach (var place in placesDominoRight)
            {
                place.Click += (sender, args) =>
                {
                    logic.MakeMoveRight(placesDominoRight.IndexOf(place));
                };
                Controls.Add(place);
            }

            MakeMarketButton();
        }

        private void MakeButtonStart()
        {
            var startButton = new Button()
            {
                Size = new Size(200, 100),
                Location = new Point(ClientSize.Width / 2 - 100, ClientSize.Height / 2 - 50),
                Text = "Начать игру",
                Font = new Font("Comic Sans", 19)
            };
            startButton.Click += (sender, args) =>
            {
                AddDominoesToHand(logic.playerDomino[0]);
                Controls.Remove(startButton);
                MakeButtonsEnabled();
            };
            Controls.Add(startButton);
        }

        
        public void MakeButtonsEnabled(bool active = true)
        {
            foreach (var place in placesDominoLeft)
                place.Enabled = active;
            foreach (var place in placesDominoRight)
                place.Enabled = active;
            buttonMarket.Enabled = active;
        }

        private void InitializeComponentGame()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Домино";
            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.Center;
            this.BackgroundImage = Image.FromFile("images domino/GameBackground.jpg");
            logic = new Logic(this);
            MakeButtonStart();
            MakeDominoButtons();
            MakeScoreLabel();
            MakeButtonsEnabled(false);
        }
    }
}