using System.ComponentModel;
using System;
using System.Drawing;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;


namespace Domino
{
    partial class FormRules
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
        
        private void InitializeComponentRules()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 740);
            this.Text = "Правила";
            this.BackColor = Color.Cornsilk;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            var rules = new Label
            {
                Size = ClientSize,
                Location = new Point(0,70),
                TextAlign = ContentAlignment.TopLeft,
                Text = "В начале игры у игрока 3 домино на руках, одна из которых 5|5. Первый ход остаётся за игроком, " +
                       "сходить он должен именно этой домино\n" +
                       "После первого хода игроку записывается 10 очков (Так как 10 / 5, почему так - расскажем ниже)\n" +
                       "Затем игрок может брать другие кости из \"Базара\"\n\n" +
                       "Всего на руках может быть не более 7 домино\n" + 
                       "Если Вы не можете никуда положить домино, то ваши ходы заканчиваются. Бот доиграет и вы увидите результат\n" +
                       "Если у Бота закончатся ходы, то вы продолжаете игру, " +
                       "пока не обыграете Бота или пока у вас не закончатся ходы\n\n" + 
                       "Ходить можно только в 2 стороны (Вправо и влево)\n" + 
                       "Что бы положить кость вправо на стол нужно нажать на правую часть домино, влево - на левую\n" + 
                       "Против Вас играет Бот, который, хоть и не имеет права первого хода, " +
                       "очень сильный противник!\n\n" +
                       "Цель игры: Набрать больше всего очков за игру\n" +
                       "Очки набираются путём подсчёта всех концов костей и чтобы оно было кратно 5\n" +
                       "(Например: Если сумма всех концов равна 14 игроку очки не засчитываются," +
                       " а если равна 5,10,15 и т.д очки идут игроку или Боту)\n" +
                       "                                                 Удачи в игре!",
                Font = new Font("Comic Sans", 12)
            };
            var rulesTitle = new Label
            {
                Size = new Size(650, 65),
                Location = new Point(0,0),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Правила",
                Font = new Font("Times New Roman", 33),
                ForeColor = Color.Black
            };
            
            Controls.Add(rules);
            Controls.Add(rulesTitle);
        }
        
    }
}