using System;
using System.Collections.Generic;
using System.Text;

namespace CordicSimulator
{
    class Fixed
    {
        private uint valor;

        public Fixed()
        {
            valor = 0;
        }

        public Fixed(float val)
        {
            valor = 0;

            float peso = 32768.0f;

            for (int i = 31; i >= 0; i--)
            {
                valor <<= 1;

                if (val >= peso)
                {
                    val -= peso;
                    valor |= 1;
                }
                else
                {
                    valor &= 0xFFFFFFFE;
                }
                peso /= 2.0f;
            }
        }

        public Fixed Suma(Fixed val)
        {
            Fixed nuevo = new Fixed();
            nuevo.valor = valor + val.valor ;
            return nuevo ;
        }

        public Fixed Resta(Fixed val)
        {
            Fixed nuevo = new Fixed();
            nuevo.valor = valor - val.valor;
            return nuevo;
        }

        public Fixed Shift(int desp)
        {
            Fixed nuevo = new Fixed();
            if (desp >= 0)
            {
                nuevo.valor = valor << desp;
            }
            else
            {
                nuevo.valor = valor >> (-desp);
                if ((valor & 1 << 31) != 0)
                {
                    for (int i = 0; i < -desp; i++)
                    {
                        nuevo.valor |= (uint)(1 << (31 - i));
                    }
                }
            }
            return nuevo;
        }

        public float getFloat()
        {
            float f = 0.0f;
            float peso = 32768.0f;
            bool negativo = false;
            uint temp = valor;

            // Comprobamos si es negativo
            if ( (temp & (1 << 31)) != 0)
            {
                temp = ~valor;
                temp += 1;
                negativo = true;
            }

            for (int i = 31; i >= 0; i--)
            {
                if ((temp & (1 << i)) != 0)
                {
                    f += peso;
                }
                peso /= 2.0f;
            }

            if (negativo)
            {
                f = -f;
            }
            return f;
        }

    }
}
