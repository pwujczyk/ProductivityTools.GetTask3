﻿using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.SingleCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.SelectCurrentRoot
{
    public class Parent : PSCmdlet.PSCommandPT<SelectCurrentRootCmdlet>
    {
        App.Task TaskStructure;
        public Parent(SelectCurrentRootCmdlet selectCurrentRootCmdlet) : base(selectCurrentRootCmdlet)
        {
            TaskStructure = TaskStructureFactory.Get(Cmdlet);
        }

        protected override bool Condition => Cmdlet.Parent.IsPresent;

        protected override void Invoke()
        {
            //get parent for id
            var currentNode = TaskStructure.SelectedNodeElementId;
            var parent = GetTaskHttpClient.Post2<int>("Task","GetParent", currentNode.ToString());

            
            TaskStructure.SelectNodeByElementId(parent.Result);
        }
    }
}