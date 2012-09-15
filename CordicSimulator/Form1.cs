using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CordicSimulator
{
    public partial class CordicSimulator : Form
    {
        private Fixed X;
        private Fixed Y;
        private Fixed Z;

        private Fixed ShiftX;
        private Fixed ShiftY;
        private Fixed Tabla;

        private int delta;

        private Fixed AddSubX;
        private Fixed AddSubY;
        private Fixed AddSubZ;

        private bool AddX;
        private bool AddY;
        private bool AddZ;

        private int iteraciones;

        private int minipaso;
        private int paso;

        private int m=1;
        private bool rotacion;

        private Fixed[] tablaCirculares;
        private Fixed[] tablaHiperbolicas;
        private Fixed[] tablaLineales;

        public CordicSimulator()
        {
            InitializeComponent();

            X = new Fixed();
            Y = new Fixed();
            Z = new Fixed();

            ShiftX = new Fixed();
            ShiftY = new Fixed();
            Tabla = new Fixed();

            delta = 1;

            AddSubX = new Fixed();
            AddSubY = new Fixed();
            AddSubZ = new Fixed();

            AddX = true;
            AddY = true;
            AddZ = true;

            tablaCirculares = new Fixed[20];
            tablaHiperbolicas = new Fixed[20];
            tablaLineales = new Fixed[20];

            // Circulares
            tablaCirculares[0] = new Fixed(0.785398163397448f);
            tablaCirculares[1] = new Fixed(0.463647609000806f);
            tablaCirculares[2] = new Fixed(0.244978663126864f);
            tablaCirculares[3] = new Fixed(0.124354994546761f);
            tablaCirculares[4] = new Fixed(0.0624188099959574f);
            tablaCirculares[5] = new Fixed(0.0312398334302683f);
            tablaCirculares[6] = new Fixed(0.0156237286204768f);
            tablaCirculares[7] = new Fixed(0.00781234106010111f);
            tablaCirculares[8] = new Fixed(0.00390623013196697f);
            tablaCirculares[9] = new Fixed(0.00195312251647882f);
            tablaCirculares[10] = new Fixed(0.000976562189559319f);
            tablaCirculares[11] = new Fixed(0.000488281211194898f);
            tablaCirculares[12] = new Fixed(0.000244140620149362f);
            tablaCirculares[13] = new Fixed(0.00012207031189367f);
            tablaCirculares[14] = new Fixed(6.10351561742088e-005f);
            tablaCirculares[15] = new Fixed(3.05175781155261e-005f);
            tablaCirculares[16] = new Fixed(1.52587890613158e-005f);
            tablaCirculares[17] = new Fixed(7.62939453110197e-006f);
            tablaCirculares[18] = new Fixed(3.8146972656065e-006f);
            tablaCirculares[19] = new Fixed(1.90734863281019e-006f);

            // Hiperbolicas
            tablaHiperbolicas[0] = new Fixed(0.0f);
            tablaHiperbolicas[1] = new Fixed(0.549306144334055f);
            tablaHiperbolicas[2] = new Fixed(0.255412811882995f);
            tablaHiperbolicas[3] = new Fixed(0.125657214140453f);
            tablaHiperbolicas[4] = new Fixed(0.062581571477003f);
            tablaHiperbolicas[5] = new Fixed(0.031260178490667f);
            tablaHiperbolicas[6] = new Fixed(0.0156262717520523f);
            tablaHiperbolicas[7] = new Fixed(0.00781265895154041f);
            tablaHiperbolicas[8] = new Fixed(0.00390626986839681f);
            tablaHiperbolicas[9] = new Fixed(0.00195312748353261f);
            tablaHiperbolicas[10] = new Fixed(0.000976562810441035f);
            tablaHiperbolicas[11] = new Fixed(0.000488281288805085f);
            tablaHiperbolicas[12] = new Fixed(0.000244140629850638f);
            tablaHiperbolicas[13] = new Fixed(0.00012207031310633f);
            tablaHiperbolicas[14] = new Fixed(6.10351563257773e-005f);
            tablaHiperbolicas[15] = new Fixed(3.0517578134473e-005f);
            tablaHiperbolicas[16] = new Fixed(1.52587890636842e-005f);
            tablaHiperbolicas[17] = new Fixed(7.62939453139803e-006f);
            tablaHiperbolicas[18] = new Fixed(3.81469726569901e-006f);
            tablaHiperbolicas[19] = new Fixed(1.90734863280787e-006f);

            // Lineales
            tablaLineales[0] = new Fixed(1.0f);
            tablaLineales[1] = new Fixed(0.5f);
            tablaLineales[2] = new Fixed(0.25f);
            tablaLineales[3] = new Fixed(0.125f);
            tablaLineales[4] = new Fixed(0.0625f);
            tablaLineales[5] = new Fixed(0.03125f);
            tablaLineales[6] = new Fixed(0.015625f);
            tablaLineales[7] = new Fixed(0.0078125f);
            tablaLineales[8] = new Fixed(0.00390625f);
            tablaLineales[9] = new Fixed(0.001953125f);
            tablaLineales[10] = new Fixed(0.0009765625f);
            tablaLineales[11] = new Fixed(0.00048828125f);
            tablaLineales[12] = new Fixed(0.000244140625f);
            tablaLineales[13] = new Fixed(0.0001220703125f);
            tablaLineales[14] = new Fixed(6.103515625e-005f);
            tablaLineales[15] = new Fixed(3.0517578125e-005f);
            tablaLineales[16] = new Fixed(1.52587890625e-005f);
            tablaLineales[17] = new Fixed(7.62939453125e-006f);
            tablaLineales[18] = new Fixed(3.814697265625e-006f);
            tablaLineales[19] = new Fixed(1.9073486328125e-006f);



            switch (cbCoordenadas.SelectedIndex)
            {
                case 0:
                    m = 1;
                    break;
                case 1:
                    m = 0;
                    break;
                case 2:
                    m = -1;
                    break;
            }

            iteraciones = 10;

            cbCoordenadas.SelectedIndex = 0;
            cbModo.SelectedIndex = 0;

            rotacion = cbModo.SelectedIndex == 0;

            paso = 0;
            minipaso = 0;

            ActualizarTextBoxs();
        }

        private void bt_Inicio_Click(object sender, EventArgs e)
        {
            X = new Fixed();
            Y = new Fixed();
            Z = new Fixed();

            ShiftX = new Fixed();
            ShiftY = new Fixed();
            Tabla = new Fixed();

            delta = 1;

            AddSubX = new Fixed();
            AddSubY = new Fixed();
            AddSubZ = new Fixed();

            AddX = true;
            AddY = true;
            AddZ = true;

            switch (cbCoordenadas.SelectedIndex)
            {
                case 0:
                    m = 1;
                    break;
                case 1:
                    m = 0;
                    break;
                case 2:
                    m = -1;
                    break;
            }

            rotacion = cbModo.SelectedIndex == 0;


            minipaso = 0;
            if (m != -1)
            {
                paso = 0;
            }
            else
            {
                paso = 1;
            }

            ActualizarTextBoxs();

            btSemipaso.Enabled = true;
            btPaso.Enabled = true;
            btResultado.Enabled = true;

            EnableConfiguration();
        }

        private void ActualizarTextBoxs()
        {
            tbX.Text = X.getFloat().ToString() ;
            tbY.Text = Y.getFloat().ToString() ;
            tbZ.Text = Z.getFloat().ToString() ;

            tbShiftY.Text = ShiftY.getFloat().ToString();
            tbShiftX.Text = ShiftX.getFloat().ToString();
            tbTabla.Text = Tabla.getFloat().ToString();

            tbAddSubX.Text = AddSubX.getFloat().ToString();
            tbAddSubY.Text = AddSubY.getFloat().ToString();
            tbAddSubZ.Text = AddSubZ.getFloat().ToString();

            tbMux.Text = delta.ToString();

            if (m != 0)
            {
                lbAddX.Visible = AddX;
                lbSubX.Visible = !AddX;
            }
            else
            {
                lbAddX.Visible = false;
                lbSubX.Visible = false;
            }
            lbAddY.Visible = AddY;
            lbSubY.Visible = !AddY;
            lbAddZ.Visible = AddZ;
            lbSubZ.Visible = !AddZ;

            tbIteraciones.Text = iteraciones.ToString();

            lbJ1.Text = paso.ToString();
            lbJ2.Text = paso.ToString();
            lbJ3.Text = paso.ToString();
        }

        private void tbX_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                float v = float.Parse(tbX.Text);

                X = new Fixed(v);
                ActualizarTextBoxs();

                errorProvider.SetError(lbX, "");
            }
            catch
            {
                e.Cancel = true;
                errorProvider.SetError(lbX, "Valor no valido");
            }
        }

        private void tbY_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                float v = float.Parse(tbY.Text);

                Y = new Fixed(v);
                ActualizarTextBoxs();

                errorProvider.SetError(lbY, "");
                PreCalcularDelta();
            }
            catch
            {
                e.Cancel = true;
                errorProvider.SetError(lbY, "Valor no valido");
            }
        }

        private void tbZ_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                float v = float.Parse(tbZ.Text);

                Z = new Fixed(v);
                ActualizarTextBoxs();

                errorProvider.SetError(lbZ, "");
                PreCalcularDelta();
            }
            catch
            {
                e.Cancel = true;
                errorProvider.SetError(lbZ, "Valor no valido");
            }
        }

        private void RestaurarCable(Panel p)
        {
            if (p.Width <= 2)
            {
                p.Width = 1;
            }
            else if (p.Height <= 2)
            {
                p.Height = 1;
            }
        }

        private void RestaurarCables()
        {
            RestaurarCable(linea1);
            RestaurarCable(linea2);
            RestaurarCable(linea3);
            RestaurarCable(linea4);
            RestaurarCable(linea5);
            RestaurarCable(linea6);
            RestaurarCable(linea7);
            RestaurarCable(linea8);
            RestaurarCable(linea9);
            RestaurarCable(linea10);
            RestaurarCable(linea11);
            RestaurarCable(linea12);
            RestaurarCable(linea13);
            RestaurarCable(linea14);
            RestaurarCable(linea15);
            RestaurarCable(linea16);
            RestaurarCable(linea17);
            RestaurarCable(linea18);
            RestaurarCable(linea19);
            RestaurarCable(linea20);
            RestaurarCable(linea21);
            RestaurarCable(linea22);
            RestaurarCable(linea23);
            RestaurarCable(linea24);
            RestaurarCable(linea25);
            RestaurarCable(linea26);
            RestaurarCable(linea27);
            RestaurarCable(linea28);
            RestaurarCable(linea29);
        }

        private void EstimularCable(Panel p)
        {
            if (p.Width <= 2)
            {
                p.Width = 2;
            }
            else if (p.Height <= 2)
            {
                p.Height = 2;
            }
        }

        private void EstimularCables(int semipaso)
        {
            switch (semipaso)
            {
                case 0:
                    EstimularCable(linea4);
                    EstimularCable(linea5);
                    EstimularCable(linea6);
                    EstimularCable(linea8);
                    EstimularCable(linea9);
                    EstimularCable(linea10);
                    EstimularCable(linea25);
                    EstimularCable(linea16);
                    EstimularCable(linea17);
                    EstimularCable(linea18);
                    EstimularCable(linea27);
                    EstimularCable(linea28);
                    EstimularCable(linea29);
                    break;
                case 1:
                    EstimularCable(linea4);
                    EstimularCable(linea7);
                    EstimularCable(linea13);
                    EstimularCable(linea8);
                    EstimularCable(linea11);
                    EstimularCable(linea14);
                    EstimularCable(linea12);
                    EstimularCable(linea15);
                    break;
                case 2:
                    EstimularCable(linea19);
                    EstimularCable(linea20);
                    EstimularCable(linea26);
                    EstimularCable(linea21);
                    EstimularCable(linea22);
                    EstimularCable(linea23);
                    EstimularCable(linea24);
                    EstimularCable(linea1);
                    EstimularCable(linea2);
                    EstimularCable(linea3);
                    break;
            }
        }

        private void CalcularSemipaso()
        {
            EstimularCables(minipaso);

            switch (minipaso)
            {
                case 0:
                    ShiftY = Y.Shift(-paso);
                    ShiftX = X.Shift(-paso);

                    switch (m)
                    {
                        case 1:
                            Tabla = tablaCirculares[paso];
                            break;
                        case 0:
                            Tabla = tablaLineales[paso];
                            break;
                        case -1:
                            Tabla = tablaHiperbolicas[paso];
                            break;
                    }

                    AddX = (m * delta > 0) ? false : true;
                    AddY = (delta > 0) ? true : false;
                    AddZ = (delta > 0) ? false : true;
                    break;
                case 1:
                    if (AddX)
                    {
                        AddSubX = X.Suma(ShiftY);
                    }
                    else
                    {
                        AddSubX = X.Resta(ShiftY);
                    }

                    if (AddY)
                    {
                        AddSubY = Y.Suma(ShiftX);
                    }
                    else
                    {
                        AddSubY = Y.Resta(ShiftX);
                    }

                    if (AddZ)
                    {
                        AddSubZ = Z.Suma(Tabla);
                    }
                    else
                    {
                        AddSubZ = Z.Resta(Tabla);
                    }



                    break;
                case 2:
                    X = AddSubX;
                    Y = AddSubY;
                    Z = AddSubZ;

                    if (rotacion)
                    {
                        delta = (AddSubZ.getFloat() > 0.0f) ? 1 : -1;
                    }
                    else
                    {
                        delta = (AddSubY.getFloat() > 0.0f) ? -1 : 1;
                    }

                    break;
            }
            minipaso++;
            if (minipaso >= 3)
            {
                minipaso = 0;
                paso++;

                if (paso == iteraciones)
                {
                    btSemipaso.Enabled = false;
                    btPaso.Enabled = false;
                    btResultado.Enabled = false;
                }
            }
        }

        private void DisableConfiguracion()
        {
            gbConfiguracion.Enabled = false;
            tbX.ReadOnly = true;
            tbY.ReadOnly = true;
            tbZ.ReadOnly = true;
        }

        private void EnableConfiguration()
        {
            gbConfiguracion.Enabled = true;
            tbX.ReadOnly = false;
            tbY.ReadOnly = false;
            tbZ.ReadOnly = false;
        }

        private void btSemipaso_Click(object sender, EventArgs e)
        {
            DisableConfiguracion();

            RestaurarCables();
            CalcularSemipaso();

            ActualizarTextBoxs();
        }

        private void btPaso_Click(object sender, EventArgs e)
        {
            DisableConfiguracion();

            RestaurarCables();

            do
            {
                CalcularSemipaso();
            } while (minipaso != 0);

            ActualizarTextBoxs();
        }

        private void btResultado_Click(object sender, EventArgs e)
        {
            DisableConfiguracion();

            while (paso != iteraciones)
            {
                CalcularSemipaso();
            };

            ActualizarTextBoxs();
            RestaurarCables();
        }

        private void tbIteraciones_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int v = int.Parse(tbIteraciones.Text);

                if (v > 20)
                {
                    throw new Exception();
                }

                iteraciones = v;
                ActualizarTextBoxs();

                errorProvider.SetError(lbIteraciones, "");
            }
            catch
            {
                e.Cancel = true;
                errorProvider.SetError(lbIteraciones, "Valor no valido");
            }
        }

        private void cbCoordenadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbCoordenadas.SelectedIndex)
            {
                case 0:
                    m = 1;
                    paso = 0;
                    break;
                case 1:
                    m = 0;
                    paso = 0;
                    break;
                case 2:
                    m = -1;
                    paso = 1;
                    break;
            }

            ActualizarTextBoxs();
        }

        private void cbModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rotacion = cbModo.SelectedIndex == 0;
            PreCalcularDelta();
        }

        private void PreCalcularDelta()
        {
            if (rotacion)
            {
                delta = (Z.getFloat() > 0.0f) ? 1 : -1;
            }
            else
            {
                delta = (Y.getFloat() > 0.0f) ? -1 : 1;
            }
        }
    }
}