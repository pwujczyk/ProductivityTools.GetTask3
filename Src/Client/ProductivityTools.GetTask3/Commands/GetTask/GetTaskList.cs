﻿using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask
{
    public class GetTaskList : PSCmdlet.PSCommandPT<GetTask3Cmdlet>
    {
        App.Task TaskStructure { get; set; }

        protected override bool Condition => true;

        public GetTaskList(GetTask3Cmdlet cmdlet) : base(cmdlet)
        {
            
            this.TaskStructure = TaskStructureFactory.Get(cmdlet);
        }

        private string FormatRow(PSElementView element)
        {
            var result = string.Empty;
            string tomatoInfo = string.Empty;
            if (element.Element.Tomatoes != null && element.Element.Tomatoes.Count > 0)
            {
                var tomatoes = element.Element.Tomatoes;
                foreach (var tomato in tomatoes)
                {
                    tomatoInfo += $" [T{tomato.TomatoId}] {DateTime.Now.Subtract(tomato.Created)} - {tomato.Status}";
                }
            }


            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    result= $"T{GetOrder(viewMetadata)}. {tomatoInfo} {domain.Name} <{viewMetadata.ChildCount}>";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    result= $"B{GetOrder(viewMetadata)}. [{domain.Name}] <{viewMetadata.ChildCount}t>";
                    break;
            }

            //pw: rewrite it to make some chain

            return result;
        }

        private int GetTomatoTime(DateTime dt)
        {
            //pw:change it to provider
            var x = DateTime.Now - dt;
            return x.Minutes;
        }

        protected override void Invoke()
        {
            VerboseHelper.WriteVerboseStatic("GetTaskList Invoke");
            App.Task ts = TaskStructureFactory.Get(this.Cmdlet);
            WriteOutput("GetTaskList");
            if (TaskStructure.CurrentElement == null)
            {
                WriteOutput("No task found");
            }
            else
            {
                WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
                DisplayList(CoreObjects.ElementType.TaskBag);
                DisplayList(CoreObjects.ElementType.Task);
            }
            //root.Elements

            // var z = GetTaskHttpClient.Get<ElementView>("Add", "{Name: \"XXX\",ParentId: 3 }");
        }

        private void Setcolor(ElementView element)
        {
            if (element.Type != ElementType.TaskBag)
            {
                if (element.Delayed())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (element.Finished.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
            }
        }

        private void ResetColor()
        {
            Console.ResetColor();
        }

        private void WriteToScreen(PSElementView element)
        {
            Setcolor(element.Element);
            WriteOutput(FormatRow(element));
            ResetColor();
        }

        private void DisplayBags()
        {
            WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
            foreach (var element in TaskStructure.Elements)
            {
                if (element.Element.Type == CoreObjects.ElementType.TaskBag)
                {
                    WriteToScreen(element);
                }
            }
        }

        private void DisplayList(CoreObjects.ElementType type)
        {
            foreach (var element in TaskStructure.Elements.Where(x => x.Element.Type == type))
            {
                WriteToScreen(element);
            }
        }

        private string GetOrder(SessionElementMetadata metadata)
        {
            return metadata.Order.ToString().PadLeft(3, '0');
        }
    }
}
