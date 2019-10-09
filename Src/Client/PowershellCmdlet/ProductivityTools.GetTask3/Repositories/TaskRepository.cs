﻿using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Commands;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskRepository : ITaskRepository
    {
        public ElementView GetStructure(int? currentNode)
        {
            VerboseHelper.WriteVerboseStatic("Calling GetStructure");
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<ElementView>("Task", Consts.TodayList, new ListRequest() { ParentId = currentNode }, VerboseHelper.WriteVerboseStatic).Result;
            return rootElement;
        }

        public async void Add(string name, int? parentId, ElementType type)
        {
            switch (type)
            {
                case ElementType.TaskBag:
                    await ProductivityTools.GetTask3.Client.Calls.Task.AddBag(name, parentId);
                    break;
                case ElementType.Task:
                    await ProductivityTools.GetTask3.Client.Calls.Task.Add(name, parentId);
                    break;
                default:
                    break;
            }

        }

        public async void Finish(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Finish(elementId);
        }

        public async void Undone(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Undone(elementId);
        }

        public async void Delay(int elementId, DateTime date)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Delay(elementId, date);
        }

        public async void AddToTomato(int[] elementIds)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.AddToTomato(elementIds);
        }
    }
}