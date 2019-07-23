using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Crypto1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {            
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = false;
            button1.Text = "Decrypt";
            if (comboBox1.SelectedIndex==1)
            {
                label4.Text = "Decrypted Image";
                label3.Text = "Encrypted Image";
            }
            else
            {
                label4.Text = "PlainText File";
                label3.Text = "CipherText File";
            }
            label5.Text = "";
            label6.Text = "";
            openFileDialog1.Dispose();
            saveFileDialog1.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox1.ReadOnly = false;
            richTextBox2.ReadOnly = true;
            button1.Text = "Encrypt";
            if (comboBox1.SelectedIndex == 1)
            {
                label3.Text = "Original Image";
                label4.Text = "Encrypted Image";
            }
            else
            {
                label3.Text = "PlainText File";
                label4.Text = "CipherText File";
            }
            label5.Text = "";
            label6.Text = "";
            openFileDialog1.Dispose();
            saveFileDialog1.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                richTextBox1.Clear();
                richTextBox2.Clear();
                richTextBox1.Visible = false;
                richTextBox2.Visible = false;
                comboBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label5.Text = "";
                label6.Text = "";
                openFileDialog1.Dispose();
                saveFileDialog1.Dispose();
            }
            else
            {
                richTextBox1.Visible = true;
                richTextBox2.Visible = true;
                comboBox1.SelectedIndex = 0;
                comboBox1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label5.Text = "";
                label6.Text = "";
                openFileDialog1.Dispose();
                saveFileDialog1.Dispose();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                openFileDialog1.Filter = "JPEG Files|*.jpeg;*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif";
                saveFileDialog1.Filter = "JPEG Files|*.jpeg;*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif";
                if (radioButton1.Checked == true)
                {
                    label3.Text = "Original Image";
                    label4.Text = "Encrypted Image";
                }
                else
                {
                    label4.Text = "Decrypted Image";
                    label3.Text = "Encrypted Image";
                }
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    label3.Text = "PlainText File";
                    label4.Text = "CipherText File";
                }
                else
                {
                    label4.Text = "PlainText File";
                    label3.Text = "CipherText File";
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    openFileDialog1.Filter = "PDF Files|*.pdf"; 
                    saveFileDialog1.Filter = "PDF Files|*.pdf"; 
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    openFileDialog1.Filter = "DOC Files|*.doc;|DOCX Files|*.docx";
                    saveFileDialog1.Filter = "DOC Files|*.doc;|DOCX Files|*.docx";
                }
                else
                {
                    openFileDialog1.Filter = "";
                    saveFileDialog1.Filter = "";
                }
            }

        }


        public char basic_enc(char c)
        {
            if (c <= '9' && c >= '0')
            {
                int x = c - '0', a;
                if (x < 5)
                    a = 2 * x + 1;
                else
                    a = 2 * x - 10;
                return (char)(a + '0');
            }
            else if (c >= 'A' && c <= 'M')
            { return (char)(c + 45); }
            else if (c >= 'N' && c <= 'Z')
            { return (char)(c + 19); }
            else if (c >= 'a' && c <= 'm')
            { return (char)(c - 19); }
            else if (c >= 'n' && c <= 'z')
            { return (char)(c - 45); }
            else
                return c;
        }

        public char basic_dec(char c)
        {
            if (c <= '9' && c >= '0')
            {
                int x = c - '0', a;
                if (x % 2 == 0)
                    a = (x + 10) / 2;
                else
                    a = (x - 1) / 2;
                return (char)(a + '0');
            }
            else if (c >= 'A' && c <= 'M')
            { return (char)(c + 45); }
            else if (c >= 'N' && c <= 'Z')
            { return (char)(c + 19); }
            else if (c >= 'a' && c <= 'm')
            { return (char)(c - 19); }
            else if (c >= 'n' && c <= 'z')
            { return (char)(c - 45); }
            else
                return c;
        }


        public string Reverse(string text)
        {
            if (text == null) return null;
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                richTextBox2.Text = "";
                if (checkBox1.Checked == true)
                {
                    if (label5.Text != "" && label6.Text != "")
                    {
                        String str = Microsoft.VisualBasic.Interaction.InputBox("Enter the secret key:", "Enter key (* 10 characters atleast)");
                        bool f = false;
                        if (str.Length < 10)
                        { f = true; }
                        if (f)
                        {
                            MessageBox.Show("Please enter a key of 10+ characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            if (comboBox1.SelectedIndex==0)
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 128; i++)
                                {
                                    B64[i] = (char)(128 + i);
                                    B46[(char)(128 + i)] = i;
                                }
                                Byte[] key = new Byte[str.Length];
                                for (int i = 0; i < str.Length; i++)
                                {
                                    key[i] = (byte)str[i];
                                  //  System.Console.Write((char)key[i]);
                                }
                                //System.Console.WriteLine();
                                int b;
                                FileStream fs = new FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                                FileStream fs1 = new FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
                                int k = 0;
                                while ((b = fs.ReadByte()) != -1)
                                {
                                    //                                System.Console.WriteLine((char)b);
                                    char s1 = (basic_enc((char)b));
                                    //                              System.Console.WriteLine(s1);
                                    char tmp;
                                    int x = (s1 + ((int)str[k] % 128)) % 128;
                                    tmp = (char)x;
                                    //                            System.Console.WriteLine(tmp);
                                    str = Reverse(str);
                                    x = (tmp + ((int)str[k] % 128)) % 128;
                                    k = (k + 1) % str.Length;
                                    tmp = B64[x];
                                    //                          System.Console.WriteLine(tmp);
                                    fs1.WriteByte((byte)tmp);
                                }
                                //Console.WriteLine(s1);
                                fs.Close();
                                fs = new FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);                                
                                HMACMD5 hmac = new HMACMD5(key);
                                fs1.WriteByte((byte)(' '));
                                byte[] hashValue = hmac.ComputeHash(fs);
                                for (int i = 0; i < hashValue.Length; i++)
                                {
                                    fs1.WriteByte(hashValue[i]);
                                //    System.Console.Write((char)hashValue[i]);
                                }
                                //System.Console.WriteLine();
                                fs.Close();
                                fs.Dispose();
                                fs1.Close();
                                fs1.Dispose();
                            }
                            else if (comboBox1.SelectedIndex == 1)
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 256; i++)
                                {
                                    B64[i] = (char)(255 - i);
                                    B46[(char)(255 - i)] = i;
                                }
                                Bitmap a, bm;
                                a = new Bitmap(openFileDialog1.FileName);
                                bm = new Bitmap(openFileDialog1.FileName);
                                int len = a.Width, hei = a.Height;
                                int k = 0;
                                for (int i = 0; i < len; i++)
                                {    
                                    for (int j = 0; j< hei; j++)
                                    {
                                        Color p = a.GetPixel(i, j),q; 
                                        byte r=p.R,g=p.G,b=p.B,v=(byte)p.ToArgb(),al=p.A;
                                        /*if (i == 0 && j == 0)
                                        {
                                            System.Console.WriteLine(r);
                                            System.Console.WriteLine(g);
                                            System.Console.WriteLine(b);
                                        }*/
                                        char tmp, s1;
                                        int x;
                                        s1 = (basic_enc((char)r));
                                        x = (s1 + ((int)str[k] % 256)) % 256;
                                        tmp = (char)x;
                                        str = Reverse(str);
                                        x = (tmp + ((int)str[k] % 256)) % 256;
                                        k = (k + 1) % str.Length;
                                        tmp = B64[x];
                                        r = (byte)tmp;
                                        s1 = (basic_enc((char)g));
                                        x = (s1 + ((int)str[k] % 256)) % 256;
                                        tmp = (char)x;
                                        str = Reverse(str);
                                        x = (tmp + ((int)str[k] % 256)) % 256;
                                        k = (k + 1) % str.Length;
                                        tmp = B64[x];
                                        g = (byte)tmp;
                                        s1 = (basic_enc((char)b));
                                        x = (s1 + ((int)str[k] % 256)) % 256;
                                        tmp = (char)x;
                                        str = Reverse(str);
                                        x = (tmp + ((int)str[k] % 256)) % 256;
                                        k = (k + 1) % str.Length;
                                        tmp = B64[x];
                                        b = (byte)tmp;
                                        /*s1 = (basic_enc((char)v));
                                        x = (s1 + ((int)str[k] % 256)) % 256;
                                        tmp = (char)x;
                                        str = Reverse(str);
                                        x = (tmp + ((int)str[k] % 256)) % 256;
                                        k = (k + 1) % str.Length;
                                        tmp = B64[x];
                                        v = (byte)tmp;
                                        s1 = (basic_enc((char)al));
                                        x = (s1 + ((int)str[k] % 256)) % 256;
                                        tmp = (char)x;
                                        str = Reverse(str);
                                        x = (tmp + ((int)str[k] % 256)) % 256;
                                        k = (k + 1) % str.Length;
                                        tmp = B64[x];
                                        al = (byte)tmp;*/
                                        /*r = (byte)~r;
                                        g = (byte)~g;
                                        b = (byte)~b;*/
                                        byte tmpr;
                                        tmpr = r;
                                        r = b;
                                        b = g;
                                        g = tmpr;
                                        r = (byte)(r ^ (i * j));
                                        g = (byte)(g ^ (i * j));
                                        b = (byte)(b ^ (i * j));
                                        /*r = (byte)(r ^ ((i ^ j) * 10));
                                        g = (byte)(g ^ ((i ^ j) * 10));
                                        b = (byte)(b ^ ((i ^ j) * 10));*/
                                        //q = Color.FromArgb(al, r, g, b);
                                        q = Color.FromArgb(r, g, b);
                                        /*if (i == 0 && j == 0)
                                        {
                                            System.Console.WriteLine(r);
                                            System.Console.WriteLine(g);
                                            System.Console.WriteLine(b);
                                        }*/
                                        //q = Color.FromArgb(v);
                                        bm.SetPixel(i, j, q);
                                        //                            st2 += B64[y];
                                    }
                                }
                                //bm.Save(saveFileDialog1.FileName);
                                bm.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            else
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 256; i++)
                                {
                                    B64[i] = (char)(255 - i);
                                    B46[(char)(255 - i)] = i;
                                }
                                int b;
                                FileStream fs = new FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                                FileStream fs1 = new FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
                                int k = 0;
                                while ((b = fs.ReadByte()) != -1)
                                {
                                    //                                System.Console.WriteLine((char)b);
                                    char s1 = (basic_enc((char)b));
                                    //                              System.Console.WriteLine(s1);
                                    char tmp;
                                    int x = (s1 + ((int)str[k] % 256)) % 256;
                                    tmp = (char)x;
                                    //                            System.Console.WriteLine(tmp);
                                    str = Reverse(str);
                                    x = (tmp + ((int)str[k] % 256)) % 256;
                                    k = (k + 1) % str.Length;
                                    tmp = B64[x];
                                    //                          System.Console.WriteLine(tmp);
                                    fs1.WriteByte((byte)tmp);
                                }
                                //Console.WriteLine(s1);
                                fs.Close();
                                fs.Dispose();
                                fs1.Close();
                                fs1.Dispose();
                            }
                            MessageBox.Show("Encryption complete!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            label5.Text = "";
                            label6.Text = "";
                            openFileDialog1.Dispose();
                            saveFileDialog1.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Atleast one file not selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    if (richTextBox1.Text == "")
                    {
                        MessageBox.Show("No text entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        String str = Microsoft.VisualBasic.Interaction.InputBox("Enter the secret key:", "Enter key (* 10 characters atleast)");
                        bool f = false;
                        if (str.Length < 10)
                        { f = true; }
                        if (f)
                        {
                            MessageBox.Show("Please enter a key of 10+ characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            Dictionary<int, char> B64 = new Dictionary<int, char>();
                            Dictionary<char, int> B46 = new Dictionary<char, int>();
                            for (int i = 0; i < 128; i++)
                            {
                                B64[i] = (char)(128 + i);
                                B46[(char)(128 + i)] = i;
                            }
                            int k = 0;
                            string str1 = "", s = richTextBox1.Text.Trim().ToString();
                            for (int i = 0; i < s.Length; i++)
                            {
                                byte b = (byte)s[i];
                                char s1 = (basic_enc((char)b));
  //                              System.Console.WriteLine(s1);
                                char tmp;
                                int x = (s1 + ((int)str[k] % 128)) % 128;
                                tmp= (char)x;
    //                            System.Console.WriteLine(tmp);
                                str = Reverse(str);
                                x = (tmp + ((int)str[k] % 128)) % 128;
                                k = (k+1) % str.Length;
                                tmp= B64[x];
                                str1 += tmp;
      //                            st2 += B64[y];
                            }
                            richTextBox2.Text = str1;
                        }
                    }
                }
            }
            else
            {
                richTextBox1.Text = "";
                if (checkBox1.Checked == true)
                {
                    if (label5.Text != "" && label6.Text != "")
                    {
                        String str = Microsoft.VisualBasic.Interaction.InputBox("Enter the secret key:", "Enter key (* 10 characters atleast)");
                        bool f = false;
                        if (str.Length < 10)
                        { f = true; }
                        if (f)
                        {
                            MessageBox.Show("Please enter a key of 10+ characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            if (comboBox1.SelectedIndex == 0)
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 128; i++)
                                {
                                    B64[i] = (char)(128 + i);
                                    B46[(char)(128 + i)] = i;
                                }
                                int b;
                                Byte[] key = new Byte[str.Length];
                                for (int i = 0; i < str.Length; i++)
                                {
                                    key[i] = (byte)str[i];
                                }
                                FileStream fs = new FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                                FileStream fs1 = new FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
                                int k = 0;
                                while ((b = fs.ReadByte()) != -1)
                                {
                                    if(b==' ')
                                        break;
                                    //                        System.Console.WriteLine((char)b);
                                    str = Reverse(str);
                                    //                      System.Console.WriteLine(str);
                                    int x = (B46[(char)b] - (str[k] % 128) + 128) % 128;
                                    //                    System.Console.WriteLine((char)x);
                                    str = Reverse(str);
                                    //                  System.Console.WriteLine(str);
                                    x = (x - (str[k] % 128) + 128) % 128;
                                    //                System.Console.WriteLine((char)x);
                                    k = (k + 1) % str.Length;
                                    fs1.WriteByte((byte)(basic_dec((char)x)));
                                }
                                String ha="";
                                while ((b = fs.ReadByte()) != -1)
                                {
                                //    System.Console.Write((char)b);
                                    ha+=(char)b;
                                }
                                fs.Close();
                                fs1.Close();
                                fs1.Dispose();
                                fs = new FileStream(saveFileDialog1.FileName, System.IO.FileMode.Open);
                                HMACMD5 hmac = new HMACMD5(key);
                                byte[] hashValue = hmac.ComputeHash(fs);
                                int flag = 0;
                                for (int i = 0; i < hashValue.Length; i++)
                                {
                                    if (hashValue[i] != ha[i])
                                    { 
                                        flag = 1; 
                                        //break; 
                                    }
                                }
                                if (flag == 1)
                                    MessageBox.Show("File integrity compromised!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                else
                                    MessageBox.Show("File integrity maintained", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fs.Close();
                                fs.Dispose();
                            }
                            else if (comboBox1.SelectedIndex == 1)
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 256; i++)
                                {
                                    B64[i] = (char)(255 - i);
                                    B46[(char)(255 - i)] = i;
                                }
                                Bitmap a, bm;
                                a = new Bitmap(openFileDialog1.FileName);
                                bm = new Bitmap(openFileDialog1.FileName);
                                int len = a.Width, hei = a.Height;
                                int k = 0;
                                for (int i = 0; i < len; i++)
                                {
                                    for (int j = 0; j < hei; j++)
                                    {
                                        Color p = a.GetPixel(i, j), q;
                                        byte r = p.R, g = p.G, b = p.B, v = (byte)p.ToArgb(), al = p.A;
                                        /*if (i == 0 && j == 0)
                                        {
                                            System.Console.WriteLine(r);
                                            System.Console.WriteLine(g);
                                            System.Console.WriteLine(b);
                                        }*/
                                        /*r = (byte)(r ^ ((i ^ j)*10));
                                        g = (byte)(g ^ ((i ^ j)*10));
                                        b = (byte)(b ^ ((i ^ j)*10));*/
                                        r = (byte)(r ^ (i * j));
                                        g = (byte)(g ^ (i * j));
                                        b = (byte)(b ^ (i * j));
                                        byte tmpr;
                                        tmpr = g;
                                        g = b;
                                        b = r;
                                        r = tmpr;
                                        /*r = (byte)~r;
                                        g = (byte)~g;
                                        b = (byte)~b;*/
                                        int x;
                                        str = Reverse(str);
                                        x = (B46[(char)r] - (str[k] % 256) + 256) % 256;
                                        str = Reverse(str);
                                        x = (x - (str[k] % 256) + 256) % 256;
                                        k = (k + 1) % str.Length;
                                        r = ((byte)(basic_dec((char)x)));
                                        str = Reverse(str);
                                        x = (B46[(char)g] - (str[k] % 256) + 256) % 256;
                                        str = Reverse(str);
                                        x = (x - (str[k] % 256) + 256) % 256;
                                        k = (k + 1) % str.Length;
                                        g = ((byte)(basic_dec((char)x)));
                                        str = Reverse(str);
                                        x = (B46[(char)b] - (str[k] % 256) + 256) % 256;
                                        str = Reverse(str);
                                        x = (x - (str[k] % 256) + 256) % 256;
                                        k = (k + 1) % str.Length;
                                        b = ((byte)(basic_dec((char)x)));
                                        /*str = Reverse(str);
                                        x = (B46[(char)al] - (str[k] % 256) + 256) % 256;
                                        str = Reverse(str);
                                        x = (x - (str[k] % 256) + 256) % 256;
                                        k = (k + 1) % str.Length;
                                        al = ((byte)(basic_dec((char)x)));
                                        str = Reverse(str);
                                        x = (B46[(char)v] - (str[k] % 256) + 256) % 256;
                                        str = Reverse(str);
                                        x = (x - (str[k] % 256) + 256) % 256;
                                        k = (k + 1) % str.Length;
                                        v = ((byte)(basic_dec((char)x)));*/
                                        q = Color.FromArgb(r, g, b);
                                        //q = Color.FromArgb(al, r, g, b);
                                        //q = Color.FromArgb(v);
                                        /*if (i == 0 && j == 0)
                                        {
                                            System.Console.WriteLine(r);
                                            System.Console.WriteLine(g);
                                            System.Console.WriteLine(b);
                                        }*/
                                        bm.SetPixel(i, j, q);
                                        //                            st2 += B64[y];
                                    }
                                }
                                bm.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                //bm.Save(saveFileDialog1.FileName);
                            }
                            else
                            {
                                Dictionary<int, char> B64 = new Dictionary<int, char>();
                                Dictionary<char, int> B46 = new Dictionary<char, int>();
                                for (int i = 0; i < 256; i++)
                                {
                                    B64[i] = (char)(255 - i);
                                    B46[(char)(255 - i)] = i;
                                }
                                int b;
                                FileStream fs = new FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                                FileStream fs1 = new FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
                                int k = 0;
                                while ((b = fs.ReadByte()) != -1)
                                {
                                    //                        System.Console.WriteLine((char)b);
                                    str = Reverse(str);
                                    //                      System.Console.WriteLine(str);
                                    int x = (B46[(char)b] - (str[k] % 256) + 256) % 256;
                                    //                    System.Console.WriteLine((char)x);
                                    str = Reverse(str);
                                    //                  System.Console.WriteLine(str);
                                    x = (x - (str[k] % 256) + 256) % 256;
                                    //                System.Console.WriteLine((char)x);
                                    k = (k + 1) % str.Length;
                                    fs1.WriteByte((byte)(basic_dec((char)x)));
                                }
                                fs.Close();
                                fs.Dispose();
                                fs1.Close();
                                fs1.Dispose();
                            }
                            MessageBox.Show("Decryption complete!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            label5.Text = "";
                            label6.Text = "";
                            openFileDialog1.Dispose();
                            saveFileDialog1.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Atleast one file not selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    if (richTextBox2.Text == "")
                    {
                        MessageBox.Show("No text entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        String str = Microsoft.VisualBasic.Interaction.InputBox("Enter the secret key:", "Enter key (* 10 characters atleast)");
                        bool f = false;
                        if (str.Length < 10)
                        { f = true; }
                        if (f)
                        {
                            MessageBox.Show("Please enter a key of 10+ characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            Dictionary<int, char> B64 = new Dictionary<int, char>();
                            Dictionary<char, int> B46 = new Dictionary<char, int>();
                            for (int i = 0; i < 128; i++)
                            {
                                B64[i] = (char)(128 + i);
                                B46[(char)(128 + i)] = i;
                            }
                            string str1 = "", s = richTextBox2.Text.Trim().ToString();
                            int k = 0;
                            for (int i = 0; i < s.Length; i++)
                            {
                                byte b = (byte)s[i];
                                //                                Console.Write(b);
                                str = Reverse(str);
                                //                      System.Console.WriteLine(str);
                                int x = (B46[(char)b] - (str[k] % 128) + 128) % 128;
                                //                    System.Console.WriteLine((char)x);
                                str = Reverse(str);
                                //                  System.Console.WriteLine(str);
                                x = (x - (str[k] % 128) + 128) % 128;
                                //                System.Console.WriteLine((char)x);
                                k = (k + 1) % str.Length;
                                str1+=(basic_dec((char)x));
                            }
                            richTextBox1.Text = str1;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
//            openFileDialog1.Filter = "Txt Files|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label6.Text = openFileDialog1.FileName.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
  //          saveFileDialog1.Filter = "Txt Files|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label5.Text = saveFileDialog1.FileName.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            openFileDialog1.Dispose();
            saveFileDialog1.Dispose();
        }


    }
}
