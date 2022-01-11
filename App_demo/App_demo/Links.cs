using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_demo
{
    class Link : IComparable
    {
        #region Members
        Node v1, v2;
        int nCost;
        System.Drawing.Point pStringPosition;
        #endregion

        #region Properties
        public Node V1
        {
            get
            {
                return v1;
            }
        }
        public Node V2
        {
            get
            {
                return v2;
            }
        }
        public int Cost
        {
            get
            {
                return nCost;
            }
        }
        public System.Drawing.Point StringPosition
        {
            get
            {
                return pStringPosition;
            }
        }
        #endregion

        public Link(Node v1, Node v2, int nCost, System.Drawing.Point pStringPosition)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.nCost = nCost;
            this.pStringPosition = pStringPosition;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            Link e = (Link)obj;
            return this.nCost.CompareTo(e.nCost);
        }

        #endregion

        internal static void QuickSort(List<Link> m_lstEdgesInitial, int nLeft, int nRight)
        {
            int i, j, x;
            i = nLeft; j = nRight;
            x = m_lstEdgesInitial[(nLeft + nRight) / 2].Cost;

            do
            {
                while ((m_lstEdgesInitial[i].Cost < x) && (i < nRight)) i++;
                while ((x < m_lstEdgesInitial[j].Cost) && (j > nLeft)) j--;

                if (i <= j)
                {
                    Link y = m_lstEdgesInitial[i];
                    m_lstEdgesInitial[i] = m_lstEdgesInitial[j];
                    m_lstEdgesInitial[j] = y;
                    i++; j--;
                }
            } while (i <= j);

            if (nLeft < j) QuickSort(m_lstEdgesInitial, nLeft, j);
            if (i < nRight) QuickSort(m_lstEdgesInitial, i, nRight);
        }
    }
}
