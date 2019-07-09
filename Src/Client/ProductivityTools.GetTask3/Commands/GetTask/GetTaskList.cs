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
        //ElementView RootElement;
        //ViewMetadata View
        //{
        //    get
        //    {
        //        return this.Cmdlet.SessionManager.ViewMetadata;
        //    }
        //}

        TaskStructure TaskStructure { get; set; }


        protected override bool Condition => true;

        public GetTaskList(GetTask3Cmdlet cmdlet) : base(cmdlet)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdlet);
        }


        private string FormatRow(PSElementView element)
        {
            var result = string.Empty;
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    return $"T{GetOrder(viewMetadata)}. {domain.Name} <{viewMetadata.ChildCount}>";
                case CoreObjects.ElementType.TaskBag:
                    return $"B{GetOrder(viewMetadata)}. [{domain.Name}] <{viewMetadata.ChildCount}t>";
            }
            return "empty";
        }

        protected override void Invoke()
        {
            TaskStructure ts = TaskStructureFactory.Get(this.Cmdlet);
            WriteOutput("GetTaskList");
            WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
            DisplayList(CoreObjects.ElementType.TaskBag);
            DisplayList(CoreObjects.ElementType.Task);
            //root.Elements

            // var z = GetTaskHttpClient.Get<ElementView>("Add", "{Name: \"XXX\",ParentId: 3 }");
        }

        private void DisplayBags()
        {
            WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
            foreach (var element in TaskStructure.Elements)
            {
                if (element.Element.Type == CoreObjects.ElementType.TaskBag)
                {
                    WriteOutput(FormatRow(element));
                }
            }
        }

        private void DisplayList(CoreObjects.ElementType type)
        {
            foreach (var element in TaskStructure.Elements.Where(x => x.Element.Type == type))
            {
                WriteOutput(FormatRow(element));
            }
        }

        //private void GetTasks()
        //{
        //    var currentNode = this.Cmdlet.SessionManager.ViewMetadata.SelectedNodeElementId;
        //    RootElement = GetTaskHttpClient.Get<ElementView>("List", currentNode.ToString());
        //    View.ItemOrder = new Dictionary<int, ElementViewMetadata>();

        //    int taskcounter = 0;
        //    //int bagcounter = 0;

        //    Action<ElementType> fillOrder = (type) =>
        //    {
        //        if (RootElement != null)
        //        {
        //            foreach (var element in RootElement.Elements.Where(x => x.Type == type))
        //            {

        //                View.ItemOrder.Add(element.ElementId, new ElementViewMetadata()
        //                {
        //                    ElementId = element.ElementId,
        //                    Order = taskcounter,
        //                    ChildCount = element.ChildElementsAmount()
        //                });
        //                taskcounter++;
        //            }
        //        }
        //    };
        //    View.SelectNodeByElementId(RootElement.ElementId);

        //    fillOrder(ElementType.TaskBag);
        //    fillOrder(ElementType.Task);
        //}

        private string GetOrder(SessionElementMetadata metadata)
        {

            return metadata.Order.ToString().PadLeft(3, '0');
        }
    }
}
