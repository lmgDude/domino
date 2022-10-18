using System;
using System.Drawing;
using System.Windows.Forms;

namespace Domino
{
    public partial class FormGame : Form
    {
        private int XLeft;
        private int XRight;
        private int YLeft;
        private int YRight;
        private int XLeftMax;
        private int YLeftMax;
        private int XRghtMax;
        private int YRightMax;
        
        public FormGame()
        {
            InitializeComponentGame();
            for (var i = 0; i < 3; i++)
               AddDominoesToHand(logic.playerDomino[i]);
            XRight = XLeft = 1000;
            YLeft = YRight = 160;
            XLeftMax = YLeftMax = 0;
            YRightMax = XRghtMax = 0;
        }
        
        public void AddDominoesToHand(Tuple<int, int> domino)
        {
            var index = logic.playerDomino.IndexOf(domino);
            placesDominoLeft[index].Image = Image.FromFile("images doubledomino/domino " + domino.Item1 + "." + domino.Item2 + ".1.jpg");
            placesDominoRight[index].Image = Image.FromFile("images doubledomino/domino " + domino.Item1 + "." + domino.Item2 + ".2.jpg");
            
        }

        public void RemoveDominoesToHand(Tuple<int, int> domino)
        {
            var index = logic.playerDomino.IndexOf(domino);
            placesDominoLeft[index].Image = placesDominoRight[index].Image = null;
        }

        public void AddDominoOnTableLeft(Tuple<int, int> domino)
        {
            Image domimoImage;
            
           if (domino.Item1 > domino.Item2)
           {
               domimoImage = Image.FromFile("images domino/domino " + domino.Item2 + "." + domino.Item1 + ".png");
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);
           }
           else domimoImage = Image.FromFile("images domino/domino " + domino.Item1 + "." + domino.Item2 + ".png");
           
           if (XLeft >= 280 && YLeft == 160 && domino.Item1 != domino.Item2) 
               domimoImage.RotateFlip(RotateFlipType.Rotate90FlipX);

           if (XLeft < 280 && YLeft >= 160 && YLeft < 580 && domino.Item1 == domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate90FlipX);
           
           if (XLeft < 280 && YLeft >= 160 && YLeft < 580 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            
           if (XLeft > 280 && YLeft <= 160 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);

           if (YLeft > 580 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate270FlipX);
           
            

            var dominoPicture = new PictureBox
            {
                Size = domimoImage.Size,
                Image = domimoImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            if (XLeft >= 280 && YLeft == 160)
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeft - domimoImage.Width, YLeft - 40);
                    YLeftMax = 120 + domimoImage.Height;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeft - domimoImage.Width, YLeft);
                    YLeftMax = 160 + domimoImage.Height;
                }
                XLeft -= domimoImage.Width;
            }

            else if (XLeft < 280 && YLeft is >= 160 and < 580)
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeft - 40, YLeftMax);
                    YLeftMax += domimoImage.Height;
                    XLeftMax = XLeft + 120;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeft, YLeftMax);
                    YLeftMax += domimoImage.Height;
                    XLeftMax = XLeft + 80;
                }
                YLeft += domimoImage.Height;
            }

            else
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeftMax, YLeft - 40);
                    XLeftMax += domimoImage.Width;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XLeftMax, YLeft);
                    XLeftMax += domimoImage.Width;
                }
                XLeft += domimoImage.Width;
            }
            Controls.Add(dominoPicture);
        }
        
        public void AddDominoOnTableRight(Tuple<int, int> domino)
        {
            Image domimoImage;
            
           if (domino.Item1 > domino.Item2)
           {
               domimoImage = Image.FromFile("images domino/domino " + domino.Item2 + "." + domino.Item1 + ".png");
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);
           }
           else domimoImage = Image.FromFile("images domino/domino " + domino.Item1 + "." + domino.Item2 + ".png");
           
           if (XRight <= 1640 && YRight == 160 && domino.Item1 != domino.Item2) 
               domimoImage.RotateFlip(RotateFlipType.Rotate90FlipX);

           if (XRight > 1640 && YRight >= 160 && YRight < 580 && domino.Item1 == domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate90FlipX);
           
           if (XRight > 1640 && YRight == 160 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            
           if (XRight > 1640 && YRight <= 160 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate180FlipX);

           if (YRight > 580 && domino.Item1 != domino.Item2)
               domimoImage.RotateFlip(RotateFlipType.Rotate270FlipX);
           
            

            var dominoPicture = new PictureBox
            {
                Size = domimoImage.Size,
                Image = domimoImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            if (XRight <= 1640 && YRight == 160)
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XRight - (domino.Item1 == 5 && domino.Item2 == 5 ? 80 : 0),
                        YRight - 40);
                    YRightMax = 120 + domimoImage.Height;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XRight, YRight);
                    YRightMax = 160 + domimoImage.Height;
                }
                XRight += domimoImage.Width;
                if (domino.Item1 == 5 && domino.Item2 == 5)
                {
                    XLeft -= 80;
                    XRight -= 80;
                }
            }

            else if (XRight > 1640 && YRight is >= 160 and < 580)
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XRight - 120, YRightMax);
                    YRightMax += domimoImage.Height;
                    XRghtMax = XRight - 40;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XRight - 80, YRightMax);
                    YRightMax += domimoImage.Height;
                    XRghtMax = XRight;
                }
                YRight += domimoImage.Height;
            }

            else
            {
                if (domino.Item1 == domino.Item2)
                {
                    dominoPicture.Location = new Point(XRghtMax - 160, YRight - 40);
                    XRghtMax -= domimoImage.Width;
                }
                else if (domino.Item1 != domino.Item2)
                {
                    dominoPicture.Location = new Point(XRghtMax - 240, YRight);
                    XRghtMax -= domimoImage.Width;
                }
                XRight -= domimoImage.Width;
            }
            Controls.Add(dominoPicture);
        }
    }
}