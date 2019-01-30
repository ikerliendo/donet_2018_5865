﻿using System;
using System.Text.RegularExpressions;

//Al hacer el using se activan los métodos de extensión de ese espacio de nombres
using Utilidades;

namespace Tipos
{
    public class Dni
    {
        private const string LETRAS = "TRWAGMYFPDXBNJZSQVHLCKE";

        public Dni(string dni)
        {
            if (dni == null)
            {
                throw new Exception("No se aceptan DNIs incorrectos");
            }

            if (!Regex.IsMatch(dni, @"^[XYZ\d]\d{7}[" + LETRAS + "]$"))
            {
                throw new Exception("El formato de DNI no es correcto");
            }

            if (!EsValido(dni))
            {
                throw new Exception("El DNI no es válido");
            }

            Numero = int.Parse(ExtraerNumero(dni));
        }

        public int Numero { get; set; }

        public char Letra
        {
            get { return CalcularLetra(Numero); }
        }

        public static bool EsValido(string dni)
        {
            string sNumero = ExtraerNumero(dni);

            char letra = dni[8];

            sNumero = sNumero.Replace('X', '0').Replace('Y', '1').Replace('Z', '2');

            return letra == CalcularLetra(int.Parse(sNumero));
        }

        private static string ExtraerNumero(string dni)
        {
            //Usando método de extensión
            return dni.CutRight(1); //dni.Substring(0, dni.Length - 1);
            //return Utilidades.Extensiones.CutRight(dni, 1);
        }

        private static char CalcularLetra(int numero)
        {
            return LETRAS[numero % 23];
        }

        public override string ToString()
        {
            return string.Format("{0:00000000}{1}", Numero, Letra);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Dni))
            {
                return false;
            }

            Dni d = (Dni)obj;

            if (d == this)
            {
                return true;
            }

            if (d.Numero == this.Numero)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            return Numero;
        }

        public static bool operator==(Dni dni1, Dni dni2)
        {
            return dni1.Equals(dni2);
        }

        public static bool operator !=(Dni dni1, Dni dni2)
        {
            return !(dni1 == dni2);
        }
    }
}
