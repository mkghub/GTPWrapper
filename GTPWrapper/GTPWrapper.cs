﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapper {
    public class GTPWrapper {
        /// <summary>
        /// Fired when there is a new command in the queue.
        /// </summary>
        public event EventHandler GotInput;
        /// <summary>
        /// Gets the queue which contains all unfinished commands
        /// </summary>
        public Queue<GTPCommand> CommandQueue { get; private set; }

        public GTPWrapper() {
            this.CommandQueue = new Queue<GTPCommand>();
        }

        /// <summary>
        /// Adds a command to the queue.
        /// </summary>
        /// <param name="input">Command string</param>
        public void Command(string input) {
            this.CommandQueue.Enqueue(new GTPCommand(input));
            if (GotInput != null) GotInput(this, new EventArgs());
        }
    }
}
