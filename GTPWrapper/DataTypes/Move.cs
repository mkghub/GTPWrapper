﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapper.DataTypes {
    /// <summary>
    /// Represents a move.
    /// </summary>
    public class Move : Board {
        /// <summary>
        /// The color of the move.
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Gets the generator vertex of the move.
        /// </summary>
        public Vertex Vertex { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Move class.
        /// </summary>
        /// <param name="board">The base board.</param>
        /// <param name="sign">The sign of the move.</param>
        /// <param name="vertex">The generator vertex of the move.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if the move is illegal.</exception>
        public Move(Board board, Sign sign, Vertex vertex) : base(board.Size) {
            this.Color = (Color)sign;
            this.Vertex = vertex;

            if (board.GetSign(vertex) != 0) throw new InvalidOperationException("Illegal move.");
            this.SetSign(vertex, sign);

            foreach (Vertex v in board.GetNeighborhood(vertex)) {
                if (board.GetSign(v) != -sign) continue;
                if (board.GetLiberties(v).Count != 0) continue;

                this.SetSign(v, -sign);
            }
        }
    }
}
