using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Numerics;



namespace Zadanie_1IB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int p = 0;
        int q = 0;
        uint n;
        uint e;
        uint phi_n;
        uint d=0;
        byte[] array;
        uint[] array1;
        public String EnText;
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs el)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Name = openFileDialog1.FileName;
                textBox1.Clear();
                textBox1.Text = File.ReadAllText(Name);

            }
        }
        private void button4_Click(object sender, EventArgs el)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Name = saveFileDialog2.FileName;

                File.WriteAllText(Name, textBox2.Text);
            }
        }
        private void button5_Click(object sender, EventArgs el)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Name = openFileDialog2.FileName;
                textBox2.Clear();
                textBox2.Text = File.ReadAllText(Name);
            }
        }
        private void button6_Click(object sender, EventArgs el)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Name = saveFileDialog1.FileName;
                File.WriteAllText(Name, textBox1.Text);
            }

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs el)
        {
            button5.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs el)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;

        }
        //static public uint InPowMod(uint x, uint y, uint n)
        //{
        //    if (y == 0) return 1;//выход из рекурсии
        //    if (y % 2 == 1)//нечетное
        //        return (x * InPowMod(x, y - 1, n)) % n;
        //    uint temp = InPowMod(x, y / 2, n);//четное-сначала считаем y/2
        //    return (temp * temp) % n;//затем в квадрат по модулю n
        //}
        static public uint InPowMod(UInt64 number, UInt64 index, UInt64 mod)
        {
            /*
			uint r = 1;
			while(y>0)
            {
                if (y%2 == 1)
                {
					r = (r * x) % n;
                }
				x = (x * x) % n;
				y >>= 1;
            }
			return r;
			*/
            if (index == 0)
                return 1;
            UInt64 res = 1;
            while (index > 0)
            {
                if (index % 2 == 1) res = checked(res * number) % mod;

                index /= 2;
                number = checked(number * number) % mod;
            }

            return (uint)res;
        }
        private bool testFerma(uint x)
        {

            Random rand1 = new Random();

            for (int i = 0; i < 10; i++)
            {
                uint a = (uint)rand1.Next(2, (int)(x - 1));
                if (InPowMod(a, x - 1, x) != 1)
                {
                    return false;
                }
            }

            return true;
        }


        private void genP()
        {
            Random rnd = new Random();
            //p = rnd.Next((int)Math.Pow(2, 4), (int)Math.Pow(2, 8));
            p = rnd.Next((int)Math.Pow(2, 8), (int)Math.Pow(2, 16));
            if (p % 2 == 0) p--;


        }
        private void genQ()
        {
            Random rnd = new Random();
            //q = rnd.Next((int)Math.Pow(2, 4), (int)Math.Pow(2, 8));
            q = rnd.Next((int)Math.Pow(2, 8), (int)Math.Pow(2, 16));
            if (q % 2 == 0) q--;

        }

        private void genE()
        {
            Random rnd = new Random();
           e = (uint)(  rnd.Next( -(int)Math.Pow(2, 16), (int)Math.Pow(2, 16) )    + (int)Math.Pow(2, 16) );
            //e = (uint)(rnd.Next(0, (int)Math.Pow(2, 8)));

        }
        private uint gcd(uint a, uint b)//(рекурентный вызов)
        {
            while (a != 0 && b != 0)
            {
                if (a >= b) a = a % b;
                else b = b % a;
            }

            return a + b;
        }


        //private void ExtendendEvklid(uint e, uint fee, ref uint d)
        //{
        //    uint a = e;
        //    uint b = fee;
        //    int[] y = new int[2] { 1, 0 };
        //    int[] u = new int[2] { 0, 1 };

        //    int ybuf, ubuf;
        //    uint r;
        //    uint q;
        //    while (b != 0)
        //    {
        //        q = a / b;

        //        r = a % b;
        //        a = b;
        //        b = r;

        //        ybuf = y[1];
        //        ubuf = u[1];

        //        y[1] = y[0] - (int)(q * y[1]);
        //        u[1] = u[0] - (int)(q * u[1]);

        //        y[0] = ybuf;
        //        u[0] = ubuf;
        //    }
        //    if (a == 1) d = (uint)(y[0] % fee);
        //}
        static public uint ExtendendEvklid(uint a, uint b)
        {
            uint
                Y1 = 1,
                Y0 = 0,
                X1 = 0,
                X0 = 1,
                r, q;
            while (b != 0)
            {
                uint tmp;
                r = a % b;
                a = b;
                b = r;
                if (b != 0)
                {
                    q = a / b;
                }
                else
                {
                    return X1;
                }
                tmp = Y0;
                Y0 = Y1 - q * Y0;
                Y1 = tmp;
                tmp = X0;
                X0 = X1 - q * X0;
                X1 = tmp;
            }
            return X1;
        }
        private void button2_Click(object sender, EventArgs el)//генерация e и n
        {
            do
            { genP(); }
            while (testFerma((uint)p) == false);
            do
            { genQ(); }
            while (testFerma((uint)q) == false ||q==p);

            n = (uint)p * (uint)q;
            for_n.Text = n.ToString();
            phi_n = (uint)(p - 1) * (uint)(q - 1);

            do
            {
                genE();
               d=ExtendendEvklid(e, phi_n);//не правильно?
                
                //Gcd(e, phi_n, out w, out d);
            } while (e>= phi_n || gcd(e, phi_n) != 1|| d==0 );
            for_e.Text = e.ToString();
            for_d.Text = d.ToString();
        }

        //static public string RSAEncrypt(String ParentText, String eKey, String nKey)
        //{
        //    UInt64 KeyE = Convert.ToUInt64(eKey);
        //    UInt64 KeyN = Convert.ToUInt64(nKey);

        //    String EncryptedText = "";
        //    for (int i = 0; i < ParentText.Length; i++)
        //    {
        //        UInt64 m = Convert.ToUInt64(ParentText[i]);
        //        uint c = (InPowMod(m, KeyE, KeyN));
        //        EncryptedText += c.ToString() + " ";
        //    }
        //    return EncryptedText;
        //}

        //static public string RSADecrypt(String CipherText, String dKey, String nKey)
        //{
        //    UInt64 KeyD = Convert.ToUInt64(dKey);
        //    UInt64 KeyN = Convert.ToUInt64(nKey);

        //    String PlainText = "";
        //    String[] ArrayNumbers = CipherText.TrimEnd().Split(' ');

        //    for (int i = 0; i < ArrayNumbers.Length; i++)
        //    {
        //        UInt64 c = Convert.ToUInt64(ArrayNumbers[i]);
        //        uint m = (InPowMod(c, KeyD, KeyN));
        //        PlainText += Convert.ToChar(m);
        //    }
        //    return PlainText;
        //}
        public void button1_Click(object sender, EventArgs el)
        {
            if (radioButton2.Checked)//шифрование
            {
                textBox2.Text = ""; ;
                var str = textBox1.Text;
                array = Encoding.GetEncoding(1251).GetBytes(str);
                array1 = new uint[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array1[i] = InPowMod(array[i], e, n);//С=M^e(mod n)
                    textBox2.Text += Convert.ToString(array1[i]);
                    textBox2.Text += " ";
                }
            }
            else//дешифровка
            {
                textBox1.Clear();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (byte)InPowMod(array1[i], d, n);
                }

                var str = Encoding.GetEncoding(1251).GetString(array);
                textBox1.Text = str;
            }
        }
       


    }
}
