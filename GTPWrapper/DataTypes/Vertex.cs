﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapper.DataTypes {
    /// <summary>
    /// Represents a Go board coordinate.
    /// </summary>
    public struct Vertex {
        /// <summary>
        /// The letters in a vertex string, i.e. the letters A to Z, excluding I.
        /// </summary>
        public static string Letters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Gets or sets the x coordinate of the point.
        /// </summary>
        public int X;
        /// <summary>
        /// Gets or sets the y coordinate of the point.
        /// </summary>
        public int Y;

        /// <summary>
        /// Initializes a new instance of the Vertex class with the given coordinates.
        /// </summary>
        /// <param name="x">The horizontal position of the point.</param>
        /// <param name="y">The vertical position of the point.</param>
        public Vertex(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the Vertex class with the given coordinate.
        /// </summary>
        /// <param name="vertex">The board coordinate consisting of one letter and one number.</param>
        public Vertex(string vertex) {
            this.Y = Vertex.Letters.IndexOf(vertex.ToUpper()[0]) + 1;
            if (this.Y == 0 || !int.TryParse(vertex.Substring(1), out this.X)) 
                throw new System.FormatException("This is not a valid vertex string.");
        }

        /// <summary>
        /// Returns the vertex string.
        /// </summary>
        public override string ToString() {
            return Vertex.Letters[this.Y - 1] + this.X.ToString();
        }
    }
}
