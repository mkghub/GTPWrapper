﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapper {
    public class GTPEngine {
        /// <summary>
        /// Fired when there is a new command in the queue.
        /// </summary>
        public event EventHandler<CommandEventArgs> GotInput;

        /// <summary>
        /// Gets the queue which contains all unfinished commands
        /// </summary>
        public Queue<Command> CommandQueue { get; private set; }
        public TextReader Input { get; private set; }
        public TextWriter Output { get; private set; }

        public GTPEngine(TextReader input, TextWriter output) {
            this.CommandQueue = new Queue<Command>();
        }

        /// <summary>
        /// Adds a command to the queue.
        /// </summary>
        /// <param name="input">Command string</param>
        public void Command(string input) {
            Command cmd = new Command(input);
            this.CommandQueue.Enqueue(cmd);

            if (GotInput != null) GotInput(this, new CommandEventArgs(cmd));
        }
    }
}
