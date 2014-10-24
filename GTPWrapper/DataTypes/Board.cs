﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapper.DataTypes {
    /// <summary>
    /// Represents a Go board.
    /// </summary>
    public class Board {
        /// <summary>
        /// Contains pairs of vertex and sign with invertible sign.
        /// </summary>
        protected Dictionary<Vertex, Sign> Arrangement = new Dictionary<Vertex, Sign>();

        /// <summary>
        /// The letters in a vertex string, i.e. the letters A to Z, excluding I.
        /// </summary>
        public static string Letters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Gets or sets the size of the board. Usually 19.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Initializes a new instance of the Board class.
        /// </summary>
        /// <param name="size">The size of the board.</param>
        public Board(int size) {
            this.Size = size;
        }

        /// <summary>
        /// Determines whether the given vertex is on the board or not.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        public bool HasVertex(Vertex vertex) {
            return HasVertex(vertex.X, vertex.Y);
        }
        /// <summary>
        /// Determines whether the given vertex is on the board or not.
        /// </summary>
        /// <param name="x">The x coordinate of the vertex.</param>
        /// <param name="y">The y coordinate of the vertex.</param>
        public bool HasVertex(int x, int y) {
            return 1 <= Math.Min(x, y) && Math.Max(x, y) <= this.Size;
        }

        /// <summary>
        /// Gets the sign at the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        public Sign GetSign(Vertex vertex) {
            return !Arrangement.ContainsKey(vertex) ? 0 : Arrangement[vertex];
        }
        /// <summary>
        /// Gets the sign at the given vertex.
        /// </summary>
        /// <param name="x">The x coordinate of the vertex.</param>
        /// <param name="y">The y coordinate of the vertex.</param>
        public Sign GetSign(int x, int y) {
            return GetSign(new Vertex(x, y));
        }
        /// <summary>
        /// Sets the sign at the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <param name="sign">The sign.</param>
        public void SetSign(Vertex vertex, Sign sign) {
            Arrangement[vertex] = sign;
        }
        /// <summary>
        /// Sets the sign at the given vertex.
        /// </summary>
        /// <param name="x">The x coordinate of the vertex.</param>
        /// <param name="y">The y coordinate of the vertex.</param>
        /// <param name="sign">The sign.</param>
        public void SetSign(int x, int y, Sign sign) {
            SetSign(new Vertex(x, y), sign);
        }

        #region Operators
        
        public static Board operator +(Board b1, Board b2) {
            Board board = new Board(Math.Max(b1.Size, b2.Size));

            foreach (Vertex v in b1.Arrangement.Keys.Union(b2.Arrangement.Keys)) {
                board.SetSign(v, b1.GetSign(v) + b2.GetSign(v));
            }

            return board;
        }

        public static Board operator -(Board b) {
            Board board = new Board(b.Size);

            foreach (Vertex v in b.Arrangement.Keys) {
                board.SetSign(v, -b.GetSign(v));
            }

            return board;
        }

        public static Board operator -(Board b1, Board b2) {
            return -b2 + b1;
        }

        #endregion
    }
}
