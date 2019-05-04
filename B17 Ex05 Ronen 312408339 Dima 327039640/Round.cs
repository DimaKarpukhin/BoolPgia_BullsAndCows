﻿namespace B17_Ex05_Ronen_312408339_Dima_327039640
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
   
    public class Round
    {
        private const int k_ButtonSize = 40;
        private const int k_GessLeight = 4;
        private FormRunGame m_FormGame;
        private FormPickAColor m_FormPickAColor;
        // $G$ CSS-999 (-3) This kind of field should be readonly.
        private Button[] m_GuessSeuquence = new Button[k_GessLeight];
        private Button m_ButtonArrow; 
        private Button[] m_ScoreSeuquence = new Button[k_GessLeight];

        public FormRunGame FormGame
        {
            get { return m_FormGame; }
            set { m_FormGame = value; }
        }

        public void InitialRound(int i_Round)
        {
            createGuessButtons(i_Round);
            m_ButtonArrow = new Button();
            m_FormGame.Controls.Add(m_ButtonArrow);
            m_ButtonArrow.Text = ">>";
            m_ButtonArrow.Location = new Point(180, (44 * i_Round) + 74);
            m_ButtonArrow.Size = new Size(k_ButtonSize, k_ButtonSize / 2);
            m_ButtonArrow.Click += new EventHandler(buttonArrow_Click);
            m_ButtonArrow.Enabled = false;
            createScoreButtons(i_Round);
        }

        public bool IsStopRound()
        {
            bool stop = true;
            int i = 0;
            while (stop && i < m_GuessSeuquence.Length)
            {
                stop = !m_GuessSeuquence[i++].Enabled;
            }

            return stop;
        }

        public void DisableRoundButtons()
        {
            foreach (Button button in m_GuessSeuquence)
            {
                button.Enabled = false;
            }

            m_ButtonArrow.Enabled = false;
        }

        private void createGuessButtons(int i_Round)
        {
            // $G$ CSS-999 (-4) You should have used constant
            for (int i = 0; i < 4; i++)
            {
                m_GuessSeuquence[i] = new Button();
                m_FormGame.Controls.Add(m_GuessSeuquence[i]);
                m_GuessSeuquence[i].Location = new Point((44 * i) + 4, (44 * i_Round) + 64);
                m_GuessSeuquence[i].Size = new Size(k_ButtonSize, k_ButtonSize);
                m_GuessSeuquence[i].UseVisualStyleBackColor = false;
                m_GuessSeuquence[i].Click += new EventHandler(buttonGuess_Click);
            }
        }

        private void buttonGuess_Click(object sender, EventArgs e)
        {
            m_FormPickAColor = new FormPickAColor();
            m_FormPickAColor.Sender = sender as Button;
            m_FormPickAColor.ShowDialog();
            m_ButtonArrow.Enabled = m_GuessSeuquence[0].UseVisualStyleBackColor && m_GuessSeuquence[1].UseVisualStyleBackColor && m_GuessSeuquence[2].UseVisualStyleBackColor && m_GuessSeuquence[3].UseVisualStyleBackColor;
        }

        private void createScoreButtons(int i_Round)
        {
            for (int i = 0; i < 2; i++)
            {
                m_ScoreSeuquence[2 * i] = new Button();
                m_FormGame.Controls.Add(m_ScoreSeuquence[2 * i]);
                m_ScoreSeuquence[2 * i].Location = new Point((i * 20) + 230, (44 * i_Round) + 65);
                m_ScoreSeuquence[2 * i].Size = new Size(16, 16);
                m_ScoreSeuquence[2 * i].Enabled = false;
                m_ScoreSeuquence[(2 * i) + 1] = new Button();
                m_FormGame.Controls.Add(m_ScoreSeuquence[2 * i + 1]);
                m_ScoreSeuquence[(2 * i) + 1].Location = new Point((i * 20) + 230, (44 * i_Round) + 85);
                m_ScoreSeuquence[(2 * i) + 1].Size = new Size(16, 16);
                m_ScoreSeuquence[(2 * i) + 1].Enabled = false;
            }
        }

        private void buttonArrow_Click(object sender, EventArgs e)
        {
            DisableRoundButtons();
            m_FormGame.CreateGuessFromString(buttonCollectionToString(m_GuessSeuquence));
            m_FormGame.BuildScore();
            setBackColorOfButtonsFromString(m_ScoreSeuquence, m_FormGame.ScoreStr);
            if (isGuessedCorrectly())
            {
                m_FormGame.DisableAllButtons();
            }

            if (m_FormGame.IsStopPlaying())
            {
                setBackColorOfButtonsFromString(m_FormGame.ButtonsInitialGuess, m_FormGame.InitialGuessStr);
            }
        }

        private bool isGuessedCorrectly()
        {
            return m_ScoreSeuquence[0].BackColor == Color.Black && m_ScoreSeuquence[1].BackColor == Color.Black && m_ScoreSeuquence[2].BackColor == Color.Black && m_ScoreSeuquence[3].BackColor == Color.Black;
        }

        private string buttonCollectionToString(Button[] i_ButtonCollection)
        {
            StringBuilder result = new StringBuilder(i_ButtonCollection.Length);

            foreach (Button button in i_ButtonCollection)
            {
                result.Append(m_FormGame.ButtonBackColorToString(button.BackColor));
            }

            return result.ToString();
        }

        private void setBackColorOfButtonsFromString(Button[] i_ButtonCollection, string i_String)
        {
            int i = 0;
            foreach (char ch in i_String)
            {    
                i_ButtonCollection[i].BackColor = m_FormGame.CharToButtonBackColor(ch);
                i_ButtonCollection[i].UseVisualStyleBackColor = true;
                i++;
            }
        }
    }
}