using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compiler
{
    public partial class Form1 : Form
    {
        string[] keyword = { "char", "long", "double", "float", "int", "do", "for", "object", "void", "public", "class", "private", "string", "switch", "case", "if", "break", "new", "while", "null", " try", "catch" };
        char[] operotor = { '+', '-', ';', '}', '{', '(', ')', '*', '/', '%', '=' };
        string s;
        int ind = 0;
        string ps;
        int x = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(op.FileName);

                try
                {

                    while ((s = sr.ReadLine().ToString()) != null)
                    {
                        richTextBox1.Text += s + "\r";

                    }
                }
                catch { }
                sr.Close();
            }
            richTextBox1.SelectAll();
            this.richTextBox1.SelectionFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            richTextBox1.SelectionColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();


            x = richTextBox1.TextLength;//length code 

            Decimal z;
            int j = 0;


            string s = "";//

            for (int i = 0; i < richTextBox1.TextLength; i++)//loop until end code 
            {
                button2.Text = s.Length.ToString();

                if (i == 0) j = 0; else j = i - 1; //يجعل المتغير جي  نفس المتعير اي -وهحد 

                if ((richTextBox1.Text[i] == ' ' //space 
                    | richTextBox1.Text[i] == '\n'//\n 
                    | richTextBox1.Text[i] == '\t' //\t 
                    | operotor.Contains(richTextBox1.Text[i]) //contains operator
                    | operotor.Contains(richTextBox1.Text[j])) //in start same operatopr or after 
                    & s.Length > 0)// must 
                {//start execute condition
                    if (keyword.Contains(s))
                    {
                        listBox1.Items.Add(s + "\t it is keyword");

                    }
                    else if (operotor.Contains(s.ToCharArray().ElementAt(0)))
                    {
                        listBox1.Items.Add(s + "\t it is operator");
                    }
                    else if (Decimal.TryParse(s, out z))
                    {
                        listBox1.Items.Add(s + "\t it is number");
                    }
                    else if (s != " " & s != "\n" & s != "\t")
                    {
                        listBox1.Items.Add("id" + "\t it is identifier");
                    }

                    if ((richTextBox1.Text[i] == ' '
                        | richTextBox1.Text[i] == '\n'
                        | richTextBox1.Text[i] == '\t'))
                        s = "";
                    else
                    {
                        s = "";
                        s += richTextBox1.Text[i];
                    }

                    continue;
                }

                s += richTextBox1.Text[i];

            }

        }
        void getinp()
        {
            ++ind;
            ps = listBox1.Items[ind].ToString().Substring(0, listBox1.Items[ind].ToString().IndexOf('\t'));
        }
        void decl()
        {
            getinp();
            if (ps=="int"||ps=="float")
            {
                Type();
                Idintlist();
                if (ps==";".ToString())
                {
                    return;
                }
                else
                {
                    MessageBox.Show("no simi colon");
                }
            }
            else
            {
                MessageBox.Show("error");
            }
           
        }

      

        private void Type()
        {
            if (ps=="int")
            {
                getinp();
                
            }
            else if (ps=="float")
            {
                getinp();
            }else
            {
                MessageBox.Show("no identifier");
            }
        }
        private void Idintlist()
        {
            if (ps == "id")
            {
                getinp();

                if (ps == ",".ToString())
                {
                    getinp();
                    Idintlist();
                }
                else
                {
                    MessageBox.Show("there is no decleration");

                }
            }
            else
            {
                MessageBox.Show("error");
            }
        }
       
        
       void Stmt()
        {
            if (ps=="id"||ps=="(".ToString())
            {
                Expr(4);
                if (ps==";".ToString())
                {
                    return;
                }
            }
            else if (ps=="if")
            {
                IfStmt();
            }
            else if (ps=="{".ToString())
            {
                CompoundStmt();
            }
            else if (ps=="int"||ps=="float")
            {
                decl();
            }
            else if (ps==";".ToString())
            {
                return;
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void CompoundStmt()
        {
            if (ps=="{".ToString())
            {
                StmtList();
                if (ps=="}".ToString())
                {
                    return;
                }
            }
        }

        private void StmtList()
        {
            if (ps=="}".ToString())
            {
                StmtList1();
            }
        }

        private void StmtList1()
        {
            if (ps=="}".ToString())
            {
                return;
            }
            else if (ps=="id"||ps=="(".ToString()||ps=="{".ToString()||ps=="int"||ps=="float"||ps==";".ToString())
            {
                Stmt();
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void IfStmt()
        {
            if (ps=="if")
            {
                getinp();
                if (ps=="(".ToString())
                {
                    Expr(5);
                    if (ps==")".ToString())
                    {
                        Stmt();
                        ElsePart();
                    }
                }
            }
        }

        private void ElsePart()
        {
            if (ps=="else")
            {
                Stmt();
            }
        }

        private void Expr(int n)
        {
            if (ps=="id")
            {
                getinp(); if (ps=="=".ToString())
                {
                    MessageBox.Show("(MOV,Q, ,P)");
                    Expr(5);
                }
            }
            else if (ps=="(".ToString()||ps=="id")
            {
                Mag(5);
                Rlist(5, 6);
            }
        }

        private void Rlist(int v1, int v2)
        {
            int p=0;
            if (ps=="==".ToString()|| ps == "<".ToString()|| ps == ">".ToString()|| ps == "<=".ToString()|| ps == ">=".ToString()|| ps == "!=".ToString())
            {
                Compare1();
                Mag(4);
                Rlist(6, 3);
                int s = alloc();
                MessageBox.Show("(MOV,1, ,s)");

            }
            else if (ps==";".ToString()||ps==")".ToString())
            {
                int q = p;
            }
        }

        private int alloc()
        {
            throw new NotImplementedException();
        }

        private void Compare1()
        {
            if (ps == "==".ToString() || ps == "<".ToString() || ps == ">".ToString() || ps == "<=".ToString() || ps == ">=".ToString() || ps == "!=".ToString())
            {
                return;
            }
        }

        private void Mag(int v)
        {
            if (ps=="(".ToString()||ps=="id")
            {
                Term(1);
                Mlist(4, 6);
            }
        }

        private void Mlist(int v1, int v2)
        {
            if (ps=="+".ToString())
            {
                Term(2);
                Mlist(4, 6);
                int s = alloc();
                MessageBox.Show("(ADD,p,r,s)");
            }
            else if (ps == "-".ToString())
            {
                Term(2);
                Mlist(4, 6);
                int s = alloc();
                MessageBox.Show("(SUB,p,r,s)");
            }
            else if (ps == "==".ToString() || ps == "<".ToString() || ps == ">".ToString() || ps == "<=".ToString() || ps == ">=".ToString() || ps == "!=".ToString())
            {
                int p = 0;
                int q = p;
            }
        }

        private void Term(int v)
        {
            if (ps=="(".ToString()||ps=="id")
            {
                Factor(5);
            }
        }

        private void Factor(int v)
        {
            if (ps=="(".ToString())
            {
                Expr(2);
                if (ps==")".ToString())
                {
                    return;
                }
            }
            else if (ps=="id")
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ind = 0;
            decl();
        }
       void Func()
        {
            if (ps=="int"||ps=="float")
            {
                Type();
                if (ps=="id")
                {
                    getinp();
                }
                else
                {
                    MessageBox.Show("no func name");
                }
                if (ps=="(".ToString())
                {
                    getinp();
                    ARList();
                }
                else
                {
                    MessageBox.Show("no ( ");
                }
                if (ps==")".ToString())
                {
                    getinp();
                    Commonst();
                }
                else
                {
                    MessageBox.Show("no )   ");
                }
            }
            else
            {
                MessageBox.Show("no declaration");
            }
        }

        private void Commonst()
        {
            throw new NotImplementedException();
        }

        private void ARList()
        {
            if (ps=="int"||ps=="float")
            {
                ARG();
                ARlist1();
            }
        }

        private void ARlist1()
        {
            if (ps == ",".ToString())
            {
                getinp();
                ARG();
                
            }
            else if (ps == ")".ToString())
            {
                return;
            }
            else
            {
                MessageBox.Show("error");
            }

        }

        private void ARG()
        {
            if (ps == "int" || ps == "float")
            {
                Type();
                if (ps == "id")
                {
                    getinp();
                }
            }
            else
            {
                MessageBox.Show("error");
            }
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
        }

       
    


