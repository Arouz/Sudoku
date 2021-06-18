using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.UI;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sudoku
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int[,] TableDesPossibles = new int[81, 12];

        // Variable Glo
        int[,] Groupe = new int[9, 9]{  { 0, 1, 2, 9, 10, 11, 18, 19, 20 },
                                        { 3, 4, 5, 12, 13, 14, 21, 22, 23 },
                                        { 6, 7, 8, 15, 16, 17, 24, 25, 26 },
                                        { 27, 28, 29, 36, 37, 38, 45, 46, 47 },
                                        { 30, 31, 32, 39, 40, 41, 48, 49, 50 },
                                        { 33, 34, 35, 42, 43, 44, 51, 52, 53 },
                                        { 54, 55, 56, 63, 64, 65, 72, 73, 74 },
                                        { 57, 58, 59, 66, 67, 68, 75, 76, 77 },
                                        { 60, 61, 62, 69, 70, 71, 78, 79, 80 } };

        int[,] Map = new int[9, 9] {    {0, 1, 2, 3, 4, 5, 6, 7, 8},
                                        {9, 10, 11, 12, 13, 14, 15, 16, 17},
                                        {18, 19, 20, 21, 22, 23, 24, 25, 26},
                                        {27, 28, 29, 30, 31, 32, 33, 34, 35},
                                        {36, 37, 38, 39, 40, 41, 42, 43, 44},
                                        {45, 46, 47, 48, 49, 50, 51, 52, 53},
                                        {54, 55, 56, 57, 58, 59, 60, 61, 62},
                                        {63, 64, 65, 66, 67, 68, 69, 70, 71},
                                        {72, 73, 74, 75, 76, 77, 78, 79, 80} };

        // Grille facile
        int[,] GF = new int[9, 9]{  {0, 5, 0, 7, 2, 4, 3, 0, 0},
                                    {3, 7, 0, 0, 0, 1, 0, 2, 4},
                                    {9, 0, 2, 0, 0, 0, 5, 0, 0},
                                    {7, 0, 4, 2, 3, 6, 0, 8, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 3, 0, 9, 4, 7, 1, 0, 2},
                                    {0, 0, 3, 0, 0, 0, 6, 0, 8},
                                    {1, 9, 0, 6, 0, 0, 0, 4, 3},
                                    {0, 0, 7, 4, 5, 3, 0, 1, 0} };

        // Grille moyenne
        int[,] GM = new int[9, 9]{  {1, 0, 0, 5, 7, 0, 3, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 5, 7, 0},
                                    {6, 0, 0, 0, 9, 0, 0, 0, 8},
                                    {0, 0, 0, 0, 0, 0, 0, 4, 1},
                                    {0, 0, 0, 6, 0, 3, 0, 0, 0},
                                    {7, 2, 8, 0, 0, 0, 0, 0, 0},
                                    {0, 9, 0, 2, 0, 6, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 1, 2, 0, 3},
                                    {3, 5, 2, 0, 0, 0, 9, 0, 0} };


        // Grille de difficulté maximale
        int[,] GD = new int[9, 9]{  {8, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 3, 6, 0, 0, 0, 0, 0},
                                    {0, 7, 0, 0, 9, 0, 2, 0, 0},
                                    {0, 5, 0, 0, 0, 7, 0, 0, 0},
                                    {0, 0, 0, 0, 4, 5, 7, 0, 0},
                                    {0, 0, 0, 1, 0, 0, 0, 3, 0},
                                    {0, 0, 1, 0, 0, 0, 0, 6, 8},
                                    {0, 0, 8, 5, 0, 0, 0, 1, 0},
                                    {0, 9, 0, 0, 0, 0, 4, 0, 0} };

        // Grille objectif
        int[,] GO = new int[9, 9]{  {0,0,0,0,0,0,3,0,0},
                                    {0,8,0,1,0,3,0,0,5},
                                    {1,0,3,0,5,0,0,7,0},
                                    {9,0,0,0,0,5,7,0,4},
                                    {3,0,0,0,4,0,0,0,8},
                                    {5,0,8,9,0,0,0,0,6},
                                    {0,3,0,0,9,0,4,0,2},
                                    {2,0,0,5,0,1,0,6,0},
                                    {0,0,5,0,0,0,0,0,0} };

        // Grille vierge
        int[,] GV = new int[9, 9]{  {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0} };


        int[,] GJ = new int[9, 9];


        public MainPage()
        {
            this.InitializeComponent();
            Afficher();
            AssignationPosition();
        }

        // Résout la grille avec la methode de backtracking
        private bool BackTracking(int Position, int[,] GoG)
        {
            double Pos = Position;

            // Si la position est egal à 81 alors le sudoku est résolu
            if (Position == 81)
                return true;
            int i = Convert.ToInt32(Math.Truncate(Pos / 9));
            int j = Position % 9;
            // Si la grille selectionnée n'est pas égal à 0 (déjà remplie) alors on passe à la suivante
            if (GoG[i,j] != 0)
                return BackTracking(Position + 1, GoG);
                
            // Test chaque nombre possible pour la position donnée
            for (int k = 1; k <= 9; k++)
            {
                if (AbsLigne(i, GoG, k) && AbsColonne(j, GoG, k) && AbsGroupe(i, j, GoG, k))
                {
                    GoG[i,j] = k;
                    if (BackTracking(Position + 1, GoG))
                        return true;
                }
            }
            GoG[i,j] = 0;
            return false;
        }

        // Ajoute les références des lignes, colonnes et Map dans le tableau du champ des possibles
        private void AssignationPosition()
        {
            for (int i = 0; i < 81; i++)
            {
                int j = i / 9;
                int h = i % 9;
                int g = 0;

                if (j < 3) {
                    if (h < 3)
                        g = 0;
                    else if (h < 6)
                        g = 1;
                    else
                        g = 2;
                } else if (j < 6) {
                    if (h < 3)
                        g = 3;
                    else if (h < 6)
                        g = 4;
                    else
                        g = 5;
                } else {
                    if (h < 3)
                        g = 6;
                    else if (h < 6)
                        g = 7;
                    else
                        g = 8;
                }

                TableDesPossibles[i, 9] = j; //Ligne
                TableDesPossibles[i, 10] = h; //Colonne
                TableDesPossibles[i, 11] = g; // Groupe
            }
        }
        // Vérifie si le chiffre est absent de la ligne, colonne ou du groupe
        private bool AbsGroupe(int i, int j, int[,] GoG, int k)
        {
            int wi = i - (i % 3);
            int wj = j - (j % 3);
            for (int x = wi; x < wi + 3; x++)
                for (int z = wj; z < wj + 3; z++)
                    if (GoG[x,z] == k)
                        return false;
            return true;
        }

        private bool AbsColonne(int j, int[,] GoG, int k)
        {
            for (int i = 0; i < 9; i++)
                if (GoG[i,j] == k)
                    return false;
            return true;
        }

        private bool AbsLigne(int i, int[,] GoG, int k)
        {
            for (int j = 0; j < 9; j++)
                if (GoG[i,j] == k)
                    return false;
            return true;
        }

        private void Afficher()
        {
            double Loop = 0;
            foreach (GridViewItem GVI in Grille.Items)
            {
                int i = Convert.ToInt32(Math.Truncate(Loop / 9));
                int j = Convert.ToInt32(Loop % 9);
                Loop++;

                if (GJ[i, j] != 0)
                    GVI.Content = GJ[i, j];
                else
                    GVI.Content = null;

                if (GVI.Content != null)
                    GVI.Foreground = new SolidColorBrush(Color.FromArgb(255, 28, 155, 0));
            }
        }

        private bool ChampDesPossibles(int Position, int[,] GoG)
        {
            double Pos = Position;
            // Vérifie qu'on est dans les limites de la grille, si non alors stop
            if (Position == 81)
                return true;
            int i = Convert.ToInt32(Math.Truncate(Pos / 9));
            int j = Position % 9;

            // Si La case possede deja une assignation alors on passe à la case suivante
            if (GoG[i,j] != 0)
                return ChampDesPossibles(Position + 1, GoG);
            for (int k = 1; k <= 9; k++)
            {
                if (AbsLigne(i, GoG, k) && AbsColonne(j, GoG, k) && AbsGroupe(i, j, GoG, k))
                {
                    TableDesPossibles[Position,(k - 1)] = k;
                }
            }
            return ChampDesPossibles(Position + 1, GoG);
        }

        private void RASChampDesPossibles()
        {
            for (int i = 0; i < 81; i++)
                for (int j = 0; j < 9; j++)
                    TableDesPossibles[i, j] = 0;
        }

        // Rempli les cases qui ne possède qu'une seule solution
        private bool CaseSolutionUnique(int[,] GoG)
        {
            int NbP = 0;
            int LbN = 0;
            bool CSU_ = false;
            for (int i = 0; i < 81; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (TableDesPossibles[i,j] != 0)
                    {
                        NbP++;
                        LbN = TableDesPossibles[i,j];
                    }
                }
                if (NbP == 1)
                {
                    GoG[TableDesPossibles[i,9],TableDesPossibles[i,10]] = LbN;
                    TB_Logs.Text += "Ligne : " + (TableDesPossibles[i, 9] + 1) + " - Colonne : " + (TableDesPossibles[i, 10] + 1) + " // Assignation : " + LbN + " M[CSU] \n";
                    TableDesPossibles[i,LbN - 1] = 0;
                    MaJAll(i, LbN - 1);
                    CSU_ = true;
                }
                NbP = 0;
                LbN = 0;
            }
            Afficher();
            return CSU_;
        }

        // Met à jour le tableau du champ des possibles
        private void MaJAll(int Position, int LbN)
        {
            MaJLigne(TableDesPossibles[Position,9], LbN);
            MaJColonne(TableDesPossibles[Position,10], LbN);
            MaJGroupe(TableDesPossibles[Position,11], LbN);
            MaJCase(Position);
        }

        private void MaJCase(int position)
        {
            for (int c = 0; c < 9; c++)
                TableDesPossibles[position,c] = 0;
        }

        private void MaJGroupe(int v, int lbN)
        {
            for (int gi = 0; gi < 9; gi++)
                TableDesPossibles[Groupe[v,gi],lbN] = 0;
        }

        private void MaJColonne(int v, int lbN)
        {
            for (int ci = 0; ci < 9; ci++)
                TableDesPossibles[Map[ci,v],lbN] = 0;
        }

        private void MaJLigne(int v, int lbN)
        {
            for (int li = 0; li < 9; li++)
                TableDesPossibles[Map[v,li],lbN] = 0;
        }

        // Célibataires cachés
        private bool CbtC(int[,] GoG)
        {
            bool CbtC_ = false;
            int[] SimG = new int[3] {0, 0, 0};

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        if (TableDesPossibles[Groupe[i,k],j] != 0)
                        {
                            SimG[0] += 1;
                            SimG[1] = (j + 1);
                            SimG[2] = Groupe[i,k];
                        }
                        if (SimG[0] > 1)
                            break;
                    }
                    if (SimG[0] == 1)
                    {
                        GJ[SimG[2] / 9,SimG[2] % 9] = SimG[1];
                        TB_Logs.Text += "Ligne : "+ ((SimG[2] / 9)+1) + " - Colonne : " + ((SimG[2] % 9)+1) + " // Assignation : " + SimG[1] + " M[CC] \n";
                        MaJAll(SimG[2], (SimG[1] - 1));
                        CbtC_ = true;
                    }
                    for (int l = 0; l < 3; l++)
                    {
                        SimG[l] = 0;
                    }
                }
            }
            Afficher();
            return CbtC_;
        }


        // Segments
        // Quand dans un carré, un chiffre n'est possible que sur un segment, alors le candidat peut être exclu de cette colonne/ligne dans les autres carrés.
        private void CheckSegment1()
        {
            int[] nboccrL = new int[2] { 0, 0 };
            int[] nboccrC = new int[2] { 0, 0 };
            // Les 3 segments ligne d'une case
            int[] pSL1 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] pSL2 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] pSL3 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // Les 3 segments colonne d'une case
            int[] pSC1 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] pSC2 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] pSC3 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //TB_Logs.Text += "-------------------------- \n";

            for (int l = 0; l < 9; l++)
            {
                // Eventail des possibles : Segment de ligne
                for (int k = 0; k < 3; k++)
                    for (int i = 0; i < 9; i++)
                    {
                        if (TableDesPossibles[Groupe[l,k],i] != 0) pSL1[i] = TableDesPossibles[Groupe[l,k],i];
                        if (TableDesPossibles[Groupe[l,k + 3],i] != 0) pSL2[i] = TableDesPossibles[Groupe[l,k + 3],i];
                        if (TableDesPossibles[Groupe[l,k + 6],i] != 0) pSL3[i] = TableDesPossibles[Groupe[l,k + 6],i];
                    }

                // Eventail des possibles : Segment de colonne
                for (int k = 0; k < 9; k += 3)
                    for (int i = 0; i < 9; i++)
                    {
                        if (TableDesPossibles[Groupe[l,k],i] != 0) pSC1[i] = TableDesPossibles[Groupe[l,k],i];
                        if (TableDesPossibles[Groupe[l,k + 1],i] != 0) pSC2[i] = TableDesPossibles[Groupe[l,k + 1],i];
                        if (TableDesPossibles[Groupe[l,k + 2],i] != 0) pSC3[i] = TableDesPossibles[Groupe[l,k + 2],i];
                    }

                // Compare chacun des segments par categories entre eux
                for (int k = 0; k < 9; k++)
                {
                    if (Array.IndexOf(pSL1,k + 1) >= 0)
                    {
                        nboccrL[0]++;
                        nboccrL[1] = 0;
                    }
                    if (Array.IndexOf(pSL2,k + 1) >= 0)
                    {
                        nboccrL[0]++;
                        nboccrL[1] = 1;
                    }
                    if (Array.IndexOf(pSL3,k + 1) >= 0)
                    {
                        nboccrL[0]++;
                        nboccrL[1] = 2;
                    }

                    if (nboccrL[0] == 1)
                    {
                        // Dans le groupe : L, La ligne : nboccrL[1]+1 est la seule pouvant contenir (k+1), il peut alors être retiré de cette ligne dans les deux autres groupes qu'elle traverse
                        //TB_Logs.Text += "Groupe = " + (l + 1) + " // Ligne : " + (nboccrL[1]+1) + " // Chiffre = " + (k + 1) + " - Ligne \n";
                        // Si la ligne concernée fait parti des groupe compris entre 1 et 3 
                        for (int i = 0; i < 9; i++)
                        {
                            // Si il s'agit du groupe 1 alors modifier que la partie des Map 2 et 3 de la ligne
                            if (l == 0)
                                if (i > 2)
                                    TableDesPossibles[Map[(nboccrL[1]),i],k] = 0;
                            // Si il s'agit du groupe 2 alors modifier que la partie des Map 1 et 3 de la ligne
                            if (l == 1)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[(nboccrL[1]),i],k] = 0;
                            // Si il s'agit du groupe 3 alors modifier que la partie des Map 1 et 2 de la ligne
                            if (l == 2)
                                if (i < 6)
                                    TableDesPossibles[Map[(nboccrL[1]),i],k] = 0;
                            // Si la ligne concernée fait parti des groupe compris entre 4 et 6 
                            if (l == 3)
                                if (i > 2)
                                    TableDesPossibles[Map[(nboccrL[1] + 3),i],k] = 0;
                            if (l == 4)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[(nboccrL[1] + 3),i],k] = 0;
                            if (l == 5)
                                if (i < 6)
                                    TableDesPossibles[Map[(nboccrL[1] + 3),i],k] = 0;
                            // Si la ligne concernée fait parti des groupes compris entre 7 et 9 
                            if (l == 6)
                                if (i > 2)
                                    TableDesPossibles[Map[(nboccrL[1] + 6),i],k] = 0;
                            if (l == 7)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[(nboccrL[1] + 6),i],k] = 0;
                            if (l == 8)
                                if (i < 6)
                                    TableDesPossibles[Map[(nboccrL[1] + 6),i],k] = 0;
                        }
                        //console.log((k + 1) + " n'est possible que dans ... L" + nboccrL[1])
                    }
                    nboccrL[0] = 0;

                    if (Array.IndexOf(pSC1,k + 1) >= 0)
                    {
                        nboccrC[0]++;
                        nboccrC[1] = 0;
                    }
                    if (Array.IndexOf(pSC2,k + 1) >= 0)
                    {
                        nboccrC[0]++;
                        nboccrC[1] = 1;
                    }
                    if (Array.IndexOf(pSC3,k + 1) >= 0)
                    {
                        nboccrC[0]++;
                        nboccrC[1] = 2;
                    }
                    if (nboccrC[0] == 1)
                    {
                        //TB_Logs.Text += "Groupe = " + (l + 1) + " // Colonne : " + (nboccrC[1] + 1) + " // Chiffre = " + (k + 1) + " \n";
                        // retire k+1 des choix pour le reste de la Colonne
                        for (int i = 0; i < 9; i++)
                        {
                            // Si il s'agit du groupe 1 alors modifier que la parti des Map 3 et 6 de la colonne
                            if (l == 0)
                                if (i > 2)
                                    TableDesPossibles[Map[i,(nboccrC[1])],k] = 0;
                            // Si il s'agit du groupe 3 alors modifier que la parti des Map 1 et 6 de la colonne
                            if (l == 3)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[i,(nboccrC[1])],k] = 0;
                            // Si il s'agit du groupe 6 alors modifier que la parti des Map 1 et 3 de la colonne
                            if (l == 6)
                                if (i < 6)
                                    TableDesPossibles[Map[i,(nboccrC[1])],k] = 0;

                            if (l == 1)
                                if (i > 2)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 3)],k] = 0;
                            if (l == 4)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 3)],k] = 0;
                            if (l == 7)
                                if (i < 6)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 3)],k] = 0;

                            if (l == 2)
                                if (i > 2)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 6)],k] = 0;
                            if (l == 5)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 6)],k] = 0;
                            if (l == 8)
                                if (i < 6)
                                    TableDesPossibles[Map[i,(nboccrC[1] + 6)],k] = 0;
                        }
                    }
                    nboccrC[0] = 0;
                }
                for (int g = 0; g < 9; g++)
                {
                    pSC1[g] = 0;
                    pSC2[g] = 0;
                    pSC3[g] = 0;
                    pSL1[g] = 0;
                    pSL2[g] = 0;
                    pSL3[g] = 0;
                }
            }
            //TB_Logs.Text += "-------------------------- \n";
        }

        private void CheckSegment2()
        {
            int[] SLp1 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] SLp2 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] SLp3 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int[] SCp1 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] SCp2 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] SCp3 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int[] nboccL = new int[2] { 0, 0 };
            int[] nboccC = new int[2] { 0, 0 };

            //TB_Logs.Text += "-------------------------- \n";

            for (int l = 0; l < 9; l++)
            {
                // Eventail des possibles : ligne / carré
                for (int k = 0; k < 3; k++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (TableDesPossibles[Map[l,k],i] != 0) SLp1[i] = TableDesPossibles[Map[l,k],i];
                        if (TableDesPossibles[Map[l,k + 3],i] != 0) SLp2[i] = TableDesPossibles[Map[l,k + 3],i];
                        if (TableDesPossibles[Map[l,k + 6],i] != 0) SLp3[i] = TableDesPossibles[Map[l,k + 6],i];
                    }
                }

                // Eventail des possibles : Segment de colonne
                for (int k = 0; k < 3; k++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (TableDesPossibles[Map[k,l],i] != 0) SCp1[i] = TableDesPossibles[Map[k,l],i];
                        if (TableDesPossibles[Map[k + 3,l],i] != 0) SCp2[i] = TableDesPossibles[Map[k + 3,l],i];
                        if (TableDesPossibles[Map[k + 6,l],i] != 0) SCp3[i] = TableDesPossibles[Map[k + 6,l],i];
                    }
                }

                /*      
                console.log('----------------------------------------------');
                console.log('L' + (l+1) + ' = ' + SLp1 + ' ' + SLp2 + ' ' + SLp3);
                console.log('C' + (l+1) + ' = ' + SCp1 + ' ' + SCp2 + ' ' + SCp3);
                console.log('----------------------------------------------'); 
                */

                for (int k = 0; k < 9; k++)
                {
                    // Chaque segment ligne
                    if (Array.IndexOf(SLp1,k + 1) >= 0)
                    {
                        nboccL[0]++;
                        nboccL[1] = 0;
                    }
                    if (Array.IndexOf(SLp2,k + 1) >= 0)
                    {
                        nboccL[0]++;
                        nboccL[1] = 1;
                    }
                    if (Array.IndexOf(SLp3,k + 1) >= 0)
                    {
                        nboccL[0]++;
                        nboccL[1] = 2;
                    }

                    if (nboccL[0] == 1)
                    {
                        //TB_Logs.Text += (k + 1) + " n'est possible que dans ... L" + (l + 1) + ", Segment : " + nboccL[1] +"\n";
                        // Boucle pour retirer les propositions inutiles
                        for (int i = 0; i < 9; i++)
                        {
                            // Si ligne = 0
                            if (l == 0)
                                // Alors retirer que les 6 derniers chiffre du groupe
                                if (i > 2)
                                    TableDesPossibles[Groupe[nboccL[1],i],k] = 0;
                            if (l == 1)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Groupe[nboccL[1],i],k] = 0;
                            if (l == 2)
                                if (i < 6)
                                    TableDesPossibles[Groupe[nboccL[1],i],k] = 0;

                            if (l == 3)
                                if (i > 2)
                                    TableDesPossibles[Groupe[nboccL[1] + 3,i],k] = 0;
                            if (l == 4)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Groupe[nboccL[1] + 3,i],k] = 0;
                            if (l == 5)
                                if (i < 6)
                                    TableDesPossibles[Groupe[nboccL[1] + 3,i],k] = 0;

                            if (l == 6)
                                if (i > 2)
                                    TableDesPossibles[Groupe[nboccL[1] + 6,i],k] = 0;
                            if (l == 7)
                                if (i < 3 && i > 5)
                                    TableDesPossibles[Groupe[nboccL[1] + 6,i],k] = 0;
                            if (l == 8)
                                if (i < 6)
                                    TableDesPossibles[Groupe[nboccL[1] + 6,i],k] = 0;
                        }
                    }
                    nboccL[0] = 0;


                    if (Array.IndexOf(SCp1,k + 1) >= 0)
                    {
                        nboccC[0]++;
                        nboccC[1] = 0;
                    }
                    if (Array.IndexOf(SCp2,k + 1) >= 0)
                    {
                        nboccC[0]++;
                        nboccC[1] = 3;
                    }
                    if (Array.IndexOf(SCp3,k + 1) >= 0)
                    {
                        nboccC[0]++;
                        nboccC[1] = 6;
                    }

                    if (nboccC[0] == 1)
                    {
                        //TB_Logs.Text += (k + 1) + " n'est possible que dans ... C" + (l + 1) + ", Segment : " + nboccC[1] + "\n";
                        for (int i = 0; i < 9; i++)
                        {
                            if (l == 0)
                                if (i != 0 && i != 3 && i != 6)
                                    TableDesPossibles[Groupe[nboccC[1],i],k] = 0;
                            if (l == 1)
                                if (i != 1 && i != 4 && i != 7)
                                    TableDesPossibles[Groupe[nboccC[1],i],k] = 0;
                            if (l == 2)
                                if (i != 2 && i != 5 && i != 8)
                                    TableDesPossibles[Groupe[nboccC[1],i],k] = 0;

                            if (l == 3)
                                if (i != 0 && i != 3 && i != 6)
                                    TableDesPossibles[Groupe[nboccC[1] + 1,i],k] = 0;
                            if (l == 4)
                                if (i != 1 && i != 4 && i != 7)
                                    TableDesPossibles[Groupe[nboccC[1] + 1,i],k] = 0;
                            if (l == 5)
                                if (i != 2 && i != 5 && i != 8)
                                    TableDesPossibles[Groupe[nboccC[1] + 1,i],k] = 0;

                            if (l == 6)
                                if (i != 0 && i != 3 && i != 6)
                                    TableDesPossibles[Groupe[nboccC[1] + 2,i],k] = 0;
                            if (l == 7)
                                if (i != 1 && i != 4 && i != 7)
                                    TableDesPossibles[Groupe[nboccC[1] + 2,i],k] = 0;
                            if (l == 8)
                                if (i != 2 && i != 5 && i != 8)
                                    TableDesPossibles[Groupe[nboccC[1] + 2,i],k] = 0;
                        }
                    }
                    nboccC[0] = 0;
                }
                for (int g = 0; g < 9; g++)
                {
                    SLp1[g] = 0;
                    SLp2[g] = 0;
                    SLp3[g] = 0;
                    SCp1[g] = 0;
                    SCp2[g] = 0;
                    SCp3[g] = 0;
                }
            }
            //TB_Logs.Text += "-------------------------- \n";
        }

        private void TwinTripletQuatrain()
        {
            //3.1.2 Triplet et quatrain
            //Quand trois cases d'un groupe ne contiennent pas d'autres chiffres que trois candidats, ces chiffres peuvent être exclus des autres cases du groupe. Attention! Il n'y a pas besoin que ces trois cases contiennent tous les chiffres du triplet, il faut seulement que ces cases soient les seules à avoir les trois chiffres en commun.

            string TQLigne = "";
            string[] ArrTQLigne = new string[9] { "", "", "", "", "", "", "", "", "" };

            string TQColonne = "";
            string[] ArrTQColonne = new string[9] { "", "", "", "", "", "", "", "", "" };

            string TQGroupe = "";
            string[] ArrTQGroupe = new string[9] { "", "", "", "", "", "", "", "", "" };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        // L
                        if (TableDesPossibles[Map[i, j], k] != 0)
                            TQLigne += k.ToString();
                        // C
                        if (TableDesPossibles[Map[j, i], k] != 0)
                            TQColonne += k.ToString();
                        // G
                        if (TableDesPossibles[Groupe[i, j], k] != 0)
                            TQGroupe += k.ToString();
                    }
                    // L
                    if (TQLigne.Length <= 4 && TQLigne.Length > 1)
                        ArrTQLigne[j] = TQLigne;
                    // C
                    if (TQColonne.Length <= 4 && TQColonne.Length > 1)
                        ArrTQColonne[j] = TQColonne;
                    // G
                    if (TQGroupe.Length <= 4 && TQGroupe.Length > 1)
                        ArrTQGroupe[j] = TQGroupe;

                    TQLigne = "";
                    TQColonne = "";
                    TQGroupe = "";
                }

                ComparaisonArrayTTQ(ArrTQLigne, 0, i);
                ComparaisonArrayTTQ(ArrTQColonne, 1, i);
                ComparaisonArrayTTQ(ArrTQGroupe, 2, i);

                for (int n = 0; n < ArrTQLigne.Length; n++)
                {
                    ArrTQLigne[n] = "";
                    ArrTQColonne[n] = "";
                    ArrTQGroupe[n] = "";
                }
            }
        }

        private void ComparaisonArrayTTQ(string[] arrTQ, int v, int i)
        {
            int SimilitudeArray = 0;
            int SimilitudeNecessaire=0;
            string SimilitudeEmplacement = "";

            for (int l = 0; l < arrTQ.Length; l++)
            {
                SimilitudeNecessaire = 0;
                SimilitudeEmplacement = "";

                if (arrTQ[l].Length > 1)
                {
                    for (int m = 0; m < arrTQ.Length; m++)
                    {
                        SimilitudeArray = 0;
                        if (m != l && arrTQ[m].Length > 1 && arrTQ[l].Length >= arrTQ[m].Length)
                        {
                            for (int k = 0; k < arrTQ[m].Length; k++)
                            {
                                if (arrTQ[l].IndexOf(arrTQ[m][k], k) >= 0)
                                {
                                    SimilitudeArray++;
                                }
                            }
                            if (SimilitudeArray == arrTQ[m].Length && SimilitudeArray > 0)
                            {
                                SimilitudeNecessaire++;
                                SimilitudeEmplacement += m;
                            }
                        }
                    }
                    if (SimilitudeNecessaire == (arrTQ[l].Length-1) && SimilitudeNecessaire > 0)
                    {
                        //TB_Logs.Text += "Ligne : " + (i+1) + "\n";
                        //TB_Logs.Text += "Case : " + (l + 1) + " - "+ arrLigneTQ[l] + "\n";
                        //TB_Logs.Text += SimilitudeEmplacement + "\n \n";
                        SimilitudeEmplacement += l;
                        MaJTTQ(i, arrTQ[l], SimilitudeEmplacement, v);
                    }
                }
            }
        }

        private void MaJTTQ(int i, string v, string similitudeEmplacement, int e)
        {
            for (int li = 0; li < 9; li++)
                if (similitudeEmplacement.IndexOf(li.ToString(), 0) == -1)
                    for (int x = 0; x < v.Length; x++)
                    {
                        if (e == 0)
                            TableDesPossibles[Map[i, li], Convert.ToInt32(v.Substring(x, 1))] = 0;
                        if (e == 1)
                            TableDesPossibles[Map[li, i], Convert.ToInt32(v.Substring(x, 1))] = 0;
                        if (e == 2)
                            TableDesPossibles[Groupe[i, li], Convert.ToInt32(v.Substring(x, 1))] = 0;
                    }
        }

        private void BT_CSU_Click(object sender, RoutedEventArgs e)
        {
            CaseSolutionUnique(GJ);
        }

        private void BT_Backtracking_Click(object sender, RoutedEventArgs e)
        {
            BackTracking(0, GJ);
            Afficher();
        }

        private void BT_Afficher_Click(object sender, RoutedEventArgs e)
        {
            Afficher();
        }

        private void BT_CC_Click(object sender, RoutedEventArgs e)
        {
            CbtC(GJ);
        }

        private void BT_CDP_Click(object sender, RoutedEventArgs e)
        {
            ChampDesPossibles(0, GJ);
            MaJCDP();
        }

        private void BT_S1_Click(object sender, RoutedEventArgs e)
        {
            CheckSegment1();
        }

        private void BT_CDPMAJ_Click(object sender, RoutedEventArgs e)
        {
            MaJCDP();
        }

        private void BT_S2_Click(object sender, RoutedEventArgs e)
        {
            CheckSegment2();
        }

        private void MaJCDP()
        {
            while (GV_Log.Items.Count >= 2)
            {
                GV_Log.Items.RemoveAt(GV_Log.Items.Count - 1);
            }

            string ListeParCase;
            for (int i = 0; i < 81; i++)
            {
                ListeParCase = "";
                for (int j = 0; j < 12; j++)
                {
                    ListeParCase += TableDesPossibles[i, j];
                }
                GV_Log.Items.Add("[" + (i + 1) + "] " + ListeParCase);
            }
        }

        private void BT_CDPRAZ_Click(object sender, RoutedEventArgs e)
        {
            RASChampDesPossibles();
            MaJCDP();
        }

        private void Grille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (GridViewItem GVI in Grille.Items)
            {
                if (GVI.IsSelected)
                {
                    string NSI = GVI.Name.Substring(1);
                    int NSI_ = Convert.ToInt32(NSI);
                    TB_Logs.Text += "Case " + (NSI_ + 1) + " : ";
                    for (int i = 0; i < 9; i++)
                        if (TableDesPossibles[NSI_, i] != 0)
                            TB_Logs.Text += TableDesPossibles[NSI_, i] + " ";
                    TB_Logs.Text += "\n";
                }
            }
        }

        private void BT_RAZLOGS_Click(object sender, RoutedEventArgs e)
        {
            TB_Logs.Text = "";
        }

        private void BT_TQ_Click(object sender, RoutedEventArgs e)
        {
            TwinTripletQuatrain();
        }

        private void BT_SOLVE_Click(object sender, RoutedEventArgs e)
        {
            bool S;
            do {
                S = false;
                CheckSegment1();
                CheckSegment2();
                TwinTripletQuatrain();
                if (CbtC(GJ))
                    S = true;
                if (CaseSolutionUnique(GJ))
                    S = true;
            } while (S);
        }

        private void CopyGrid(int[,] A, int[,] B)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    A[i,j] = B[i,j];

        }

        private void CB_CDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var z = (CB_CDG.SelectedItem as ComboBoxItem).Content;
            switch (z.ToString())
            {
                case "Vide":
                    CopyGrid(GJ, GV);
                    break;
                case "Facile":
                    CopyGrid(GJ, GF);
                    break;
                case "Moyen":
                    CopyGrid(GJ, GM);
                    break;
                case "Difficile":
                    CopyGrid(GJ, GD);
                    break;
                case "Très difficile":
                    CopyGrid(GJ, GO);
                    break;
                default:
                    break;
            }
            TB_Logs.Text = "";
            RASChampDesPossibles();
            ChampDesPossibles(0,GJ);
            MaJCDP();
            Afficher();
        }
    }
}

//private void MaJTQGroupe(int i, string v, string similitudeEmplacement)
//{
//    for (int li = 0; li < 9; li++)
//        if (similitudeEmplacement.IndexOf(li.ToString(), 0) == -1)
//            for (int x = 0; x < v.Length; x++)
//                TableDesPossibles[Groupe[i, li], Convert.ToInt32(v.Substring(x, 1))] = 0;
//}

//private void MaJTQColonne(int i, string v, string similitudeEmplacement)
//{
//    for (int li = 0; li < 9; li++)
//        if (similitudeEmplacement.IndexOf(li.ToString(), 0) == -1)
//            for (int x = 0; x < v.Length; x++)
//                TableDesPossibles[Map[li, i], Convert.ToInt32(v.Substring(x, 1))] = 0;
//}

//private void MaJTQLigne(int i, string v, string similitudeEmplacement)
//{
//    for (int li = 0; li < 9; li++)
//        if (similitudeEmplacement.IndexOf(li.ToString(), 0) == -1)
//            for (int x = 0; x < v.Length; x++)
//                TableDesPossibles[Map[i, li], Convert.ToInt32(v.Substring(x, 1))] = 0;
//}

//private void Twin()
//{
//    string StriLtwin = "";
//    string[] ArrLigneTwin = new string[9] { null, null, null, null, null, null, null, null, null };
//    string StriCtwin = "";
//    string[] ArrColonneTwin = new string[9] { null, null, null, null, null, null, null, null, null };
//    string StriGtwin = "";
//    string[] ArrGroupeTwin = new string[9] { null, null, null, null, null, null, null, null, null };

//    for (int i = 0; i < 9; i++)
//    {
//        for (int j = 0; j < 9; j++)
//        {
//            for (int k = 0; k < 9; k++)
//            {
//                // L
//                if (TableDesPossibles[Map[i, j], k] != 0)
//                {
//                    StriLtwin += k.ToString();
//                }
//                // C
//                if (TableDesPossibles[Map[j, i], k] != 0)
//                {
//                    StriCtwin += k.ToString();
//                }
//                // G
//                if (TableDesPossibles[Groupe[i, j], k] != 0)
//                {
//                    StriGtwin += k.ToString();
//                }
//            }
//            // L
//            if (StriLtwin.Length == 2)
//                ArrLigneTwin[j] = StriLtwin;
//            // C
//            if (StriCtwin.Length == 2)
//                ArrColonneTwin[j] = StriCtwin;
//            // G
//            if (StriGtwin.Length == 2)
//                ArrGroupeTwin[j] = StriGtwin;

//            StriGtwin = "";
//            StriLtwin = "";
//            StriCtwin = "";
//        }
//        //console.log(ArrLigneTwin);
//        // L
//        ComparaisonArray(ArrLigneTwin, 1, i);
//        // C
//        ComparaisonArray(ArrColonneTwin, 2, i);
//        // G
//        ComparaisonArray(ArrGroupeTwin, 3, i);
//    }
//}
//private void ComparaisonArray(string[] ArrayT, int z, int i)
//{
//    for (int l = 0; l < ArrayT.Length; l++)
//    {
//        for (int m = (l + 1); m < ArrayT.Length; m++)
//        {
//            if (ArrayT[l] == ArrayT[m] && ArrayT[l] != null)
//            {

//                int c1 = Convert.ToInt32(ArrayT[l].Substring(0, 1));
//                int c2 = Convert.ToInt32(ArrayT[l].Substring(1, 1));

//                switch (z)
//                {
//                    case 1:
//                        //TB_Logs.Text += "Ligne " + z + " - Paire : " + c1 + c2 + "\n";
//                        MaJTLigne(i, l, m, c1, c2);
//                        break;
//                    case 2:
//                        //TB_Logs.Text += "Colonne " + z + " - Paire : " + c1 + c2 + "\n";
//                        MaJTColonne(i, l, m, c1, c2);
//                        break;
//                    case 3:
//                        //TB_Logs.Text += "Groupe " + z + " - Paire : " + c1 + c2 + "\n";
//                        MaJTGroupe(i, l, m, c1, c2);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//    }
//    for (int n = 0; n < ArrayT.Length; n++)
//    {
//        ArrayT[n] = null;
//    }
//}

//private void MaJTGroupe(int i, int l, int m, int c1, int c2)
//{
//    for (int gi = 0; gi < 9; gi++)
//        if (gi != l && gi != m)
//        {
//            TableDesPossibles[Groupe[i, gi], c1] = 0;
//            TableDesPossibles[Groupe[i, gi], c2] = 0;
//        }
//}

//private void MaJTColonne(int i, int l, int m, int c1, int c2)
//{
//    for (int ci = 0; ci < 9; ci++)
//        if (ci != l && ci != m)
//        {
//            TableDesPossibles[Map[ci, i], c1] = 0;
//            TableDesPossibles[Map[ci, i], c2] = 0;
//        }
//}

//private void MaJTLigne(int i, int l, int m, int c1, int c2)
//{
//    for (int li = 0; li < 9; li++)
//        if (li != l && li != m)
//        {
//            TableDesPossibles[Map[i, li], c1] = 0;
//            TableDesPossibles[Map[i, li], c2] = 0;
//        }
//}