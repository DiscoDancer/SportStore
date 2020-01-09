﻿namespace Todolist.Core.Entities
{
    public class ToDoItem: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; private set; }
    }
}
