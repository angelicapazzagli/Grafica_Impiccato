namespace Grafica_Impiccato
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int n_tentativi = 0;
            string file;
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
        }
        string livello, tema;
        private void btnA_Click(object sender, EventArgs e)
        {

        }

        private void btnB_Click(object sender, EventArgs e)
        {

        }

        private void btnC_Click(object sender, EventArgs e)
        {

        }

        private void btnD_Click(object sender, EventArgs e)
        {

        }

        private void btnE_Click(object sender, EventArgs e)
        {

        }

        private void btnF_Click(object sender, EventArgs e)
        {

        }

        private void btnG_Click(object sender, EventArgs e)
        {

        }

        private void btnH_Click(object sender, EventArgs e)
        {

        }

        private void btnI_Click(object sender, EventArgs e)
        {

        }

        private void btnJ_Click(object sender, EventArgs e)
        {

        }

        private void btnK_Click(object sender, EventArgs e)
        {

        }

        private void btnL_Click(object sender, EventArgs e)
        {

        }

        private void btnM_Click(object sender, EventArgs e)
        {

        }

        private void btnN_Click(object sender, EventArgs e)
        {

        }

        private void btnO_Click(object sender, EventArgs e)
        {

        }

        private void btnP_Click(object sender, EventArgs e)
        {

        }

        private void btnQ_Click(object sender, EventArgs e)
        {

        }

        private void btnR_Click(object sender, EventArgs e)
        {

        }

        private void btnS_Click(object sender, EventArgs e)
        {

        }

        private void btnT_Click(object sender, EventArgs e)
        {

        }

        private void btnU_Click(object sender, EventArgs e)
        {

        }

        private void btnV_Click(object sender, EventArgs e)
        {

        }

        private void btnW_Click(object sender, EventArgs e)
        {

        }

        private void btnX_Click(object sender, EventArgs e)
        {

        }

        private void btnY_Click(object sender, EventArgs e)
        {

        }

        private void btnZ_Click(object sender, EventArgs e)
        {

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
    }
}
