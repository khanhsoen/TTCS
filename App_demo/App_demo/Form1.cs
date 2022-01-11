using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App_demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Reset();
        }
        #region Member Variables
        const int m_nRadius = 20;
        const int m_nHalfRadius = (m_nRadius / 2);

        Color m_colVertex = Color.Aqua;
        Color m_colEdge = Color.Red;

        List<Node> m_lstVertices;
        List<Link> m_lstEdgesInitial, m_lstEdgesFinal;

        Node m_vFirstVertex, m_vSecondVertex;

        bool m_bDrawEdge, m_bSolved;

        #endregion

        #region Events

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Point pClicked = new Point(e.X - m_nHalfRadius, e.Y - m_nHalfRadius);
            if (Control.ModifierKeys == Keys.Control)//if Ctrl is pressed
            {
                if (!m_bDrawEdge)
                {
                    m_vFirstVertex = GetSelectedVertex(pClicked);
                    m_bDrawEdge = true;
                }
                else
                {
                    m_vSecondVertex = GetSelectedVertex(pClicked);
                    m_bDrawEdge = false;
                    if (m_vFirstVertex != null && m_vSecondVertex != null && m_vFirstVertex.Name != m_vSecondVertex.Name)
                    {
                        Cost formCost = new Cost();
                        formCost.ShowDialog();

                        Point pStringPoint = GetStringPoint(m_vFirstVertex.pPosition, m_vSecondVertex.pPosition);
                        m_lstEdgesInitial.Add(new Link(m_vFirstVertex, m_vSecondVertex, formCost.m_nCost, pStringPoint));
                        panel1.Invalidate();
                    }
                }
            }
            else
            {
                m_lstVertices.Add(new Node(m_lstVertices.Count, pClicked));
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawVertices(g);
            DrawEdges(g);
            g.Dispose();
        }

        // Find Minimal spanning tree using Kruskal
        private void Solve_Click(object sender, EventArgs e)
        {
            if (m_lstVertices.Count > 2)
            {
                if (m_lstEdgesInitial.Count < m_lstVertices.Count - 1)
                {
                    MessageBox.Show("Missing Edges", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Solve.Enabled = false;
                    int nTotalCost = 0;
                    m_lstEdgesFinal = SolveGraph(ref nTotalCost);
                    m_bSolved = true;
                    panel1.Invalidate();
                    MessageBox.Show("Total Cost:" + nTotalCost.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Clear the drawing area
        private void Clear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clear form ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                Solve.Enabled = true;
                Graphics g = panel1.CreateGraphics();
                g.Clear(panel1.BackColor);
                Reset();
            }
        }

        #endregion

        #region Methods

        #region Drawing

        private void DrawEdges(Graphics g)
        {
            Pen P = new Pen(m_colEdge);
            List<Link> lstEdges = m_bSolved ? m_lstEdgesFinal : m_lstEdgesInitial;
            foreach (Link e in lstEdges)
            {
                Point pV1 = new Point(e.V1.pPosition.X + m_nHalfRadius, e.V1.pPosition.Y + m_nHalfRadius);
                Point pV2 = new Point(e.V2.pPosition.X + m_nHalfRadius, e.V2.pPosition.Y + m_nHalfRadius);
                g.DrawLine(P, pV1, pV2);
                DrawString(e.Cost.ToString(), e.StringPosition, g);
            }
        }

        private void DrawString(string strText, Point pDrawPoint, Graphics g)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(strText, drawFont, drawBrush, pDrawPoint);
        }

        private void DrawVertices(Graphics g)
        {
            Pen P = new Pen(m_colVertex);
            Brush B = new SolidBrush(m_colVertex);
            foreach (Node v in m_lstVertices)
            {
                g.DrawEllipse(P, v.pPosition.X, v.pPosition.Y, m_nRadius, m_nRadius);
                g.FillEllipse(B, v.pPosition.X, v.pPosition.Y, m_nRadius, m_nRadius);
                DrawString(v.Name.ToString(), v.pPosition, g);
            }
        }

        private Node GetSelectedVertex(Point pClicked)
        {
            Node vReturn = null;
            double dDistance;
            foreach (Node v in m_lstVertices)
            {
                dDistance = GetDistance(v.pPosition, pClicked);
                if (dDistance <= m_nRadius)
                {
                    vReturn = v;
                    break;
                }
            }
            return vReturn;
        }

        private double GetDistance(Point pStart, Point pFinish)
        {
            return Math.Sqrt(Math.Pow(pStart.X - pFinish.X, 2) + Math.Pow(pStart.Y - pFinish.Y, 2));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Point GetStringPoint(Point pStart, Point pFinish)
        {
            int X = (pStart.X + pFinish.X) / 2;
            int Y = (pStart.Y + pFinish.Y) / 2;
            return new Point(X, Y);
        }
        #endregion

        private void Reset()
        {
            m_lstVertices = new List<Node>();
            m_lstEdgesInitial = new List<Link>();
            m_bSolved = false;
            m_vFirstVertex = m_vSecondVertex = null;
        }

        private List<Link> SolveGraph(ref int nTotalCost)
        {
            Link.QuickSort(m_lstEdgesInitial, 0, m_lstEdgesInitial.Count - 1);
            List<Link> lstEdgesRetun = new List<Link>(m_lstEdgesInitial.Count);
            foreach (Link ed in m_lstEdgesInitial)
            {
                Node vRoot1, vRoot2;
                vRoot1 = ed.V1.GetRoot();
                vRoot2 = ed.V2.GetRoot();
                if (vRoot1.Name != vRoot2.Name)
                {
                    nTotalCost += ed.Cost;
                    Node.Join(vRoot1, vRoot2);
                    lstEdgesRetun.Add(new Link(ed.V1, ed.V2, ed.Cost, ed.StringPosition));
                }
            }
            return lstEdgesRetun;
        }

        #endregion
    }
}