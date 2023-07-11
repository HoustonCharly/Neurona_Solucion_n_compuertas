using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practica3
{
    public partial class Form1 : Form
    {
        int entrada;
        double[,] datos;
        double[] pesos, neurona;
        double tamano, umbral;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            entrada = int.Parse(tBEntradas.Text);//lee el dato que entra el en textbox
            tamano = Math.Pow(2, entrada);//Se eleva 2^n para obtenr el tamaño de la tabla
            datos = new double[(int)tamano, entrada];//Vector usando tamaño y entradas

            //Se comienza a generar la tabla de acuerdo al numero de bits de entrada
            Gridtabla.RowCount = (int)tamano;//genera las filas para los datos
            for (int i = 0; i < entrada; i++)
            {
                Gridtabla.Columns.Add("valX", "X"+(i));//el primer valor es X0 hasta X+1
            }
            Gridtabla.Columns[entrada].HeaderText = "Y";

            int contador = 0;
            int aux1 = 0;
            if (entrada > 0)
            {
                int aux2 = (int)tamano / 2;
                for (int j = 0; j < entrada; j++)
                {
                    for (int i = 0; i < tamano; i++)
                    {
                        if (aux1 == 0)
                        {
                            datos[i, j] = 0;
                            Gridtabla.Rows[i].Cells[j].Value = 0;
                            contador++;

                        }
                        else if (aux1 == 1)
                        {
                            datos[i, j] = 1;
                            Gridtabla.Rows[i].Cells[j].Value = 1;
                            contador--;
                        }
                        if (contador == aux2)
                        {
                            aux1 = 1;
                        }
                        if (contador == 0)
                        {
                            aux1 = 0;
                        }
                    }
                    aux2 = aux2 / 2;
                }
            }
            //limpia por si se desea cambiar el valor de los pesos
            Gridpesos.Columns.Clear();
            Gridpesos.Rows.Clear();
            //para el vector de la Tablita de pesos
            pesos = new double[entrada];
            Gridpesos.Columns.Add("pesos", "Pesos");
            for (int i = 0; i < entrada; i++)
            {
                Gridpesos.Rows.Add();
            }
        }

        private void firmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LOS INCREIBLES:\n" +
                "\tRivera Rivera Juan Carlos\n" );
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            neurona = new double[(int)tamano];
            umbral = double.Parse(tBumbral.Text);//Lee el valor del textbox umbral
            Gridtabla.Columns[entrada].HeaderText = "Salida";
            try
            {
                //Genera un Vector que Guarda los datos de los pesos insertados
                for (int p = 0; p < entrada; p++)
                {
                    pesos[p] = double.Parse(Gridpesos.Rows[p].Cells[0].Value.ToString());
                }
                Gridtabla.ColumnCount = entrada+1;//Se agrega la columna de salida
                //Se hace la operacion evaluando las columnas de la tabla por el vector de pesos
                for (int n = 0; n < tamano; n++)
                {
                    for (int k = 0; k<entrada; k++)
                    {
                        neurona[n] += datos[n, k]*pesos[k];
                        
                        if (neurona[n]>umbral)
                        {
                            Gridtabla.Rows[n].Cells[entrada].Value = 1;
                            //imprime en el grid si es este el caso si no
                        }
                        else
                        {
                            Gridtabla.Rows[n].Cells[entrada].Value = 0;
                            //imrpime este otro en el grid
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la entrada de datos...!");
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Gridpesos.Columns.Clear();
            Gridtabla.Columns.Clear();
            tBEntradas.Clear();
            tBumbral.Clear();

        }

    }
}
