﻿using GTPWrapper;
using GTPWrapper.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTPWrapperTest {
    public class Program {
        static void Main(string[] args) {
            Board b1 = new Board(1), b2 = new Board(1);
            Board b = -b2 + b1;

            Engine engine = new Engine();
            engine.NewCommand += engine_NewCommand;
            engine.ResponsePushed += engine_ResponsePushed;

            while (true) {
                string input = Console.ReadLine();
                engine.ParseString(input);
            }
        }

        static void engine_NewCommand(object sender, CommandEventArgs e) {
            Engine engine = (Engine)sender;
            engine.PushResponse(new Response(e.Command, e.Command.Name == "error", e.Command.Name));
        }

        static void engine_ResponsePushed(object sender, ResponseEventArgs e) {
            Console.Write(e.Response.ToString() + "\n\n");
        }
    }
}
