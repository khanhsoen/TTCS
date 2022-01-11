using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_demo
{
    class Node
    {
        #region Members
        int nName;
        public int nRank;
        public Node vRoot;
        public System.Drawing.Point pPosition;
        #endregion

        #region Properties
        public int Name
        {
            get
            {
                return nName;
            }
        }
        #endregion

        public Node(int nName, System.Drawing.Point pPosition)
        {
            this.nName = nName;
            nRank = 0;
            this.vRoot = this;
            this.pPosition = pPosition;
        }

        #region Methods
        internal Node GetRoot()
        {
            if (this.vRoot != this)// am I my own parent ? (am i the root ?)
            {
                this.vRoot = this.vRoot.GetRoot();// No? then get my parent
            }
            return this.vRoot;
        }

        internal static void Join(Node vRoot1, Node vRoot2)
        {

            if (vRoot2.nRank < vRoot1.nRank)//is the rank of Root2 less than that of Root1 ?
            {
                vRoot2.vRoot = vRoot1;//yes! then Root1 is the parent of Root2 (since it has the higher rank)
            }
            else //rank of Root2 is greater than or equal to that of Root1
            {
                vRoot1.vRoot = vRoot2;//make Root2 the parent
                if (vRoot1.nRank == vRoot2.nRank)//both ranks are equal ?
                {
                    vRoot1.nRank++;//increment one of them, we need to reach a single root for the whole tree
                }
            }
        }
        #endregion
    }
}
