﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace GTPWrapper.Sgf {
    /// <summary>
    /// Represents a SGF game tree.
    /// </summary>
    public class SgfGameTree {
        /// <summary>
        /// The node list of the game tree.
        /// </summary>
        public List<SgfNode> Nodes { get; set; }
        /// <summary>
        /// The list of all subtrees.
        /// </summary>
        public List<SgfGameTree> GameTrees { get; set; }

        /// <summary>
        /// Initializes a new instance of the SgfGameTree class.
        /// </summary>
        public SgfGameTree() {
            this.Nodes = new List<SgfNode>();
            this.GameTrees = new List<SgfGameTree>();
        }
    }
}