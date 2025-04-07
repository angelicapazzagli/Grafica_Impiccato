namespace Grafica_Impiccato
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            while (chiudi == false)
            {
                file = scelta_livello(ref n_tentativi, livello);
                string[] parole = File.ReadAllLines(file);
                string[] range = new string[10];
                indizi = ins_indizi(livello);
                string[] indizi_diff = new string[parole.Length];
                if (livello == "3" || livello == "Difficile")
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
            }
            string[] parole_indovinate = p_indovinate.Split(' ');
            string[] parole_non_indovinate = non_indovinate.Split(' ');
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
        char lettera;
        bool utilizzata, utilizzato_j = false, utilizzato_b = false, utilizzato_i = false, chiudi = false, insLettera, insParola;
        int indice_casuale = -1, n_tentativi = 0, punteggio_tot = 0, n_jolly = 3, n_indizi = 3, n_bonus = 3;
        string livello, tema, parola_utente, file = "", segreta = "", p_indovinate = "", non_indovinate = "", indizi, indizio = "";
        char[] parola_segreta;
        char[] sostituta;
        void indovina_parola(ref char[] sostituta, char[] parola_segreta, string segreta, ref int n_tentativi, ref string p_indovinate, ref string non_indovinate, string livello, ref int punteggio_tot)
        {
            int lettere_indovinate = 0, punti;
            string l_provate = "", p_provate = "";

            while (sostituta.Contains('_'))
            {
                punti = 0;
                for (int i = 0; i < sostituta.Length; i++)
                {
                    lblParola.Text += (sostituta[i] + " ");
                }
                lblTentativi.Text = n_tentativi.ToString();
                if (chiudi == true)
                {
                    for (int i = 0; i < sostituta.Length; i++)
                    {
                        sostituta[i] = parola_segreta[i];
                    }
                }
                else if (insLettera == true)
                {
                    char[] lettere_provate = stringa_array(l_provate);
                    for (int i = 0; i < lettere_provate.Length; i++)
                    {
                        lblLettere.Text += lettere_provate[i] + "  ";
                    }
                    l_provate += lettera;
                    if (segreta.Contains(lettera))
                    {
                        for (int i = 0; i < parola_segreta.Length; i++)
                        {
                            if (parola_segreta[i] == lettera && sostituta[i] != lettera)
                            {
                                sostituta[i] = lettera;
                                lettere_indovinate++;
                            }
                        }
                    }
                    tentativi_finiti(n_tentativi, segreta, ref non_indovinate, sostituta, parola_segreta);
                    if (!sostituta.Contains('_'))
                    {
                        p_indovinate += segreta + " ";
                        punti = punteggio(livello);
                    }
                    insLettera = false;
                }
                else if (insParola == true)
                {
                    string[] parole_provate = p_provate.Split(' ');
                    for (int i = 0; i < parole_provate.Length; i++)
                    {
                        lblParole.Text += (parole_provate[i] + " ");
                    }
                    p_provate += parola_utente + " ";
                    if (parola_utente.ToLower() == segreta.ToLower())
                    {
                        lblIndovinate.Text += parola_utente + " ";
                        for (int i = 0; i < sostituta.Length; i++)
                        {
                            sostituta[i] = parola_segreta[i];
                        }
                        punti = punteggio(livello);
                    }
                    else
                    {
                        n_tentativi--;
                    }
                    tentativi_finiti(n_tentativi, segreta, ref non_indovinate, sostituta, parola_segreta);
                }
                insParola = false;
                punteggio_tot += punti;
            }
        }
        string scelta_livello(ref int n_tentativi, string livello)
        {
            bool scelta = false;
            while (scelta == false)
            {
                if (livello == "Facile" || livello == "1")
                {
                    scelta = true;
                    n_tentativi = 3;
                    return "parole_facili.txt";
                }
                else if (livello == "Medio" || livello == "2")
                {
                    scelta = true;
                    n_tentativi = 5;
                    return "parole_medie.txt";
                }
                else if (livello == "Difficile" || livello == "3")
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
                if (tema == "Paesi" || tema == "1")
                {
                    scelta = true;
                    inizio = 0;
                    fine = 9;
                }
                else if (tema == "Calciatori" || tema == "2")
                {
                    scelta = true;
                    inizio = 10;
                    fine = 19;
                }
                else if (tema == "Mestieri" || tema == "3")
                {
                    scelta = true;
                    inizio = 20;
                    fine = 29;
                }
                else if (tema == "Brand" || tema == "4")
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
            if (livello == "Difficile" || livello == "3")
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
            if (n_tentativi <= 0)
            {
                non_indovinate += segreta + " ";
                for (int i = 0; i < sostituta.Length; i++)
                {
                    sostituta[i] = parola_segreta[i];
                }
            }
        }
        int punteggio(string livello)
        {
            int punti = 0;
            if (livello == "Facile" || livello == "1")
            {
                punti = 5;
            }
            else if (livello == "Medio" || livello == "2")
            {
                punti = 10;
            }
            else if (livello == "Difficile" || livello == "3")
            {
                punti = 20;
            }
            return punti;
        }
        void jolly(ref char[] sostituta, string segreta, ref int n_jolly, ref bool utilizzato_j)
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
        void utilizzo_indizi(ref int n_indizi, string livello, char[] sostituta, string indizio, ref bool utilizzato_i)
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
        void bonus(ref int bonus_lettera, string segreta, ref char[] sostituta, string livello, ref bool utilizzato_b)
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
            insLettera = true;
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            lettera = 'b';
            insLettera = true;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            lettera = 'c';
            insLettera = true;
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            lettera = 'd';
            insLettera = true;
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            lettera = 'e';
            insLettera = true;
        }

        private void btnF_Click(object sender, EventArgs e)
        {
            lettera = 'f';
            insLettera = true;
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            lettera = 'g';
            insLettera = true;
        }

        private void btnH_Click(object sender, EventArgs e)
        {
            lettera = 'h';
            insLettera = true;
        }

        private void btnI_Click(object sender, EventArgs e)
        {
            lettera = 'i';
            insLettera = true;
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            lettera = 'j';
            insLettera = true;
        }

        private void btnK_Click(object sender, EventArgs e)
        {
            lettera = 'k';
            insLettera = true;
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            lettera = 'l';
            insLettera = true;
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            lettera = 'm';
            insLettera = true;
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            lettera = 'n';
            insLettera = true;
        }

        private void btnO_Click(object sender, EventArgs e)
        {
            lettera = 'o';
            insLettera = true;
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            lettera = 'p';
            insLettera = true;
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            lettera = 'q';
            insLettera = true;
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            lettera = 'r';
            insLettera = true;
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            lettera = 's';
            insLettera = true;
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            lettera = 't';
            insLettera = true;
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            lettera = 'u';
            insLettera = true;
        }

        private void btnV_Click(object sender, EventArgs e)
        {
            lettera = 'v';
            insLettera = true;
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            lettera = 'w';
            insLettera = true;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            lettera = 'x';
            insLettera = true;
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            lettera = 'y';
            insLettera = true;
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            lettera = 'z';
            insLettera = true;
        }

        private void btnJolly_Click(object sender, EventArgs e)
        {
            bool utilizzato_j = false;
        }

        private void btnCasuale_Click(object sender, EventArgs e)
        {
            bool utilizzato_b = false;
        }

        private void btnSuggerimento_Click(object sender, EventArgs e)
        {
            bool utilizzato_i = false;
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
            chiudi = true;
        }
        private void txtBoxParola_TextChanged(object sender, EventArgs e)
        {
            parola_utente = txtBoxParola.Text;
            insParola = true;
        }

        private void btnGenera_Click(object sender, EventArgs e)
        {

            indovina_parola(ref sostituta, parola_segreta, segreta, ref n_tentativi, ref p_indovinate, ref non_indovinate, livello, ref punteggio_tot);
        }
    }
}
