using System;

namespace Grafica_Impiccato
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        char lettera;
        bool utilizzata = false, chiudi = false, insLettera, insParola;
        int indice_casuale = -1, n_tentativi = 0, punteggio_tot = 0, n_jolly = 3, n_indizi = 3, n_bonus = 3;
        string livello, tema, parola_utente, file = "", segreta = "", p_indovinate = "", non_indovinate = "", indizi, indizio = "";
        char[] parola_segreta;
        char[] sostituta;
        public Form1()
        {
            InitializeComponent();
        }

        void indovina_lettere(ref int n_tentativi, char[] sostituta, char[] parola_segreta, char lettera, string livello, string segreta, ref string p_indovinate, ref string non_indovinate, ref int punteggio_tot)
        {
            string s = "";
            int punti = 0;
            if (segreta.Contains(lettera))
            {
                for (int i = 0; i < parola_segreta.Length; i++)
                {
                    if (parola_segreta[i] == lettera && sostituta[i] != lettera)
                    {
                        sostituta[i] = lettera;
                    }
                }
            }
            else
            {
                n_tentativi--;
            }
            for (int i = 0; i < sostituta.Length; i++)
            {
                s += sostituta[i] + " ";
            }
            lblParola.Text = s;
            lblTentativi.Text = "TENTATIVI RIMASTI: " + n_tentativi.ToString();
            if (n_tentativi <= 0)
            {
                tentativi_finiti(n_tentativi, segreta, ref non_indovinate, sostituta, parola_segreta);
            }
            else if (!sostituta.Contains('_'))
            {
                lblParola.Text = "PAROLA INDOVINATA.";
                p_indovinate += segreta + " ";
                punti = punteggio(livello);
            }
            punteggio_tot += punti;
        }
        void indovina_parole(ref int n_tentativi, char[] sostituta, char[] parola_segreta, ref string p_indovinate, string parola_utente, string livello, string segreta, ref string non_indovinate, ref int punteggio_tot)
        {
            string s = "";
            int punti = 0;
            if (parola_utente.ToLower() == segreta.ToLower())
            {
                for (int i = 0; i < sostituta.Length; i++)
                {
                    sostituta[i] = parola_segreta[i];
                }
                lblParola.Text = "PAROLA INDOVINATA.";
                p_indovinate += segreta + " ";
                punti = punteggio(livello);
            }
            else
            {
                n_tentativi--;
            }
            lblTentativi.Text = "TENTATIVI RIMASTI: " + n_tentativi.ToString();
            if (n_tentativi <= 0)
            {
                tentativi_finiti(n_tentativi, segreta, ref non_indovinate, sostituta, parola_segreta);
            }
            punteggio_tot += punti;
        }

        string scelta_livello(ref int n_tentativi, string livello)
        {
            bool scelta = false;
            while (scelta == false)
            {
                if (livello == "Facile")
                {
                    scelta = true;
                    n_tentativi = 3;
                    return "parole_facili.txt";
                }
                else if (livello == "Medio")
                {
                    scelta = true;
                    n_tentativi = 5;
                    return "parole_medie.txt";
                }
                else if (livello == "Difficile")
                {
                    scelta = true;
                    n_tentativi = 7;
                    return "parole_difficili.txt";
                }
            }
            return "Errore";
        }
        void scelta_tema(string[] parole, string[] range, string[] range_ind, string[] indizi_diff)
        {
            int inizio = 0, fine = 0;
            bool scelta = false;
            while (scelta == false)
            {
                if (tema == "Paesi")
                {
                    scelta = true;
                    inizio = 0;
                    fine = 9;
                }
                else if (tema == "Calciatori")
                {
                    scelta = true;
                    inizio = 10;
                    fine = 19;
                }
                else if (tema == "Mestieri")
                {
                    scelta = true;
                    inizio = 20;
                    fine = 29;
                }
                else if (tema == "Brand")
                {
                    scelta = true;
                    inizio = 30;
                    fine = 39;
                }
                int pos = 0, pos1 = 0;
                for (int i = inizio; i <= fine; i++)
                {
                    range[pos++] = parole[i];
                    range_ind[pos1++] = indizi_diff[i];
                }
            }
        }
        string ins_indizi(string livello)
        {
            if (livello == "Difficile")
            {
                return "indizi_difficili.txt";
            }
            return "";
        }
        char[] sostituzione(string segreta)
        {
            char[] sostituita = new char[segreta.Length];
            for (int i = 0; i < segreta.Length; i++)
            {
                sostituita[i] = '_';
            }
            return sostituita;
        }
        char[] stringa_array(string segreta)
        {
            char[] parola_segreta = new char[segreta.Length];
            for (int i = 0; i < segreta.Length; i++)
            {
                parola_segreta[i] = segreta[i];
            }
            return parola_segreta;
        }
        void tentativi_finiti(int n_tentativi, string segreta, ref string non_indovinate, char[] sostituta, char[] parola_segreta)
        {
            non_indovinate += segreta + " ";
            for (int i = 0; i < sostituta.Length; i++)
            {
                sostituta[i] = parola_segreta[i];
            }
            lblParola.Text = "Tentativi esauriti.";
        }
        int punteggio(string livello)
        {
            int punti = 0;
            if (livello == "Facile")
            {
                punti = 5;
            }
            else if (livello == "Medio")
            {
                punti = 10;
            }
            else if (livello == "Difficile")
            {
                punti = 20;
            }
            return punti;
        }
        void jolly(char[] sostituta, string segreta, int n_jolly, bool utilizzato_j)
        {
            if (utilizzato_j == false)
            {
                if (n_jolly > 0)
                {
                    if (sostituta[0] == '_' || sostituta[sostituta.Length - 1] == '_')
                    {
                        sostituta[0] = segreta[0];
                        sostituta[sostituta.Length - 1] = segreta[sostituta.Length - 1];
                        n_jolly--;
                        utilizzato_j = true;
                        for (int i = 0; i < sostituta.Length; i++)
                        {
                            lblParola.Text += sostituta[i] + " ";
                        }
                    }
                }
            }
        }
        void utilizzo_indizi(int n_indizi, string livello, char[] sostituta, string indizio, bool utilizzato_i)
        {
            if (utilizzato_i == false)
            {
                if (n_indizi > 0)
                {
                    if (livello == "Difficile")
                    {
                        n_indizi--;
                        utilizzato_i = true;
                        lblCommento.Text = indizio;
                        for (int i = 0; i < sostituta.Length; i++)
                        {
                            lblParola.Text += sostituta[i] + " ";
                        }
                    }
                }
            }
        }
        void bonus(int bonus_lettera, string segreta, char[] sostituta, string livello, bool utilizzato_b)
        {
            int indice = -1, casuale;
            char lettera;
            if (livello == "Medio")
            {
                if (utilizzato_b == false)
                {
                    if (bonus_lettera > 0)
                    {
                        bonus_lettera--;
                        utilizzato_b = true;
                        while (indice == -1)
                        {
                            casuale = r.Next(0, sostituta.Length);
                            if (sostituta[casuale] == '_')
                            {
                                indice = casuale;
                            }
                        }
                        lettera = segreta[indice];
                        for (int i = 0; i < sostituta.Length; i++)
                        {
                            if (segreta[i] == lettera)
                            {
                                sostituta[i] = lettera;
                            }
                        }
                    }
                    for (int i = 0; i < sostituta.Length; i++)
                    {
                        lblParola.Text += sostituta[i] + " ";
                    }
                }
            }
        }
        private void btnA_Click(object sender, EventArgs e)
        {
            lettera = 'a';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            lettera = 'b';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            lettera = 'c';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            lettera = 'd';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            lettera = 'e';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            lettera = 'f';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            lettera = 'g';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            lettera = 'h';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            lettera = 'i';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            lettera = 'j';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnK_Click(object sender, EventArgs e)
        {
            lettera = 'k';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            lettera = 'l';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            lettera = 'm';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            lettera = 'n';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            lettera = 'o';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            lettera = 'p';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            lettera = 'q';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            lettera = 'r';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            lettera = 's';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            lettera = 't';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            lettera = 'u';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            lettera = 'v';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            lettera = 'w';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            lettera = 'x';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            lettera = 'y';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            lettera = 'z';
            lblLettere.Text += lettera + " ";
            indovina_lettere(ref n_tentativi, sostituta, parola_segreta, lettera, livello, segreta, ref p_indovinate, ref non_indovinate, ref punteggio_tot);
        }

        private void btnJolly_Click(object sender, EventArgs e)
        {

        }

        private void btnCasuale_Click(object sender, EventArgs e)
        {

        }

        private void btnSuggerimento_Click(object sender, EventArgs e)
        {

        }

        private void checkFacile_CheckedChanged(object sender, EventArgs e)
        {
            livello = checkFacile.Text;
        }

        private void checkMedio_CheckedChanged(object sender, EventArgs e)
        {
            livello = checkMedio.Text;
        }

        private void checkDifficile_CheckedChanged(object sender, EventArgs e)
        {
            livello = checkDifficile.Text;
        }

        private void checkPaesi_CheckedChanged(object sender, EventArgs e)
        {
            tema = checkPaesi.Text;
        }

        private void checkCalciatori_CheckedChanged(object sender, EventArgs e)
        {
            tema = checkCalciatori.Text;
        }

        private void checkMestieri_CheckedChanged(object sender, EventArgs e)
        {
            tema = checkMestieri.Text;
        }

        private void checkBrand_CheckedChanged(object sender, EventArgs e)
        {
            tema = checkBrand.Text;
        }

        private void btnChiudi_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtBoxParola_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenera_Click(object sender, EventArgs e)
        {
            lblParola.Text = "";
            lblTentativi.Text = "TENTATIVI RIMASTI: ";
            lblParole.Text = "Parole provate: ";
            lblLettere.Text = "Lettere provate: ";
            file = scelta_livello(ref n_tentativi, livello);
            string[] parole = File.ReadAllLines(file);
            string[] range = new string[10];
            indizi = ins_indizi(livello);
            string[] indizi_diff = new string[parole.Length];
            if (livello == "Difficile")
            {
                indizi_diff = File.ReadAllLines(indizi);
            }
            string[] range_ind = new string[10];
            scelta_tema(parole, range, range_ind, indizi_diff);
            utilizzata = true;
            while (utilizzata == true)
            {
                indice_casuale = r.Next(range.Length - 1);
                segreta = range[indice_casuale];
                indizio = range_ind[indice_casuale];
                if (p_indovinate.Contains(segreta) == false)
                {
                    utilizzata = false;
                }
            }
            parola_segreta = stringa_array(segreta);
            sostituta = sostituzione(segreta);
            for (int i = 0; i < sostituta.Length; i++)
            {
                lblParola.Text += sostituta[i] + " ";
            }
            lblTentativi.Text = "TENTATIVI RIMASTI: " + n_tentativi.ToString();
        }

        private void lblParola_Click(object sender, EventArgs e)
        {

        }

        private void btnInsParola_Click(object sender, EventArgs e)
        {
            parola_utente = txtBoxParola.Text;
            insParola = true;
            lblParole.Text += "\n" + txtBoxParola.Text.ToLower();
            txtBoxParola.Text = "";
            indovina_parole(ref n_tentativi, sostituta, parola_segreta, ref p_indovinate, parola_utente, livello, segreta, ref non_indovinate, ref punteggio_tot);
        }

        private void btnRisultati_Click(object sender, EventArgs e)
        {
            string[] parole_indovinate = p_indovinate.Split(' ');
            string[] parole_non_indovinate = non_indovinate.Split(' ');
            lblIndovinate.Text = "PAROLE INDOVINATE: ";
            lblNonIndovinate.Text = "PAROLE NON INDOVINATE: ";
            lblPunti.Text = "PUNTI: ";
            for (int i = 0; i < parole_non_indovinate.Length; i++)
            {
                for (int j = 0; j < parole_indovinate.Length; j++)
                {
                    if (parole_non_indovinate[i] == parole_indovinate[j])
                    {
                        parole_non_indovinate[i] = "";
                    }
                }
            }
            for (int i = 0; i < parole_indovinate.Length; i++)
            {
                lblIndovinate.Text += parole_indovinate[i] + "  ";
            }
            for (int i = 0; i < parole_non_indovinate.Length; i++)
            {
                lblNonIndovinate.Text += parole_non_indovinate[i] + "  ";
            }
            lblPunti.Text += punteggio_tot;
        }
    }
}
